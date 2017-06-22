using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointMasterCS : MonoBehaviour {
	public LayerMask LayerToIgnore = -1; // Layer to ignore, when do raycasting
	public int layerMask;

	public bool drawPathLines = false;

	private List<Waypoint> 	waypointList = new List<Waypoint>(); // List of all waypoints
	private List<Waypoint> 	opendList 	= new List<Waypoint>();       
	private List<Waypoint> 	closedList 	= new List<Waypoint>();
	public 	List<Vector3>	pathList 	= new List<Vector3>(); 
	
	private int WaypointID = 0;   
	
	//class Waypoint
	public class Waypoint 
	{
	    public float af 	= 0.0f;      // the sum of g and h
	    public float ag 	= 0.0f;   // the actual shortest distance traveled from initial node to current node
	    public float ah 	= 0.0f;   // the estimated (or "heuristic") distance from current node to goal
	    public int ID 		= 0;
	
	    public Vector3 Pos; 
	    public Waypoint creator = null;    						// Parent of this waypoint
	   	public List<Waypoint> childList = new List<Waypoint>();    // List of child nodes
	
	}
	
	void Start()
	{
	    layerMask = ~LayerToIgnore.value;
	   
	    // Iterate through childs and create waypoint list
		// 월드상의 노드 객체 만큼 만큼 Waypoint 를 생성.
	    foreach(Transform child in transform)
	    {
	        var waypInst = new Waypoint();
	        waypInst.Pos = child.transform.position;
	        WaypointID++;
	        waypInst.ID = WaypointID;
	        waypointList.Add(waypInst);
	        //Destroy (child.transform.gameObject);
	    }
	}
	
	// Find waypoint nodes, that current node can see
	// 루트 노드(waypInst)에 인접한 자식 노드들을 추가.
	public void FindChild(Waypoint waypInst)
	{
	    //  Clear previous data first
	    waypInst.childList.Clear();
	    //iterate through each node of the list
	    Vector3 Dir;
	    foreach(Waypoint node in waypointList)
	    {
	        if(node.ID != waypInst.ID)
	        {
	            Dir = node.Pos - waypInst.Pos;
	            Dir.Normalize();
				// 루트 노드(waypInst) 에서 ray를 쏴서 장애물에 걸리지 않으면.
	            if((Physics.Raycast(waypInst.Pos, Dir, Vector3.Distance(waypInst.Pos, node.Pos), layerMask)) == false)
	            {
	                waypInst.childList.Add(node);
	            }
	        }
	    }
	}
	
	// Clear list of child nodes
	void FreeChild()
	{
	    foreach(Waypoint node in waypointList)
	    {
	        node.childList.Clear();
	    }
	}
	
	// Find path
	public int FindPath( Vector3 start, Vector3 dest )
	{
	    int resultPath 		= -1;
	    Waypoint tempNode	= null;
	   
	    // Clear previous data, before path finding 
	    opendList.Clear();
	    closedList.Clear();
	    pathList.Clear();
	    
		foreach(Waypoint node in waypointList)
	    {
	        node.creator = null;
	    }
	   	
	    // Add the starting location to the open list
	    var StartLoc = new Waypoint();
	    StartLoc.ah = Vector3.Distance(start, dest);
	    StartLoc.af = StartLoc.ah;
	    StartLoc.Pos = start;
	    opendList.Add(StartLoc);
	   
	    // Check to see if we can trace ray to the destination node without path finding
	    Vector3 Dir = dest - start;
	    Dir.Normalize();
	    if((Physics.Raycast(start, Dir, Vector3.Distance(start, dest), layerMask)) == false)
	    {
	        // Add start location
	        pathList.Add(start);
	        // Add destination location
	        pathList.Add(dest);
	       
	        // Path is found
	        resultPath = 1;
	    }
	   	
	    // Repeat until path is found, or it doesn't exist
	    if(resultPath == -1)
	    {
	        // If opend list isn't empty
	        while(opendList.Count != 0)
	        {
				// Find lowest f value in opend list
				float f = (opendList[0] as Waypoint).af;
				int index = 0;
				for(int i = 0; i < opendList.Count ; i++)
				{
					if((opendList[i] as Waypoint).af < f)
					{
						f = (opendList[i] as Waypoint).af;
						index = i;
					}
				}
				
				// if current node already has list of children, do nothing
				if((opendList[index] as Waypoint).childList.Count == 0)
				{
					FindChild(opendList[index]);
				}
	
	            // Add current node to the closed list
	            closedList.Add(opendList[index]);
	            // And remove it from the opend list
	            opendList.RemoveAt(index);
	           
	            // Check all child nodes of node we currently added to closed list
				Waypoint LastNode = closedList[closedList.Count-1];
				foreach(Waypoint child in LastNode.childList)
	            {
	                int skip = 0;
	               
	                // If current node in the closed list skip this loop cycle
	                foreach(Waypoint node in closedList)
	                {
	                    if(child.ID == node.ID)
	                    {
	                        skip = 1;
	                        break;
	                    }
					}
	                // Should we skip this loop cycle?
	                if(skip == 0)
	                {
	                    // If this node is already in the opend list, check to see if this path to that node is better
	                    // (if the G score for that node is lower if we use the current node to get there)
						foreach(Waypoint node in opendList)
						{
							if(child.ID == node.ID)
							{
								skip = 1;
								float dist = Vector3.Distance((closedList[closedList.Count-1] as Waypoint).Pos, child.Pos);
								if(((closedList[closedList.Count-1] as Waypoint).ag + dist) < child.ag)
								{
									// If new G score better, recalculate F and G scores for this node
									// and change the "creator" of this node
									child.creator = closedList[closedList.Count-1];
									child.ag = (closedList[closedList.Count-1] as Waypoint).ag + dist;
									child.ah = Vector3.Distance(child.Pos, dest);
									child.af = child.ag + child.ah;
								}
							}
						}
						
						// If current node isn't in the opend list, add it there
						if(skip == 0)
						{
							child.creator = closedList[closedList.Count-1];
							child.ag = (closedList[closedList.Count-1] as Waypoint).ag + Vector3.Distance((closedList[closedList.Count-1] as Waypoint).Pos, child.Pos);
							child.ah = Vector3.Distance(child.Pos, dest);
							child.af = child.ag + child.ah;
							opendList.Add(child);
						}
	                   
	                    // If current node can see target
	                    Dir = dest - child.Pos;
	                    Dir.Normalize();
	                    if((Physics.Raycast(child.Pos, Dir, Vector3.Distance(child.Pos, dest), layerMask)) == false)
	                    {
	                        // Remember this node
	                        if(tempNode == null)
	                        {
	                            tempNode = child;
	                        }
	                        else
	                        {
	                            // If another child node has lower f value 
	                            if(child.af < tempNode.af)
	                            {
	                                tempNode = child;
	                            }
	                        }
	                    }
	                }            
	            }
	        }
	       
	        // if there was any node, which was able to see destination position
	        if(tempNode != null)
	        {
	            //  Push destination coordinates to the path list         
	            pathList.Add(dest);
	               
	            // Start from the destination node, go from each node to its
	            // creator node until we reach the starting node
	            Waypoint next = tempNode;
	            while(next!= null)
	            {
					pathList.Insert(0,next.Pos);
	                //pathList.Unshift(next.Pos);
	                next = next.creator;                
	            }
	               
	            resultPath = 1;
	        }
	        else // Path doesn't exist
	        {
	            resultPath = 0;
	        }
	    }
	   
	    return resultPath;
	}
	
	
	void OnDrawGizmos()
	{
	    if((pathList.Count != 0) && drawPathLines == true)
	    {   
	        Gizmos.color = Color.green;
	        for (int i = 0 ; i < pathList.Count-1; i++)
	        {
	            Gizmos.DrawLine(pathList[i], pathList[i+1]);
	        }
	    }
	}
}

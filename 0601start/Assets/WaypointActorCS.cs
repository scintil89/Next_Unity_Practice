using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointActorCS : MonoBehaviour {
	
	public GameObject thePath;	// Waypoint master GameObject 
	public float moveSpeed;
	public float turnSpeed;
	public float gravity;
	public float MinDistance = 0.0f;
	public float RayRate;
	
	private WaypointMasterCS master; 		// waypoint master component
	private List<Vector3> PathToFollow = new List<Vector3>();
	private CharacterController contr;
	private bool corotine 	= true;
	private bool seeDest 	= true;
	
	private Vector3 moveDirection;
	private Vector3 goTo;	// Go to position
	private GameObject followObj = null;	// Game object to follow for
	private bool nearDest = false; // If actor near followObj
	private Vector3 lastPos;	// Check if actor isn't stuck
	
	void Start()
	{
		master = thePath.GetComponent<WaypointMasterCS>();
		contr = GetComponent<CharacterController>();
		
		if(moveSpeed == 0)
			moveSpeed = 1;
		if(turnSpeed == 0)
			turnSpeed = 1;
		if(gravity == 0)
			gravity = 9.8f;
		
		if(!master)
			Debug.Log("WaypointMaster is not defined");
		if(!contr)
			Debug.Log("CharacterController is not found");
	}

	void Update () 
	{
		if(master)
		{
			// Call functions only fixed times per second
			if(corotine)
			{
				StartCoroutine(LookForDest());
				StartCoroutine(FindPath());
				StartCoroutine(checkLastPos());
			}
			
			// Move actor along path
			if(PathToFollow.Count != 0 && !nearDest)
			{
				if(followObj)
					PathToFollow[PathToFollow.Count-1] = followObj.transform.position;
				else
					PathToFollow[PathToFollow.Count-1] = goTo;
					
				Vector3 pathPos = PathToFollow[0];			
				var dir = pathPos - transform.position;
				var rot = Quaternion.LookRotation(dir);
				transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime * turnSpeed); 
				transform.rotation = new Quaternion( 0, transform.rotation.y, 0, transform.rotation.w );
				//transform.rotation.z=0;
				dir.Normalize();
				
				if(contr.isGrounded)
					moveDirection = transform.forward * (moveSpeed / 100);
			
				if(Vector3.Distance(transform.position, PathToFollow[0]) < MinDistance)
					PathToFollow.RemoveAt(0);
			
			}
			
			// Apply gravity
			moveDirection.y -= gravity * Time.deltaTime;
			// Move the controller			
			contr.Move(moveDirection);
		}		
	}

	// Move to goTo position
	public void GoTo(Vector3 dest)
	{
		goTo = dest;
		
		// Try to find path
		PathToFollow.Clear();
		master.FindPath(transform.position, dest);
		if(master.pathList.Count != 0)
		{
			//print("Path is found");
			foreach(Vector3 node in master.pathList)
			{
				PathToFollow.Add(node);
			}
		}
	}

	private IEnumerator FindPath()
	{
		corotine = false;
		
		if(!seeDest)
		{
			if(followObj)
				goTo = followObj.transform.position;
			//print("FindPath called");
			PathToFollow.Clear();
			master.FindPath(transform.position, goTo);
			if(master.pathList.Count != 0)
			{
				foreach(Vector3 node in master.pathList)
				{
					PathToFollow.Add(node);
				}
			}
		}
		yield return new WaitForSeconds (RayRate);
		corotine = true;
	}

	// Check to see if actor can see destination
	private IEnumerator LookForDest()
	{
		corotine = false;
		
		if(followObj)
		{
			goTo = followObj.transform.position;
			
			Collider followCollider  = followObj.GetComponent<Collider>();
			nearDest = false;
			if(followCollider)
			{
				var boundBox = Vector3.Distance(followCollider.bounds.max, followCollider.bounds.center);
				if(Vector3.Distance(transform.position, goTo) <= (contr.radius+boundBox+ MinDistance*2))
					nearDest = true;
			}
		}
		
		if(PathToFollow.Count != 0 || (followObj && !nearDest))
		{
			var tempGameObj = new GameObject();
			seeDest = false;
				
			if(PathToFollow.Count > 1)
				tempGameObj.transform.position = PathToFollow[PathToFollow.Count-2];
			else
				tempGameObj.transform.position = transform.position;
			
			tempGameObj.transform.LookAt(goTo);
			var dist = Vector3.Distance(tempGameObj.transform.position, goTo);
			if((Physics.Raycast(tempGameObj.transform.TransformPoint(contr.radius, 0, 0), tempGameObj.transform.forward, dist, master.layerMask)) == false)
			{
				if((Physics.Raycast(tempGameObj.transform.TransformPoint(-contr.radius, 0, 0), tempGameObj.transform.forward, dist, master.layerMask)) == false)
					seeDest = true;
			}
			
			// Chect to see if actor can see shorter path to the next node of PathToFollow list
			if(PathToFollow.Count > 1)
			{
				tempGameObj.transform.position = transform.position;
				Vector3 pathPos = PathToFollow[1];
				tempGameObj.transform.LookAt(pathPos);
				dist = Vector3.Distance(transform.position, PathToFollow[1]);
				if((Physics.Raycast(tempGameObj.transform.TransformPoint(contr.radius, 0, 0), tempGameObj.transform.forward, dist, master.layerMask)) == false)
				{
					if((Physics.Raycast(tempGameObj.transform.TransformPoint(-contr.radius, 0, 0), tempGameObj.transform.forward, dist, master.layerMask)) == false)
					{
						PathToFollow.RemoveAt(0);
					}
				}
			}
			
			Destroy(tempGameObj);
		}
		
		yield return new WaitForSeconds(RayRate);
		corotine = true;
	}
	
	// Check if actor is not stuck somewhere
	private IEnumerator checkLastPos()
	{
		corotine = false;
		
		if((Vector3.Distance(lastPos, transform.position) < 0.6) && !nearDest && PathToFollow.Count != 0)
		{
			seeDest = false;
			FindPath();
			//print("checkLastPos() called");
		}
			
		lastPos = transform.position;
		
		yield return new WaitForSeconds (RayRate);
		corotine = true;
	}
}
	/*
	@script RequireComponent(CharacterController)
	}
	 
	 */
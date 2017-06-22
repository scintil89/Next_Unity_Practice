using UnityEngine;
using System.Collections;

public class mainCS : MonoBehaviour {

	public GameObject Player;
	public GameObject Follower;
	public GameObject waypMaster;
	
	private WaypointActorCS playerActor;
	private WaypointActorCS followerActor;
	private WaypointMasterCS master; 
	
	private float heightPoint;
	
	void Start()
	{
		if(!Player)
			Debug.Log("Player is not defined");
		if(!Follower)
			Debug.Log("Follower is not defined");
		if(!waypMaster)
			Debug.Log("WaypointMaster is not defined");
			
		if(Player && Follower && waypMaster)
		{
			playerActor 	= Player.GetComponent<WaypointActorCS>();
			followerActor 	= Follower.GetComponent<WaypointActorCS>();
			master 			= waypMaster.GetComponent<WaypointMasterCS>();
			
			CharacterController contr = Player.GetComponent<CharacterController>();
			heightPoint = contr.height / 2;
		}
	}
	
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			// Find clicked point in 3d space
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			RaycastHit hit; 
			if (Physics.Raycast (ray, out hit, 100)) 
			{
				var goTo = hit.point;
				goTo = goTo + hit.normal * heightPoint;
				playerActor.GoTo(goTo);
			} 
		}
	}
}

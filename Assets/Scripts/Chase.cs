using UnityEngine;
using System.Collections;

public class Chase : MonoBehaviour {
	
	public GameObject goal;

	bool IsNavMesh;
	
	// Use this for initialization
	void Start () {
		goal = GameObject.FindGameObjectWithTag ("Player");
		
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.transform.position;

		IsNavMesh = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(IsNavMesh)
		{
			NavMeshAgent agent = GetComponent<NavMeshAgent>();
			agent.destination = goal.transform.position;
		}

		if(Input.GetKeyDown ("7"))
		{
			IsNavMesh = false;
		}

		//Goal is the desired location from the navmesh
	}
}

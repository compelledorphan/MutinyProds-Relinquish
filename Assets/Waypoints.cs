using UnityEngine;
using System.Collections;

public class Waypoints : MonoBehaviour 
{
    GameObject[] WaypointArray;
    int NumberOfWaypoints = 0;

	// Use this for initialization
	void Start () 
    {
        //WaypointArray = GetComponentsInChildren<GameObject>();
        WaypointArray = GameObject.FindGameObjectsWithTag("Waypoint");

        foreach(GameObject Waypoint in WaypointArray)
        {
            NumberOfWaypoints++;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        getRandomPoint();
	}

    public Vector3 getRandomPoint()
    {
        //int iMax = WaypointArray.Length;

        Vector3 Pos;
        Pos.x = 0;
        Pos.y = 0;
        Pos.z = 0;

        int RandomWaypoint = Random.Range(0, NumberOfWaypoints);
        Pos = WaypointArray[RandomWaypoint].transform.position;

        return(Pos);
    }
}

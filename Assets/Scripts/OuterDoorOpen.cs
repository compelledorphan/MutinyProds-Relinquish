using UnityEngine;
using System.Collections;

public class OuterDoorOpen : MonoBehaviour 
{

    public GameObject[] Doors;  // Ones to add are (2, 4, 7, 11) as of 01/11/2015

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].GetComponent<NewDoorRules>().SetOpen(true);
            }
        }
        else
        {
            // Do nothing
        }
    }
}

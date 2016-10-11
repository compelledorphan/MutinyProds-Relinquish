using UnityEngine;
using System.Collections;

public class DoorHighlight : MonoBehaviour 
{
    public Material DoorOutlined;
    public Material DoorNormal;

    Renderer DoorRend;

	// Use this for initialization
	void Start () 
    {
        DoorRend = GetComponentInChildren<Renderer>() as Renderer;
        if(DoorRend == null)
        {
            Debug.Log("DoorRend = null");
        }
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("SwapDoor");
            DoorRend.material = DoorOutlined;
        }
        else
        {
            // Do nothing
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoorRend.material = DoorNormal;
        }
        else
        {
            // Do nothing
        }
    }
}

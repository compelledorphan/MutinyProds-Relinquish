using UnityEngine;
using System.Collections;

public class DoorRules : MonoBehaviour {
	public float smooth = 2.0f;
	public float DoorOpenAngle = 90.0f;
	float DoorCloseAngle = 0.0f;

	bool open;
	bool AxisInUse = false;
	bool InTriggerZone = false;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxisRaw("Use") == 1)
		{
			if(AxisInUse == false)
			{
				UseDoor();
				AxisInUse = true;
			}
		}
		
		if(Input.GetAxisRaw("Use") == 0)
		{
			AxisInUse = false;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			InTriggerZone = true;
			open = !open;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		InTriggerZone = false;
	}

	void UseDoor()
	{
		if(InTriggerZone == true)
		{
			if(open == true)
			{
				Quaternion target = Quaternion.Euler(0.0f, DoorOpenAngle, 0.0f);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
			}
			else if(open == false)
			{
				Quaternion target1 = Quaternion.Euler(0.0f, DoorCloseAngle, 0.0f);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * smooth);	
			}
		}
	}
}

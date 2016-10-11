using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boyancy : MonoBehaviour 
{
	SpawnerShell ParentScript;
	GameObject ParentObj;

	GameObject Player;
	
	Vector2 floatY;
	float originalY;
	
	float floatStrength = 0.3f;

	StaticSettings GameSettings;
	
	// Use this for initialization
	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player") as GameObject;
		
		this.originalY = this.transform.position.y;

		ParentObj = transform.parent.gameObject;
		ParentScript = ParentObj.GetComponent<SpawnerShell>();

		GameSettings = FindObjectOfType(typeof(StaticSettings)) as StaticSettings;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Quaternion Rot = Quaternion.Euler(-90.0f, Player.transform.localRotation.eulerAngles.y, 0.0f);

		transform.localRotation = Player.transform.localRotation;
		
		transform.position = new Vector3(transform.position.x,
		                                 originalY + ((float)Mathf.Sin(Time.time * 0.1f) * floatStrength),
		                                 transform.position.z);

		if(ParentScript.ContinueSpawning && ParentScript.IsVisible)
		{
			if(GameSettings.getToolTips())
			{
				this.GetComponent<MeshRenderer>().enabled = true;
			}
			else
			{
				this.GetComponent<MeshRenderer>().enabled = false;
			}
		}
		else
		{
			this.GetComponent<MeshRenderer>().enabled = false;
		}
	}
}


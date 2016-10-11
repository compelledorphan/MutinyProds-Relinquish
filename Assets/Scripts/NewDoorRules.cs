using UnityEngine;
using System.Collections;

public class NewDoorRules : MonoBehaviour {
	public float smooth = 2.0f;
	float DoorOpenAngle = 150.0f;
	//public float DoorClosedAngle = 150.0f;
	float DoorCloseAngle = 0.0f;
	public float DegToRotate = 150;
	
	bool AxisInUse = false;
	bool InTriggerZone = false;
	
	bool open;
	
	public SpawnerShell[] SpawnersToActivate;

	UIManager UIMan;
	TutorialManager TutMan;
	
	// Use this for initialization
	void Start () 
	{

		UIMan = FindObjectOfType(typeof(UIManager)) as UIManager;

		TutMan = FindObjectOfType(typeof(TutorialManager)) as TutorialManager;
		//	DoorCloseAngle = transform.rotation.y;
		
		DoorCloseAngle = transform.eulerAngles.y;
		
		DoorOpenAngle = DoorCloseAngle + DegToRotate;
		
		//DoorOpenAngle = DoorCloseAngle + 150.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(open == true)
		{
			Quaternion target = Quaternion.Euler(0.0f, DoorOpenAngle, 0.0f);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
		}
		
		if(open == false)
		{
			Quaternion target1 = Quaternion.Euler(0.0f, DoorCloseAngle, 0.0f);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * smooth);
			
		}
		
		if(InTriggerZone == true)
		{
			if(Input.GetAxisRaw("Use") == 1)
			{
				if(AxisInUse == false)
				{
					if(!TutMan.CheckLock())
					{
                        Open();
					}
				}
			}
			
			if(Input.GetAxisRaw("Use") == 0)
			{
				AxisInUse = false;
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && !open)
		{
			InTriggerZone = true;
			UIMan.DoorTipUIOn();
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			InTriggerZone = false;
			UIMan.CleanToolTips();
		}
	}

    void Open()
    {
        for (int i = 0; i < SpawnersToActivate.Length; i++)
        {
            GetComponent<AudioSource>().Play();
            open = true;
            AxisInUse = true;
            SpawnersToActivate[i].Settings.IsEnabled = true;
        }
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game>().GetActiveSpawners();
    }
 
    public void SetOpen(bool _Open)
    {
        // Please for the love of god, this really only makes sense if you send true. It'll close doors otherwise etc. 

        open = _Open;
    }
}

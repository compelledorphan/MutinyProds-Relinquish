using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    Vector3 Destination;
    Vector3 LastKnownDestination;
    public GameObject Player;

    public float fViewDistance = 1000.0f;

	public Vector3 EditorDest;
    
    bool IsUsingNavMesh;
    NavMeshAgent agent;

    int iFrame;

	public int BehaviourState;
	bool bFollowing = false;

	public int SoundDelay = 10;
	float SoundTimer;

	public AudioSource[] sounds;
	
	public float SlowSpeedMultiplyer = 0.4f;
	float MaxSpeed;
	float SlowSpeed;
    // 0 = chill
    // 1 = wander
    // 2 = Investigate
    // 3 = follow
    
    // Use this for initialization
	void Awake()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		agent = GetComponent<NavMeshAgent>();
		sounds = GetComponents<AudioSource>();
	}

    void Start () 
    {
		SoundTimer = SoundDelay;

        //Player = GameObject.FindGameObjectWithTag ("Player");
        
        //agent = GetComponent<NavMeshAgent>();

        Destination = Player.transform.position;

		//sounds = GetComponents<AudioSource>();

        //IsUsingNavMesh = true;

		BehaviourState = 2;

		MaxSpeed = agent.speed;
		SlowSpeed = MaxSpeed * SlowSpeedMultiplyer;
    }
    
    // Update is called once per frame
    void Update () 
    {
        iFrame++;
       // Destination = Player.transform.position;

		if(agent.isPathStale == true)
		{
			BehaviourState = 0;
		}
		else
		{

		}

		switch(BehaviourState)
        {
            case 0: //chill
                Chill();
                break;
            case 1: //wander
                Wander();
                break;
            case 2: //seek
                Investigate();
                break;
            case 3: //follow
                Follow();
                break;
        }

        if(Input.GetKeyDown ("7"))
        {
			BehaviourState = 0;
        }


        agent.destination = Destination;
		EditorDest = Destination;

		SoundTimer += Time.deltaTime;

		CatchBrokenAgents();
    }




    void Chill()
    {
		bFollowing = false;
		SoundTimer = 0.0f;
        if(iFrame % 5 == 1)
        {
            bool bVisible = LookAtPlayer();
            if(bVisible)
            {
                //follow the player!
				BehaviourState = 3;
				agent.speed = MaxSpeed;
            }
            else
            {
				agent.speed = 0.0f;
				//bSeen = false;
                //do nothing, stay in current state.
            }
        }

        Destination = transform.position;
    }

    void Wander()
    {
        LookAtPlayer();
    }

    void Investigate()
    {
		Destination = LastKnownDestination;

		bFollowing = false;
        if(iFrame % 5 == 1)
        {
            bool bVisible = LookAtPlayer();
            if(bVisible)
            {
                //follow the player!
				BehaviourState = 3;
            }
            else
            {
				//agent.speed = SlowSpeed;
				//bSeen = false;
                //do nothing, stay in current state.
            }
        }

		if(Vector3.Distance (Destination, transform.position) < 5)
		{
			if(Vector3.Distance (Destination, Player.transform.position) < 5)
			{
				//destination is the player, do nothing
			}
			else
			{
				//destination is nearby and not the player. Chill li'l bro.
				BehaviourState = 0;
			}
		}
    }

    void Follow()
    {
		Vector3 ChasePt = Player.transform.position;
		ChasePt.z += 1.0f;
		Destination = ChasePt;

		if(!bFollowing && SoundTimer > SoundDelay)
		{
			int iSound = Random.Range(0,3);
			sounds[iSound].Play();
			SoundTimer = 0.0f;
			agent.speed = MaxSpeed;
		}
		bFollowing = true;

        if(iFrame % 5 == 1)
        {
            bool bVisible = LookAtPlayer();
            if(bVisible)
            {
                //do nothing, stay in current state.
            }
            else
            {
                // cannot see the player, go into Investigate mode.
                LastKnownDestination = Destination;
				BehaviourState = 2;
            }
        }
    }

    bool LookAtPlayer()
    {
        bool bVisible = false;
        //if(iFrame % 5 == 1)
        {
            Vector3 Direction;
            Direction.x = Player.transform.position.x - transform.position.x;
			Direction.y = Player.transform.position.y - transform.position.y + 0.2f;
            Direction.z = Player.transform.position.z - transform.position.z;

			Vector2 Direction2D;
			Direction2D.x = transform.position.x;
			Direction2D.y = transform.position.z;

			Vector2 Rot2D;
			Rot2D.x = transform.eulerAngles.x;
			Rot2D.y = transform.eulerAngles.z;

			Vector3 Pos;
			Pos.x = transform.position.x;
			Pos.y = transform.position.y + 0.2f;
			Pos.z = transform.position.z;
            
            Direction = Direction.normalized;
			Ray myRay = new Ray(Pos,Direction);

            RaycastHit hit;
            if(Physics.Raycast (myRay, out hit, fViewDistance))
            {
                if(hit.collider.gameObject == Player)
                {
					//if(Vector2.Angle (Direction2D, Rot2D) < 80)
					{
                    	bVisible = true;
					}
                }
            }
        }

        return(bVisible);
    }

	void CatchBrokenAgents()
	{
		//if(BehaviourState == 2)
		//{
		//	Vector3 MiddleOfMap;
		//	MiddleOfMap.x = 0;
		//	MiddleOfMap.y = 0;
		//	MiddleOfMap.z = 0;
		//
		//	if(Vector3.Distance(MiddleOfMap, transform.position) < 6)
		//	{
		//		//enemy is chilling near the middle of the map, God knows why.
		//		//Lets set him back on track!
		//		Destination = Player.transform.position;
		//		BehaviourState = 3;
		//	}
		//}

		Vector3 MiddleOfMap;
    	MiddleOfMap.x = 0;
     	MiddleOfMap.y = 0;
     	MiddleOfMap.z = 0;

		if(Vector3.Distance(MiddleOfMap, Destination) == 0.0f)
		{
			Vector3 ChasePt = Player.transform.position;
			ChasePt.z += 1.0f;
			Destination = ChasePt;
			//Destination = Player.transform.position;
			BehaviourState = 3;
		}
	}
}

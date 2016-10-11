using UnityEngine;
using System.Collections;

public class SpawnerRules : MonoBehaviour {
	public GameObject Enemy;
	private Transform goTransform;
	GameObject EnemyManager;
	
	bool AxisInUse = false;
	bool InTriggerZone = false;

	public bool IsEnabled = false;
	
	[HideInInspector]
	public float SpawnDelayInSeconds = 0.5f;
	[HideInInspector]
	public bool ContinueSpawning = true;
	
	public bool CanBeDisabled = true;
	public float MinimumSpawnDelay = 1.0f;
	public float MaximumSpawnDelay = 4.0f;
	
	[HideInInspector]
	public float MinimumSpawnDelayModifier = 0.0f;
	[HideInInspector]
	public float MaximumSpawnDelayModifier = 0.0f;
	
	public float DisableTime;
	//bool UsePressed = false;
	
	public float MinimumSpeed = 1.0f;
	public float MaximumSpeed = 7.0f;
	
	public GameObject DisableParticleEffect;
	public GameObject SpawnParticleEffect;
	
	// Use this for initialization
	void Start () {
		this.goTransform = GetComponent<Transform>();
		EnemyManager = GameObject.FindGameObjectWithTag("EManager");
		
		Random.seed = System.Environment.TickCount;
		
		StartCoroutine("SpawnEnemy");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Use") == 1)
		{
			if(AxisInUse == false)
			{
				DisableSpawn();
				AxisInUse = true;
			}
		}
		
		if(Input.GetAxisRaw("Use") == 0)
		{
			AxisInUse = false;
		}
	}
	
	IEnumerator SpawnEnemy()
	{
		while(true)
		{
			if(IsEnabled == true)
			{
				if(ContinueSpawning == false)
				{
					SpawnDelayInSeconds = DisableTime;
				}
				else
				{
					//Debug.Log("SpawnedEnemy");
					GameObject tempGameObject;
					EnemyManager = GameObject.FindGameObjectWithTag("EManager");
					
					tempGameObject = (GameObject)Instantiate (Enemy, new Vector3 (goTransform.position.x, goTransform.position.y, goTransform.position.z), Quaternion.identity);
					tempGameObject.transform.parent = EnemyManager.transform;
					tempGameObject.GetComponent<NavMeshAgent>().speed = Random.Range(MinimumSpeed, MaximumSpeed);
					
				#region Spawn rate modifers
				
				float MinSpawnDelayModded = MinimumSpawnDelay + MinimumSpawnDelayModifier;
				float MaxSpawnDelayModded = MaximumSpawnDelay + MaximumSpawnDelayModifier;
				
				Mathf.Clamp(MinSpawnDelayModded, 0, MinimumSpawnDelay + MinimumSpawnDelayModifier);
				Mathf.Clamp(MaxSpawnDelayModded, 0, MaximumSpawnDelay + MaximumSpawnDelayModifier);
				
				if(MinSpawnDelayModded < 0)
				{
					MinSpawnDelayModded = 0;
				}
				
				if(MaxSpawnDelayModded < 0)
				{
					MaxSpawnDelayModded = 0;
				}
				#endregion
					
					SpawnDelayInSeconds = Random.Range(MinSpawnDelayModded, MaxSpawnDelayModded);
						
					tempGameObject = Instantiate (SpawnParticleEffect, this.transform.position, Quaternion.identity) as GameObject;
					tempGameObject.transform.parent = this.transform;
				}
			}
            
            ContinueSpawning = true;
            
            yield return new WaitForSeconds(SpawnDelayInSeconds);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InTriggerZone = true;
        }
        else
        {
            // Do nothing
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        InTriggerZone = false;
    }
    
    void DisableSpawn()
    {
        if(InTriggerZone == true && CanBeDisabled == true)
        {
            GameObject tempGameObject;
            
            Debug.Log("Disabled Spawn");
            ContinueSpawning = false;
            tempGameObject = Instantiate(DisableParticleEffect, this.transform.position, Quaternion.identity) as GameObject;
            
            tempGameObject.transform.parent = this.transform;
        }
        else
		{
			// Do nothing
		}
	}
}

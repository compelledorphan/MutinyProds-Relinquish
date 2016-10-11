using UnityEngine;
using System.Collections;
using StructsAndEnums;

public class SpawnerShell : MonoBehaviour {

	public bool IsVisible = false;
	bool AxisInUse = false;
	bool InTriggerZone = false;
	public SpawnerSettings Settings;
	[HideInInspector]
	public Vector3 Location;
	public bool ContinueSpawning = true;
	GameObject EnemyManager;
	public int SpawnerID;
	[HideInInspector]
	public bool GlobalCooldown;

	public Material AlternateMaterial;
	public GameObject AlternateDisableParticle;
	
	public Material OriginalMaterial;
	bool SwappedMats = false;
	bool HasStartedRoutine = false;
	
	Renderer rend;

	UIManager UIMan;
	TutorialManager TutMan;
	Material[] mats;

    Component Particle;

	public float ToDelay = 0.0f;

    public GameObject SpecialEnemyPrefab;
    GameObject EnemyPrefab;

    AudioSource DisableSoundSource;

    private IEnumerator coroutine;

	// Use this for initialization
	void Start () 
	{
        coroutine = SpawnEnemy();

		rend = GetComponent<Renderer>();
		mats = rend.materials;

		Settings.IsSpecialSpawner = false;
		EnemyManager = GameObject.FindGameObjectWithTag("EManager");

        EnemyPrefab = Settings.EnemyToSpawn.Enemy;

		TutMan = FindObjectOfType(typeof(TutorialManager)) as TutorialManager;

        DisableSoundSource = GetComponent<AudioSource>();

	//	OriginalMaterial = rend.materials[1];

		UIMan = FindObjectOfType(typeof(UIManager)) as UIManager;

        
		if(IsVisible == true)
		{
			StartCoroutine(coroutine);
			HasStartedRoutine = true;
		}
		else
		{
			// Do nothing
		}

		//childrend = GetComponentInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(IsVisible == false)
		{
			rend.enabled = false;
			//childrend.enabled = false;
		}
		else
		{
			rend.enabled = true;
			if(HasStartedRoutine == false)
			{
				StartCoroutine(coroutine);
				HasStartedRoutine = true;
			}
			else
			{
				// Do nothing
			}
			//childrend.enabled = true;
		}

        if(Settings.IsSpecialSpawner == true)
        {
            Settings.EnemyToSpawn.Enemy = SpecialEnemyPrefab;
        }
        else
        {
            Settings.EnemyToSpawn.Enemy = EnemyPrefab;
        }

		if(Settings.IsSpecialSpawner != true)
		{
			mats[1] = OriginalMaterial;
			rend.materials = mats;
			SwappedMats = false;
		}
		else
		{
			// Do nothing
		}

		if(Input.GetAxisRaw("Use") == 1)
		{
			if(AxisInUse == false)
			{
				if(InTriggerZone == true)
				{
					// Report triggered
					HasBeenTriggered();
					DisableSpawn();
					Settings.IsSpecialSpawner = false;
				}
				else
				{
					// Do nothing
				}

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

			if(IsVisible && ContinueSpawning)
			{
				if(Settings.IsSpecialSpawner)
				{
					UIMan.SpecialTipUIOn();
				}
				else
				{
					UIMan.SpawnerTipUIOn();
				}
			}
		}
		else
		{
			// Do nothing
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			InTriggerZone = false;
			UIMan.CleanToolTips();
		}
		InTriggerZone = false;
		//UIMan.SpawnerTipUIOff();
	}

	void HasBeenTriggered()
	{
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game>().SpawnerTriggered(SpawnerID, Settings.IsSpecialSpawner);
	}

	IEnumerator SpawnEnemy()
	{
		while(true)
		{ 
			if(Settings.IsEnabled == true)
			{
				if(GlobalCooldown == false)
				{
                    if (ContinueSpawning == false)
                    {
                        Settings.SpawnDelayInSeconds = Settings.DisableTime;
                    }
                    else
                    {
                        GameObject tempGameObject = null;

                        if (Settings.PreSpawnParticle != null)
                        {
                            GetComponent<PKFxFX>().StartEffect();

                            yield return new WaitForSeconds(3);

                            GetComponent<PKFxFX>().StopEffect();
                        }

                        if (ContinueSpawning == false)
                        {
                            Settings.SpawnDelayInSeconds = Settings.DisableTime;
                        }
                        else
                        {
                            //Debug.Log("SpawnedEnemy");
                            EnemyManager = GameObject.FindGameObjectWithTag("EManager");

                            if(EnemyManager != null)
                            {
                                tempGameObject = (GameObject)Instantiate(Settings.EnemyToSpawn.Enemy, this.transform.position, Quaternion.identity);
                                tempGameObject.transform.parent = EnemyManager.transform;
                                tempGameObject.GetComponent<NavMeshAgent>().speed = Random.Range(Settings.MinimumSpeed, Settings.MaximumSpeed);
                            }

                            Debug.Log("Spawned at:" + SpawnerID);

                            if (Settings.IsSpecialSpawner == true)
                            {
                                EnemyRunWalk tempRun = tempGameObject.GetComponent<EnemyRunWalk>();
                                if (tempRun != null)
                                {
                                    tempRun.SpecialTrailParticle.gameObject.SetActive(true);
                                    tempRun.SpecialTrailParticleActive = true;
                                }
                                else
                                {
                                    Debug.Log("Missing EnemyRunWalk on spawned enemies");
                                }
                            }


                            #region Spawn rate modifers

                            float MinSpawnDelayModded = Settings.SpawningRateMinimum + Settings.SpawnDelayModifierMin;
                            float MaxSpawnDelayModded = Settings.SpawningRateMaximum + Settings.SpawnDelayModifierMax;

                            Mathf.Clamp(MinSpawnDelayModded, 0, Settings.SpawningRateMinimum + Settings.SpawnDelayModifierMin);
                            Mathf.Clamp(MaxSpawnDelayModded, 0, Settings.SpawningRateMaximum + Settings.SpawnDelayModifierMax);

                            if (MinSpawnDelayModded < 0)
                            {
                                MinSpawnDelayModded = 0;
                            }

                            if (MaxSpawnDelayModded < 0)
                            {
                                MaxSpawnDelayModded = 0;
                            }
                            #endregion

                            Settings.SpawnDelayInSeconds = Random.Range(MinSpawnDelayModded, MaxSpawnDelayModded);

                            tempGameObject = Instantiate(Settings.SpawnParticle, this.transform.position, Quaternion.identity) as GameObject;
                            tempGameObject.transform.parent = this.transform;
                        }
                    }
				}
				else
				{
					Settings.SpawnDelayInSeconds = ToDelay;
					GlobalCooldown = false;
				}
			}
			
			ContinueSpawning = true;

			//StartCoroutine("PreSpawnParticleEffect");

			yield return new WaitForSeconds(Settings.SpawnDelayInSeconds);
		}
	}

	IEnumerator PreSpawnParticleEffect()
	{
		bool Wait = false;

		while(true)
		{
			if(Wait == true)
			{
				GameObject tempGameObject = Instantiate (Settings.PreSpawnParticle, this.transform.position, Quaternion.identity) as GameObject;
				tempGameObject.transform.parent = this.transform;
					
				yield break;
			}
			else
			{
				Wait = true;
			}

			yield return new WaitForSeconds(Settings.SpawnDelayInSeconds - Settings.PreSpawnWarningTime);
		}
	}

	void DisableSpawn()
	{
		if(IsVisible == true)
		{
			if(InTriggerZone == true && Settings.CanBeDisabled == true)
			{
				GameObject tempGameObject;
				
				Debug.Log("Disabled Spawn"); 
				ContinueSpawning = false;
                DisableSoundSource.Play();
				tempGameObject = Instantiate(Settings.DisableParticle, this.transform.position, Quaternion.identity) as GameObject;
				
				tempGameObject.transform.parent = this.transform;

				UIMan.SpawnerTipUIOff();
				TutMan.UnlockDoors();

				if(Settings.IsSpecialSpawner == true)
				{
					UIMan.JuicyUI();
				}
			}
			else
			{
				// Do nothing
			}
		}
		else
		{
			// Do nothing
		}
	}

	public void SwapMaterial()
	{
		if(SwappedMats == false)
		{
			Material[] mats = rend.materials;
			mats[1] = AlternateMaterial;
			rend.materials = mats;
			SwappedMats = true;
		}
		else
		{
			Material[] mats = rend.materials;
			mats[1] = OriginalMaterial;
			rend.materials = mats;
			SwappedMats = false;
		}
	}

    public void KillCoroutine()
    {
        StopCoroutine(coroutine);
    }
}

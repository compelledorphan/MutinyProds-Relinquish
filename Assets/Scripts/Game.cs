using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StructsAndEnums;

public class Game : MonoBehaviour {
	List<GameObject> ActiveSpawners = new List<GameObject>();
	GameObject[] Spawners;
	GameObject EnemyManager;
	ScoringRules ScoreManager;
	int CurrentSpecialSpawnerID = 7;
	int PrevSpecialIndex;
	int RandomIndex;

	public int InitialSpecialSpawnMinID = 0;
	public int InitialSpecialSpawnMaxID = 0;

	public float GlobalCooldownMinChangePerSec;
	public float GlobalCooldownMaxChangePerSec;

	public float SoftResetCooldownTime;

    bool IsDead = false;

	// Use this for initialization
	void Start () 
	{
		Random.seed = System.Environment.TickCount;
		Spawners = GameObject.FindGameObjectsWithTag("Spawner");
		ScoreManager = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;
        
		GetActiveSpawners();

		InitialSpecialSpawner();

		StartCoroutine("GlobalSpawningIncrementor");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void GetActiveSpawners()
	{
		ActiveSpawners.Clear();

		for(int i = 0; i < Spawners.Length - 1; i += 1)
		{
			if(Spawners[i].GetComponent<SpawnerShell>().IsVisible == true)
			{
				ActiveSpawners.Add(Spawners[i]);
			}
			else
			{
				// Do nothing
			}
		}
	}

	void ActivateNewSpawner()
	{
		List<GameObject> InactiveSpawners = new List<GameObject>();

		Debug.Log (ActiveSpawners.Count);
		Debug.Log(Spawners.Length);

		if(ActiveSpawners.Count < Spawners.Length)
		{
			for(int i = 0; i <= Spawners.Length - 1; i += 1)
			{
				if(Spawners[i].GetComponent<SpawnerShell>().IsVisible == false)
				{
					InactiveSpawners.Add(Spawners[i]);
				}
				else
				{
					// Do nothing
				}
			}

			RandomIndex = Random.Range (0, InactiveSpawners.Count-1);
			PrevSpecialIndex = RandomIndex;

			InactiveSpawners[RandomIndex].GetComponent<SpawnerShell>().IsVisible = true;
			InactiveSpawners[RandomIndex].SetActive(true);
		}
		else
		{
			// Do nothing, all active
		}

		GetActiveSpawners();
	}

	public void SpawnerTriggered(int _SpawnerID, bool _IsSpecial)
	{
		if(_SpawnerID == CurrentSpecialSpawnerID && _IsSpecial == true)
		{

			for(int i = 0; i < ActiveSpawners.Count - 1; i++)
			{
				SpawnerShell AllTemp = Spawners[i].GetComponent<SpawnerShell>() as SpawnerShell;
				if(AllTemp != null)
				{
					if(AllTemp.SpawnerID == CurrentSpecialSpawnerID)
					{
						AllTemp.Settings.IsSpecialSpawner = false; 
					}
				}
			}

			ScoreManager.IncreaseMultiplierLevel(1);

			while(PrevSpecialIndex == RandomIndex)
			{
				RandomIndex = Random.Range (0, ActiveSpawners.Count);
			}

			PrevSpecialIndex = RandomIndex;

			SpawnerShell temp = ActiveSpawners[RandomIndex].GetComponent<SpawnerShell>() as SpawnerShell;
			if(temp != null)
			{
				temp.SwapMaterial();
				CurrentSpecialSpawnerID = temp.SpawnerID;
				temp.Settings.IsSpecialSpawner = true;
			}
		}
		else
		{	// This ensures all spawners that aren't special stay that way
			for(int i = 0; i < ActiveSpawners.Count - 1; i++)
			{
				SpawnerShell temp = Spawners[i].GetComponent<SpawnerShell>() as SpawnerShell;
				if(temp != null)
				{
					if(temp.SpawnerID != CurrentSpecialSpawnerID)
					{
						temp.Settings.IsSpecialSpawner = false;
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
		}
	}

	IEnumerator GlobalSpawningIncrementor()
	{
		while(true)
		{
			for(int i = 0; i < Spawners.Length - 1; i++)
			{
				SpawnerShell temp = Spawners[i].GetComponent<SpawnerShell>() as SpawnerShell;
				if(temp != null)
				{
					if(temp.Settings.SpawningRateMinimum > 0.1f)
					{
						temp.Settings.SpawningRateMinimum += GlobalCooldownMinChangePerSec;
					}
					else
					{
						temp.Settings.SpawningRateMinimum = 0.1f;
					}

					if(temp.Settings.SpawningRateMaximum > 0.1f)
					{
						temp.Settings.SpawningRateMaximum += GlobalCooldownMaxChangePerSec;
					}
					else
					{
						temp.Settings.SpawningRateMaximum = 0.1f;
					}
				}
				else
				{
					// Do nothing
				}
			}

			yield return new WaitForSeconds(1.0f);
		}
	}

	public void SoftResetTriggered(SoftResetSettings _Settings)
	{
		if(_Settings.DoesMultiplierReset == true)
		{
			ScoreManager.ResetMultiplier(0);
		}
		else
		{
			// Do nothing
		}
		
		if(_Settings.DoesTriggerCooldown == true)
		{
			 SoftResetSpawnerDisable();
		}
		else
		{
			// Do nothing
		}
		
		if(_Settings.DoesChangeNumberOfSpawners == true)
		{
			ActivateNewSpawner();
		}
		else
		{
			// Do nothing
		}

		ResetSpawnrate();
	}

	void SoftResetSpawnerDisable()
	{
		for(int i = 0; i < ActiveSpawners.Count - 1; i++)
		{
			SpawnerShell temp = ActiveSpawners[i].GetComponent<SpawnerShell>();
			if(temp != null)
			{
				temp.ToDelay = SoftResetCooldownTime;
				temp.GlobalCooldown = true;
			}
		}

		//ActiveSpawners[0].GetComponent<SpawnerShell>().GlobalCooldown = true;
		//ActiveSpawners[0].GetComponent<SpawnerShell>().Settings.SpawnDelayInSeconds = SoftResetCooldownTime;

	}

	void InitialSpecialSpawner()
	{
		int RandomInitalID;
		RandomInitalID = Random.Range (InitialSpecialSpawnMinID, InitialSpecialSpawnMaxID);

		int IDWanted = 0;
		SpawnerShell temp;

		for(int i = 0; i < ActiveSpawners.Count; i++)
		{
			temp = ActiveSpawners[i].GetComponent<SpawnerShell>();
			if(temp != null)
			{
				if(temp.SpawnerID == InitialSpecialSpawnMinID)
				{
					IDWanted = i;
				}
				else
				{
					// Do nothing
				}
			}
		}


		temp = ActiveSpawners[IDWanted].GetComponent<SpawnerShell>();
		if(temp != null)
		{
			temp.SwapMaterial();
			CurrentSpecialSpawnerID = temp.SpawnerID;
			temp.Settings.IsSpecialSpawner = true;
		}
	}

	void ResetSpawnrate()
	{
		for(int i = 0; i < Spawners.Length - 1; i++)
		{
			SpawnerShell temp = Spawners[i].GetComponent<SpawnerShell>() as SpawnerShell;
			if(temp != null)
			{
				temp.Settings.SpawningRateMinimum = temp.Settings.BaseRateMin;
				temp.Settings.SpawningRateMaximum = temp.Settings.BaseRateMax;
			}
		}
	}

    public void SetDeath(bool _Death)
    {
        IsDead = _Death;
    }

    public bool GetDeath()
    {
        return(IsDead);
    }

}

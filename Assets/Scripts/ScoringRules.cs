using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StructsAndEnums;

public class ScoringRules : MonoBehaviour {
	private GameObject scorekeeper;
	GameObject Scorer;
	float fScore = 0.0f;
	string FinalString;

	Highscore Score;
	string PlayerName;

	UIManager UIMan;

	float BonusScore = 10;
	
	public GameObject EnemyManager;
	public GameObject ParticleEffectIncreaseMultiplier; // Maybe add a particle effect?
	
	public ScoreMultiplier[] Multipliers;
	
	//	List<float> LocalHighscores = new List<float>();
	
	int TotalKilledEnemies = 0;
	int CurrentRoundKilledEnemies = 0;
	int CurrentMultiplierLevel = 0;
	
	public ScoringSettings Settings;
	
	// Use this for initialization
	void Start () {
		UIMan = FindObjectOfType(typeof(UIManager)) as UIManager;
		UpdateScoreOnScreen ();

		for(int i = 0; i < Multipliers.Length; i++)
		{
			if(Multipliers[i].MultiplyBy == 0)
			{
				Multipliers[i].MultiplyBy = i + 1;
			}
			else
			{
				// Do nothing
			}
		}

        PlayerName = System.Environment.UserName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void UpdateScoreOnScreen()
	{ 
		//FinalString = fScore.ToString();
		//FinalString += "\nMultiplier: x" + Multipliers[CurrentMultiplierLevel].MultiplyBy.ToString();
		//FinalString += "\nEnemies Killed: " + CurrentRoundKilledEnemies.ToString();
		//Debug.Log(FinalString);
	}
	
	public void EnemyKilled(float _BaseScoreValue)      // adds what ever is passed as base to be multiplied
	{	
		if(Settings.PerKillScoring == true)
		{
			if(Settings.MultipliersEnabled == true)
			{
				fScore += MultiplyScore(_BaseScoreValue, CurrentRoundKilledEnemies);
			}
			else
			{
				fScore += _BaseScoreValue;
			}
			
			UpdateScoreOnScreen();
		}
		else
		{
			// Do nothing
		}
		
		CurrentRoundKilledEnemies++;
	}

    public void EnemyKilled()       // Auto uses 10.0f as base
    {
        if (Settings.PerKillScoring == true)
        {
            if (Settings.MultipliersEnabled == true)
            {
                fScore += MultiplyScore(10.0f, CurrentRoundKilledEnemies);
            }
            else
            {
                fScore += 10.0f;
            }

            UpdateScoreOnScreen();
        }
        else
        {
            // Do nothing
        }

        CurrentRoundKilledEnemies++;
    }
	
	public void AnnouceDeath()
	{
		FinalString = "You Died! \nScore: " + fScore.ToString();
		Debug.Log(FinalString);
		SaveOutScoreDetails();
		UIMan.Death ();
		//Reset ();
		//Instantiate(EnemyManager, Vector3.zero, Quaternion.identity);
	}
	
	public void AnnouceWin()
	{
		FinalString = "You Win! \nScore: " + fScore.ToString();
		Debug.Log(FinalString);
		SaveOutScoreDetails();
		UIMan.Survived ();
		//Reset ();
		//Instantiate(EnemyManager, Vector3.zero, Quaternion.identity);
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(0,0,100,100), FinalString);
	}
	
	public void Reset()
	{
		fScore = 0;
		TotalKilledEnemies = 0;
		CurrentRoundKilledEnemies = 0;
		CurrentMultiplierLevel = 0;
		//ChangeSpawnRate();
		Instantiate(EnemyManager, Vector3.zero, Quaternion.identity);
	}
	
	public void SoftReset()
	{
		FinalString = "Soft Reset!\n" + "Current Enemies Killed: " + CurrentRoundKilledEnemies.ToString();
		FinalString = "\nMultiplier Reset!";
		TotalKilledEnemies += CurrentRoundKilledEnemies;
		CurrentMultiplierLevel = 0;
		BonusScore = 10.0f;
		//ChangeSpawnRate();
	}
	
	float MultiplyScore(float _BaseScoreValue, int _iEnemiesKilled)
	{
		float fMultipliedScore = 0;

		Mathf.Clamp(CurrentMultiplierLevel, 0, Multipliers.Length - 1);

		if(Settings.MultipliersTiedToEnemies == true)
		{
			if(CurrentRoundKilledEnemies >= Multipliers[Multipliers.Length - 1].EnemiesKilled)
			{
				// Do nothing
			}
			else if(CurrentRoundKilledEnemies >= Multipliers[CurrentMultiplierLevel].EnemiesKilled)
			{
				IncreaseMultiplierLevel(1);
			}
		}
		else
		{
			// perform score multiply
			fMultipliedScore = _BaseScoreValue * Multipliers[CurrentMultiplierLevel].MultiplyBy;
		}
		
		return(fMultipliedScore);
	}
	
	void ChangeSpawnRate()
	{
		GameObject[] Spawners = GameObject.FindGameObjectsWithTag("Spawner");
		
		for(int i = 0; i < Spawners.Length; i++)
		{
			Spawners[i].GetComponent<SpawnerRules>().MinimumSpawnDelayModifier = Multipliers[CurrentMultiplierLevel].MinimumSpawnDelayModifier;
			Spawners[i].GetComponent<SpawnerRules>().MaximumSpawnDelayModifier = Multipliers[CurrentMultiplierLevel].MaximumSpawnDelayModifier;
		}
	}
	
	void ParticleEffect()
	{
		GameObject tempGameObject;
		
		tempGameObject = Instantiate(ParticleEffectIncreaseMultiplier, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity) as GameObject;
		
		tempGameObject.transform.parent = this.transform;
	}
	public void IncreaseMultiplierLevel(int _AmountToIncrease)
	{
		if(CurrentMultiplierLevel > Multipliers.Length)
		{
			//do nothing
		}
		else
		{
			CurrentMultiplierLevel += _AmountToIncrease;
			// Clamp multiplier so not to cause array overrun user array.length
			Mathf.Clamp(CurrentMultiplierLevel, 0, Multipliers.Length);
			// Celebrate going up a multuplier
			ParticleEffect();

			//fScore 
			fScore += BonusScore;

			BonusScore = BonusScore * 2.0f;

			//Juicy UI
			UIMan.JuicyUI();
			
			if(Settings.MultiplersAffectingSpawnRates == true)
			{
				ChangeSpawnRate();
			}
			else
			{
				// Do nothing
			}
		}
	}
	
	public void ResetMultiplier(int _LevelToResetTo)
	{
		CurrentMultiplierLevel = _LevelToResetTo;
		Mathf.Clamp(CurrentMultiplierLevel, 0, (Multipliers.Length - 1));
	}

	void SaveOutScoreDetails()
	{
		Score.Score = fScore;

		TotalKilledEnemies += CurrentRoundKilledEnemies;
		Score.EnemiesKilled = TotalKilledEnemies;

		Score.GameMode = "Standard";

		Score.GameTime = 0.0f;

		Score.Name = PlayerName;

		Score.Multiplier = CurrentMultiplierLevel;

		HighScoring temp = GetComponentInParent<HighScoring>() as HighScoring;
		if(temp != null)
		{
            Debug.Log(Score.Score);
			temp.SaveHighscores(Score);
		}
		else
		{
			// Do nothing

            Debug.Log("Failed getting HighScoring");
		}
	}

	public float getScore()
	{
		return(fScore);
	}

	public int getMultiplyer()
	{
		return(CurrentMultiplierLevel);
	}

	public float getScoreBonus()
	{
		return(BonusScore);
	}
}
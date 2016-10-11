using UnityEngine;
using System.Collections;

#pragma warning disable 0414

public class EndGameRules : MonoBehaviour {
	
	ScoringRules Scorer;
	bool AxisInUse = false;
	bool InTriggerZone = false;
	
	bool HighscoreOnDeath;
	bool HighscoreOnWin;
	bool HighscoreOnTime;
	
	bool IsGameMaxTime = false;
	float MaxTime = 0.0f;
	
	// Use this for initialization
	void Start () {
		Scorer = FindObjectOfType (typeof(ScoringRules)) as ScoringRules;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetAxisRaw("Use") == 1)
		{
			if(AxisInUse == false)
			{
				EndGame();
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
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		InTriggerZone = false;
	}
	
	void EndGame()
	{
		//if(HighscoreOnWin == true)
		//{
			if(InTriggerZone == true)
			{
				GameObject CurrentEManager;
				CurrentEManager = GameObject.FindGameObjectWithTag("EManager");
				Destroy(CurrentEManager);
				Scorer.AnnouceWin();
			}
			else
			{
				// Do nothing
			}
		//}
		//else
		//{
		//	// TODO
		//}
	}
	
	public void SetupEndGameRules(bool _HighscoreDeath, bool _HighscoreWin, bool _HighscoreTime)
	{
		HighscoreOnDeath = _HighscoreDeath;
		HighscoreOnWin = _HighscoreWin;
		HighscoreOnTime = _HighscoreTime;
	}
	
	public void SetupEndGameRules(bool _HighscoreDeath, bool _HighscoreWin, bool _HighscoreTime, bool _TimeLimited, float _MaxTime)
	{
		HighscoreOnDeath = _HighscoreDeath;
		HighscoreOnWin = _HighscoreWin;
		HighscoreOnTime = _HighscoreTime;
		IsGameMaxTime = _TimeLimited;
		MaxTime = _MaxTime;
	}
}

#pragma warning restore 0414

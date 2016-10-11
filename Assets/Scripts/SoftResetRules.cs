using UnityEngine;
using System.Collections;
using StructsAndEnums;

#pragma warning disable 0414

public class SoftResetRules : MonoBehaviour {

	UIManager UIMan;

	ScoringRules Scorer;
	bool AxisInUse = false;
	bool InTriggerZone = false;
	
	public SoftResetSettings Settings;
	
	public GameObject TriggerParticles;
	
	// Use this for initialization
	void Start () {
		Scorer = FindObjectOfType (typeof(ScoringRules)) as ScoringRules;
		UIMan = FindObjectOfType(typeof(UIManager)) as UIManager;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetAxisRaw("Use") == 1)
		{
			if(AxisInUse == false)
			{
				SoftReset();
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
			UIMan.SoftResetTipUIOn();
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		InTriggerZone = false;
		UIMan.CleanToolTips();
	}
	
	void SoftReset()
	{
		if(InTriggerZone == true)
		{
			GameObject tempGameObject = GameObject.FindGameObjectWithTag("GameManager");
			tempGameObject.GetComponent<Game>().SoftResetTriggered(Settings);
			
			SpawnParticle(TriggerParticles);
		}
	}
	
	//	void ActivateCooldown()
	//	{
	//		GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnerManager>().GlobalSpwanerCooldown(CooldownRate);
	//	}
	
	void SpawnParticle(GameObject _Particle)
	{
		GameObject tempGameObject;
		tempGameObject = Instantiate(_Particle, this.transform.position, Quaternion.identity) as GameObject;
		tempGameObject.transform.parent = this.transform;
	}
	//
	//	public void SetupSoftResetRules(bool _MultiplerReset, bool _TriggerCooldown, float _CooldownRate, bool _NumSpawnersChange, int _NumOfSpawnersToChange)
	//	{
	//		DoesMultiplierReset = _MultiplerReset;
	//		DoesTriggerCooldown = _TriggerCooldown;
	//		CooldownRate = _CooldownRate;
	//		DoesChangeNumberOfSpawners = _NumSpawnersChange;
	//		NumberOfSpawnersToChange = _NumOfSpawnersToChange;
	//	}
}

#pragma warning restore 0414

//using UnityEngine;
//using System.Collections;
//
//public class SoftResetRules : MonoBehaviour {
//
//	ScoringRules scorer;
//	bool AxisInUse = false;
//	bool InTriggerZone = false;
//
//	public GameObject TriggerParticles;
//
//	// Use this for initialization
//	void Start () {
//		scorer = FindObjectOfType (typeof(ScoringRules)) as ScoringRules;
//	}
//	
//	// Update is called once per frame
//	void FixedUpdate () {
//		if(Input.GetAxisRaw("Use") == 1)
//		{
//			if(AxisInUse == false)
//			{
//				SoftReset();
//				AxisInUse = true;
//			}
//		}
//		
//		if(Input.GetAxisRaw("Use") == 0)
//		{
//			AxisInUse = false;
//		}
//	}
//
//	void OnTriggerEnter(Collider other)
//	{
//		if(other.gameObject.tag == "Player")
//		{
//			InTriggerZone = true;
//		}
//	}
//	
//	void OnTriggerExit(Collider other)
//	{
//		InTriggerZone = false;
//	}
//
//	void SoftReset()
//	{
//		if(InTriggerZone == true)
//		{
//			scorer.SoftReset();
//
//			GameObject tempGameObject;
//			tempGameObject = Instantiate(TriggerParticles, this.transform.position, Quaternion.identity) as GameObject;
//			tempGameObject.transform.parent = this.transform;
//		}
//	}
//}

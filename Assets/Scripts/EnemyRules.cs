using UnityEngine;
using System.Collections;

public class EnemyRules : MonoBehaviour {
	ScoringRules scorer;

	// Use this for initialization
	void Start () {
		scorer = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver()
	{
		if(Input.GetAxisRaw("Fire1") != 0)
		{
			Destroy(gameObject);
			scorer.EnemyKilled(1.0f);
		}
	}
}

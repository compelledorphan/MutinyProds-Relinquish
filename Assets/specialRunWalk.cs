using UnityEngine;
using System.Collections;

public class specialRunWalk : MonoBehaviour 
{
	
	Animator anim;
	NavMeshAgent agent;
	
	public ParticleSystem SpecialTrailParticle;
	public bool SpecialTrailParticleActive = false;
	public GameObject ParticleContainer;
	public GameObject RagdollPrefab;
	
	// Use this for initialization
	void Start () 
	{
		anim= GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float speed = (agent.speed / 9.1f);
		anim.SetFloat ("Speed", speed);
	}
}

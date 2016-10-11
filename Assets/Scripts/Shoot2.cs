using UnityEngine;
using System.Collections;

public class Shoot2 : MonoBehaviour 
{
	ScoringRules scorer;

	//public GameObject bullet;
	public float delayTime = 0.5f;
	public GameObject bulletHole;

	private float counter = 0.0f;

	public Light MuzzleLight2;
	public GameObject MuzzleLight;
	public ParticleEmitter MuzzleFlash;
	float MuzzleTimer = 0.0f;

	public GameObject DeathParticles;
	ParticleEmitter TempDeathparticles;
	float DeathParticleTimer = 0.0f;
	public float DeathParticleLength = 1.0f;
	GameObject TempDeathParticles;

	public float Accuracy = 0.01f;
	//float MuzzleCooler = 0.1f;

	public BulletManagement BulletHoleManager;
	GameObject tempGameObject;

	// Use this for initialization
	void Start () 
	{
		scorer = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;
	//	BulletHoleManager = FindObjectOfType(typeof(BulletManagement)) as BulletManagement;
		BulletHoleManager = (GameObject.FindGameObjectWithTag("BulletHoleManager")).GetComponent<BulletManagement>() as BulletManagement;
	}

	void FixedUpdate () 
	{
		if(Input.GetAxisRaw("Fire1") != 0 && counter > delayTime)
		{
			//GetComponent<ParticleEmitter>().
			//if(MuzzleTimer < 0.1f && MuzzleFlash)
			{
				MuzzleFlash.Emit();
				MuzzleLight2.enabled = true;
				MuzzleTimer = 0.0f;
				//MuzzleLight.activeSelf = true;
			}

			//Instantiate(bullet, transform.position, transform.rotation);
			GetComponent<AudioSource>().Play();
			counter = 0;

			RaycastHit hit;

			Vector3 randomAngle = new Vector3(Random.Range((-Accuracy), Accuracy), 
			                                  Random.Range((-Accuracy), Accuracy), 
			                                  Random.Range((-Accuracy), Accuracy));

			Vector3 inaccurateRay = new Vector3(transform.forward.x + randomAngle.x, 
			                                    transform.forward.y + randomAngle.y,
			                                    transform.forward.z + randomAngle.z);
			//inaccurateRay.x 

			Ray ray = new Ray(transform.position, inaccurateRay);
			if(Physics.Raycast(ray, out hit, 100.0f) && (hit.collider.gameObject.tag == "Enviroment"))
			{
				tempGameObject = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				tempGameObject.transform.parent = BulletHoleManager.transform;
				BulletHoleManager.AddBullet(tempGameObject);
			}
			else if(Physics.Raycast(ray, out hit, 100.0f) && (hit.collider.gameObject.tag == "Enemy"))
			{
				Destroy(hit.collider.gameObject);
				scorer.EnemyKilled(1.0f);

				TempDeathParticles = Instantiate(DeathParticles, hit.transform.position, Quaternion.identity) as GameObject;
				//TempDeathParticles.transform.parent = this.transform;

				DeathParticleTimer = 0;
			}
		}

		if(MuzzleTimer > 0.02f && MuzzleFlash)
		{
			MuzzleFlash.emit = false;
			MuzzleLight2.enabled = false;
			//MuzzleLight.activeSelf = false;
		}

		//if(MuzzleTimer < 0 && Input.GetAxisRaw("Fire1"))
		{
			//MuzzleLight.active = !MuzzleLight.active;
		}

		if(DeathParticleTimer > DeathParticleLength)
		{
			Destroy(TempDeathParticles);
		}

		counter += Time.deltaTime;
		MuzzleTimer += Time.deltaTime; 
	}
}

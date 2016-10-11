using UnityEngine;
using System.Collections;

public class RifleShoot : MonoBehaviour 
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
    
    public float Accuracy = 0.01f;
    //float MuzzleCooler = 0.1f;
    
    public BulletManagement BulletHoleManager;
    GameObject tempGameObject;

	public GameObject DeathParticles;
	GameObject ParticleManager;

	public AudioSource[] sounds3;
	public AudioSource RifleSound;

	// Use this for initialization
    void Start () 
    {
        scorer = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;
		ParticleManager = GameObject.FindGameObjectWithTag("ParticleManager");
        BulletHoleManager = (GameObject.FindGameObjectWithTag("BulletHoleManager")).GetComponent<BulletManagement>() as BulletManagement;

		MuzzleFlash.emit = false;
        MuzzleLight2.enabled = false;

		sounds3 = GetComponents<AudioSource>();
		RifleSound = sounds3[0];
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
				tempGameObject = Instantiate(bulletHole, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                tempGameObject.transform.parent = BulletHoleManager.transform;
                BulletHoleManager.AddBullet(tempGameObject);
            }
            else if(Physics.Raycast(ray, out hit, 100.0f) && (hit.collider.gameObject.tag == "Enemy"))
            {
                Destroy(hit.collider.gameObject);
                scorer.EnemyKilled(1.0f);

				tempGameObject = Instantiate(DeathParticles, hit.transform.position, Quaternion.identity) as GameObject;
				tempGameObject.transform.parent = ParticleManager.transform;

            }
			else if(Physics.Raycast (ray, out hit, 100.0f) && (hit.collider.gameObject.tag == "Door"))
			{
				tempGameObject = Instantiate(bulletHole, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
				tempGameObject.transform.parent = hit.transform;
				BulletHoleManager.AddBullet(tempGameObject);
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

        counter += Time.deltaTime;
        MuzzleTimer += Time.deltaTime; 
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PistolShot : MonoBehaviour 
{
    public GameObject KillScoreUI;

    public GameObject GunObject;
    Animator anim;

   Camera MainCam;

	GameObject CrosshairF1;
	GameObject CrosshairF2;
	GameObject CrosshairF3;
	int AccuracyState = 0;
	float AccuracyCoolTimer = 0.0f;
	public float AccuracyCoolRate = 0.2f;

	ScoringRules scorer;
	
	//public GameObject bullet;
	public float delayTime = 0.5f;
	public GameObject bulletHole;

	public bool AutoReload = false;
	int AmmoClip;
	public int FullAmmoClip = 6;
	public float ReloadTime = 2.0f;
	float ReloadTimer = 0.0f;
	bool m_bReloading = false;
	GameObject Gun;
	
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
	ParticleEmitter TempDeathparticles;
	float DeathParticleTimer = 0.0f;
	public float DeathParticleLength = 1.0f;
	GameObject TempDeathParticles;

	bool bShooting = false;

	public AudioSource[] sounds2;
	public AudioSource PistolSound;
	public AudioSource PistolClick;
	public AudioSource PistolReload;

	//bullet HUD
	public GameObject Bullets;
	GameObject BulletsHUD;
	Image[] BulletChildren;
	//public Image BulletFrom;
	public Sprite BulletGold;
	public Sprite BulletEmpty;

	bool bHud;
	bool SetupAmmo;

    bool bZoom = false;
    float tParam  = 0.0f;
    float ZoomSpeed = 10.0f;
    bool ZoomReset = false;

    GameObject SpecialParticleTarget;
    
    bool bActive = false;

    GameObject Player;
    GameObject CamBoom;
    
    float fixCamTimer = 0.0f;
    float RecoilTime = 0.05f;

    public float AccuracyMultiplyer = 1.0f;

    crosshairController Crosshair;

	// Use this for initialization
	void Start () 
	{
        Gun = GameObject.FindGameObjectWithTag("PistolObject") as GameObject;

        Player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        CamBoom = GameObject.FindGameObjectWithTag("CameraBoom") as GameObject;

        anim = GunObject.GetComponent<Animator>();

        SpecialParticleTarget = GameObject.FindGameObjectWithTag("ParticleManager") as GameObject;

		SetupAmmo = false;
		bHud = false;

		AmmoClip = FullAmmoClip;

        Crosshair = FindObjectOfType(typeof(crosshairController)) as crosshairController;

        scorer = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;

		//  BulletHoleManager = FindObjectOfType(typeof(BulletManagement)) as BulletManagement;
		BulletHoleManager = (GameObject.FindGameObjectWithTag("BulletHoleManager")).GetComponent<BulletManagement>() as BulletManagement;
		MuzzleFlash.emit = false;
		MuzzleLight2.enabled = false;

		CrosshairF1 = GameObject.FindGameObjectWithTag("Crosshair1");
		CrosshairF2 = GameObject.FindGameObjectWithTag("Crosshair2");
		CrosshairF3 = GameObject.FindGameObjectWithTag("Crosshair3");

        MainCam = (GameObject.FindGameObjectWithTag("MainCamera")).GetComponent<Camera>() as Camera;

		sounds2 = GetComponents<AudioSource>();
		PistolSound = sounds2[0];
		PistolClick = sounds2[1];
		PistolReload = sounds2[2];

		BulletsHUD = Instantiate(Bullets) as GameObject;

		BulletChildren = BulletsHUD.GetComponentsInChildren<Image>();


	}
	
	void FixedUpdate () 
	{
        if(bActive)
        {
            ActivePistol();
            fixCam();
        }
        else
        {
            //Pistol is not active, do nothing
        }
    

	}

	public void SetAmmoClip(int _iAmmo)
	{
		AmmoClip = _iAmmo;
	}

	public int GetAmmoClip()
	{
		return(AmmoClip);
	}

	void ProcessAmmoHUD()
	{
		//BulletChildren[0].sprite = BulletEmpty;
		//BulletChildren[1].sprite = BulletEmpty;
		//BulletChildren[2].sprite = BulletEmpty;
		//BulletChildren[3].sprite = BulletEmpty;
		//BulletChildren[4].sprite = BulletEmpty;
		//BulletChildren[5].sprite = BulletEmpty;
		//DisableHUD();


		for(int i = 0; i < 6; ++i)
		{
			if(i < AmmoClip && SetupAmmo)
			{
				BulletChildren[i].sprite = BulletGold;
			}
			else
			{
				BulletChildren[i].sprite = BulletEmpty;
			}
		}

	}

	public void EnableHUD()
	{
		bHud = true;
		for(int i = 0; i < 6; ++i)
		{
			if(SetupAmmo == false)
			{
				break;
			}
			BulletChildren[i].enabled = true;
		}
	}

	public void DisableHUD()
	{
		bHud = false;
		for(int i = 0; i < 6; ++i)
		{
			if(SetupAmmo == false)
			{
				break;
			}
			BulletChildren[i].enabled = false;
		}
	}

	public int getAmmoCount()
	{
		return(AmmoClip);
	}

    public void Draw()
    {
        anim = GunObject.GetComponent<Animator>();
        anim.Play("PSDraw", -1);
        bZoom = false;
    }

    void AnimWalker()
    {
        if( Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    void Zoom()
    {
        if( Input.GetAxisRaw("Zoom") != 0)
        {
            if(bZoom == false)
            {
                tParam = 0;
            }
            bZoom = true;
            if (tParam < 1) 
            {
                tParam += Time.deltaTime * ZoomSpeed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
                MainCam.fieldOfView = Mathf.Lerp(60, 45, tParam);
            }
        }
        else
        {
            if(bZoom == true)
            {
                tParam = 0;
            }
            bZoom = false;
            if (tParam < 1) 
            {
                tParam += Time.deltaTime * ZoomSpeed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
                MainCam.fieldOfView = Mathf.Lerp(45, 60, tParam);
            }
        }
    }

    void ActivePistol()
    {
        AccuracyAlgorithm();
        AnimWalker();
        Zoom();
        
        if( AmmoClip == 0 && Input.GetAxisRaw("Fire1") != 0 && m_bReloading == false && bShooting == false && AutoReload)
        {
            PistolReload.Play ();
            m_bReloading = true;
            ReloadTimer = -ReloadTime;
            //Gun.transform.Rotate (45, 0, 0);
            anim.Play("PSReload", -1);
        }
        
        if(Input.GetAxisRaw("Fire1") != 0 && counter > delayTime && bShooting == false && AmmoClip == 0 && m_bReloading == false)
        {
            PistolClick.Play ();
            bShooting = true;
        }
        
        if(Input.GetAxisRaw("Fire1") != 0 && counter > delayTime && bShooting == false && AmmoClip > 0)
        {
            anim.Play("PSShot", -1, 0.0f);
            bShooting = true;
            //AccuracyCoolTimer += 1 * AccuracyCoolRate +AccuracyCoolRate;
            //
            //if(AccuracyCoolTimer > 3 * AccuracyCoolRate)
            //{
            //    AccuracyCoolTimer = 3 * AccuracyCoolRate;
            //}

            AccuracyMultiplyer++;
            AccuracyMultiplyer++;
            
            AmmoClip--;
            PistolSound.Play();
            
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
            
            Vector3 randomAngle = new Vector3(Random.Range((-Accuracy * AccuracyMultiplyer), Accuracy * AccuracyMultiplyer), 
                                              Random.Range((-Accuracy * AccuracyMultiplyer), Accuracy * AccuracyMultiplyer), 
                                              Random.Range((-Accuracy * AccuracyMultiplyer), Accuracy * AccuracyMultiplyer));
            
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
            else if(Physics.Raycast(ray, out hit, 100.0f) && ((hit.collider.gameObject.tag == "Enemy") || (hit.collider.gameObject.tag == "SpecialEnemy")))
            {
                EnemyRunWalk tempRun = hit.collider.gameObject.GetComponent<EnemyRunWalk>();
                
                if(tempRun.SpecialTrailParticleActive == true)
                {
                    tempRun.ParticleContainer.transform.parent = SpecialParticleTarget.transform;
                }
                else
                {
                    // Do nothing
                }

                Instantiate(KillScoreUI);
                
                tempGameObject = Instantiate(tempRun.RagdollPrefab, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation) as GameObject;
                
                Rigidbody tempRigid = tempGameObject.GetComponentInChildren<Rigidbody>() as Rigidbody;
                tempRigid.AddForceAtPosition(new Vector3(ray.direction.x + 100.0f, ray.direction.y, ray.direction.z + 10.0f), hit.point);
                
                GameObject RagdollCollect = GameObject.FindGameObjectWithTag("RagdollManager") as GameObject;
                RagdollCollect.GetComponent<RagdollManager>().AddRagdolls(tempGameObject);
                tempGameObject.transform.parent = RagdollCollect.transform;


                GameObject Camera = GameObject.FindGameObjectWithTag("MainCamera") as GameObject;

                if (Camera != null)
                {
                    WarningScreenNoise tempComp = Camera.GetComponent<WarningScreenNoise>() as WarningScreenNoise;

                    if (tempComp != null)
                    {
                        tempComp.EnemyKilled();
                    }
                    else
                    {
                        Debug.Log("Null Comppistol");
                    }
                }
                else
                {
                    Debug.Log("null camera");
                }


                Destroy(hit.collider.gameObject);
                scorer.EnemyKilled();
                
                TempDeathParticles = Instantiate(DeathParticles, hit.transform.position, Quaternion.identity) as GameObject;
                //TempDeathParticles.transform.parent = this.transform;
                
                DeathParticleTimer = 0;
            }
            else if(Physics.Raycast (ray, out hit, 100.0f) && (hit.collider.gameObject.tag == "Door"))
            {
                tempGameObject = Instantiate(bulletHole, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                tempGameObject.transform.parent = hit.transform;
                BulletHoleManager.AddBullet(tempGameObject);
            }

            Vector3 Amount;
            Amount.x = 1.0f;
            Amount.y = 1.0f;
            Amount.z = 1.0f;
            iTween.ShakeRotation(CamBoom, Amount, RecoilTime);
            CamBoom.transform.Rotate (-2.0f, 0.0f, 0.0f);
        }
        else if(Input.GetAxisRaw("Fire1") == 0)
        {
            bShooting = false;
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
        
        if(ReloadTimer < 0)
        {
            //reloading
        }
        else
        {
            if(m_bReloading)
            {
                m_bReloading = false;
                AmmoClip = FullAmmoClip;
                //Gun.transform.Rotate (-45, 0, 0);
            }
        }
        
        if(Input.GetAxisRaw("Reload") != 0 && m_bReloading == false  && AmmoClip != FullAmmoClip)
        {
            PistolReload.Play ();
            AmmoClip = 0;
            m_bReloading = true;
            ReloadTimer = -ReloadTime;
            //Gun.transform.Rotate (45, 0, 0);
            anim.Play("PSReload", -1);
        }
        
        ReloadTimer += Time.deltaTime;
        counter += Time.deltaTime;
        MuzzleTimer += Time.deltaTime; 
        
        //if(AccuracyCoolTimer > 0)
        //{
        //    AccuracyCoolTimer -= Time.deltaTime;
        //}
        //
        //if(AccuracyCoolTimer > 2 * AccuracyCoolRate)
        //{
        //    //CrosshairF1.SetActive (false);
        //    //CrosshairF2.SetActive (false);
        //    //CrosshairF3.SetActive (true);
        //    AccuracyState = 8;
        //}
        //else if(AccuracyCoolTimer > 1 * AccuracyCoolRate)
        //{
        //    //CrosshairF1.SetActive (false);
        //    //CrosshairF2.SetActive (true);
        //    //CrosshairF3.SetActive (false);
        //    AccuracyState = 4;
        //}
        //else
        //{
        //    //CrosshairF1.SetActive (true);
        //    //          CrosshairF2.SetActive (false);
        //    //          CrosshairF3.SetActive (false);
        //    AccuracyState = 1;
        //}
        
        if(bHud)
        {
            ProcessAmmoHUD();
        }
        SetupAmmo = true;
    }

    public void SetActive(bool _bActive)
    {
        bActive = _bActive;
    }

    public void ActivatePistol()
    {
        bActive = true;
        if(Gun != null)
        {
            Gun.SetActive(true);
        }

        //Transform temptrans = this.GetComponentInChildren<Transform>() as Transform;
        //Debug.Log(temptrans);
        //if(temptrans != null)
        //{
        //    temptrans.gameObject.SetActive(true);
        //}
        EnableHUD();
    }

    public void DisablePistol()
    {
        bActive = false;
       if(Gun != null)
       {
           Gun.SetActive(false);
       }
        //Transform temptrans = this.GetComponentInChildren<Transform>() as Transform;
        //if(temptrans != null)
        //{
        //    temptrans.gameObject.SetActive(false);
        //}
        DisableHUD();
    }

    void fixCam()
    {
        fixCamTimer += Time.fixedDeltaTime;
        
        //if(fixCamTimer > RecoilTime)
        {
            //CamBoom.transform.localRotation = Quaternion.Euler (0,0,0);
            Quaternion target = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            CamBoom.transform.localRotation = Quaternion.Slerp(CamBoom.transform.localRotation, target, Time.deltaTime * 4.1f);
            CamBoom.transform.localRotation = Quaternion.Euler(CamBoom.transform.localRotation.eulerAngles.x, 0.0f, CamBoom.transform.localRotation.eulerAngles.z);
        }
    }

    void AccuracyAlgorithm()
    {
        float MinAccuracy = 1.0f;

        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            //do thie thing!
            if(Input.GetAxisRaw("Sprint") != 0)
            {
                MinAccuracy = 8.0f;
            }
            else
            {
                MinAccuracy = 4.0f;
            }
        }
        else
        {
            MinAccuracy = 1.0f;
        }

        AccuracyMultiplyer = Mathf.Lerp(AccuracyMultiplyer, MinAccuracy, Time.deltaTime * 2.0f);
        TellCrosshair();
    }

    public float GetAccuracy()
    {
        return(AccuracyMultiplyer);
    }

    public void TellCrosshair()
    {
        Crosshair.SetAccuracy(AccuracyMultiplyer);
    }

   
}

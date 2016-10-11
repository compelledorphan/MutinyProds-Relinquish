using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShotgunShoot : MonoBehaviour 
{
    public GameObject KillScoreUI;

    ScoringRules scorer;
    
    public GameObject GunObject;
    Animator anim;
    
    //public GameObject bullet;
    public float delayTime = 0.5f;
    public GameObject bulletHole;
    
    private float counter = 0.0f;
    
    public bool AutoReload = false;
    int AmmoClip;
    public int FullAmmoClip = 2;
    public float ReloadTime = 2.0f;
    float ReloadTimer = 0.0f;
    bool m_bReloading = false;
    GameObject Gun;
    
    public Light MuzzleLight2;
    public GameObject MuzzleLight;
    public ParticleEmitter MuzzleFlash;
    float MuzzleTimer = 0.0f;
    
    public int Spray = 8;
    
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
    
    public AudioSource[] sounds;
    public AudioSource Shotgun2;
    public AudioSource ShotgunClick;
    public AudioSource ShotgunReload;
    
    public GameObject Bullets;
    GameObject BulletsHUD;
    Image[] BulletChildren;
    //public Image BulletFrom;
    public Sprite BulletGold;
    public Sprite BulletEmpty;
    
    bool SetupAmmo;
    
    bool bHud;
    
    Camera MainCam;
    bool bZoom = false;
    float tParam  = 0.0f;
    float ZoomSpeed = 10.0f;
    
    GameObject SpecialParticleTarget;
    
    bool bActive = false;
    
    public float ShakeAmount = 0.2f;
    GameObject Player;
    GameObject CamBoom;
    
    float fixCamTimer = 0.0f;
    float RecoilTime = 0.15f;

    crosshairController Crosshair;
    
    // Use this for initialization
    void Start () 
    {
        Gun = GameObject.FindGameObjectWithTag("ShotgunObject") as GameObject;
        
        Player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        CamBoom = GameObject.FindGameObjectWithTag("CameraBoom") as GameObject;
        
        anim = GunObject.GetComponent<Animator>();
        
        SpecialParticleTarget = GameObject.FindGameObjectWithTag("ParticleManager") as GameObject;

        Crosshair = FindObjectOfType(typeof(crosshairController)) as crosshairController;
        
        SetupAmmo = false;
        
        bHud = false;
        
        AmmoClip = FullAmmoClip;
        
        scorer = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;
        //  BulletHoleManager = FindObjectOfType(typeof(BulletManagement)) as BulletManagement;
        BulletHoleManager = (GameObject.FindGameObjectWithTag("BulletHoleManager")).GetComponent<BulletManagement>() as BulletManagement;
        MuzzleFlash.emit = false;
        MuzzleLight2.enabled = false;
        
        sounds = GetComponents<AudioSource>();
        Shotgun2 = sounds[0];
        ShotgunClick = sounds[1];
        ShotgunReload = sounds[2];
        
        BulletsHUD = Instantiate(Bullets) as GameObject;
        
        BulletChildren = BulletsHUD.GetComponentsInChildren<Image>();
        
        MainCam = (GameObject.FindGameObjectWithTag("MainCamera")).GetComponent<Camera>() as Camera;
        
        DisableShotgun();
    }
    
    void FixedUpdate () 
    {
        if(bActive)
        {
            ActiveShotgun();
        }
        else
        {
            //do nothing
        }
        
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
        
        
        for(int i = 0; i < 2; ++i)
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
        for(int i = 0; i < 2; ++i)
        {
            if(SetupAmmo == false)
            {
                break;
            }
            BulletChildren[i].enabled = true;
        }
        
        for(int i = 2; i < 6; ++i)
        {
            if(SetupAmmo == false)
            {
                break;
            }
            BulletChildren[i].enabled = false;
        }
    }
    
    public void DisableHUD()
    {
        bHud = true;
        for(int i = 0; i < 2; ++i)
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
        MainCam = (GameObject.FindGameObjectWithTag("MainCamera")).GetComponent<Camera>() as Camera;
        anim = GunObject.GetComponent<Animator>();
        anim.Play("SGDraw", -1);
        MainCam.fieldOfView = 60.0f;
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
            //if (tParam < 1) 
            //{
            //    tParam += Time.deltaTime * ZoomSpeed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            //    MainCam.fieldOfView = Mathf.Lerp(60, 30, tParam);
            //}
        }
        else
        {
            //if(bZoom == true)
            //{
            //    tParam = 0;
            //}
            //bZoom = false;
            //if (tParam < 1) 
            //{
            //    tParam += Time.deltaTime * ZoomSpeed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            //    MainCam.fieldOfView = Mathf.Lerp(30, 60, tParam);
            //}
        }
    }
    
    void ActiveShotgun()
    {
        AccuracyAlgorithm();

        if( AmmoClip == 0 && Input.GetAxisRaw("Fire1") != 0 && m_bReloading == false && bShooting == false && AutoReload)
        {
            m_bReloading = true;
            ReloadTimer = -ReloadTime;
            //Gun.transform.Rotate (45, 0, 0);
            anim.Play("SGReload", -1);
            ShotgunReload.Play();
        }
        
        if(Input.GetAxisRaw("Fire1") != 0 && counter > delayTime && bShooting == false && AmmoClip == 0 && m_bReloading == false)
        {
            ShotgunClick.Play ();
            bShooting = true;
        }
        
        if(Input.GetAxisRaw("Fire1") != 0 && counter > delayTime && bShooting == false && AmmoClip > 0)
        {
            Vector3 Amount;
            Amount.x = 2.0f;
            Amount.y = 2.0f;
            Amount.z = 2.0f;
            
            anim.Play("SGShot", -1, 0.0f);
            iTween.ShakeRotation(CamBoom, Amount, RecoilTime);
            CamBoom.transform.Rotate (-4.0f, 0.0f, 0.0f);
            
            bShooting = true;
            //GetComponent<ParticleEmitter>().
            //if(MuzzleTimer < 0.1f && MuzzleFlash)
            {
                MuzzleFlash.Emit();
                MuzzleLight2.enabled = true;
                MuzzleTimer = 0.0f;
                //MuzzleLight.activeSelf = true;
            }
            
            AmmoClip--;
            
            //Instantiate(bullet, transform.position, transform.rotation);
            Shotgun2.Play();
            counter = 0;
            
            //
            for(int i = 0; i < Spray; ++i)
            {
                Vector3 randomAngle = new Vector3(Random.Range((-Accuracy), Accuracy), 
                                                  Random.Range((-Accuracy), Accuracy), 
                                                  Random.Range((-Accuracy), Accuracy));
                
                float BulletScale = Random.Range (0.020f, 0.040f);
                
                Vector3 inaccurateRay = new Vector3(transform.forward.x + randomAngle.x, 
                                                    transform.forward.y + randomAngle.y,
                                                    transform.forward.z + randomAngle.z);
                
                RaycastHit hit;
                //inaccurateRay.x 
                
                Ray ray = new Ray(transform.position, inaccurateRay);
                if(Physics.Raycast(ray, out hit, 100.0f) && (hit.collider.gameObject.tag == "Enviroment"))
                {
                    tempGameObject = Instantiate(bulletHole, hit.point + hit.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                    tempGameObject.transform.localScale = new Vector3(transform.localScale.x * BulletScale,
                                                                      transform.localScale.y * BulletScale, 
                                                                      transform.localScale.z * BulletScale);
                    tempGameObject.transform.parent = BulletHoleManager.transform;
                    BulletHoleManager.AddBullet(tempGameObject);
                }
                else if(Physics.Raycast(ray, out hit, 100.0f) && ((hit.collider.gameObject.tag == "Enemy") || (hit.collider.gameObject.tag == "SpecialEnemy")))
                {

                    EnemyRunWalk tempRun = hit.collider.gameObject.GetComponent<EnemyRunWalk>();
                    
                    if (tempRun.SpecialTrailParticleActive == true)
                    {
                        tempRun.ParticleContainer.transform.parent = SpecialParticleTarget.transform;
                    }
                    else
                    {
                        // Do nothing
                    }
                    
                    if((hit.collider.gameObject.GetComponent ("Shot") as Shot).bShot == true)
                    {
                        //do nothing
                    }
                    else
                    {
                        
                        (hit.collider.gameObject.GetComponent ("Shot") as Shot).bShot = true;
                        
                        tempGameObject = Instantiate(tempRun.RagdollPrefab, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation) as GameObject;
                        
                        Rigidbody tempRigid = tempGameObject.GetComponentInChildren<Rigidbody>() as Rigidbody;
                        tempRigid.AddForceAtPosition(new Vector3(ray.direction.x + 100.0f, ray.direction.y, ray.direction.z + 10.0f), hit.point);
                        
                        GameObject RagdollCollect = GameObject.FindGameObjectWithTag("RagdollManager") as GameObject;
                        RagdollCollect.GetComponent<RagdollManager>().AddRagdolls(tempGameObject);
                        tempGameObject.transform.parent = RagdollCollect.transform;

                        Instantiate(KillScoreUI);

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
                        scorer.EnemyKilled(1.0f);
                        
                        TempDeathParticles = Instantiate(DeathParticles, hit.transform.position, Quaternion.identity) as GameObject;
                        //TempDeathParticles.transform.parent = this.transform;
                        
                        DeathParticleTimer = 0;
                    }
                }
                else if(Physics.Raycast (ray, out hit, 100.0f) && (hit.collider.gameObject.tag == "Door"))
                {
                    tempGameObject = Instantiate(bulletHole, hit.point + hit.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                    tempGameObject.transform.parent = hit.transform;
                    BulletHoleManager.AddBullet(tempGameObject);
                }
            }
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
        
        if(Input.GetAxisRaw("Reload") != 0 && m_bReloading == false && AmmoClip != FullAmmoClip)
        {
            AmmoClip = 0;
            m_bReloading = true;
            ReloadTimer = -ReloadTime;
            //Gun.transform.Rotate (45, 0, 0);
            anim.Play("SGReload", -1);
            ShotgunReload.Play();
        }
        
        ReloadTimer += Time.deltaTime;
        counter += Time.deltaTime;
        MuzzleTimer += Time.deltaTime; 
        
        if(bHud)
        {
            ProcessAmmoHUD();
        }
        SetupAmmo = true;
        
        AnimWalker();
        
        fixCam();
    }
    
    public void SetActive(bool _bActive)
    {
        bActive = _bActive;
    }
    
    public void ActivateShotgun()
    {
        bActive = true;
        if(Gun != null)
        {
            Gun.SetActive(true);
        }
        //Transform temptrans = this.GetComponentInChildren<Transform>() as Transform;
        //if(temptrans != null)
        //{
        //    temptrans.gameObject.SetActive(true);
        //}
        EnableHUD();
    }
    
    public void DisableShotgun()
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
            CamBoom.transform.localRotation = Quaternion.Slerp(CamBoom.transform.localRotation, target, Time.deltaTime * 2.1f);
            CamBoom.transform.localRotation = Quaternion.Euler(CamBoom.transform.localRotation.eulerAngles.x, 0.0f, CamBoom.transform.localRotation.eulerAngles.z);
        }
    }

    void AccuracyAlgorithm()
    {
        Crosshair.SetAccuracy(12.0f);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

using UnityStandardAssets.ImageEffects;

public class UIManager : MonoBehaviour {

	ScoringRules scorer;
    TutorialManager TutMan;
    GunManager GunMan;
    Game GameMan;

    GameObject UICam;
    GameObject DeathCamObject;
    Camera DeathCam;
    BlurOptimized BlurEffect;
    SepiaTone DeathEffect;
    CameraMotionBlur DeathMotion;
    public CanvasGroup DeathCanvas;

    public CanvasGroup JuicyCanvasGroup;
	public Text HUDScore;
	public Text HUDMultiplyer;
	public Text HUDJuicyMultiplyer;
	public Text HUDJuicyScoreBonus;
	//public Canvas FadeMultiplyer;

	public GameObject YouDiedScreen;
	public GameObject YouWinScreen;
	public GameObject PauseScreen;
	public GameObject Crosshair;

	public GameObject Player;

	public GameObject ToolTipSpawner;
	public GameObject ToolTipSpecial;
	public GameObject ToolTipStarter;
	public GameObject ToolTipDoor;
	public GameObject ToolTipDoorLocked;
	public GameObject ToolTipSoftReset;

    public GameObject HUD;


	public GameObject ToolTipsOnText;
	public GameObject ToolTipsOffText;

	float fadeSpeed = 2.0f;

	bool m_bButtonPressed = false;

	public bool m_bToolTips;

	int m_iGameState = 0;

	float GameTimer = 0;
	bool IsStartSpawnerTip = true;

	StaticSettings GameSettings;

    float DeathTimer = 0.0f;
    bool DeathCompleted = false;

    public AudioSource[] sounds;
    public AudioSource DeathSound;
    public AudioSource DeathSound2;
    public AudioSource DeathSound3;

    GameObject CamBoom;

    public GameObject Level;

    public GameObject ShakeRoot;


	// 0 = in game
	// 1 = paused
	// 2 = main menu
	// 3 = Dead
	// 4 = won
	// 5 =

	// Use this for initialization
	void Start () 
	{
		scorer = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;

		GameSettings = FindObjectOfType(typeof(StaticSettings)) as StaticSettings;

		TutMan = FindObjectOfType(typeof(TutorialManager)) as TutorialManager;

        GunMan = FindObjectOfType(typeof(GunManager)) as GunManager;

        GameMan = FindObjectOfType(typeof(Game)) as Game;

        CamBoom = GameObject.FindGameObjectWithTag("CameraBoom") as GameObject;

        ShakeRoot = GameObject.FindGameObjectWithTag("ShakeRoot") as GameObject;

		//scorer = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;

        UICam = GameObject.FindGameObjectWithTag ("UICam");
        DeathCamObject = GameObject.FindGameObjectWithTag ("DeathCam");
        DeathCam = DeathCamObject.GetComponent<Camera>();
        DeathCam.enabled = false;
        BlurEffect = UICam.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();
        BlurEffect.enabled = false;

        DeathEffect = UICam.GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>();
        DeathEffect.enabled = false;

        DeathMotion = UICam.GetComponent<UnityStandardAssets.ImageEffects.CameraMotionBlur>();
        DeathMotion.enabled = false;

		ToggleToolTipsOn();

		//Time.timeScale = 0.0f; 
		//YouDiedScreen.SetActive(true);
		//Death ();
		Time.timeScale = 1.0f; 
		YouDiedScreen.SetActive (false);
		YouWinScreen.SetActive (false);
		PauseScreen.SetActive (false);
		Crosshair.SetActive (true);
		CleanToolTips();
		ToolTipStarter.SetActive (true);

		m_bToolTips = GameSettings.getToolTips();
		//GameSettings

		JuicyUI();

        sounds = GetComponents<AudioSource>();
        DeathSound = sounds[0];
        DeathSound2 = sounds[1];
        DeathSound3 = sounds[2];
	}
	
	// Update is called once per frame
	void Update () 
	{
		updateHUD();

		m_bToolTips = GameSettings.getToolTips();

		GameTimer += Time.deltaTime; 
		if(GameTimer > 7 && IsStartSpawnerTip == true)
		{
			IsStartSpawnerTip = false;
			CleanToolTips();
		}

		if(m_iGameState == 0)
		{

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;

			if(Input.GetAxisRaw ("Pause") != 0 && m_bButtonPressed == false) 
			{
				m_iGameState = 1;
				m_bButtonPressed = true;
				//Player.SetActive (false);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}

			if(Input.GetAxisRaw ("Pause") == 0)
			{
				m_bButtonPressed = false;
			}

		}
		else if(m_iGameState == 1)
		{
			Time.timeScale = 0.0f; 
			YouDiedScreen.SetActive (false);
			YouWinScreen.SetActive (false);
			PauseScreen.SetActive (true);
			Crosshair.SetActive (false);
            BlurEffect.enabled = true;
		
            GunMan.DisableHUD();

			Player.GetComponent<FirstPersonController>().enabled = false;


			//Player.SetActive (false);

			if(Input.GetAxisRaw ("Pause") != 0 && m_bButtonPressed == false) 
			{
				m_bButtonPressed = true;
				UnPause ();
			}
			
			if(Input.GetAxisRaw ("Pause") == 0)
			{
				m_bButtonPressed = false;
			}
		}
		else if(m_iGameState == 2)
		{
			// Main Menu

			Time.timeScale = 0.0f; 
			YouDiedScreen.SetActive (false);
			YouWinScreen.SetActive (false);
			PauseScreen.SetActive (false);
			Crosshair.SetActive (false);
		}
		else if(m_iGameState == 3)
		{
			//you died
            DeathTimer += Time.deltaTime;
            if(DeathTimer > 0.5f && DeathCompleted == false)
            {
                DeathComplete();
            }
           
            //BlurEffect.blurSize = DeathTimer * 0.1f;
            //BlurEffect.downsample = 0;

            //DeathCamObject.transform.Rotate(Vector3.left, 200.0f * Time.deltaTime);

            Quaternion target = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            DeathCamObject.transform.localRotation = Quaternion.Slerp(DeathCamObject.transform.localRotation, target, Time.deltaTime * 6.0f);

            DeathCanvas.alpha = DeathTimer * 2.0f;;

            //Vector3 TempPos = DeathCamObject.transform.position.y - (DeathTimer * 2.0f);
            Player.transform.Translate(Vector3.up * Time.deltaTime * -3.0f, Space.World);
            //DeathCamObject.transform.position.y -= DeathTimer * 2.0f;

            //DeathEffect.

		}
		else if(m_iGameState == 4)
		{
			//you won

			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;

			Time.timeScale = 0.0f; 
			YouDiedScreen.SetActive (false);
			YouWinScreen.SetActive (true);
			PauseScreen.SetActive (false);
			Crosshair.SetActive (false);

			Player.GetComponent<FirstPersonController>().enabled = false;
		}
	}

	public void UnPause()
	{
		Time.timeScale = 1.0f; 
		YouDiedScreen.SetActive (false);
		YouWinScreen.SetActive (false);
		PauseScreen.SetActive (false);
		Crosshair.SetActive (true);
		m_iGameState = 0;
		Player.SetActive (true);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		Player.GetComponent<FirstPersonController>().enabled = true;
        BlurEffect.enabled = false;

        GunMan.EnableHUD();

	}

	public void Death()
	{
        DeathSound.Play();
        DeathSound2.Play();
        DeathSound3.Play();

        DeathCam.enabled = true;
        m_iGameState = 3;

        DeathTimer = 0.0f;

        DeathEffect.enabled = true;
        BlurEffect.enabled = true;
        DeathMotion.enabled = true;

		Time.timeScale = 0.3f;  

		Cursor.lockState = CursorLockMode.None;

		Player.GetComponent<FirstPersonController>().enabled = false;

        YouDiedScreen.SetActive (true);
        YouWinScreen.SetActive (false);
        PauseScreen.SetActive (false);
        Crosshair.SetActive (false);

        DeathCanvas.alpha = -1.0f;
	}

    void DeathComplete()
    {

        DeathUI();
        DeathCompleted = true;

        m_iGameState = 3;
        
        Time.timeScale = 0.0f;  
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Player.GetComponent<FirstPersonController>().enabled = false;

        HUD.SetActive(false);

        GunMan.DisableHUD();
    }

	public void Restart()
	{
		Destroy(GameObject.FindGameObjectWithTag("EManager"));
		scorer.Reset();
		UnPause();
		Vector3 Pos;
		Pos.x = -5.8f;
		Pos.y = 2.0f;
		Pos.z = -0.8f;
		Vector3 Look;
		Look.x = 90.0f;
		Look.y = 270.0f;
		Look.z = 90.0f;
		Player.transform.position = (Pos);
		Player.transform.rotation = (Quaternion.Euler(Look));
	}

	public void Survived()
	{
		m_iGameState = 3;
		
		Time.timeScale = 0.0f;  
		YouDiedScreen.SetActive (false);
		YouWinScreen.SetActive (true);
		PauseScreen.SetActive (false);
		Crosshair.SetActive (false);
		
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		Player.GetComponent<FirstPersonController>().enabled = false;
	}

	public void SpawnerTipUIOn()
	{
		CleanToolTips();
		if(m_bToolTips)
		{
			ToolTipSpawner.SetActive (true);
		}
	}

	public void DoorTipUIOn()
	{
		CleanToolTips();
		if(m_bToolTips)
		{
			if(TutMan.CheckLock())
			{
				ToolTipDoorLocked.SetActive (true);
			}
			else
			{
				ToolTipDoor.SetActive (true);
			}
		}
	}

	public void StarterTipUIOn()
	{
		CleanToolTips();
		if(m_bToolTips)
		{
			ToolTipStarter.SetActive (true);
		}
	}

	public void SpecialTipUIOn()
	{
		CleanToolTips();
		if(m_bToolTips)
		{
			ToolTipSpecial.SetActive (true);
		}
	}

	public void SoftResetTipUIOn()
	{
		CleanToolTips();
		if(m_bToolTips)
		{
			ToolTipSoftReset.SetActive (true);
		}
	}

	public void SpawnerTipUIOff()
	{
		CleanToolTips ();
	}

	public void ToggleToolTipsOn()
	{
		GameSettings.setToolTips(true);
		ToolTipsOnText.SetActive (true);
		ToolTipsOffText.SetActive (false);
		m_bToolTips = true;
	}

	public void ToggleToolTipsOff()
	{
		GameSettings.setToolTips(false);
		CleanToolTips();
		ToolTipsOnText.SetActive (false);
		ToolTipsOffText.SetActive (true);
		m_bToolTips = false;
	}

	public void ToggleToolTips()
	{
		if(m_bToolTips)
		{
			GameSettings.setToolTips(false);
			ToolTipsOnText.SetActive (false);
			ToolTipsOffText.SetActive (true);
			m_bToolTips = false;
		}
		else
		{
			GameSettings.setToolTips(true);
			ToolTipsOnText.SetActive (true);
			ToolTipsOffText.SetActive (false);
			m_bToolTips = true;
		}
	}

	public void CleanToolTips()
	{
		ToolTipSpawner.SetActive (false);
		ToolTipSpecial.SetActive (false);
		ToolTipStarter.SetActive (false);
		ToolTipDoor.SetActive (false);
		ToolTipSoftReset.SetActive (false);
		ToolTipDoorLocked.SetActive (false);
	}

	void updateHUD()
	{
		float Score = scorer.getScore ();
		float ScoreBonus = scorer.getScoreBonus () / 2;
		int Multiplyer = scorer.getMultiplyer () + 1;

		HUDScore.text = "" + Score;
		HUDMultiplyer.text = "x" + Multiplyer;

		HUDJuicyMultiplyer.text = "x" + Multiplyer;
		HUDJuicyScoreBonus.text = "" + ScoreBonus;

        JuicyCanvasGroup.alpha -= Time.deltaTime / 2.0f;
		//HUDJuicyScoreBonus.text =

		//if(HUDJuicyMultiplyer.color.a < 1.0f)
		{
			//float alpha = HUDJuicyMultiplyer.color.a + 5.0f * Time.deltaTime;
			//HUDJuicyMultiplyer.color = new Color(HUDJuicyMultiplyer.color.r, HUDJuicyMultiplyer.color.g, HUDJuicyMultiplyer.color.b, alpha);
		}
	}

	public void JuicyUI()
	{
		int Multiplyer = scorer.getMultiplyer () + 1;
		float ScoreBonus = scorer.getScoreBonus () / 2;
		HUDJuicyMultiplyer.text = "x" + Multiplyer;
        HUDJuicyScoreBonus.text = "&+" + ScoreBonus;
        if(Multiplyer == 1)
        {
            JuicyCanvasGroup.alpha = 0.0f;
        }
        else
        {
            JuicyCanvasGroup.alpha = 1.0f;
        }

        if(Multiplyer > 1)
        {
            ShakeIt();
        }
	}

    void DeathUI()
    {
        float Score = scorer.getScore ();

        HUDJuicyMultiplyer.text = "";
        HUDJuicyScoreBonus.text = "&+" + Score;

        JuicyCanvasGroup.alpha = 0.0f;

    }

    void ShakeIt()
    {
        Vector3 Amount;
        Amount.x = 0.1f;
        Amount.y = 0.1f;
        Amount.z = 0.1f;

        //iTween.ShakeRotation(Level, Amount, 2.0f);
        iTween.ShakePosition(ShakeRoot, Amount, 2.0f);
    }

	//private IEnumerator FadeOutCR1()
	//{
	//	float duration = 1.5f; //0.5 secs
	//	float currentTime = 0f;
	//	while(currentTime < duration)
	//	{
	//		float alpha = Mathf.Lerp(1f, 0f, currentTime/duration);
	//		HUDJuicyMultiplyer.color = new Color(HUDJuicyMultiplyer.color.r, HUDJuicyMultiplyer.color.g, HUDJuicyMultiplyer.color.b, alpha);
	//		//HUDJuicyScoreBonus.color = new Color(HUDJuicyScoreBonus.color.r, HUDJuicyScoreBonus.color.g, HUDJuicyScoreBonus.color.b, alpha);
	//		currentTime += Time.deltaTime;
	//		yield return null;
	//	}
	//	yield break;
	//}
    //
	//private IEnumerator FadeOutCR()
	//{
	//	float duration = 1.5f; //0.5 secs
	//	float currentTime = 0f;
	//	while(currentTime < duration)
	//	{
	//		float alpha = Mathf.Lerp(1f, 0f, currentTime/duration);
	//		HUDJuicyMultiplyer.color = new Color(HUDJuicyMultiplyer.color.r, HUDJuicyMultiplyer.color.g, HUDJuicyMultiplyer.color.b, alpha);
	//		HUDJuicyScoreBonus.color = new Color(HUDJuicyScoreBonus.color.r, HUDJuicyScoreBonus.color.g, HUDJuicyScoreBonus.color.b, alpha);
	//		currentTime += Time.deltaTime;
	//		yield return null;
	//	}
	//	yield break;
	//}
}

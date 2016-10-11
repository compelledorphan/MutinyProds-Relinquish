using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunManager : MonoBehaviour {
	
	//public GameObject Rifle;
	//public GameObject ShotGun;
	//public GameObject Pistol;

	public GameObject ActiveRifle;
	public GameObject ActiveShotgun;
	public GameObject ActivePistol;

	int iCurrentWeapon = 1;
	bool m_bInputHeld = false;

	PistolShot PistolScript;
	ShotgunShoot ShotgunScript;
    Winchester RifleScript;

	bool WeaponChange;
	bool WeaponChange2;

	bool FirstLoad = true;

	int iFrame;

	//GameObject TempObject;
	
	// Use this for initialization
	void Start () 
    {

		Vector3 newPos;
		newPos.x = transform.position.x + 0.0f;
		newPos.y = transform.position.y - 0.0f;
		newPos.z = transform.position.z + 0.0f;
		
		//ActiveRifle = Instantiate(Rifle, newPos, transform.rotation) as GameObject;
		//ActiveRifle.transform.parent = this.transform;
        //
		//ActiveShotgun = Instantiate(ShotGun, newPos, transform.rotation) as GameObject;
		//ActiveShotgun.transform.parent = this.transform;
        //
		//ActivePistol = Instantiate(Pistol, newPos, transform.rotation) as GameObject;
		//ActivePistol.transform.parent = this.transform;

		PistolScript = GetComponentInChildren<PistolShot>();
		ShotgunScript = GetComponentInChildren<ShotgunShoot>();
        RifleScript = GetComponentInChildren<Winchester>();

		ActiveRifle.SetActive (true);
		ActiveShotgun.SetActive (true);
		ActivePistol.SetActive (true);

        ActivatePistol();
				
		iCurrentWeapon = 1;
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(FirstLoad)
        {
            EnableHUD();
            ActivatePistol();
        }
        FirstLoad = false;

     if(Input.GetKeyDown ("1"))
		{
			//WeaponChange = true;
			//WeaponChange2 = true;
			iCurrentWeapon = 1;
            ActivatePistol();
			
		}
		if(Input.GetKeyDown ("2"))
		{
			//WeaponChange = true;
			//WeaponChange2 = true;
			iCurrentWeapon = 2;
            ActivateShotgun();
			
		}
        if(Input.GetKeyDown ("3"))
        {
            iCurrentWeapon = 3;
            ActivateRifle();
            
        }
		//if(Input.GetKeyDown ("3"))
		//{
		//	WeaponChange = true;
		//	WeaponChange2 = true;
		//	iCurrentWeapon = 3;			
		//}
		if (Input.GetAxisRaw ("ChangeWeapon") != 0) {
			if(m_bInputHeld == true)
			{
				//do nothing
			}
			else
			{
                m_bInputHeld = true;

                if (iCurrentWeapon == 1) 
                {

                    iCurrentWeapon = 2;
                    ActivateShotgun();
                } 
                else if (iCurrentWeapon == 2) 
                {
                    iCurrentWeapon = 3;
                    ActivateRifle();
                }
                else if (iCurrentWeapon == 3) 
                {
                    iCurrentWeapon = 1;
                    ActivatePistol();
                }
			}
		} 
		else 
		{
			m_bInputHeld = false;
		}
      }

    public void DisableHUD()
    {
        ShotgunScript.DisableHUD();
        PistolScript.DisableHUD();
        RifleScript.DisableHUD();
    }

    public void EnableHUD()
    {
        if(iCurrentWeapon == 1)
        {
            PistolScript.EnableHUD();
        }
        else if(iCurrentWeapon == 2)
        {
            ShotgunScript.EnableHUD();
        }
        else if(iCurrentWeapon == 3)
        {
            RifleScript.EnableHUD();
        }

    }

    void ActivatePistol()
    {
        PistolScript.ActivatePistol();
        ShotgunScript.DisableShotgun();
        RifleScript.DisableRifle();
        PistolScript.Draw();
    }

    void ActivateShotgun()
    {
        ShotgunScript.ActivateShotgun();
        PistolScript.DisablePistol();
        RifleScript.DisableRifle();
        ShotgunScript.Draw();
    }

    void ActivateRifle()
    {
        ShotgunScript.DisableShotgun();
        PistolScript.DisablePistol();
        RifleScript.ActivateRifle();
        RifleScript.Draw();
    }
}

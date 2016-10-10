using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class crosshairController : MonoBehaviour 
{
    float Accuracy;

    public GameObject CrossHairUp;
    public GameObject CrossHairDown;
    public GameObject CrossHairLeft;
    public GameObject CrossHairRight;

    Vector3 CrosshairUpStart;
    Vector3 CrosshairDownStart;
    Vector3 CrosshairLeftStart;
    Vector3 CrosshairRightStart;

    WarningScreenNoise EnemyProx;

	// Use this for initialization
	void Start () 
    {
        CrosshairUpStart =  CrossHairUp.transform.position;
        CrosshairDownStart =  CrossHairDown.transform.position;
        CrosshairLeftStart =  CrossHairLeft.transform.position;
        CrosshairRightStart =  CrossHairRight.transform.position;

        EnemyProx = FindObjectOfType(typeof(WarningScreenNoise)) as WarningScreenNoise;
	}
	
	// Update is called once per frame
	void Update () 
    {
        ProcessAccuracy();
        EnemyProximity();
	}

    public void SetAccuracy(float _Accuracy)
    {
        Accuracy = _Accuracy;
    }

    void ProcessAccuracy()
    {
        Vector3 NewUpPos;
        NewUpPos = CrossHairUp.transform.position;
        NewUpPos.y = CrosshairUpStart.y + Accuracy * 5 - 5;
        CrossHairUp.transform.position = NewUpPos;

        Vector3 NewDownPos;
        NewDownPos = CrossHairDown.transform.position;
        NewDownPos.y = CrosshairDownStart.y - Accuracy * 5 + 5;
        CrossHairDown.transform.position = NewDownPos;

        Vector3 NewLeftPos;
        NewLeftPos = CrossHairLeft.transform.position;
        NewLeftPos.x = CrosshairLeftStart.x - Accuracy * 5 + 5;
        CrossHairLeft.transform.position = NewLeftPos;

        Vector3 NewRightPos;
        NewRightPos = CrossHairRight.transform.position;
        NewRightPos.x = CrosshairRightStart.x + Accuracy * 5 - 5;
        CrossHairRight.transform.position = NewRightPos;

    }

    void EnemyProximity()
    {
        if(EnemyProx.EnenmiesInCollider)
        {
            CrossHairUp.GetComponent<Image>().color = Color.Lerp(CrossHairUp.GetComponent<Image>().color,Color.red, Time.deltaTime * 2.0f);
            
            CrossHairDown.GetComponent<Image>().color = Color.Lerp(CrossHairDown.GetComponent<Image>().color,Color.red, Time.deltaTime * 2.0f);
            
            CrossHairLeft.GetComponent<Image>().color = Color.Lerp(CrossHairLeft.GetComponent<Image>().color,Color.red, Time.deltaTime * 2.0f);
            
            CrossHairRight.GetComponent<Image>().color = Color.Lerp(CrossHairRight.GetComponent<Image>().color,Color.red, Time.deltaTime * 2.0f);
        }
        else
        {
            CrossHairUp.GetComponent<Image>().color = Color.Lerp(CrossHairUp.GetComponent<Image>().color,Color.white, Time.deltaTime * 1.0f);
            
            CrossHairDown.GetComponent<Image>().color = Color.Lerp(CrossHairDown.GetComponent<Image>().color,Color.white, Time.deltaTime * 1.0f);
            
            CrossHairLeft.GetComponent<Image>().color = Color.Lerp(CrossHairLeft.GetComponent<Image>().color,Color.white, Time.deltaTime * 1.0f);
            
            CrossHairRight.GetComponent<Image>().color = Color.Lerp(CrossHairRight.GetComponent<Image>().color,Color.white, Time.deltaTime * 1.0f);
        }
    }
}

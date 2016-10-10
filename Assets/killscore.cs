using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class killscore : MonoBehaviour 
{
    public Text KillScore;
    public GameObject KillScoreObj;
    ScoringRules scorer;

    float LifeTimer = 2.0f;

    bool FirstRun = true;

    Vector3 Pos;

    float GameTime = 0.0f;

	// Use this for initialization
	void Start () 
    {
        scorer = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;

        Pos =  KillScoreObj.transform.position;
        Pos.y += 100;
	}
	
	// Update is called once per frame
	void Update () 
    {
        GameTime += Time.deltaTime;

	   if(FirstRun)
        {
            int Multiplyer = scorer.getMultiplyer () + 1;
            KillScore.text = "" + Multiplyer * 10.0f;
        }

        Vector3 NewPos;
        NewPos = KillScoreObj.transform.position;
        NewPos.y = Pos.y + GameTime * (150.0f - GameTime * 15.0f);
        KillScoreObj.transform.position = NewPos;

        Color NewCol = Color.white;
        NewCol.a = 0.0f;

        KillScore.color = Color.Lerp(KillScore.color,NewCol, Time.deltaTime * 1.0f);

        if(GameTime > 5.0f)
        {
            DestroyObject(gameObject);
        }
	}
}

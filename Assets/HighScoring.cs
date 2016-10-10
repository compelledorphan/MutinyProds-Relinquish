using UnityEngine;
using System.Collections;
using SentientBytes.Perlib;
using StructsAndEnums;
using System.Collections.Generic;

public class HighScoring : MonoBehaviour {

	public int MaxHighScores;

	List<Highscore> Highscores = new List<Highscore>();

	void Awake()
	{
		LoadHighScores();
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void SaveHighscores(Highscore _ScoreStruct)
    {
        if(PlayerPrefs.HasKey("Score"))
        {
            float CurrentHighScore;      
            bool Success = float.TryParse(PlayerPrefs.GetString("Score"), out CurrentHighScore);

            Debug.Log("CHS" + CurrentHighScore.ToString());

            if(Success)
            {
                if(CurrentHighScore >= _ScoreStruct.Score)
                {
                    PlayerPrefs.SetString("Score", CurrentHighScore.ToString());
                }
                else
                {
                    PlayerPrefs.SetString("Score", _ScoreStruct.Score.ToString());
                }
            }
            else
            {
                Debug.Break();
            }

        }
        else
        {
            PlayerPrefs.SetString("Score", _ScoreStruct.Score.ToString());
        }

        #region Old Save
        //Highscores.Add(_ScoreStruct);

        //Highscores.Sort((s1, s2) => s1.Score.CompareTo(s2.Score));

        //if(Highscores.Count > MaxHighScores)
        //{
        //    Highscores.RemoveAt(Highscores.Count - 1);
        //}
        //else
        //{
        //    // Do nothing
        //}

        //for(int i = 1; i <= Highscores.Count; i++)
        //{
        //    string SCORENAME = "ScoreName" + i.ToString();
        //    string SCOREVALUE = Highscores[i - 1].Score.ToString();

        //    Debug.Log(SCOREVALUE);  // Outputting zero?!
        //    //Debug.Break();

        //    PlayerPrefs.SetString(SCORENAME, System.Environment.UserName);
        //    PlayerPrefs.SetString("ScoreValue" + i.ToString(), SCOREVALUE);
        //    //Debug.Log("ScoreValue" + i.ToString());
        //}
        #endregion
    }

    void LoadHighScores()
	{
        Highscore temp;
        string NameKey;
        string ScoreKey;

        for(int i = 0; i < 10; i++)
        {
            NameKey = "ScoreName" + (i + 1).ToString();
            ScoreKey = "ScoreValue" + (i + 1).ToString();
            //Debug.Log(ScoreKey);
            temp.Name = PlayerPrefs.GetString(NameKey);
            //temp.Score = float.Parse(PlayerPrefs.GetString(ScoreKey));
            float.TryParse(PlayerPrefs.GetString("Score"), out temp.Score);
           //Debug.Log(temp.Score);

            temp.EnemiesKilled = 0;
            temp.GameMode = "1";
            temp.GameTime = 0.0f;
            temp.Multiplier = 0;

            Highscores.Add(temp);
        }
	}

	public List<Highscore> GetHighscores()
	{
		return(Highscores);
	}
}

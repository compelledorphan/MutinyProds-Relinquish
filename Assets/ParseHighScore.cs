using UnityEngine;
using System.Collections;
using SentientBytes.Perlib;
using StructsAndEnums;
using System.Collections.Generic;
using UnityEngine.UI;

public class ParseHighScore : MonoBehaviour {

	static Perlib savefile;
	List<Highscore> Highscores = new List<Highscore>();

	public bool IsNameBox = false;
	public bool IsScoreBox = false;

	public int PositionNumber;

	public Text Textbox;

    string NameKey = "ScoreName";
    string ScoreKey = "ScoreValue";
	
	void Awake()
	{
        NameKey += PositionNumber.ToString();
        ScoreKey += PositionNumber.ToString();

		Load ();
	}

	// Use this for initialization
	void Start () 
	{
		Parse ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void Load()
	{
        Parse();
	}

	void Parse()
    {
        if(PlayerPrefs.HasKey("Score"))
        {
            Textbox.text = PlayerPrefs.GetString("Score");
        }
        else
        {
            Textbox.text = "42";
        }

        #region Old Parse
        /*
        //if(IsNameBox == true)
        //{
        //    if(PlayerPrefs.HasKey(NameKey))
        //    {
        //        Textbox.text = PlayerPrefs.GetString(NameKey);
        //    }
        //    else
        //    {
        //        Textbox.text = "Liam";
        //    }
        //}
        //else
        //{
        //    if(PlayerPrefs.HasKey(ScoreKey))
        //    {
        //        Textbox.text = (PlayerPrefs.GetString(ScoreKey));
        //    }
        //    else
        //    {
        //        Textbox.text = "77";
        //    }
        //}
         */
        #endregion
    }	
}
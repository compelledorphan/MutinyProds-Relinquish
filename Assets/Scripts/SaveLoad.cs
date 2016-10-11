using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SentientBytes.Perlib;

#pragma warning disable 219, 649

public class SaveLoad : MonoBehaviour
{
	static Perlib LocalHighscores;

	void Awake()
	{
        string SavePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        Debug.Log(SavePath);
		if (LocalHighscores == null)
		{
            LocalHighscores = new Perlib("Highscores.ffs");
			//LocalHighscores = new Perlib("LocalHighscores.argh", SavePath, "wearepirates", "MutinyProductions");
		}
        
		Load();
	}

	public void Save(List<float> _ScoreList)
	{
		LocalHighscores.SetValue("LocalScoreList", _ScoreList);
		LocalHighscores.Save();
	}

	public List<float> Load()
	{
		List<float> Highscorelist = new List<float>();

		LocalHighscores.GetValue("LocalScoreList", Highscorelist);

		return(Highscorelist);
	}
}

#pragma warning restore 219, 649
﻿using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel(string LevelName)
	{
		Application.LoadLevel(LevelName);
	}

	public void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Quit()
	{
		Application.Quit ();
		//UnityEditor.EditorApplication.isPlaying = false;
	}
}

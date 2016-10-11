using UnityEngine;
using System.Collections;

public class GameEngineRules : MonoBehaviour 
{

    bool ToolTipsToggle;

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Application.targetFrameRate = 60; 

        ToolTipsToggle = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}

    public void SetToolTips(bool _ToolTipsToggle)
    {
        ToolTipsToggle = _ToolTipsToggle;
    }
}

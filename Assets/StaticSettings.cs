using UnityEngine;
using System.Collections;

public class StaticSettings : MonoBehaviour {

	public static bool ToolTips;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public bool getToolTips()
	{
		return(ToolTips);
	}

	public void setToolTips(bool _ToolTips)
	{
		ToolTips = _ToolTips;
	}
	
}

using UnityEngine;
using System.Collections;

public class RandLoadingTip : MonoBehaviour 
{
	public GameObject LoadTip1;
	public GameObject LoadTip2;
	public GameObject LoadTip3;
	public GameObject LoadTip4;
	public GameObject LoadTip5;
	
	// Use this for initialization
	void Start () 
	{
		LoadTip1.SetActive(false);
		LoadTip2.SetActive(false);
		LoadTip3.SetActive(false);
		LoadTip4.SetActive(false);
		LoadTip5.SetActive(false);
		
		int Selected;
		Selected = Random.Range (1,6);
		
		switch(Selected)
		{
		case 1:
		{
			LoadTip1.SetActive(true);
		}
			break;
		case 2:
		{
			LoadTip2.SetActive(true);
		}
			break;
		case 3:
		{
			LoadTip3.SetActive(true);
		}
			break;
		case 4:
		{
			LoadTip4.SetActive(true);
		}
			break;
		case 5:
		{
			LoadTip5.SetActive(true);
		}
			break;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}

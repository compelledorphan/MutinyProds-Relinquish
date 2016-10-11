using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManagement : MonoBehaviour {
	
	public int MaxNumberOfBullets = 100;
	List<GameObject> BulletList = new List<GameObject>();
	public float FadeInterval = 0.1f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void ManageBullets()
	{
		//	while(BulletList[0].GetComponent<Renderer>().material.color.a != 0.0f)
		//	{
		//		BulletList[0].GetComponent<Renderer>().material.color.a -= 0.1f;
		//		yield WaitForSeconds(FadeInterval);
		//	}
		if(BulletList.Count > MaxNumberOfBullets)
		{
			GameObject tempObj = BulletList[0];
			BulletList.RemoveAt(0);
			Destroy (tempObj);
		}
	}
	
	public void AddBullet(GameObject _bullet)
	{
		BulletList.Add(_bullet);
		ManageBullets();
	}
}

//foreach (Transform child in transform) 
//{
//	children.Add(child.gameObject);
//}
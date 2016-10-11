using UnityEngine;
using System.Collections;

public class KillAllSpawnerCoroutines : MonoBehaviour {

    Component[] Spawners;

	// Use this for initialization
	void Start () 
    {
        Spawners = GetComponentsInChildren<SpawnerShell>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void KillAll()
    {
        for(int i = 0; i < Spawners.Length; i++)
        {
            SpawnerShell tempSpawnershell;

            tempSpawnershell = Spawners[i] as SpawnerShell;
            tempSpawnershell.KillCoroutine();
        }
    }
}

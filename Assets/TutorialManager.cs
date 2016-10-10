using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
    
    public bool DoorsLocked;
    
    // Use this for initialization
    void Start () 
    {
        DoorsLocked = true;
    }
    
    // Update is called once per frame
    void Update () 
    {
        
    }
    
    public void UnlockDoors()
    {
        DoorsLocked = false;
    }

    public bool CheckLock()
    {
        return(DoorsLocked);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RagdollManager : MonoBehaviour 
{
    public int MaxRagdolls = 10;
    List<GameObject> RagdollList = new List<GameObject>();
    public float FadeInterval = 0.1f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ManageRagdolls()
    {
        if (RagdollList.Count > MaxRagdolls)
        {
            GameObject tempObj = RagdollList[0];
            RagdollList.RemoveAt(0);
            Destroy(tempObj);
        }
    }

    public void AddRagdolls(GameObject _Ragdoll)
    {
        bool Hold = false;
        SpecialRagdollVariables tempScript = _Ragdoll.GetComponent<SpecialRagdollVariables>();
        if(tempScript != null)
        {
            Hold = false;
        }
        else
        {
            Hold = true;
        }

        if (Hold)
        {
            RagdollList.Add(_Ragdoll);
            ManageRagdolls();
        }
        else
        {
            Destroy(_Ragdoll);
        }
    }
}
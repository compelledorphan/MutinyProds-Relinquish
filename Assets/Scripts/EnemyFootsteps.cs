using UnityEngine;
using System.Collections;

public class EnemyFootsteps : MonoBehaviour 
{
    public AudioClip[] Footsteps;
    public AudioSource FootstepPlayer;

    int CurrentClip = 0;

    Vector3 OldPosition;

	// Use this for initialization
	void Start () 
    {
        FootstepPlayer.clip = Footsteps[CurrentClip];
        OldPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(OldPosition != this.transform.position)
        {
            if(FootstepPlayer.isPlaying != true)
            {
                CurrentClip++;
                if(CurrentClip == Footsteps.Length)
                {
                    CurrentClip = 0;
                }

                FootstepPlayer.clip = Footsteps[CurrentClip];
                FootstepPlayer.Play();
            }
            else
            {
                // Do nothing
            }
        }
        else
        {
            FootstepPlayer.Stop();
        }
	
	}
}

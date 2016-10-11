using UnityEngine;
using System.Collections;
using TOZ.ImageEffects;
using System.Collections.Generic;

public class WarningScreenNoise : MonoBehaviour {

    PP_Noise Noise;
    AudioSource Static;
    List<Collider> InTriggerZone = new List<Collider>();
    [HideInInspector]
    public bool EnenmiesInCollider = false;
    GameObject ClosestEnemy = null;
    float SphereRadius;

	// Use this for initialization
	void Start () 
    {
        Noise = GetComponent<PP_Noise>() as PP_Noise;

        Static = GetComponent<AudioSource>() as AudioSource;

        SphereCollider sphere = GetComponent<SphereCollider>() as SphereCollider;

        if (sphere != null)
        {
            SphereRadius = sphere.radius;
        }
     }
	
	// Update is called once per frame
	void Update () 
    {
        if (InTriggerZone.Count != 0)
        {
            Noise.enabled = true;
            EnenmiesInCollider = true;
        }
        else
        {
            Noise.enabled = false;
            EnenmiesInCollider = false;
        }

        SetEffectAmount();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "SpecialEnemy")
            {
                if (!InTriggerZone.Contains(other))
                {
                    InTriggerZone.Add(other);
                    FindClosestEnemy();
                }
                else
                {
                    // Do nothing
                }
            }
            else
            {
                // Do nothing
            }
        }
        else
        {
            // Do nothing
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(InTriggerZone.Contains(other))
        {
            InTriggerZone.Remove(other);
            ClosestEnemy = null;
            FindClosestEnemy();
        }
        else
        {
            // Do nothing
        }
    }

    public void RemoveDeadEnemy(Collider _Enemy)
    {
        if (InTriggerZone.Contains(_Enemy))
        {
            InTriggerZone.Remove(_Enemy);
        }
        else
        {
            // Do nothing
        }
    }

    public void EnemyKilled()
    {
        SphereCollider sphere = GetComponent<SphereCollider>() as SphereCollider;

        if(sphere != null)
        {
            if(sphere.radius == SphereRadius)
            {
                sphere.radius = 0.1f;
                InTriggerZone.Clear();
                sphere.radius = SphereRadius;
                ClosestEnemy = null;
                FindClosestEnemy();
            }
            else
            {
                // Do nothing
            }
        }
        else
        {
            // Do nothing
        }
    }

    void FindClosestEnemy()
    {
        if (InTriggerZone.Count != 0 || InTriggerZone != null)
        {
            foreach (Collider enemy in InTriggerZone)
            {
                if (ClosestEnemy == null)
                {
                    ClosestEnemy = enemy.gameObject;
                }
                else
                {
                    if (Mathf.Abs(Vector3.Distance(enemy.gameObject.transform.position, this.transform.position)) >= Mathf.Abs(Vector3.Distance(ClosestEnemy.transform.position, this.transform.position)))
                    {
                        ClosestEnemy = enemy.gameObject;
                    }
                    else
                    {
                        // Do nothing
                    }
                }
            }
        }
    }

    void SetEffectAmount()
    {
        if (ClosestEnemy != null)
        {
            float EffectAmount = (Mathf.Abs(Vector3.Distance(ClosestEnemy.transform.position, this.transform.position)) / SphereRadius);
			EffectAmount = - EffectAmount;
			EffectAmount += 1;
            Noise.noiseScale = EffectAmount;
            Static.volume = EffectAmount;
        }
        else
        {
            Noise.noiseScale = 0.0f;
            Static.volume = 0.0f;
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerCollisionRules : MonoBehaviour {

	ScoringRules scorer;
    bool IsDead = false;

	// Use this for initialization
	void Start () {
		scorer = FindObjectOfType(typeof(ScoringRules)) as ScoringRules;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "MainCamera")
        {
            if (other.gameObject.tag == "Enemy" && IsDead == false)
            {
                IsDead = true;
                Debug.Log("Collide");

                RaycastHit hit;
                Vector3 RayStart = other.transform.position;
                Vector3 RayDirection = (this.transform.position - other.transform.position).normalized;
                //Vector3 RayDirection = other.transform.LookAt(this.transform.position);

                Ray ray = new Ray(RayStart, RayDirection);

                if (Physics.Raycast(ray, out hit, 5.0f) && (hit.collider.gameObject.tag == "Player"))
                {
                    //Destroy(GameObject.FindGameObjectWithTag("EManager"));
                    scorer.AnnouceDeath();
                    Debug.Log("Collide with player confirmed");
                }
                else
                {
                    // Do nothing
                }
            }
        }
        else
        {
            // Do nothing
        }

        if (other.gameObject.tag == "SpecialEnemy" && IsDead == false)
        {
            IsDead = true;
            scorer.AnnouceDeath();
            Debug.Log("Collide");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeManager : MonoBehaviour {

    public Camera MettleCam;
    public float range;
    public LayerMask thisLayerMask;
    public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire3")) {
            Debug.Log("Fire");
            Attack(); 
        }
	}

    void Attack() {
        RaycastHit hit;
        if (Physics.Raycast(MettleCam.transform.position, MettleCam.transform.forward, out hit, range, thisLayerMask)) {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(damage);
                Debug.Log("Hit");
            }
           

        }
    }
}

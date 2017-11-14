using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeManager : MonoBehaviour {

    public Camera MettleCam;
    public float range;
    public LayerMask thisLayerMask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2")) {
            Attack(); 
        }
	}

    void Attack() {
        RaycastHit hit;
        if (Physics.Raycast(MettleCam.transform.position, MettleCam.transform.forward, out hit, range, thisLayerMask)) {

            Debug.Log(hit.transform.name);

        }
    }
}

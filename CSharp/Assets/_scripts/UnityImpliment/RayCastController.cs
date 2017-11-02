using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastController : MonoBehaviour {

    public Camera rayCamera;
    public GameObject prefab;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(2)) {
            InvokeRepeating("FireRayCast", 0.05f, 0.05f);
        }

        if (Input.GetMouseButtonUp(2)) {
            CancelInvoke("FireRayCast");
        }
       
    }

    void FireRayCast() {
        Ray castRay = rayCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hitDetect = Physics.Raycast(castRay.origin, castRay.direction, out hit);
        if (hitDetect == true) {

            GameObject currentInstance = (GameObject)Instantiate(prefab, hit.point, Quaternion.identity);
            Destroy(currentInstance, 0.1f);
        }
    }
}

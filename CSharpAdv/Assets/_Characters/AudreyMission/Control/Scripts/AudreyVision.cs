using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudreyVision : MonoBehaviour {

    private Camera AudEyes;
    private float defaultFOV;
    private Animator thisAnimator;

	// Use this for initialization
	void Start () {
        AudEyes = GetComponent<Camera>();
        thisAnimator = GetComponent<Animator>();
        defaultFOV = AudEyes.fieldOfView;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("zoom")){
            AudEyes.fieldOfView = defaultFOV /1.5f;
        }  else {
            AudEyes.fieldOfView = defaultFOV;
        }

    }
}

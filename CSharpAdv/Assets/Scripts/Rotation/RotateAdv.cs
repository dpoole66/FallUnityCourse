using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAdv : MonoBehaviour {
    private Transform ThisTransform = null;
    public float RotSpeed = 90.0f;
    public float damping = 50.0f;

    public Transform Target = null;

	// Use this for initialization
	void Awake () {
        ThisTransform = this.GetComponent<Transform>();	
	}
	
	// Update is called once per frame
	void Update () {

            AutoRotDamp();

        if (Input.GetMouseButton(1)) {
            RightTurn();
        }
        
        if (Input.GetMouseButton(0)) {
            LeftTurn();
        }
    }

    //Right Turn
    void RightTurn() {
        ThisTransform.rotation *= Quaternion.AngleAxis(RotSpeed * Time.deltaTime, Vector3.up);
        //ThisTransform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    }

    //Left Turn
    void LeftTurn() {
        ThisTransform.rotation *= Quaternion.AngleAxis(RotSpeed * Time.deltaTime * -1, Vector3.up);
        //ThisTransform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    }

    //Auto Rotate
    void AutoRot() {

        Quaternion DestRot = Quaternion.LookRotation(Target.position - ThisTransform.position, Vector3.up);
        ThisTransform.rotation = Quaternion.RotateTowards(ThisTransform.rotation, DestRot, RotSpeed * Time.deltaTime);
        
        //ThisTransform.rotation = Quaternion.LookRotation(Target.position - ThisTransform.position, Vector3.up);

    }

    // Auto Rotate with Dampening
    void AutoRotDamp() {
        Quaternion DestRot = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);
        Quaternion smoothRot = Quaternion.Slerp(transform.rotation, DestRot, 1.0f - (Time.deltaTime * damping));
        transform.rotation = smoothRot;
    }
}

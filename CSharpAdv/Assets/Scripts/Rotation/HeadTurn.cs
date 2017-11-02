using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTurn : MonoBehaviour {

    private Transform ThisTransform = null;
    public float RotSpeed = 90.0f;
    public float damping = 50.0f;
    public Transform Target;


    // Use this for initialization
    void Awake() {
        ThisTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate() {
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
}

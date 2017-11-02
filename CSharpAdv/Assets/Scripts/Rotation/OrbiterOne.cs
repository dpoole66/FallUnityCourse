using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiterOne : MonoBehaviour {
    //-----------------------------------------------
    public Transform Pivot = null;
    private Transform ThisTransform = null;
    private Quaternion DestRot = Quaternion.identity;
    //-----------------------------------------------
    public float PivotDistance = 8.0f;
    public float RotSpeed = 10.0f;
    private float RotX = 0.0f;
    private float RotY = 0.0f;
    //-----------------------------------------------
    void Awake() {
        ThisTransform = GetComponent<Transform>();
    }
    //-----------------------------------------------
    //-----------------------------------------------
    void Update () {
        float Horz = Input.GetAxis("Horizontal");
        float Vert = Input.GetAxis("Vertical");

        RotX += Vert * Time.deltaTime * RotSpeed;
        RotY += Horz * Time.deltaTime * RotSpeed;

        Quaternion YRot = Quaternion.Euler(0f, RotY, 0f);
        DestRot = YRot * Quaternion.Euler(RotX, 0f, 0f);

        ThisTransform.rotation = DestRot;

        ThisTransform.position = Pivot.position + 
            ThisTransform.rotation * Vector3.forward * -PivotDistance;
	}
    //----------------------------------------------
}

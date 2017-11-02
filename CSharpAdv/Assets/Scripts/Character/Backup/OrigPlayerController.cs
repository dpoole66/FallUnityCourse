using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrigPlayerController : MonoBehaviour {
    //-----------------------------------------------
    public float MaxSpeed = 10.0f;
    public float RotSpeed = 150.0f;
    private Transform ThisTransform = null;
    private CharacterController ThisController = null;
    private Transform ThisCharCam = null;
    //-----------------------------------------------

    //-----------------------------------------------
    void Awake() {
        ThisTransform = GetComponent<Transform>();
        ThisController = GetComponent<CharacterController>();
        ThisCharCam = GetComponent<Transform>();

    }
    //-----------------------------------------------

    //-----------------------------------------------
    void FixedUpdate() {
        float Horz = Input.GetAxis("Horizontal");
        float Vert = Input.GetAxis("Vertical");
        //-------------------------------------------
        ThisTransform.rotation *= Quaternion.Euler(0.0f, RotSpeed * Horz * Time.deltaTime, 0.0f);
        //-------------------------------------------
        ThisController.SimpleMove(ThisTransform.forward * MaxSpeed * Vert);
        //ThisTransform.position += ThisTransform.forward * MaxSpeed * Vert * Time.deltaTime;
        //ThisCharCam.rotation = *= Quaternion.LookRotation(Target.position - ThisTransform.position, Vector3.up);
        //-------------------------------------------   
    }

    // Looking at items of interest
    public void LookAtMe(Transform Target) {


        Quaternion DestRot = Quaternion.LookRotation(Target.position - ThisTransform.position, Vector3.up);
        ThisCharCam.rotation = Quaternion.RotateTowards(ThisTransform.rotation, DestRot, RotSpeed * Time.deltaTime);




        //ThisCharCam.rotation *= Quaternion.LookRotation(Target.position - ThisTransform.position, Vector3.up);
        //C.enabled = false;
        //-----------------------------------------------
    }
}

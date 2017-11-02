using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon2 : MonoBehaviour {
    //-----------------------------------------------
    //-----------------------------------------------
    public float maxSpeed = 10.0f;
    public float rotSpeed = 150.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    //-----------------------------------------------
    private Transform ThisTransform = null;
    private CharacterController ThisController = null;
    //-----------------------------------------------
    private Vector3 moveDirection = Vector3.zero;
    //-----------------------------------------------
    //-----------------------------------------------


    //-----------------------------------------------
    //-----------------------------------------------
    void Awake() {
        ThisTransform = GetComponent<Transform>();
        ThisController = GetComponent<CharacterController>();
    }
    //-----------------------------------------------
    //-----------------------------------------------

    //-----------------------------------------------
    //-----------------------------------------------
    void FixedUpdate() {

        float Horz = Input.GetAxis("Horizontal");
        float Vert = Input.GetAxis("Vertical");

        //-------------------------------------------
        ThisTransform.rotation *= Quaternion.Euler(0.0f, rotSpeed * Horz * Time.deltaTime, 0.0f);
        //-------------------------------------------

        //-------------------------------------------
        moveDirection = new Vector3(0.0f, 0.0f, Vert);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= maxSpeed;

        ThisController.Move(moveDirection * Time.deltaTime);
        //-------------------------------------------

    }
    //-----------------------------------------------
    //-----------------------------------------------
}

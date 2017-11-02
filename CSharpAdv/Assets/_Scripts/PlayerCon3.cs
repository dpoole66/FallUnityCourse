using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerCon3 : MonoBehaviour {
    //-----------------------------------------------
    //-----------------------------------------------
    public float maxSpeed = 10.0f;
    public float rotSpeed = 150.0f;
    //-----------------------------------------------
    public float jumpForce = 80.0f;
    public float gravity = 20.0f;
    public float GroundedDist = 0.1f;
    public bool IsGrounded = false;
    public LayerMask GroundLayer;
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

        // Rotate
        ThisTransform.rotation *= Quaternion.Euler(0.0f, rotSpeed * Horz * Time.deltaTime, 0.0f);

        // Move
        ThisController.Move(ThisTransform.TransformDirection(moveDirection) * Time.deltaTime);

        // Jump 
        moveDirection.z = Vert * maxSpeed;
        // jumping: check IsGrounded
        IsGrounded = (DistanceToGround() < GroundedDist) ? true : false;
        // jumping final check
        if (Input.GetAxisRaw("Jump") != 0 && IsGrounded) 
            moveDirection.y = jumpForce;
        // apply grav
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        //-------------------------------------------
    }

    public float DistanceToGround() {
        RaycastHit hit;
        float distanceToGround = 0;
        if (Physics.Raycast(ThisTransform.position, -Vector3.up, out hit, Mathf.Infinity, GroundLayer))
            distanceToGround = hit.distance;
        return distanceToGround;

    }
    //-----------------------------------------------
    //-----------------------------------------------
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float velocity, horizontalVelocity;
    public GameObject target;
    public Light playerSpot;
    public Camera mainCamera;
    public Camera playerCamera;
    public float rotDegPerSec = 15.0f;
    public float rotDegAmount = 360.0f;
    private float totalRot = 0;


    void Start() {
        playerSpot.enabled = false;
        mainCamera.enabled = true;
        playerCamera.enabled = false;
    }

    void Update() {
        float xMovement = Input.GetAxis("Mouse X");

        // Rotation
        //if (xMovement != 0) {
        //    transform.eulerAngles += Vector3.up * xMovement * Time.deltaTime * horizontalVelocity;
        //}

        // Transform FOWARD/BACKWARD
        if (Input.GetKeyDown(KeyCode.W)) {
            InvokeRepeating("Forward", 0.05f, 0.05f);  
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            CancelInvoke("Forward");
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            InvokeRepeating("Backward", 0.05f, 0.05f);
        }

        if (Input.GetKeyUp(KeyCode.S)) {
            CancelInvoke("Backward");
        }


        // Transorm UP Key D
        if (Input.GetKeyDown(KeyCode.D)) {
            InvokeRepeating("Up", 0.05f, 0.05f);
        }

        if (Input.GetKeyUp(KeyCode.D)) {
            CancelInvoke("Up");
        }
        // Down Key A
        if (Input.GetKeyDown(KeyCode.A)) {
            InvokeRepeating("Down", 0.05f, 0.05f); 
        }

        if (Input.GetKeyUp(KeyCode.A)) {
            CancelInvoke("Down");
        }

        // Rotate
        
        if (Input.GetMouseButton(0)) {
            //if (Mathf.Abs(totalRot) < Mathf.Abs(rotDegAmount))
                RotateL();
            
        }

  
        if (Input.GetMouseButton(1)) {
            //float rotation = Time.deltaTime * 200;
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.right), Time.deltaTime);
            RotateR();
        }
    }


    // Transform Controllers:

    public void Forward() {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    public void Backward() {
        transform.position -= transform.forward * velocity * Time.deltaTime;
    }

   void Up() {
        transform.position += Vector3.up * velocity * Time.deltaTime;
    }

    void Down() {
        transform.position -= Vector3.up * velocity * Time.deltaTime;
    }

    // Rotation Control
    public void RotateR() {
        //float rotation = Time.deltaTime * 200;
        //transform.Rotate(0, rotation, 0, Space.World);
        float currentAngle = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.AngleAxis(currentAngle + (Time.deltaTime * rotDegPerSec), Vector3.up);
        totalRot += Time.deltaTime * rotDegPerSec;
    }

    public void RotateL() {
        float currentAngle = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.AngleAxis(currentAngle + (Time.deltaTime * rotDegPerSec * -1), Vector3.up);
        totalRot += Time.deltaTime * rotDegPerSec;
    }


    // Lights and Camera
    public void PlayerSpot() {
        playerSpot.enabled = !playerSpot.enabled;
    }

    public void PlayerCamera() {
        playerCamera.enabled = !playerCamera.enabled;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public AudioClip Boing;
    private Paddle paddle;
    private bool hasStarted = false;
    private Vector3 paddleToBallVector;
    private Rigidbody2D ballRigidBody2D;

    // init with ball and paddle offset 
    void Start() {
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
        ballRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()   {
        if (!hasStarted)    { 
            this.transform.position = paddle.transform.position + paddleToBallVector;

            if (Input.GetMouseButtonDown(0))    {
                hasStarted = true;
                this.ballRigidBody2D.velocity = new Vector2(1f, 15f);
            }
        }

        

        

    }

    void OnCollisionEnter2D(Collision2D collision) {
            
        AudioSource.PlayClipAtPoint(Boing, transform.position, 1.8f);
    }
}



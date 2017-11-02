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

                // Rigidbody2D differes from course "this.rigidbody2d.velocity" is obsolete.
                hasStarted = true;
                this.ballRigidBody2D.velocity = new Vector2(1f, 15f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        Vector2 tweak = new Vector2 (Random.Range(-0.6f, 0.4f), Random.Range(0.7f, 0.13f));
        
        if (hasStarted) {
            AudioSource.PlayClipAtPoint(Boing, transform.position);

            ballRigidBody2D.velocity += tweak;
        }
    }
}

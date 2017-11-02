using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballextra : MonoBehaviour {

    public Ball ball;
    public Paddle paddle;
    private Vector3 paddlePos;
    private Vector3 ballPos;
    private Vector3 paddleToBallVector;

    // Use this for initialization
    void Start () {

        ballPos = ball.transform.position;

        paddlePos = paddle.transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        paddleToBallVector = ballPos - paddlePos;

        ballPos = paddlePos + paddleToBallVector;

        Debug.Log(paddleToBallVector);
    }

}

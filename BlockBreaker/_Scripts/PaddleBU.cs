using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    public bool autoPlay = false;
    private Ball ball;

    float mousePosInBlocks;

   void Start()    {
        ball = GameObject.FindObjectOfType<Ball>();
        
    }

    void Update()   {
        if (!autoPlay) {
            MoveWithMouse();
        }
        else{
            AutoPlay();
        }
    }


    /// <summary>
    /// Auto Play setup for testing. 
    /// </summary>
    void AutoPlay() {
        Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);
        Vector3 ballPos = ball.transform.position;
        
        
        // mouse/blocks ratio
        mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;

        // Move Paddle to the balls position using the new "ballPos" vector
        paddlePos.x = Mathf.Clamp(ballPos.x, 0.5f, 15.5f);

        this.transform.position = paddlePos;
    }

    /// <summary>
    /// Standard Gameplay Mouse/Paddle config. 
    /// </summary>

    void MoveWithMouse(){
        Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);

        mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        //Debug.Log(mousePosInBlocks);

        paddlePos.x = Mathf.Clamp(mousePosInBlocks, 0.5f, 15.5f);

        this.transform.position = paddlePos;
    }
}

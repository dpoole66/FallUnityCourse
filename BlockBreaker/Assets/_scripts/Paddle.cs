using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    public bool autoPlay = false;
    public AudioClip paddle;
    private Ball ball;
    public float force;
    public Rigidbody2D rb;
    public float minX;
    public float maxX;

    float mousePosInBlocks;

   void Start()    {
        ball = GameObject.FindObjectOfType<Ball>();
        
    }

    void Update()   {
        if (!autoPlay) {
            MoveWithMouse();
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
        paddlePos.x = Mathf.Clamp(ballPos.x, minX, maxX);

        this.transform.position = paddlePos;
    }

    /// <summary>
    /// Standard Gameplay Mouse/Paddle config. 
    /// </summary>

    void MoveWithMouse(){
        Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);

        mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        //Debug.Log(mousePosInBlocks);

        paddlePos.x = Mathf.Clamp(mousePosInBlocks, minX, maxX);

        this.transform.position = paddlePos;
    }


    //void OnCollisionEnter2D(Collision2D collision)  {
    // AudioSource.PlayClipAtPoint(paddle, transform.position, 0.8f);
    // }

    void OnMouseDown()  {
        AudioSource.PlayClipAtPoint(paddle, transform.position, 0.8f);

        Vector3 paddlePosHit = new Vector3(0.5f, this.transform.position.y, 0f);
        Vector3 hitDir = paddlePosHit = transform.position;
    
        rb.AddForce(hitDir * force);
        Debug.Log("Slap");
    }

    

}

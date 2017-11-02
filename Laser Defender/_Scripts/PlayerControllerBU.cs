using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour   {
    static MusicPlayer instance = null;
    public float speed = 5.0f;
    public GameObject enemyPrefab;
    float xMin;
    float xMax;

    void Start(){
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0.03f, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(0.97f, 0, distance));
        xMin = leftMost.x;
        xMax = rightMost.x;
    }

	void Update(){
		if (Input.GetKey (KeyCode.LeftArrow)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
		else if (Input.GetKey(KeyCode.RightArrow)){
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        Debug.Log(transform.position);
    }

}




using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float velocity, horizontalVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float xMovement = Input.GetAxis ("Mouse X");

		if (xMovement != 0)
		{
			transform.eulerAngles += transform.up * xMovement * Time.deltaTime * horizontalVelocity;
		}

		if (Input.GetKey (KeyCode.W))
		{
			// execute some code while W key is being pressed
			transform.position += transform.forward * velocity * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S))
		{
			transform.position -= transform.forward * velocity * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A))
		{
			transform.position -= transform.right * velocity * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D))
		{
			transform.position += transform.right * velocity * Time.deltaTime;
		}
	}
}

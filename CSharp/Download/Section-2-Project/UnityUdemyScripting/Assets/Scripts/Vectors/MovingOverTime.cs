using UnityEngine;
using System.Collections;

public class MovingOverTime : MonoBehaviour {

	//public Vector3 a, b;
	public Vector3 target;
	public float duration;

	void Start () {
		//a = new Vector3 (3, 1, -5);
		StartCoroutine (MoveTo (target, duration));
	}

	// Update is called once per frame
	void Update () {
		//DotProduct (a, b);
	}

	public IEnumerator MoveTo (Vector3 target, float duration)
	{
		//Get the oriented segment
		Vector3 diff = (target - transform.position);
		//Initialize a counter that will keep track of time
		float counter = 0;
		while (counter <= duration) // While the counter hasn't reach the duration
		{
			float step = (diff.magnitude * Time.deltaTime) / duration; // calculate the step value
			transform.position += diff.normalized * step; // increase the position by diff normalized * step
			counter += Time.deltaTime; // Increase the counter by the frame's duration
			yield return null; // Get this loop running in the next frame. In other words, don't wait any frames
		}
		transform.position = target; // Just in case, set the object's position as being the target
	}

	public void DotProduct (Vector3 a, Vector3 b)
	{
		// Draw the vector lines going from this game object's position
		Debug.DrawLine (transform.position, transform.position + a);
		Debug.DrawLine (transform.position, transform.position + b);
		// Calculate the angle by applying arc cosine to the dot product's result
		Vector3 ab = (b - a); // Oriented segment
		// Canculate the angle between two points. Mathf.Acos gives you the value in radians, so it was converted to angles
		float angle = (180 * Mathf.Acos (Vector3.Dot (a.normalized, ab.normalized)))/Mathf.PI;
		print ("Angle between 'a' and 'b' is " + angle + "˚");
	}
}

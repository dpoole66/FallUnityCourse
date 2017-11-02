using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProduct : MonoBehaviour {

 public   Vector3 a, b;

	// Use this for initialization
	void Start () {
        a = new Vector3(3, 1, -5);
		
	}
	
	// Update is called once per frame
	void Update () {
        GetDotProduct(a, b);
    }
    public void GetDotProduct(Vector3 a, Vector3 b) {
        // Draw the vector lines going from this game object's position
        Debug.DrawLine(transform.position, transform.position + a);
        Debug.DrawLine(transform.position, transform.position + b);


        // Calculate the angle by applying arc cosine to the dot product's result
        Vector3 ab = (b - a); // Oriented segment

        // Calculate the angle between two points. Mathf.Acos gives you the value in radians, so it was converted to angles
        float angle = (180 * Mathf.Acos(Vector3.Dot(a.normalized, ab.normalized))) / Mathf.PI;
        print("Angle between 'a' and 'b' is " + angle + "˚");
    }

}

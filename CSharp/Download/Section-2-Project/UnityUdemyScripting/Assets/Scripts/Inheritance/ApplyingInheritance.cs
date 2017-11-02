using UnityEngine;
using System.Collections;

public class ApplyingInheritance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Bird b = new Bird (1);
		print (b.Velocity);
		Airplane a = new Airplane (10000, 200, "Airbus", 20000);
		print (a.Velocity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

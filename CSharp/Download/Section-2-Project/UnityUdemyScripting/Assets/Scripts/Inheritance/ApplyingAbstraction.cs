using UnityEngine;
using System.Collections;

public class ApplyingAbstraction : MonoBehaviour {

	public FlyingThing[] thingsThatFly;

	// Use this for initialization
	void Start () {
		thingsThatFly = new FlyingThing[2];
		thingsThatFly[0] = new Bird (2);
		thingsThatFly[1] = new Airplane (1000, 200, "Embraer", 1000);
		thingsThatFly[0].Fly (); // this is a bird
		thingsThatFly[1].Fly (); // this is an airplane
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

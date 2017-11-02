using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyingAbstraction : MonoBehaviour {

    public Flight[] flyingThings;

    void Start() {
        flyingThings = new Flight[2];
        flyingThings[0] = new Airplane(100.0f, 500.0f, 66, "Birdbrain", 500.0f);
        flyingThings[1] = new Bird(10, "Bat", 10.0f);
        flyingThings[0].Fly();
        flyingThings[1].Fly();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

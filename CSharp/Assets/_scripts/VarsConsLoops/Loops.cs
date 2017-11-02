using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loops : MonoBehaviour {

    public int age;

	// Use this for initialization
	void Start () {
        while (age < 21) {
            Debug.Log(age);
        }
        do {
            age++;
        } while (age < 21);
    }
	
	// Update is called once per frame
	void Update () {
    
    }
}

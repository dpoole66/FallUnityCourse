using UnityEngine;
using System.Collections;

public class ConditionalStatements : MonoBehaviour {

	public int age;
	public bool canBuy;

	// Use this for initialization
	void Start () {
		if (age >= 21) {
			// this is going to be executed if age is greather than or equals to 21
			canBuy = true;
		} else if (age < 0) {
			age = 0;
			canBuy = false;
		} else {
			canBuy = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

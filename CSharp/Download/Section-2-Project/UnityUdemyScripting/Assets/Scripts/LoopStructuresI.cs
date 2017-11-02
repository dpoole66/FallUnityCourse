using UnityEngine;
using System.Collections;

public class LoopStructuresI : MonoBehaviour {

	public int age;

	// Use this for initialization
	void Start () {
		while (age < 21) {
			//age = age + 1;
			//age += 2;
			age++;
		}
		// here, age is always 21

		do{
			age++;
		} while (age < 21);

		// here, age is always 22
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

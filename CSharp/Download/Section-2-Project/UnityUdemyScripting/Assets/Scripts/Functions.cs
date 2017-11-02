using UnityEngine;
using System.Collections;

public class Functions : MonoBehaviour {

	public string myText;
	public int valueA, valueB;
	public float result;

	// Functions III
	public int integer;
	public float[] arrayOfValues;

	// Use this for initialization
	void Start () {
		DoSomething (ref integer);
		DoSomething (arrayOfValues);
		//result = Divide (valueA, valueB);
		//print (result);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	// type of access (optional) - return type - name - (parameters/arguments)[optional] {}
	void SayHello () {
		print ("Hello");
	}

	void SaySomething (string something) {
		// the parameter 'something' just has a meaning inside this function.
		print (something);
	}

	// Return types

	int Sum (int a, int b) {
		int value = a + b;
		return value;
	}

	int Subtract (int a, int b) {
		int result = a - b;
		return result;
	}

	float Divide (float a, float b) {
		if (b == 0){
			print ("Cannot divide by zero.");
			return 0;
		} else {
			float value = a/b;
			return value;
		}
	}

	// Overload functions and passing by value and reference

	// passing by value
	void DoSomething (ref int v) {
		v += 3;
	}

	// passing by reference	
	void DoSomething (float[] array){
		array[0] = 4.5f;
	}

}

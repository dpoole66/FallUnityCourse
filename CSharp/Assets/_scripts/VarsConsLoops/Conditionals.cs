using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditionals : MonoBehaviour {

    public int age;
    private bool willSell;
    private bool callCops;
    public bool validId;
    public bool notWasted;
    public bool hasPayment;
    public bool payExtra;

	// Use this for initialization
	void Start () {
        if (age >= 21 && validId == true && hasPayment == true) {
            willSell = true;
        } else if (age <= 0) {
            age = 0;
            willSell = false;
        } else if (willSell == false && payExtra == true) {
            willSell = true;
        } else if (age >= 21 && validId == false) {
            callCops = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (willSell == false) {
            Debug.Log("I don't care if your baby Jesus, just over an extra $5!");
        } else if (payExtra == true) {
            Debug.Log("Take you Hooch and hit the road ya Bum!");
        } else if (callCops == true) {
            Debug.Log("I'm calling the COPS!");
        } else {
            Debug.Log("Have a great day ya Filthy Animal!");
        }
        

	}
}

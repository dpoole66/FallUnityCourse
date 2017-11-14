using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class1 : MonoBehaviour {
    public string Name;
    public Renderer ThisObject;
    private bool GetSick;

    public void Start() {
       GetSick = false; 
    }

    public void Update() {

        if (GetSick == true) {
            ThisObject.GetComponent<Renderer>().material.color = Color.blue;
        } else {
            ThisObject.GetComponent<Renderer>().material.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            TurnBlue();
        }
    }

    public void TurnBlue() {
            Debug.Log("This is " + Name);
            GetSick = !GetSick;
    }
}

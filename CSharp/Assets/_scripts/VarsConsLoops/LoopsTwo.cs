using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopsTwo : MonoBehaviour {

    public int[] values;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < values.Length; i++) {
            values[i] = i;
        } 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

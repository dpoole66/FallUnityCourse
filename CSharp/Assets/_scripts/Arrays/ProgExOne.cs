using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgExOne : MonoBehaviour {

    public int[] values;
    public int minVal;

	// Use this for initialization
	void Start () {
        minVal = values[0];
        for (int i = 1; i < values.Length; i++) {

            if (values[i] < minVal) {
                minVal = values[i];
            }
        }
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

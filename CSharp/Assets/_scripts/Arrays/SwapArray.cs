using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapArray : MonoBehaviour {

    public float[] array;

	// Use this for initialization
	void Start () {
        SwapIndices(array, 2, 4);
        Debug.Log("SwapIndices fired");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SwapIndices(float[] array, int indexA, int indexB) {
        float tempVal = array[indexA];
        Debug.Log("tempVal = " + tempVal);
        array[indexA] = array[indexB];
        Debug.Log("indexA = " + indexB);
        array[indexB] = tempVal;
        Debug.Log("indexA = " + tempVal);
    }
}

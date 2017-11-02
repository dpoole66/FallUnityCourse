using UnityEngine;
using System.Collections;

public class ProgrammingExampleII : MonoBehaviour {

	public float[] array;

	// Use this for initialization
	void Start () {
		SwapIndices (array, 2, 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SwapIndices (float[] array, int indexA, int indexB)
	{
		float temporaryValue = array[indexA];
		array[indexA] = array[indexB];
		array[indexB] = temporaryValue;
	}
}

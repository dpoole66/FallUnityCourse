using UnityEngine;
using System.Collections;

public class ProgrammingExampleI : MonoBehaviour {

	public int[] values;
	public int biggest;

	// Use this for initialization
	void Start () {
		biggest = values[0];
		for (int i = 1; i < values.Length; i++)
		{
			if (values[i] > biggest)
			{
				biggest = values[i];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

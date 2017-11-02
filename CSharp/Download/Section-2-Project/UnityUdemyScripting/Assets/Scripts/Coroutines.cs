using UnityEngine;
using System.Collections;

public class Coroutines : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (PrintingAsTimePasses (5));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PrintingAsTimePasses (int delay) {
		print ("Hello!");
		// this is the place where a want a delay
		StartCoroutine (CountTo10 ());
		print ("Hello again!");
		yield return new WaitForSeconds (delay * 2);
		print ("Hey, I'm still here!");
	}

	IEnumerator CountTo10 ()
	{
		for (int i = 0; i <= 10; i++)
		{
			print (i);
			yield return new WaitForSeconds (1f);
		}
	}
}

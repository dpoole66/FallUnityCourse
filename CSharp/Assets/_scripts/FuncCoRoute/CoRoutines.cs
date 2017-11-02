using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoRoutines : MonoBehaviour {

    public int delay;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(DisplayInTime(delay));
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            StartCoroutine(Counter());
        }
    }

    IEnumerator DisplayInTime (int countDown) {
        countDown = delay;
        Debug.Log("Fire One");
        yield return new WaitForSeconds(delay);
        // Adding Counter() in
        yield return StartCoroutine(Counter());
        Debug.Log("Fire Two");
        yield return new WaitForSeconds(delay + 3);
        Debug.Log("I'm firing another...");
        yield return new WaitForSeconds(delay + 2);
        Debug.Log("Fire Three");
    }

    IEnumerator Counter() {
        for (int i = 0; i <= 10; i++) {
            print(i);
            yield return new WaitForSeconds(1.0f);
        }

    }
}

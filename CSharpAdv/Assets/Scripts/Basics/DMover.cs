using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMover : MonoBehaviour {
    public float MaxSpeed = 1.0f;
    private Transform ThisTransform = null;

	// Use this for initialization
	void Awake () {
        ThisTransform = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        ThisTransform.position += ThisTransform.forward * MaxSpeed * Time.deltaTime;

    }
}

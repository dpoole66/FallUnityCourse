using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoShow : MonoBehaviour {
    public bool ShowGizmos = true;
    public GameObject GizmoNode;
    public string MyIcon = string.Empty;
    [Range(0.0f, 50.0f)]
    public float Range = 1.0f;

    void OnDrawGizmos() {
        if (!ShowGizmos) return;

        //Gizmos.DrawIcon(transform.position, MyIcon, true);
        //Draw sphere range
        //Gizmos.color = Color.black;
        //Gizmos.DrawWireSphere(transform.position, Range);
        //Draw forward vector
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * Range);

    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

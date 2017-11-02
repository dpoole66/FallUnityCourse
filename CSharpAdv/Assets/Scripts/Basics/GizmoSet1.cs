using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoSet1 : MonoBehaviour {
    public bool ShowGizmos = true;
    public string MyIcon = string.Empty;
    [Range(0.0f, 50.0f)]
    public float Range = 7.0f;

    void OnDrawGizmos() {
        if (!ShowGizmos) return;

        Gizmos.DrawIcon(transform.position, MyIcon, true);
        //Draw sphere range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range);
        //Draw forward vector
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * Range * 2);

    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

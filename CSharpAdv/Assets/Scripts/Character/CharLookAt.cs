using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CharLookAt : MonoBehaviour {

    public Transform head = null;
    public Vector3 lookAtTargetPos;
    public float lookAtCoolTime = 0.2f;
    public float lookAtHeatTime = 0.2f;
    public bool looking = true;

    private Vector3 lookAtPos;
    private Animator animator;
    private float lookAtWeight = 0.0f;

	// Use this for initialization
	void Start () {

        if (!head) {
            Debug.LogError("No head mom. Look at disabled");
            enabled = false;
            return;
        }

        animator = GetComponent<Animator>();
        lookAtTargetPos = head.position + transform.forward;
        lookAtPos = lookAtTargetPos; 
		
	}

    void OnAnimatorIK() {
        lookAtTargetPos.y = head.position.y;
        float lookAtTargetWeight = looking ? 1.0f : 0.0f;

        Vector3 currentDir = lookAtPos - head.position;
        Vector3 futureDir = lookAtTargetPos - head.position;

        float blendTime = lookAtTargetWeight > lookAtWeight ? lookAtHeatTime : lookAtCoolTime;
        lookAtWeight = Mathf.MoveTowards(lookAtWeight, lookAtTargetWeight, Time.deltaTime / blendTime);
        animator.SetLookAtWeight(lookAtWeight, 0.2f, 0.5f, 0.7f, 0.5f);
        animator.SetLookAtPosition(lookAtPos);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

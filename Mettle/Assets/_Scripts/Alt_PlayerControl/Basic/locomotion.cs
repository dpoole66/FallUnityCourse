using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locomotion : MonoBehaviour {
    Animator ThisAnimator;

    int AttackHash = Animator.StringToHash("Attack");
    int runStateHash = Animator.StringToHash("Base Layer.Run");

	// Use this for initialization
	void Start () {
        ThisAnimator = GetComponent<Animator>();          
	}
	
	// Update is called once per frame
	void Update () {
        float move = Input.GetAxis("Vertical");
        ThisAnimator.SetFloat("Speed", move);

        AnimatorStateInfo stateInfo = ThisAnimator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKeyDown(KeyCode.Space) && stateInfo.fullPathHash == runStateHash) {
            ThisAnimator.SetTrigger(AttackHash);
        }
	}
}

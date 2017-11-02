using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class IKController : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    public Transform charHead = null;
    public Transform lookAt = null;

    //--
	void Start () {
        animator = GetComponent<Animator>();
	}
    //--
	
	//--
	void ONAnimatorIK () {
        if (animator) {
            if (ikActive) {
                //Set look at target
                if(lookAt !=null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookAt.position);
                }

                if (charHead != null) {
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, charHead.rotation);
                }
            } else {
                animator.SetLookAtWeight(0);
            }
        }
	}
    //--
}

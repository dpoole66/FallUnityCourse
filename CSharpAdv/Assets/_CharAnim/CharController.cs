using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharController : MonoBehaviour {

    public Transform Target;       
    private float RotSpeed = 90.0f;

    private Animator ThisAnimator = null;
    private Transform ThisTransform = null;

    private int VertHash = Animator.StringToHash("Vertical");
    private int HorzHash = Animator.StringToHash("Horizontal");
  

    // Use this for initialization
    void Awake () {
        ThisAnimator = GetComponent<Animator>();
        //ThisTransform = GetComponent<SkeletonBone>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float Horz = CrossPlatformInputManager.GetAxis("Horizontal");
        float Vert = CrossPlatformInputManager.GetAxis("Vertical");

        ThisAnimator.SetFloat(HorzHash, Horz, 0.1f, Time.deltaTime);
        ThisAnimator.SetFloat(VertHash, Vert, 0.1f, Time.deltaTime);

    }



    //public void LookAtMe(GameObject Target) {
    //    Quaternion DestRot = Quaternion.LookRotation(Target.position - ThisTransform.position, Vector3.up);
    //    ThisTransform.rotation = Quaternion.RotateTowards(ThisTransform.rotation, DestRot, RotSpeed * Time.deltaTime);
    //    return; 
    //}
}

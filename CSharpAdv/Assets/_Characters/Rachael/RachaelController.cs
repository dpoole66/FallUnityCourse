using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RachaelController : MonoBehaviour {

    //-----------------------------------------------
    public float MaxSpeed = 1.7f;
    public float RotSpeed = 60.0f;
    private Transform ThisTransform = null;
    private CharacterController ThisController = null;
    private Animator ThisAnimator = null;
    public Transform AudHead;
    private Vector3 headScreenPos, headToCursor, aimPos, finalAimPos, finalMovement, camDirTransform;

    //-----------------------------------------------

    private int VertHash = Animator.StringToHash("Vertical");
    private int HorzHash = Animator.StringToHash("Horizontal");

    private Vector3 moveDirection = Vector3.zero;

    //-----------------------------------------------
    void Awake() {

        ThisAnimator = GetComponent<Animator>();
        ThisController = GetComponent<CharacterController>();
        ThisTransform = GetComponent<Transform>();
    }
    //-----------------------------------------------

    //-----------------------------------------------
    void FixedUpdate() {
        float Horz = CrossPlatformInputManager.GetAxis("Horizontal");
        float Vert = CrossPlatformInputManager.GetAxis("Vertical");

        ThisAnimator.SetFloat(HorzHash, Horz, 0.1f, Time.deltaTime);
        ThisAnimator.SetFloat(VertHash, Vert, 0.1f, Time.deltaTime);
        //-------------------------------------------
        ThisTransform.rotation *= Quaternion.Euler(0.0f, RotSpeed * Horz * Time.deltaTime, 0.0f);
        //-------------------------------------------
        ThisController.SimpleMove(ThisTransform.forward * MaxSpeed * Vert);
        //ThisTransform.position += ThisTransform.forward * MaxSpeed * Vert * Time.deltaTime;
        //ThisCharCam.rotation = *= Quaternion.LookRotation(Target.position - ThisTransform.position, Vector3.up);
        //-------------------------------------------   
        ThisTransform.rotation *= Quaternion.Euler(0.0f, RotSpeed * Horz * Time.deltaTime, 0.0f);
    }

}

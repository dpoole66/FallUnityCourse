/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MettleSightMod : MonoBehaviour {
    private VISION_STATE currentstate = VISION_STATE.TRIGGER;

    //------------------------------------------
    //How sensitive should we be to sight
    public enum SightSensitivity { STRICT, LOOSE };

    //Sight sensitivity
    public SightSensitivity Sensitity = SightSensitivity.STRICT;

    //Can we see target
    public bool CanSeeTarget = false;

    //FOV
    public float FieldOfView = 60f;

    //Reference to target
    public Transform AI_Target = null;

    //Reference to eyes
    public Transform EyePoint = null;

    //Reference to transform component
    private Transform ThisTransform = null;

    //Reference to sphere collider
    private SphereCollider ThisCollider = null;

    //Reference to last know object sighting, if any
    public Vector3 LastKnowSighting = Vector3.zero;

    // Testing color swap when target seen

    public Renderer TargetColor;

    //------------------------------------------
    void Awake() {
        ThisTransform = GetComponent<Transform>();
        ThisCollider = GetComponent<SphereCollider>();
        LastKnowSighting = ThisTransform.position;
        TargetColor.material.color = Color.black;

    }
    //------------------------------------------

    // FSM for Vision. Testing to fix errors with original script
    //-----------------------------------------------------------

    public enum VISION_STATE { FOV, LOS, UPDATE, TRIGGER }
    private VISION_STATE currentState = VISION_STATE.TRIGGER;

    // VISION STATES
    public VISION_STATE CurrentState {
        get {   return currentState;    }
        set {

            currentState = value;
            StopAllCoroutines();

            switch (currentState) {
                case VISION_STATE.TRIGGER:
                StartCoroutine(vTRIGGER());
                break;
            }

            switch (currentState) {
                case VISION_STATE.UPDATE:
                StartCoroutine(vUPDATE());
                break;
            }

            switch (currentState) {
                case VISION_STATE.FOV:
                StartCoroutine(vFOV());
                break;
            }

            switch (currentState) {
                case VISION_STATE.LOS:
                StartCoroutine(vLOS());
                break;
            }

        }
    }
    //-----------------------------------------------------------


    public IEnumerator vTRIGGER() {
        while (currentState == VISION_STATE.TRIGGER) {
            //Talk to UPDATE to get FOV and LOS
            StartCoroutine( vUPDATE() );
            
            //Update last known sighting
            if (CanSeeTarget) {
                LastKnowSighting = AI_Target.position;
                //If player, then can see player -- test with Red color swap on target
                TargetColor.material.color = Color.red;
            } else {
                TargetColor.material.color = Color.black;
                LastKnowSighting = AI_Target.position;
            }
            
            yield return null;
            
        }

        yield break;
    }

    public IEnumerator vUPDATE() {
        while (currentState == VISION_STATE.UPDATE) {

            switch (Sensitity) {
                case SightSensitivity.STRICT:
                CanSeeTarget = vFOV() && vLOS();
                break;

                case SightSensitivity.LOOSE:
                CanSeeTarget = vFOV() || vLOS();
                break;
            }

            yield break;
        }

    }

    public IEnumerator vFOV() {
        while (currentState == VISION_STATE.FOV) {

            //Get direction to target
            Vector3 DirToTarget = AI_Target.position - EyePoint.position;
            //Debug.Log("Direction " + DirToTarget);

            //Get angle between forward and look direction
            float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);
            //Debug.Log("Angle " + Angle);

            //Are we within field of view?
            if (Angle <= FieldOfView) {
                //Debug.Log("In FOV");
                yield return true;
            }

            yield return false;

        }
    }

    public IEnumerator vLOS() {
        while (currentState == VISION_STATE.LOS) {

            RaycastHit Info;

            if (Physics.Raycast(EyePoint.position, (AI_Target.position - EyePoint.position).normalized, out Info, ThisCollider.radius)) {
                if (Info.transform.CompareTag("Enemy"))
                    yield return true;
            }

            yield return false;
        }
    }
}

*/
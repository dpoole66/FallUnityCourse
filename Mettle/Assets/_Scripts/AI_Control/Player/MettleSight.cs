using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MettleSight : MonoBehaviour {


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

    private Color colorToggle;

    //------------------------------------------
    void Awake() {
        ThisTransform = GetComponent<Transform>();
        ThisCollider = GetComponent<SphereCollider>();
        LastKnowSighting = ThisTransform.position;

        //TargetColor.material.color = Color.black;

    }
    //------------------------------------------

    bool InFOV() {
        //Get direction to target
        Vector3 DirToTarget = AI_Target.position - EyePoint.position;
        //Debug.Log("Direction " + DirToTarget);

        //Get angle between forward and look direction
        float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);
        //Debug.Log("Angle " + Angle);

        //Are we within field of view?
        if (Angle <= FieldOfView) {
            //Debug.Log("In FOV");
            return true;
        }

        //Not within view
        return false;
    }

    //------------------------------------------

    bool ClearLineofSight() {
        RaycastHit Info;

        if (Physics.Raycast(EyePoint.position, (AI_Target.position - EyePoint.position).normalized, out Info, ThisCollider.radius)) {
            if (Info.transform.CompareTag("Enemy"))
                return true;
        }
        return false;
     }

    //------------------------------------------

    void UpdateSight() {
        switch (Sensitity) {
            case SightSensitivity.STRICT:
            CanSeeTarget = InFOV() && ClearLineofSight();
            break;

            case SightSensitivity.LOOSE:
            CanSeeTarget = InFOV() || ClearLineofSight();
            break;
        }
    }

    //------------------------------------------

        //void OnTriggerStay(Collider Other) {
        void OnTriggerStay(Collider Other) {

        UpdateSight();

        //Update last known sighting
        if (CanSeeTarget) { 

            LastKnowSighting = AI_Target.position;
            TargetColor.material.color = Color.white;
            return;

        } else {
            LastKnowSighting = AI_Target.position;
            TargetColor.material.color = Color.black;
            return;
        }

    }

}

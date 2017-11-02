using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSightRachael : MonoBehaviour {

    //SWITCHES
    //How sensitive are we with sight?
    public enum SightSensitivity { STRICT, LOOSE};

    public SightSensitivity Sensitivity = SightSensitivity.STRICT;

    //Can we see target
    public bool CanSeeTarget = false;

    //Reference to last target location
    public Vector3 LastKnowSighting = Vector3.zero;

    //FOV
    public float FieldOfView = 45f;

    //Reference to target
    public Transform Target = null;

    //Reference to eyes
    public Transform EyePoint = null;

    //Name of target
    private Collider _Other;

    //Reference to sphere collider
    private SphereCollider ThisCollider = null;

    //Reference to transform component
    private Transform ThisTransform = null;

    void Awake() {
        ThisTransform = GetComponent<Transform>();
        ThisCollider = GetComponent<SphereCollider>();
        LastKnowSighting = ThisTransform.position;
    }

    bool InFOV() {
        // Get target direction
        Vector3 DirToTarget = Target.position - EyePoint.position;

        // Get angle between forward and the "look" direction. 
        // This is based off of local EyePoint? Head turning will hold? 
        float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);

        // FOV check
        if (Angle <= FieldOfView) {
            return true;
        } 

        return false;
      
    }

    void UpdateSight() {
        switch(Sensitivity) {
            case SightSensitivity.STRICT:
            CanSeeTarget = InFOV() && ClearLineofSight();
            break;

            case SightSensitivity.LOOSE:
            CanSeeTarget = InFOV() || ClearLineofSight();
            break;
        }
    }

    bool ClearLineofSight() {

        RaycastHit Info;

        if (Physics.Raycast(EyePoint.position, (Target.position - EyePoint.position).normalized, out Info, ThisCollider.radius)) {
            if (Info.transform.CompareTag("Player"))
                return true;
        }

        return false;
    }

    void OnTriggerStay(Collider Other) {

        UpdateSight();

        if (CanSeeTarget) {
            LastKnowSighting = Target.position;
            Debug.Log("I See You!");
        } 

    }
}

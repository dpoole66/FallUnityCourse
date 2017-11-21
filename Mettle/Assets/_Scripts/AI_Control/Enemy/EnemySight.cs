using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

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
    private Transform EnemyTransform = null;

    public Renderer TargetColor;

    //Reference to eyes
    public Transform EyePoint = null;

    //Reference to transform component
    private Transform ThisTransform = null;

    //Reference to sphere collider
    private SphereCollider ThisCollider = null;

    //Reference to last know object sighting, if any
    public Vector3 LastKnowSighting = Vector3.zero;




    //------------------------------------------
    void Awake() {
        ThisTransform = GetComponent<Transform>();
        EnemyTransform = AI_Target.GetComponent<Transform>();
        ThisCollider = GetComponent<SphereCollider>();
        TargetColor = AI_Target.GetComponent<Renderer>();
        LastKnowSighting = ThisTransform.position;

    }
    //------------------------------------------

    bool InFOV() {
        //Get direction to target
        Vector3 DirToTarget = EnemyTransform.position - EyePoint.position;

        //Get angle between forward and look direction
        float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);

        //Are we within field of view?
        if (Angle <= FieldOfView) {  
            return true;
        }

        //Not within view
        return false;
    }

    //------------------------------------------

    bool ClearLineofSight() {
        RaycastHit Info;

        if (Physics.Raycast(EyePoint.position, (EnemyTransform.position - EyePoint.position).normalized, out Info, ThisCollider.radius)) {
            if (Info.transform.CompareTag("Player"))
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

        } else {
            LastKnowSighting = AI_Target.position;
        }

    }

    void Update() {
        if (CanSeeTarget) {
            //If player, then can see player -- test with Red color swap on target
            TargetColor.material.color = Color.white;
        } else {
            TargetColor.material.color = Color.black;
        }
    }

}

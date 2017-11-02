using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    private Transform ThisTransform = null;
    public Transform Ground;
    // Use this for initialization
    void Awake() {
        ThisTransform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update () {
        float GroundPos = Ground.position.y;

        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.0f));

        TargetPos = new Vector3(TargetPos.x, GroundPos + 1.02f, TargetPos.z);

        transform.position = TargetPos;

        Vector3 LookAtTarget = TargetPos - ThisTransform.position;              

    }
}

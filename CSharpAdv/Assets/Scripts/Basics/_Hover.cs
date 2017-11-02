using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class _Hover : MonoBehaviour {

    private Transform ThisTransform = null;
    public float DistFromGround = 2.0f;
    public float MaxSpeed = 10.0f;
    public float AngleSpeed = 5.0f;
    private Vector3 DestUp = Vector3.zero;

	// Use this for initialization
	void Awake () {
        ThisTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        float Horz = CrossPlatformInputManager.GetAxis("Horizontal");
        float Vert = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 NewPos = ThisTransform.position;
        NewPos += ThisTransform.forward * Vert * MaxSpeed * Time.deltaTime;
        NewPos += ThisTransform.right * Horz * MaxSpeed * Time.deltaTime;

        RaycastHit Hit;
        if(Physics.Raycast(ThisTransform.position, -Vector3.up, out Hit)) {
            NewPos.y = (Hit.point + Vector3.up * DistFromGround).y;
            DestUp = Hit.normal;
        }

        ThisTransform.position = NewPos;
        ThisTransform.up = Vector3.Slerp(ThisTransform.up, DestUp, AngleSpeed * Time.deltaTime);
		
	}
}

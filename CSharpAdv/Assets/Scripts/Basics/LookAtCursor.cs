using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAtCursor : MonoBehaviour {

    private Transform ThisTransform = null;

    void Awake() {
        ThisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {

        //Vector3 MousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        Vector3 MousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MousePosWorld = new Vector3(MousePosWorld.x, ThisTransform.position.y, MousePosWorld.z);
        //MousePosWorld = new Vector3(MousePosWorld.x, MousePosWorld.y, MousePosWorld.z);

        Vector3 LookDirection = MousePosWorld - ThisTransform.position;

        ThisTransform.localRotation = Quaternion.LookRotation(LookDirection.normalized, Vector3.up);

        Debug.Log(LookDirection);
    }
}

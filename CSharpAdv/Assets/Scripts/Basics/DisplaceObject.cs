using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaceObject : MonoBehaviour {
    public float DisplaceAmount = 0.50f;
    private Transform ThisTransform = null;
    //-------------------------------------------------

    //-------------------------------------------------
    [SerializeField]
    private Vector3 LocalForward;
    [SerializeField]
    private Vector3 TransformForward;
    //-------------------------------------------------

    //-------------------------------------------------
    void Awake () {
        ThisTransform = GetComponent<Transform>();
	}
    //-------------------------------------------------

    //-------------------------------------------------
    void Update () {
        LocalForward = Vector3.forward;
        TransformForward = ThisTransform.forward;
        Vector3 LocalSpaceD = Vector3.forward * DisplaceAmount * Time.deltaTime;
        Vector3 WorldSpaceD = ThisTransform.rotation * LocalSpaceD;       // Using Quaternion Rotation with .transform  (quaternion first then the multyply vector).

        ThisTransform.position += WorldSpaceD;

    }
    //-------------------------------------------------

    //-------------------------------------------------
}

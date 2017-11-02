using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToRail : MonoBehaviour {

    public Transform Target = null;
    private Transform ThisTransform = null;

    public Transform LineStart = null;
    public Transform LineEnd = null;
    // attempting to add Lerp
    public float speed = 6.5f;
    private float startTime;
    private float movementLength;
    private Vector3 squeze;

    // Use this for initialization
    void Awake() {
        ThisTransform = GetComponent<Transform>();
    }

    void Start() {
        startTime = Time.time;
        movementLength = Vector3.Distance(LineStart.position, LineEnd.position);
    }

    // Update is called once per frame
    void Update() {
        // add Lerp
        float distCovered = (Time.time - startTime) / speed;
        float fracMovement = distCovered / movementLength;

        //squeze = Vector3.Lerp(Target.position - LineStart.position, LineEnd.position, fracMovement);

        Vector3 Normal = (LineEnd.position - LineStart.position).normalized;
        Vector3 Slide = Vector3.Project(Target.position - LineStart.position, Normal);

        Vector3 Pos = LineStart.position + Vector3.Lerp(Slide, LineEnd.position, fracMovement);

        //Vector3 Pos = LineStart.position + Vector3.Project(Target.position - LineStart.position, Normal);


        // Below, squeze replaces Pos
        Pos.x = Mathf.Clamp(Pos.x, Mathf.Min(LineStart.position.x, LineEnd.position.x), Mathf.Max(LineStart.position.x, LineEnd.position.x));
        Pos.y = Mathf.Clamp(Pos.y, Mathf.Min(LineStart.position.y, LineEnd.position.y), Mathf.Max(LineStart.position.y, LineEnd.position.y));
        Pos.z = Mathf.Clamp(Pos.z, Mathf.Min(LineStart.position.z, LineEnd.position.z), Mathf.Max(LineStart.position.z, LineEnd.position.z));

        ThisTransform.position = Pos;

    }
}


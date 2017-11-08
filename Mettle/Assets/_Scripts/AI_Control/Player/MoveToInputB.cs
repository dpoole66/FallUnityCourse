using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]

public class MoveToInputB : MonoBehaviour {
    Animator ThisAnimator;
    //Movement Plus:
    Vector2 smoothDeltaPosition = Vector2.zero;
    //
    RaycastHit hitInfo = new RaycastHit();
    UnityEngine.AI.NavMeshAgent ThisAgent;
    public Vector3 velocity;
    bool Patrol;
    public float walkSpeed;
    bool isWalking = false;


    // Use this for initialization
    void Start () {
        ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ThisAnimator = GetComponent<Animator>();
        //Movement Plus:
        ThisAgent.updatePosition = false;
        //
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                ThisAgent.destination = hitInfo.point;
        }

        Vector3 worldDeltaPosition = ThisAgent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if delta time is safe
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        UpdateAnimator();

    }

    void UpdateAnimator() {

        if (Time.deltaTime > 1e-5f) {
            //velocity = ThisAgent.destination - transform.position / Time.deltaTime;
            velocity = ThisAgent.velocity;
        }

        bool isMoving = velocity.magnitude > 0.5f && ThisAgent.remainingDistance > ThisAgent.radius;

        ThisAnimator.SetBool("Patrol", Patrol);
        ThisAnimator.SetBool("Is Moving", isMoving);
        ThisAnimator.SetInteger("VeloX", Mathf.RoundToInt(velocity.x));
        ThisAnimator.SetInteger("VeloZ", Mathf.RoundToInt(velocity.z));


        //Set Patrol toggle
        if (Input.GetButtonDown("Patrol")) {
            Patrol = !Patrol;
        }

        //Set Walk true/false
        if (velocity.x != 0.0f) {
            ThisAnimator.SetInteger("Walk", 1);
            isWalking = true;
        } else {
            ThisAnimator.SetInteger("Walk", 0);
        }

    }
}

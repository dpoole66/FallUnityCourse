using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class LocomotionMouseAgent : MonoBehaviour {
    private Animator thisAnimator;
    private UnityEngine.AI.NavMeshAgent agent = null;

    RaycastHit hitInfo = new RaycastHit();

    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    private bool noGesture = false;
    private bool idleSet = false;
    private bool waveGesture = false;
    private bool loopyGesture = false;

    private bool turnR = false;
    private bool turnL = false;

    public bool setPatrol;
    public Transform PatrolDestination = null;


    void Awake() {
        thisAnimator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updatePosition = false;
    }

    void Update() {

        Vector3 worldDeltaPosition = agent.destination - transform.position;

        //Move to Agent
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Mouse");
            RayCast();
            Debug.Log("RayCast");

        }

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float walkDampen = 0.005f;
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        // Hey, I replaced "smoothDeltaPosition with agent.destination in the Lerp values below
        smoothDeltaPosition = Vector2.Lerp(agent.destination, deltaPosition, walkDampen);

        // Update velocity if delta time is safe
        // if (Time.deltaTime > 1e-5f)
        velocity = smoothDeltaPosition / Time.deltaTime;


        //bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;
        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;
        bool idleSet = velocity.magnitude < 0.5f && agent.remainingDistance < agent.radius;

        // Update animation parameters
        thisAnimator.SetBool("patrol", setPatrol);
        thisAnimator.SetBool("idle", idleSet);
        thisAnimator.SetBool("no", noGesture);
        thisAnimator.SetBool("wave", waveGesture);
        thisAnimator.SetBool("loopy", loopyGesture);
        thisAnimator.SetBool("move", shouldMove);
        thisAnimator.SetFloat("velx", velocity.x);
        thisAnimator.SetFloat("vely", velocity.y);


        //No Gesture
        if (Input.GetKeyDown(KeyCode.N)) {
            noGesture = true;
        }
        if (Input.GetKeyUp(KeyCode.N)) {
            noGesture = false;
        }

        //Wave Gesture
        if (Input.GetKeyDown(KeyCode.W)) {
            waveGesture = true;
        }
        if (Input.GetKeyUp(KeyCode.W)) {
            waveGesture = false;
        }

        //Loopy Gesture
        if (Input.GetKeyDown(KeyCode.L)) {
            loopyGesture = true;
        }

        if (Input.GetKeyUp(KeyCode.L)) {
            loopyGesture = false;
        }

        //Patrol
        if (Input.GetKeyDown(KeyCode.P)) {
            setPatrol = true;
        }

        if (Input.GetKeyUp(KeyCode.P)) {
            loopyGesture = false;
        }

        //LookAt
        LookAtAudrey lookAt = GetComponent<LookAtAudrey>();
        if (lookAt)
            lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

    }


    void OnAnimatorMove() {

        Vector3 position = thisAnimator.rootPosition;
        position = agent.nextPosition;
        transform.position = position;

        //Patrol
        if (setPatrol == true) {
            agent.destination = PatrolDestination.position;
            transform.position = position;
            //} else {        
            //    transform.position = position;   
        }

    }
    void RayCast() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo)) {
                agent.destination = hitInfo.point;
                transform.position = agent.nextPosition;
                agent.updatePosition = true;
            }
    }
}



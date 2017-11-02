using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class LocoSimpleAgent : MonoBehaviour {


    Animator anim;
   // NavMeshAgent agent;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    RaycastHit hitInfo = new RaycastHit();
    UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //No auto update for agent position. Should this be addressed with FixedUpdate?
        agent.updatePosition = true;
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        //Map worldDeltaposition to LOCAL space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        //Low-pas filter the deltaMove    Mathf.Min returns the smallest of the two values given.
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.5f);

        //Update velocity with time
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update the parameters set in the Animator for this Char
        anim.SetBool("move", shouldMove);
        anim.SetFloat("AudreyHorizontal", velocity.x);
        anim.SetFloat("AudreyVertical", velocity.y);

        //GetComponent<CharLookAt>().lookAtTargetPos = agent.steeringTarget + transform.forward;
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))

                agent.velocity = velocity;
                agent.destination = hitInfo.point;
        }

    }

    //void OnAnimatorMove() {
    //    //Update this position to agent position  
    //    Vector3 position = anim.rootPosition;
    //    position.y = agent.nextPosition.y;
    //    transform.position = position;
    //}
}

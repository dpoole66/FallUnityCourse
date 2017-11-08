using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]

public class MoveToInput : MonoBehaviour {
    Animator ThisAnimator;
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
    }

    // Update is called once per frame
    void Update() {

        if (Time.deltaTime > 1e-5f) {
            //velocity = ThisAgent.destination - transform.position / Time.deltaTime;
            velocity = ThisAgent.velocity;
        }
            

        ThisAnimator.SetBool("Patrol", Patrol);
        //ThisAnimator.SetInteger("Walk", Mathf.RoundToInt(velocity.y));
        ThisAnimator.SetInteger("VeloX", Mathf.RoundToInt(velocity.x));
        ThisAnimator.SetInteger("VeloZ", Mathf.RoundToInt(velocity.z));



        if (Input.GetButtonDown("Patrol")) {
            Patrol = !Patrol;
        }

        if (velocity.x != 0.0f) {
            ThisAnimator.SetInteger("Walk", 1);
            isWalking = true;
            Debug.Log("Is Walking True");
        } else {
            ThisAnimator.SetInteger("Walk", 0);
        }
            
        

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                ThisAgent.destination = hitInfo.point;
                
        }
    }
}

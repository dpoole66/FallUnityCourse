using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//namespace UnityStandardAssets.Characters.ThirdPerson

//[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(AI_MettleStateMachine))]

public class AI_MettleCharacter : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent ThisAgent { get; private set; } // the navmesh agent required for the path finding
    public AI_MettleStateMachine ThisChar { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for


    private void Start() {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        ThisAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        ThisChar = GetComponent<AI_MettleStateMachine>();

        ThisAgent.updateRotation = false;
        ThisAgent.updatePosition = true;
    }


    private void Update() {
        if (target != null)
            ThisAgent.SetDestination(target.position);
        
        if (ThisAgent.remainingDistance > ThisAgent.stoppingDistance)
            ThisChar.Move(ThisAgent.desiredVelocity);
        else
            ThisChar.Move(Vector3.zero);
        
    }


    public void SetTarget(Transform target) {
        this.target = target;
    }
}

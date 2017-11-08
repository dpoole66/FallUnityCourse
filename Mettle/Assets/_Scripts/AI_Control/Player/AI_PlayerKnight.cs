using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class AI_PlayerKnight : MonoBehaviour {

    //-- Setup Finite State Machine with 3 states. Set current state to Patrol.
    public enum ENEMY_STATE {IDLE, PATROL, CHASE, ATTACK};
    public ENEMY_STATE currentState = ENEMY_STATE.IDLE;

    public ENEMY_STATE CurrentState {

        get { return currentState; }
        set {
            currentState = value;
            StopAllCoroutines();

            switch (currentState) {
                case ENEMY_STATE.IDLE:
                StartCoroutine(AIIdle());
                break;
            }
            switch (currentState) {
                case ENEMY_STATE.PATROL:
                    StartCoroutine(AIPatrol());
                break;
            }

            switch (currentState) {
                case ENEMY_STATE.CHASE:
                    StartCoroutine(AIChase());
                break;
            }

            switch (currentState) {
                case ENEMY_STATE.ATTACK:
                StartCoroutine(AIAttack());
                break;
            }
        }

    }

    //-- Creating States as Coroutines. Little nuggets that can run on their own.

    public IEnumerator AIIdle() {
        while (currentState == ENEMY_STATE.IDLE) {
            yield return null;
        }
        yield break;
    }

    public IEnumerator AIPatrol() {
        while (currentState == ENEMY_STATE.PATROL) {
            yield return null;
        }
        yield break;
    }

    public IEnumerator AIChase() {
        while (currentState == ENEMY_STATE.CHASE) {
            yield return null;
        }
        yield break;
    }

    public IEnumerator AIAttack() {
        while (currentState == ENEMY_STATE.ATTACK) {
            yield return null;
        }
        yield break;
    }


    //-- Primary body after FSM
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    private Animator ThisAnimator = null;
    public Transform PatrolDestination = null;

	// Use this for initialization
	void Awake () {
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisAnimator = GetComponent<Animator>();

        ThisAgent.updateRotation = true;
        ThisAgent.updatePosition = true;
    }
	
    void OnAnimatorMove() {
        transform.position = ThisAgent.nextPosition;
    }

	// Update is called once per frame
	void Update () {
        //ThisAgent.SetDestination(ThisAgent.nextPosition);
	}
}

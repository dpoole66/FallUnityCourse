using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class AI_EnemyKnight : MonoBehaviour {

    //-- Primary body before FSM
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    public Transform PatrolDestination;
    Animator ThisAnimator;
    public Vector3 velocity;
    bool isWalking = false;
    bool Patrol;

    // Use this for initialization
    void Awake() {
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisAnimator = GetComponent<Animator>();
        ThisAgent.updatePosition = false;
    }

    void Update() {

        Vector3 position = ThisAnimator.rootPosition;
        position = ThisAgent.nextPosition;
        transform.position = position;
        velocity = ThisAgent.velocity;

        int roundedValue = Mathf.RoundToInt(velocity.z);
        int moveValue = Mathf.Abs(roundedValue);

        ThisAnimator.SetInteger("VeloX", Mathf.RoundToInt(velocity.x));
        ThisAnimator.SetInteger("VeloZ", Mathf.RoundToInt(velocity.z));

        if (Mathf.Abs(moveValue) == 0) {
            ThisAnimator.SetInteger("Move", 0);
        }

        if (Mathf.Abs(moveValue) == 1) {
            ThisAnimator.SetInteger("Move", 1);
        }

        if (Mathf.Abs(moveValue) == 2) {
            ThisAnimator.SetInteger("Move", 2);
        }

        if (Mathf.Abs(moveValue) == 3) {
            ThisAnimator.SetInteger("Move", 3);
        }

        if (ThisAgent.SetDestination(PatrolDestination.position)) {
            ThisAnimator.SetBool("Patrol", true);
        }

        bool Moving = velocity.magnitude > 0.0f;

    }

    
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    //-- Setup Finite State Machine with 3 states. Set current state to Patrol.

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public enum ENEMY_STATE {PATROL, CHASE, ATTACK};
    public ENEMY_STATE currentState = ENEMY_STATE.PATROL;

    public ENEMY_STATE CurrentState {

        get { return currentState; }
        set {
            currentState = value;
            StopAllCoroutines();

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
    public IEnumerator AIPatrol() {
        while (currentState == ENEMY_STATE.PATROL) { 
            Debug.Log("Patrol");
            yield return null;
        }
        yield break;
    }

    public IEnumerator AIChase() {
        while (currentState == ENEMY_STATE.CHASE) {
            Debug.Log("Chase");
            yield return null;
        }
        yield break;
    }

    public IEnumerator AIAttack() {
        while (currentState == ENEMY_STATE.ATTACK) {
            Debug.Log("Attack");
            yield return null;
        }
        yield break;
    }
}

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
    bool Patrol;
    bool Chase;
    bool Attack;

    //Hashing the Animator params
    int MovemntStageHash = Animator.StringToHash("Movement Stage");
    int IdleHash = Animator.StringToHash("Idle");
    int PatrolHash = Animator.StringToHash("Patrol");
    int WalkHash = Animator.StringToHash("Walk");
    int RunHash = Animator.StringToHash("Run");
    int AttackHash = Animator.StringToHash("Attack");

    //int runStateHash = Animator.StringToHash("Base Layer.Run");


    // Use this for initialization
    void Awake() {
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisAnimator = GetComponent<Animator>();
        ThisAgent.updatePosition = false;
    }

    void FixedUpdate() {

        Vector3 position = ThisAnimator.rootPosition;
        position = ThisAgent.nextPosition;
        transform.position = position;
        velocity = ThisAgent.velocity;

        int roundedValue = Mathf.RoundToInt(velocity.z);
        int moveValue = Mathf.Abs(roundedValue);

        ThisAnimator.SetInteger("VeloX", Mathf.RoundToInt(velocity.x));
        ThisAnimator.SetInteger("VeloZ", Mathf.RoundToInt(velocity.z));

        if (Mathf.Abs(moveValue) == 0) {
            ThisAnimator.SetInteger(MovemntStageHash, 0);
            ThisAnimator.SetBool(IdleHash, true);
        } else {
            ThisAnimator.SetBool(IdleHash, false);
        }

        if (Mathf.Abs(moveValue) == 1) {
            ThisAnimator.SetInteger(MovemntStageHash, 1);
            ThisAnimator.SetBool(WalkHash, true);
        } else {
            ThisAnimator.SetBool(WalkHash, false);
        }

        if (Mathf.Abs(moveValue) == 2) {
            ThisAnimator.SetInteger(MovemntStageHash, 2);
            ThisAnimator.SetBool(RunHash, true);
        } else {
            ThisAnimator.SetBool(RunHash, false);
        }

        if (Mathf.Abs(moveValue) == 3) {
            ThisAnimator.SetInteger(MovemntStageHash, 3);
        }

        if (ThisAgent.SetDestination(PatrolDestination.position)) {
            ThisAnimator.SetBool(PatrolHash, true);
        }

        //AnimatorStateInfo stateInfo = ThisAnimator.GetCurrentAnimatorStateInfo(0);

        //if (Input.GetKeyDown(KeyCode.X) && stateInfo.fullPathHash == runStateHash) {
        //    ThisAnimator.SetTrigger(AttackHash);
        //}

        if (Input.GetKeyDown(KeyCode.X)) {
            ThisAnimator.SetTrigger(AttackHash);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class PlayerStateMachine : MonoBehaviour {

    //-- Primary body before FSM
    RaycastHit hitInfo = new RaycastHit();
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    public Transform PatrolDestination;
    Animator ThisAnimator;
    public Vector3 velocity;
    bool Idle;
    bool Walk;
    bool Run;
    bool Attack;

    //Hashing the Animator params
    int MovemntStageHash = Animator.StringToHash("Movement Stage");
    int IdleHash = Animator.StringToHash("Idle");
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

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                ThisAgent.destination = hitInfo.point;
        }

        AnimatorStateInfo stateInfo = ThisAnimator.GetCurrentAnimatorStateInfo(0);

        if (Mathf.Abs(velocity.z) == 1.0f && stateInfo.fullPathHash != WalkHash) {    // Set Walk 
            ThisAnimator.SetInteger(MovemntStageHash, 1);
            ThisAnimator.SetBool(WalkHash, true);
        } else {
            //ThisAnimator.SetInteger(WalkHash, 0);
        }

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

        //  bool Moving = velocity.magnitude > 0.0f;

        if (Input.GetKeyDown(KeyCode.X)) {
            ThisAnimator.SetTrigger(AttackHash);
        }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    //-- Setup Finite State Machine with 3 states. Set current state to Patrol.

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public enum PLAYER_STATE { IDLE, CHASE, ATTACK };
    private PLAYER_STATE currentState = PLAYER_STATE.IDLE;

    // States
    public PLAYER_STATE CurrentState {

        get { return currentState; }
        set {
            currentState = value;
            StopAllCoroutines();

            switch (currentState) {
                case PLAYER_STATE.IDLE:
                StartCoroutine(AIPatrol());
                break;
            }

            switch (currentState) {
                case PLAYER_STATE.CHASE:
                StartCoroutine(AIChase());
                break;
            }

            switch (currentState) {
                case PLAYER_STATE.ATTACK:
                StartCoroutine(AIAttack());
                break;
            }
        }

    }


    //--------------------------------------------------------------------------
    //--------------------------------------------------------------------------
    // IEnumerator coroutines
    //--------------------------------------------------------------------------
    //--------------------------------------------------------------------------

    //-- Creating States as Coroutines. Little nuggets that can run on their own.
    public IEnumerator AIPatrol() {
        while (currentState == PLAYER_STATE.IDLE) {
            Debug.Log("Patrol");
            yield return null;
        }
        yield break;
    }

    public IEnumerator AIChase() {
        while (currentState == PLAYER_STATE.CHASE) {
            Debug.Log("Chase");
            yield return null;
        }
        yield break;
    }

    public IEnumerator AIAttack() {
        while (currentState == PLAYER_STATE.ATTACK) {
            Debug.Log("Attack");
            yield return null;
        }
        yield break;
    }
}

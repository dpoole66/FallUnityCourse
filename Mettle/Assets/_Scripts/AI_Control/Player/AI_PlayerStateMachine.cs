using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class AI_PlayerStateMachine : MonoBehaviour {
    
    //Set up player 
    RaycastHit hitInfo = new RaycastHit();
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    private SightLineDetect ThisLineSight = null;

    //Set up enemy
    private Transform EnemyTransform = null;
    public AI_EnemyHealth EnemyHealth = null;
    public float MaxDamage = 10f;

    //Player animator
    Animator ThisAnimator;
    public Transform IdleDestination = null;
    public Vector3 velocity;
    bool Idle;
    bool Walk;
    bool Run;
    bool Attack;

    //Hashing the Animator params
    int MovemntStageHash = Animator.StringToHash("Movement Stage");
    int IdleHash = Animator.StringToHash("Idle");
    int ChaseHash = Animator.StringToHash("Chase");
    int AttackHash = Animator.StringToHash("Attack");

    //int runStateHash = Animator.StringToHash("Base Layer.Run");


    // Use this for initialization
    void Awake() {
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisLineSight = GetComponent<SightLineDetect>();
        ThisAnimator = GetComponent<Animator>();
        EnemyTransform = EnemyHealth.GetComponent<Transform>();
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

        int roundedValue = Mathf.RoundToInt(velocity.z);
        int moveValue = Mathf.Abs(roundedValue);

        ThisAnimator.SetInteger("VeloX", Mathf.RoundToInt(velocity.x));
        ThisAnimator.SetInteger("VeloZ", Mathf.RoundToInt(velocity.z));

        if (Mathf.Abs(moveValue) == 0) {
            ThisAnimator.SetInteger(MovemntStageHash, 0);
        }

        if (Mathf.Abs(moveValue) == 1) {
            ThisAnimator.SetInteger(MovemntStageHash, 1);
        } 

        if (Mathf.Abs(moveValue) == 2) {
            ThisAnimator.SetInteger(MovemntStageHash, 2);
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
    [SerializeField]
    private PLAYER_STATE currentstate = PLAYER_STATE.IDLE;


    public enum PLAYER_STATE { IDLE, CHASE, ATTACK };
    private PLAYER_STATE currentState = PLAYER_STATE.IDLE;

    // S
    public PLAYER_STATE CurrentState {

        get { return currentState; }
        set {
            currentState = value;
            StopAllCoroutines();

            switch (currentState) {
                case PLAYER_STATE.IDLE:
                StartCoroutine(AIIdle());
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
    public IEnumerator AIIdle() {
        while (currentstate == PLAYER_STATE.IDLE) {
            //Set Patrol trigger in Animator
            ThisAnimator.SetTrigger(IdleHash);
            //Set strict search
            ThisLineSight.Sensitity = SightLineDetect.SightSensitivity.STRICT;

            //Chase to patrol position
            ThisAgent.isStopped = true;
            ThisAgent.SetDestination(IdleDestination.position);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //If we can see the target then start chasing
            if (ThisLineSight.CanSeeTarget) {
                ThisAgent.isStopped = false;
                CurrentState = PLAYER_STATE.ATTACK;
                yield break;
            }

            //Wait until next frame
            yield return null;
        }
    }

    public IEnumerator AIChase() {
        //Loop while chasing
        while (currentState == PLAYER_STATE.CHASE) {
            //Set Chase trigger in Animator
            ThisAnimator.SetTrigger(ChaseHash);
            //Set loose search
            ThisLineSight.Sensitity = SightLineDetect.SightSensitivity.LOOSE;

            //Chase to last known position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(ThisLineSight.LastKnowSighting);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //Have we reached destination?
            if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance) {
                //Stop agent
                ThisAgent.isStopped = true;

                //Reached destination but cannot see player
                if (!ThisLineSight.CanSeeTarget)
                    CurrentState = PLAYER_STATE.IDLE;
                else //Reached destination and can see player. Reached attacking distance
                    CurrentState = PLAYER_STATE.ATTACK;
                //Set Attack trigger in Animator
                ThisAnimator.SetTrigger(AttackHash);

                yield break;
            }

            //Wait until next frame
            yield return null;
        }
    }

    public IEnumerator AIAttack() {
        //Loop while chasing and attacking
        while (currentstate == PLAYER_STATE.ATTACK) {
            //Set Attack trigger in Animator
            ThisAnimator.SetTrigger(AttackHash);
            //Chase to player position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(EnemyTransform.position);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //Has player run away?
            if (ThisAgent.remainingDistance > ThisAgent.stoppingDistance) {
                //Change back to chase
                CurrentState = PLAYER_STATE.CHASE;
                yield break;
            } else {
                //Attack
                EnemyHealth.HealthPoints -= MaxDamage * Time.deltaTime;
            }

            //Wait until next frame
            yield return null;
        }

        yield break;
    }
}

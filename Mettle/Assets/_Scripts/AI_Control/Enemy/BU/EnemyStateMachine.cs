using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class EnemyStateMachine : MonoBehaviour {

    //--------------------------------------------------------------------------
    //--------------------------------------------------------------------------
    // Declare needed components and variables
    //--------------------------------------------------------------------------
    //--------------------------------------------------------------------------

    [SerializeField]
    private ENEMY_STATE currentstate = ENEMY_STATE.PATROL;

    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    private Animator ThisAnimator;  
    //private Transform ThisTransform = null;

    public AI_PlayerHealth PlayerHealth = null;
    private Transform PlayerTransform = null;

    private SightLineDetect ThisLineSight = null;
    public Transform PatrolDestination = null;
    public Vector3 velocity;
    bool Patrol;
    bool Chase;
    bool Attack;

    public float MaxDamage = 10f;

    //--------------------------------------------------------------------------
    //--------------------------------------------------------------------------

    //Hashing the Animator params ----------------------------------------------
    int MovemntStageHash = Animator.StringToHash("Movement Stage");
    int IdleHash = Animator.StringToHash("Idle");
    int PatrolHash = Animator.StringToHash("Patrol");
    int WalkHash = Animator.StringToHash("Walk");
    int RunHash = Animator.StringToHash("Run");
    int ChaseHash = Animator.StringToHash("Chase");
    int AttackHash = Animator.StringToHash("Attack");
    //int runStateHash = Animator.StringToHash("Base Layer.Run");

    //--------------------------------------------------------------------------
    //--------------------------------------------------------------------------  

    void Awake() {
        ThisLineSight = GetComponent<SightLineDetect>();
        ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ThisAnimator = GetComponent<Animator>();   
        //ThisTransform = GetComponent<Transform>();
        PlayerTransform = PlayerHealth.GetComponent<Transform>();
        ThisAgent.updatePosition = false;
        
    }

    //--------------------------------------------------------------------------
    //--------------------------------------------------------------------------

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

        //if (ThisAgent.SetDestination(PatrolDestination.position)) {
        //    ThisAnimator.SetBool(PatrolHash, true);
        //}

        if (Input.GetKeyDown(KeyCode.X)) {
            ThisAnimator.SetTrigger(AttackHash);
        }

        //bool Moving = velocity.magnitude > 0.0f;

    }
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------


    //-- Setup Finite State Machine with 3 states. Set current state to Patrol.

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public enum ENEMY_STATE { PATROL, CHASE, ATTACK };
    private ENEMY_STATE currentState = ENEMY_STATE.PATROL;



    // Set State
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
    

    //--------------------------------------------------------------------------
    //--------------------------------------------------------------------------
    // IEnumerator coroutines
    //--------------------------------------------------------------------------
    //--------------------------------------------------------------------------

    //-- Creating States as Coroutines. Little nuggets that can run on their own.
    public IEnumerator AIPatrol() {
        while (currentstate == ENEMY_STATE.PATROL) {
            //Set strict search
            ThisLineSight.Sensitity = SightLineDetect.SightSensitivity.STRICT;
            ThisAnimator.SetTrigger(PatrolHash);

            //Chase to patrol position
            ThisAgent.isStopped = true;
            //ThisAgent.Resume();
            ThisAgent.SetDestination(PatrolDestination.position);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //If we can see the target then start chasing
            if (ThisLineSight.CanSeeTarget) {
                ThisAgent.isStopped = true;
                //ThisAgent.Stop();
                CurrentState = ENEMY_STATE.CHASE;
                yield break;
            }

            //Wait until next frame
            yield return null;
        }
    }

    public IEnumerator AIChase() {
        //Set loose search
        ThisLineSight.Sensitity = SightLineDetect.SightSensitivity.LOOSE;
        ThisAnimator.SetTrigger(ChaseHash);

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
                CurrentState = ENEMY_STATE.PATROL;
            else //Reached destination and can see player. Reached attacking distance
                CurrentState = ENEMY_STATE.ATTACK;

            yield break;
        }

        //Wait until next frame
        yield return null;
    }

    public IEnumerator AIAttack() {
        //Loop while chasing and attacking
        while (currentstate == ENEMY_STATE.ATTACK) {
            //Chase to player position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(PlayerTransform.position);
            ThisAnimator.SetTrigger(AttackHash);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //Has player run away?
            if (ThisAgent.remainingDistance > ThisAgent.stoppingDistance) {
                //Change back to chase
                CurrentState = ENEMY_STATE.CHASE;
                yield break;
            } else {
                //Attack
                PlayerHealth.HealthPoints -= MaxDamage * Time.deltaTime;
            }

            //Wait until next frame
            yield return null;
        }

        yield break;
    }
}
    


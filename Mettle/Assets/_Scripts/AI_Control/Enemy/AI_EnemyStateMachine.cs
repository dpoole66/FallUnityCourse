using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_EnemyStateMachine : MonoBehaviour {

    public enum ENEMY_STATE { PATROL, CHASE, ATTACK };
    //------------------------------------------
    public ENEMY_STATE CurrentState {
        get { return currentstate; }

        set {
            //Update current state
            currentstate = value;

            //Stop all running coroutines
            StopAllCoroutines();

            switch (currentstate) {
                case ENEMY_STATE.PATROL:
                StartCoroutine(AIPatrol());
                break;

                case ENEMY_STATE.CHASE:
                StartCoroutine(AIChase());
                break;

                case ENEMY_STATE.ATTACK:
                StartCoroutine(AIAttack());
                break;
            }
        }
    }

    [SerializeField]
    private ENEMY_STATE currentstate = ENEMY_STATE.PATROL;

    Animator ThisAnimator;
    public Vector3 velocity;
    bool Idle;
    bool Walk;
    bool Run;
    bool Attack;

    private SightLineDetect ThisLineSight = null;
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    public AI_PlayerHealth PlayerHealth = null;
    private Transform PlayerTransform = null;
    public Transform PatrolDestination = null;
    public float MaxDamage = 10f;

    //Hashing the Animator params
    int MovemntStageHash = Animator.StringToHash("Movement Stage");
    int PatrolHash = Animator.StringToHash("Patrol");
    int ChaseHash = Animator.StringToHash("Chase");
    int AttackHash = Animator.StringToHash("Attack");

    //------------------------------------------
    //------------------------------------------
    void Awake() {
        ThisLineSight = GetComponent<SightLineDetect>();
        ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        PlayerTransform = PlayerHealth.GetComponent<Transform>();
        ThisAgent.updatePosition = false;
        ThisAnimator = GetComponent<Animator>();

        //Configure starting state
        CurrentState = ENEMY_STATE.PATROL;
    }
 

    //------------------------------------------
    //------------------------------------------
    //Update to drive animator
    //------------------------------------------
    //------------------------------------------
    void FixedUpdate() {

        Vector3 position = ThisAnimator.rootPosition;
        position = ThisAgent.nextPosition;
        transform.position = position;
        velocity = ThisAgent.velocity;

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

    //------------------------------------------
    //------------------------------------------
    // Coroutines to drive state machine
    //------------------------------------------
    //------------------------------------------
    public IEnumerator AIPatrol() {
        //Loop while patrolling
        while (currentstate == ENEMY_STATE.PATROL) {
            //Set Patrol trigger in Animator
            ThisAnimator.SetTrigger(PatrolHash);
            //Set strict search
            ThisLineSight.Sensitity = SightLineDetect.SightSensitivity.STRICT;

            //Chase to patrol position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(PatrolDestination.position);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //If we can see the target then start chasing
            if (ThisLineSight.CanSeeTarget) {
                ThisAgent.isStopped = true;
                CurrentState = ENEMY_STATE.CHASE;
                yield break;
            }

            //Wait until next frame
            yield return null;
        }
    }
    //------------------------------------------
    public IEnumerator AIChase() {
        //Loop while chasing
        while (currentstate == ENEMY_STATE.CHASE) {
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
                    CurrentState = ENEMY_STATE.PATROL;
                else //Reached destination and can see player. Reached attacking distance
                    CurrentState = ENEMY_STATE.ATTACK;
                    //Set Attack trigger in Animator
                    ThisAnimator.SetTrigger(AttackHash);

                yield break;
            }

            //Wait until next frame
            yield return null;
        }
    }
    //------------------------------------------
    public IEnumerator AIAttack() {
        //Loop while chasing and attacking
        while (currentstate == ENEMY_STATE.ATTACK) {
            //Set Attack trigger in Animator
            ThisAnimator.SetTrigger(AttackHash);
            //Chase to player position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(PlayerTransform.position);

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
    //------------------------------------------
}

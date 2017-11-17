using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class AI_EnemyStateMachine : MonoBehaviour {

    [SerializeField]
    private ENEMY_STATE currentstate = ENEMY_STATE.IDLE;
    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 0.3f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;

    Animator ThisAnimator;
    Rigidbody ThisPhysics;
    public Vector3 velocity;
    

    //Currently not used
    bool Idle;
    bool Patrol;
    bool Chase;
    bool Attack;

    private EnemySight ThisLineSight = null;
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    public AI_PlayerHealth PlayerHealth = null;
    private Transform PlayerTransform = null;
    private Transform ThisTransform = null;
    public Transform PatrolDestination = null;
    public float MaxDamage = 10f;


    //Hashing the Animator params
    int ForwardHash = Animator.StringToHash("Forward");
    int TurnHash = Animator.StringToHash("Turn");
    int IdleHash = Animator.StringToHash("Idle");
    int PatrolHash = Animator.StringToHash("Patrol");
    int ChaseHash = Animator.StringToHash("Chase");
    int AttackHash = Animator.StringToHash("Attack");
    int inMotionHash = Animator.StringToHash("inMotion");

    //------------------------------------------
    //------------------------------------------
    void Awake() { 
        ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ThisLineSight = GetComponent<EnemySight>();
        ThisAnimator = GetComponent<Animator>();
        ThisPhysics = GetComponent<Rigidbody>();
        PlayerTransform = PlayerHealth.GetComponent<Transform>();
        ThisTransform = GetComponent<Transform>();
        ThisAgent.updatePosition = false;

        //Configure starting state
        CurrentState = ENEMY_STATE.IDLE;
    }
 

    //------------------------------------------
    //------------------------------------------
    //Update to drive animator
    //------------------------------------------
    //------------------------------------------
    void FixedUpdate() {

        Vector3 position = ThisAnimator.rootPosition;
        Vector3 move;
        position = ThisAgent.nextPosition;
        transform.position = position;
        velocity = ThisAgent.velocity;
        move = velocity;

        //UpdateAnimator(move);
        ThisAnimator.SetFloat("Forward", move.z, 0.1f, Time.deltaTime);
        ThisAnimator.SetFloat("Turn", move.x, 0.1f, Time.deltaTime);

        if (move.magnitude > 0.0f) {
            ThisAnimator.speed = m_AnimSpeedMultiplier;
        } else {
            ThisAnimator.speed = 1.0f;
        }

        // Set inMotion 
        if (move.x + move.z != 0.0f) {
            ThisAnimator.SetTrigger(inMotionHash);
        } 

        // Debugs for states
        if (Input.GetKeyDown(KeyCode.I)) {
            CurrentState = ENEMY_STATE.IDLE;
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            CurrentState = ENEMY_STATE.PATROL;
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            CurrentState = ENEMY_STATE.CHASE;
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            CurrentState = ENEMY_STATE.ATTACK;
        }
    }

  
    //------------------------------------------
    //------------------------------------------

    public enum ENEMY_STATE { IDLE, PATROL, CHASE, ATTACK };

    //------------------------------------------
    public ENEMY_STATE CurrentState {
        get { return currentstate; }

        set {
            //Update current state
            currentstate = value;

            //Stop all running coroutines
            StopAllCoroutines();

            switch (currentstate) {

                case ENEMY_STATE.IDLE:
                StartCoroutine(AIIdle());
                break;

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

    //------------------------------------------
    //------------------------------------------
    // Coroutines to drive state machine
    //------------------------------------------
    //------------------------------------------
    public IEnumerator AIIdle() {
        
        // Loop idle
        while (currentstate == ENEMY_STATE.IDLE) {
            //Set Trigger
            ThisAnimator.SetTrigger(IdleHash);

            ThisLineSight.Sensitity = EnemySight.SightSensitivity.STRICT;

            //Stand in place
            ThisAgent.isStopped = true;
            ThisAgent.SetDestination(ThisAnimator.rootPosition);
            yield return null;

            //If we can see the target then start chasing
            if (ThisLineSight.CanSeeTarget) {
                ThisAgent.isStopped = false;
                CurrentState = ENEMY_STATE.CHASE;
                yield break;
            }

            yield return null;
        }
        


    }

    public IEnumerator AIPatrol() {

        //Loop while patrolling
        while (currentstate == ENEMY_STATE.PATROL) {
            //Set Trigger
            ThisAnimator.SetTrigger(PatrolHash);

            //Set strict search
            ThisLineSight.Sensitity = EnemySight.SightSensitivity.STRICT;

            //Chase to patrol position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(PatrolDestination.position);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //If we can see the target then start chasing
            if (ThisLineSight.CanSeeTarget) {
                ThisAgent.isStopped = false;
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
            //Set Trigger
            ThisAnimator.SetTrigger(ChaseHash);

            //Set loose search
            ThisLineSight.Sensitity = EnemySight.SightSensitivity.LOOSE;

            //Chase to last known position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(ThisLineSight.LastKnowSighting);
            
            //Wait until path is computed
            while (ThisAgent.pathPending)   
            yield return null;

            //Have we reached destination?
            float distTo = Vector3.Distance(ThisTransform.position, ThisAgent.destination);

            if (distTo <= 1.0f) {
                //Stop agent
                ThisAgent.isStopped = true;
                Debug.Log("STOPPED");

                //Reached destination but cannot see player
                if (!ThisLineSight.CanSeeTarget)
                    CurrentState = ENEMY_STATE.IDLE;
                else //Reached destination and can see player. Reached attacking distance
                    CurrentState = ENEMY_STATE.ATTACK;
               
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

            //Set Trigger
            ThisAnimator.SetTrigger(AttackHash);

            //Attack standing range
            float attackRange = Vector3.Distance(ThisTransform.position, ThisAgent.destination);

            if (ThisLineSight.CanSeeTarget && attackRange <= 2.0f) {
                Debug.Log("Range");
      
                //Stand in place
                ThisAgent.isStopped = true;
                ThisAgent.SetDestination(ThisAnimator.rootPosition);
             
                yield return null;

            }  else {

                CurrentState = ENEMY_STATE.CHASE;

                yield return null;
            }
            //Wait for next frame
            yield return null;
        }

        yield break;
    }
    //------------------------------------------
}

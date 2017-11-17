using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class AI_MettleStateMachine : MonoBehaviour {

    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 0.3f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;


    //Set up player 
    RaycastHit hitInfo = new RaycastHit();
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    private MettleSight ThisLineSight = null;

    //Set up enemy
    //private Transform EnemyTransform = null;
    //public AI_EnemyHealth EnemyHealth = null;
    public float MaxDamage = 10f;

    //Player animator
    Animator ThisAnimator;
    Rigidbody ThisPhysics;
    public Vector2 velocity;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;
    bool m_IsGrounded;
    float m_OrigGroundCheckDistance;
    //Mettle and Enemy
    private Transform ThisTransform = null;
    public Transform EnemyTransform = null;

    //States
    bool Idle;
    bool Chase;
    bool Attack;
    bool Moving;

    //Hashing the Animator params
    int MoveingHash = Animator.StringToHash("Moving");
    int IdleHash = Animator.StringToHash("Idle");
    int MoveHash = Animator.StringToHash("Move");
    int RunHash = Animator.StringToHash("Run");
    int AttackHash = Animator.StringToHash("Attack");
    float HorizontalHash = Animator.StringToHash("Turn");
    float VerticalHash = Animator.StringToHash("Forward");

    int runStateHash = Animator.StringToHash("Base Layer.Run");


    // Use this for initialization
    void Awake() {
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisLineSight = GetComponent<MettleSight>();
        ThisAnimator = GetComponent<Animator>();
        ThisPhysics = GetComponent<Rigidbody>();
        //EnemyTransform = EnemyHealth.GetComponent<Transform>();
        ThisAgent.updatePosition = false;
        //m_OrigGroundCheckDistance = m_GroundCheckDistance;

    }
   

    //------------------------------------------
    //------------------------------------------
    //Update to drive animator
    //------------------------------------------
    //------------------------------------------
    public void Move(Vector3 move) {

        Vector3 position = ThisAnimator.rootPosition;
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
            ThisAnimator.SetTrigger(MoveHash);
        }

        // Debugs for states
        if (Input.GetKeyDown(KeyCode.O)) {
            CurrentState = PLAYER_STATE.IDLE;
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            CurrentState = PLAYER_STATE.MOVE;
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            CurrentState = PLAYER_STATE.ATTACK;
        }
    }

    void CheckGroundStatus() {
        RaycastHit hitInfo;
        Debug.Log("HitGround");
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance)) {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            ThisAnimator.applyRootMotion = true;
        } else {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            ThisAnimator.applyRootMotion = false;
        }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    //-- Setup Finite State Machine with 3 states. Set current state to Patrol.

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    [SerializeField]
    private PLAYER_STATE currentstate = PLAYER_STATE.IDLE;


    public enum PLAYER_STATE { IDLE, MOVE, ATTACK };
    private PLAYER_STATE currentState = PLAYER_STATE.IDLE;

    // STATES
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
                case PLAYER_STATE.MOVE:
                StartCoroutine(AIMove());
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
        // Loop idle
        while (currentstate == PLAYER_STATE.IDLE) {
            //Set Trigger
            ThisAnimator.SetTrigger(IdleHash);

            ThisLineSight.Sensitity = MettleSight.SightSensitivity.STRICT;

            //Stand in place
            ThisAgent.isStopped = true;
            ThisAgent.SetDestination(ThisAnimator.rootPosition);
            yield return null;

            //If we can see the target then start chasing
            if (ThisLineSight.CanSeeTarget) {
                ThisAgent.isStopped = true;
                CurrentState = PLAYER_STATE.MOVE;
                yield break;
            }

            yield return null;
        }
    }


    public IEnumerator AIMove() {
       
        //Loop while Moveing
        while (currentState == PLAYER_STATE.MOVE && Moving == true) {

            //Set loose search
            ThisLineSight.Sensitity = MettleSight.SightSensitivity.LOOSE;

            //Move to last known position
            ThisAgent.isStopped = false;
            //ThisAgent.SetDestination(ThisLineSight.LastKnowSighting);

            //Wait until path is computed
            while (ThisAgent.pathPending)

            //Set Move trigger in Animator
            ThisAnimator.SetTrigger(MoveHash);

            yield return null;

            //Have we reached destination?
            float distTo = Vector3.Distance(ThisTransform.position, ThisAgent.destination);

            if (distTo <= 1.0f) {
                //Stop agent
                ThisAgent.isStopped = true;
                Debug.Log("STOPPED");

                //Reached destination but cannot see player
                if (!ThisLineSight.CanSeeTarget)
                    CurrentState = PLAYER_STATE.IDLE;
                else //Reached destination and can see player. Reached attacking distance
                    CurrentState = PLAYER_STATE.ATTACK;

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

            //Attack standing range
            float attackRange = Vector3.Distance(ThisTransform.position, ThisAgent.destination);

            ThisAgent.isStopped = false;
            //ThisAgent.SetDestination(EnemyTransform.position);

            if (ThisLineSight.CanSeeTarget && attackRange <= 2.0f) {
                Debug.Log("Range");

                //Stand in place
                ThisAgent.isStopped = true;
                ThisAgent.SetDestination(ThisAnimator.rootPosition);

                yield return null;

            }
                //EnemyHealth.HealthPoints -= MaxDamage * Time.deltaTime;

            //Wait until next frame
            yield return null;
        }

        yield break;
    }
}

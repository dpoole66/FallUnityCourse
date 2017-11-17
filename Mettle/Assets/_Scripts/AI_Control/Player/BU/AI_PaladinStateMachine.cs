using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))] 
[RequireComponent(typeof(Animator))]

public class AI_PaladinStateMachine : MonoBehaviour {

    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 0.3f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;


    //Set up player 
    RaycastHit hitInfo = new RaycastHit();
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    private SightLineDetect ThisLineSight = null;

    //Set up enemy
    //private Transform EnemyTransform = null;
    //public AI_EnemyHealth EnemyHealth = null;
    public float MaxDamage = 10f;

    //Player animator
    Animator ThisAnimator;
    Rigidbody ThisPhysics;
    public Transform IdleDestination = null;
    public Vector2 velocity;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;
    bool m_IsGrounded;
    float m_OrigGroundCheckDistance;

    //States
    bool Idle;
    bool Chase;
    bool Attack;
    bool Moving;

    //Hashing the Animator params
    int MoveingHash = Animator.StringToHash("Moving");
    int IdleHash = Animator.StringToHash("Idle");
    int MoveHash = Animator.StringToHash("Move");
    int AttackHash = Animator.StringToHash("Attack");
    float HorizontalHash = Animator.StringToHash("Turn");
    float VerticalHash = Animator.StringToHash("Forward");

    //int runStateHash = Animator.StringToHash("Base Layer.Run");


    // Use this for initialization
    void Awake() {
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisLineSight = GetComponent<SightLineDetect>();
        ThisAnimator = GetComponent<Animator>();
        ThisPhysics = GetComponent<Rigidbody>();
        //EnemyTransform = EnemyHealth.GetComponent<Transform>();
        //ThisAgent.updatePosition = false;
        m_OrigGroundCheckDistance = m_GroundCheckDistance;

    }

    // Maths used for forard and turning
    public void Move(Vector3 move) {

        //convert world relative move input vector to local realative
        // turn amount and forward amount needed to move forward 
        if (move.magnitude > 1.0f) move.Normalize();
        move = transform.InverseTransformDirection(move);

        CheckGroundStatus();

        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;

        //Extra rotation by adding to root rotation already in animations
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);

        //UpdateAnimator(move);
        ThisAnimator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        ThisAnimator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        if (move.magnitude > 0.0f) {
            ThisAnimator.speed = m_AnimSpeedMultiplier;
        } else {
            ThisAnimator.speed = 1.0f;
        }

        if (m_ForwardAmount + m_TurnAmount == 0.0f) {
            ThisAnimator.SetBool("Moveing", false);
            ThisAnimator.SetTrigger(IdleHash);
        } else ThisAnimator.SetBool("Moveing", true);

        if (m_IsGrounded && Time.deltaTime > 0.0f) {
            Vector3 v = (ThisAnimator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

            //Keep the Y of the velcity
            v.y = ThisPhysics.velocity.y;
            ThisPhysics.velocity = v;
        }

    }


    void CheckGroundStatus() {
        RaycastHit hitInfo;
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
                case PLAYER_STATE.MOVE:
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
        while (currentState == PLAYER_STATE.MOVE) {
            
            //Set loose search
            ThisLineSight.Sensitity = SightLineDetect.SightSensitivity.LOOSE;

            //Chase to last known position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(ThisLineSight.LastKnowSighting);

            //Wait until path is computed
            while (ThisAgent.pathPending)

                //Set Chase trigger in Animator
                ThisAnimator.SetTrigger(MoveHash);

            yield return null;

            //Have we reached destination?
            if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance) {
                //Stop agent
                ThisAgent.isStopped = true;
                ThisAnimator.SetTrigger(IdleHash);

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
            //ThisAgent.SetDestination(EnemyTransform.position);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //Has player run away?
            if (ThisAgent.remainingDistance > ThisAgent.stoppingDistance) {
                //Change back to chase
                CurrentState = PLAYER_STATE.MOVE;
                yield break;
            } else {
                //Attack
                //EnemyHealth.HealthPoints -= MaxDamage * Time.deltaTime;
            }

            //Wait until next frame
            yield return null;
        }

        yield break;
    }
}

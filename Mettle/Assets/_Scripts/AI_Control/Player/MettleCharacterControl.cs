using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//namespace UnityStandardAssets.Characters.ThirdPerson {
namespace Mettle {

    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(Mettle3PCharacter))]


    public class MettleCharacterControl : MonoBehaviour {

        // Assemble Mettle components
        public UnityEngine.AI.NavMeshAgent MettleAgent { get; private set; } 
        public Mettle3PCharacter MettleChar { get; private set; } 
        public Transform target; 

        // Vision and awarness
        private MettleSight ThisLineSight = null;

        // Enemy Player
        private float attackRange;
        public Transform EnemyTarget;
        public AI_EnemyHealth EnemyHealth;
        public float MaxDamage;

        // Mettle State Machine Animator switching
        private Animator MettleAnimator;
        private Transform MettleTransform;

        bool Moving;

        float HorizontalHash = Animator.StringToHash("Turn");
        float VerticalHash = Animator.StringToHash("Forward");
        int IdleHash = Animator.StringToHash("Idle");
        int MoveHash = Animator.StringToHash("Move");
        int RunHash = Animator.StringToHash("Run");
        int AttackHash = Animator.StringToHash("Attack");
        int OnGroundHash = Animator.StringToHash("OnGround");
        int CrouchHash = Animator.StringToHash("Crouch");
        float JumpHash = Animator.StringToHash("Jump");
        float JumpLegHash = Animator.StringToHash("JumpLeg");


        // Mettle State Machine
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

        private void Start() {   

            // get the components on the object we need ( should not be null due to require component so no need to check )
            MettleAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            MettleChar = GetComponent<Mettle3PCharacter>();
            MettleAnimator = GetComponent<Animator>();
            EnemyHealth = GetComponent<AI_EnemyHealth>();
            EnemyTarget = GetComponent<Transform>();

            MettleAgent.updateRotation = false;
            MettleAgent.updatePosition = true;
        }

        //-------------------------------------
        //-------------------------------------
        // Mettle FSM Coroutines 
        //-------------------------------------
        //-------------------------------------

        // IDLE
        public IEnumerator AIIdle() {
            
            // Loop idle
            while (currentstate == PLAYER_STATE.IDLE && Moving == false) {

                ThisLineSight.Sensitity = MettleSight.SightSensitivity.LOOSE;

                //Stand in place
                MettleAgent.isStopped = true;
                MettleAgent.SetDestination(MettleAnimator.rootPosition);

                yield return null;
            }
        }

        // MOVE
        public IEnumerator AIMove() {

            //Loop while Moveing
            while (currentState == PLAYER_STATE.MOVE && Moving == true) {

                //Set loose search
                ThisLineSight.Sensitity = MettleSight.SightSensitivity.STRICT;

                //Wait until path is computed
                while (MettleAgent.pathPending)

                //Set Move trigger in Animator
                //MettleAnimator.SetTrigger(MoveHash);

                yield return null;

                //Have we reached destination?
                float distTo = Vector3.Distance(MettleTransform.position, MettleAgent.destination);

                if (distTo <= 0.3f) {
                    //Stop agent
                    MettleAgent.isStopped = true;
                    Debug.Log("STOPPED");
                   
                    yield break;
                }


                //Wait until next frame
                yield return null;
            }
        }

        // ATTACK
        public IEnumerator AIAttack() {
            Debug.Log("Attack Started");
            //Loop while chasing and attacking
            while (currentstate == PLAYER_STATE.ATTACK) {
                //Chase to player position
                MettleAgent.isStopped = false;
                MettleAgent.SetDestination(EnemyTarget.position);

                //Wait until path is computed
                while (MettleAgent.pathPending)
                    yield return null;

                //Has player run away?
                if (MettleAgent.remainingDistance > MettleAgent.stoppingDistance) {
                    //Change back to chase
                    CurrentState = PLAYER_STATE.MOVE;
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

        //-------------------------------------
        //-------------------------------------

        //-------------------------------------
        //-------------------------------------

        private void Update() {
            if (target != null)

                MettleAgent.SetDestination(target.position);

            if (MettleAgent.remainingDistance > MettleAgent.stoppingDistance)

                MettleChar.Move(MettleAgent.desiredVelocity, false, false);

            else
                MettleChar.Move(Vector3.zero, false, false);
                


           if(MettleAgent.velocity.magnitude != 0.0f) {

                MettleAnimator.SetTrigger(MoveHash);
                MettleAnimator.ResetTrigger(IdleHash);
                Moving = true;
                MettleAnimator.SetBool("Moving", true);

            } else {

                MettleAnimator.SetTrigger(IdleHash);
                MettleAnimator.ResetTrigger(MoveHash);
                Moving = false;
                MettleAnimator.SetBool("Moving", false);
            }
            
        }


        public void SetTarget(Transform target) {
            this.target = target;
        }

        public void AttackNow() {

            StartCoroutine(AIAttack());

            //Debug.Log("Got the Message");

        }
    }
}


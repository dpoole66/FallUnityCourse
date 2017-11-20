using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//namespace UnityStandardAssets.Characters.ThirdPerson {
namespace Mettle {

    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(Mettle3PCharacter))]


    public class MettleBareControl : MonoBehaviour {

        // Assemble Mettle components
        public UnityEngine.AI.NavMeshAgent MettleAgent { get; private set; } 
        public Mettle3PCharacter MettleChar { get; private set; } 
        public Transform goTarget; 

        // Vision and awarness
        private MettleSight ThisLineSight = null;

        // Enemy Player
        //private float attackRange;
        //public Transform EnemyTarget;
        //public AI_EnemyHealth EnemyHealth;
        //public float MaxDamage;
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



        private void Start() {   

            // get the components on the object we need ( should not be null due to require component so no need to check )
            MettleAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            MettleChar = GetComponent<Mettle3PCharacter>();
            MettleAnimator = GetComponent<Animator>();
            //EnemyHealth = GetComponent<AI_EnemyHealth>();
            //EnemyTarget = GetComponent<Transform>();

            MettleAgent.updateRotation = false;
            MettleAgent.updatePosition = true;
        }


        private void Update() {
            if (goTarget != null)

                MettleAgent.SetDestination(goTarget.position);

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
            this.goTarget = target;
        }

    }
}


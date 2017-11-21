using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//namespace UnityStandardAssets.Characters.ThirdPerson {
namespace Mettle {

    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    //[RequireComponent(typeof(EnemyCharacterControl))]


    public class EnemyBareControl : MonoBehaviour {

        // Assemble Mettle components
        public UnityEngine.AI.NavMeshAgent EnemyMettleAgent { get; private set; }
        public EnemyCharacterControl EnemyMettleChar { get; private set; }

        // Mettle Movement
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
            EnemyMettleAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            EnemyMettleChar = GetComponent<EnemyCharacterControl>();
            MettleAnimator = GetComponent<Animator>();

            EnemyMettleAgent.updateRotation = false;
            EnemyMettleAgent.updatePosition = true;
        }


        private void Update() {
            //if (goTarget != null)

            EnemyMettleAgent.SetDestination(transform.position);

            if (EnemyMettleAgent.remainingDistance > EnemyMettleAgent.stoppingDistance)

                EnemyMettleChar.Move(EnemyMettleAgent.desiredVelocity, false, false);

            else
                EnemyMettleChar.Move(Vector3.zero, false, false);



            if (EnemyMettleAgent.velocity.magnitude != 0.0f) {

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

    }
}


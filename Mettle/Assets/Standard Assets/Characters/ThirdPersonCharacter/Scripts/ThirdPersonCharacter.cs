using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class ThirdPersonCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		Rigidbody m_Rigidbody;
        private UnityEngine.AI.NavMeshAgent ThisAgent = null;
        Animator ThisAnimator;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;

        bool m_Attack;  
        bool m_Chase;
        bool m_Patrol;

        //public Transform PatrolDestination = null;
        public Vector3 velocity;
        //Vector2 velocity = Vector2.zero;     // Original velocity (Vector2)

        //Hashing the Animator params
        int MovemntStageHash = Animator.StringToHash("Movement Stage");
        int PatrolHash = Animator.StringToHash("Patrol");
        int ChaseHash = Animator.StringToHash("Chase");
        int AttackHash = Animator.StringToHash("Attack");


        void Awake()
		{
            ThisAnimator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();   
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            ThisAgent.updatePosition = false;

        }


		public void Move(Vector3 move, bool crouch, bool jump)
		{

			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation();

			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}

		
		void UpdateAnimator(Vector3 move)
		{

            // update the animator parameters
            int roundedValue = Mathf.RoundToInt(velocity.z);
            int moveValue = Mathf.Abs(roundedValue);

            ThisAnimator.SetInteger("VeloX", Mathf.RoundToInt(velocity.x));
            ThisAnimator.SetInteger("VeloZ", Mathf.RoundToInt(velocity.z));
            // Actions

            ThisAnimator.SetTrigger(AttackHash);
            ThisAnimator.SetTrigger(ChaseHash);
            ThisAnimator.SetTrigger(PatrolHash);

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

            //bool Moving = velocity.magnitude > 0.0f;

            if (Input.GetKeyDown(KeyCode.X)) {
                ThisAnimator.SetTrigger(AttackHash);
            }

            // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
            // which affects the movement speed because of the root motion.
            if (m_IsGrounded && move.magnitude > 0)
			{
                ThisAnimator.speed = m_AnimSpeedMultiplier;
               
			}
			else
			{
                // don't use that while airborne
                ThisAnimator.speed = 1;
			}

		}



		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}


		public void OnAnimatorMove()
		{
            Vector3 position = ThisAnimator.rootPosition;
            position = ThisAgent.nextPosition;
            transform.position = position;


            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (m_IsGrounded && Time.deltaTime > 0)
			{
				Vector3 v = (ThisAnimator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}

        }


		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
            #if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
            #endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
                ThisAnimator.applyRootMotion = true;
			}
			else
			{
				m_IsGrounded = false;
				m_GroundNormal = Vector3.up;
                ThisAnimator.applyRootMotion = false;
			}
		}
	}
}

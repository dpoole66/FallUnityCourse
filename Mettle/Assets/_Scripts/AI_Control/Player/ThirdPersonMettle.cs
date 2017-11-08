using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class ThirdPersonMettle : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;

		[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;


		Rigidbody m_Rigidbody;
        private UnityEngine.AI.NavMeshAgent ThisAgent = null;
        Animator m_Animator;

		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
        Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;



        bool m_OnGuard;
        bool m_Attack;
        bool m_AttackThor;
        bool m_Defend;
        bool m_Retire;
        bool m_Patrol;
        Vector2 smoothDeltaPosition = Vector2.zero;


        public Transform PatrolDestination = null;
        Vector2 velocity = Vector2.zero;



        void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


            ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            ThisAgent.updatePosition = false;

        }

        void Update() {

            Vector3 worldDeltaPosition = ThisAgent.destination - transform.position;

            // Map 'worldDeltaPosition' to local space
            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            // Low-pass filter the deltaMove
            float walkDampen = 0.005f;
            float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);

            // Hey, I replaced "smoothDeltaPosition with agent.destination in the Lerp values below
            smoothDeltaPosition = Vector2.Lerp(ThisAgent.destination, deltaPosition, walkDampen);

            // Update velocity if delta time is safe
            // if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;
        }


		public void Move(Vector3 move)
		{
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);

            m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation();

			// send input and other state parameters to the animator
			//UpdateAnimator(move);
		}

		void UpdateAnimator()
		{
            
            // update the animator parameters
            m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
            // Actions
            m_Animator.SetBool("OnGuard", m_OnGuard);
            m_Animator.SetBool("Attack", m_Attack);
            m_Animator.SetBool("Attack_Thor", m_AttackThor);
            m_Animator.SetBool("Defend", m_Defend);
            m_Animator.SetBool("Retire", m_Retire);
            m_Animator.SetBool("Patrol", m_Patrol);
            m_Animator.SetFloat("VeloX", velocity.x);
            m_Animator.SetFloat("VeloY", velocity.y);


            if (Input.GetButtonDown("Patrol")){
                m_Patrol = !m_Patrol;
            }

            if (Input.GetButtonDown("Attack")) {
                m_Attack = !m_Attack;
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
            Vector3 position = m_Animator.rootPosition;
            position = ThisAgent.nextPosition;
            transform.position = position;

            //Patrol
            if (m_Patrol == true) {
                ThisAgent.destination = PatrolDestination.position;
                transform.position = position; 
            }

        }

	}
}

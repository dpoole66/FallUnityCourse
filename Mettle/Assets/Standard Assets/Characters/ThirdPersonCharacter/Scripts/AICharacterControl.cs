using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent ThisAgent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter ThisCharacter { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            ThisAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            ThisCharacter = GetComponent<ThirdPersonCharacter>();

            ThisAgent.updateRotation = false;
            ThisAgent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                ThisAgent.SetDestination(target.position);

            if (ThisAgent.remainingDistance > ThisAgent.stoppingDistance)
                ThisCharacter.Move(ThisAgent.desiredVelocity, false, false);
            else
                ThisCharacter.Move(Vector3.zero, false, false);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}

using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson {
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(Mettle3PCharacter))]
    public class MettleCharacterControl : MonoBehaviour {
        public UnityEngine.AI.NavMeshAgent MettleAgent { get; private set; } // the navmesh agent required for the path finding
        public Mettle3PCharacter MettleChar { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for


        private void Start() {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            MettleAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            MettleChar = GetComponent<Mettle3PCharacter>();

            MettleAgent.updateRotation = false;
            MettleAgent.updatePosition = true;
        }


        private void Update() {
            if (target != null)
                MettleAgent.SetDestination(target.position);

            if (MettleAgent.remainingDistance > MettleAgent.stoppingDistance)
                MettleChar.Move(MettleAgent.desiredVelocity, false, false);
            else
                MettleChar.Move(Vector3.zero, false, false);
        }


        public void SetTarget(Transform target) {
            this.target = target;
        }
    }
}


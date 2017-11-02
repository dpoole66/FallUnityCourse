using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Rachael : MonoBehaviour {

    //Rachael State Machine
    public enum RACHAEL_STATE { PATROL, CHASE, ATTACK };
    private RACHAEL_STATE currentState = RACHAEL_STATE.PATROL;
    //--
    public RACHAEL_STATE CurrentState {

        get { return currentState; }

        set { 
            currentState = value;
            StopAllCoroutines();

            switch(currentState) {
                case RACHAEL_STATE.PATROL:
                    StartCoroutine(AIPatrol());
                break;

                case RACHAEL_STATE.CHASE:
                    StartCoroutine(AIChase());
                break;

                case RACHAEL_STATE.ATTACK:
                    StartCoroutine(AIAttack());
                break;
            }
        }
    }

    //--
    [SerializeField]
    private RACHAEL_STATE currentstate = RACHAEL_STATE.PATROL;
    //Nav Mesh and Patrol var setup:
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    public Transform PatrolDestination = null;

    void Awake() {
        ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start() {
        //Configure starting state
        CurrentState = RACHAEL_STATE.PATROL;
    }

    void Update() {
        ThisAgent.SetDestination(PatrolDestination.position);
    }

    //Co-routines
    public IEnumerator AIPatrol() {
        while(currentState == RACHAEL_STATE.PATROL) {
            yield return null;
        }

    }

    public IEnumerator AIChase() {
        while (currentState == RACHAEL_STATE.CHASE) {
            yield return null;
        }
    }

    public IEnumerator AIAttack() {
        while (currentState == RACHAEL_STATE.ATTACK) {
            yield return null;
        }
    }
}

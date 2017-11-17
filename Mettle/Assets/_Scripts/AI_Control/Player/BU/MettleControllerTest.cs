using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mettle {


public class MettleControllerTest : MonoBehaviour {

    public float strikeDistance = 1.8f;
    public float strikeRate = 0.5f;
    public MettleStrike strikeScript;

    private Animator mettleAnimator;
    private NavMeshAgent mettleAgent;
    private Transform mettlePosition;
    private MettleSight ThisLineSight = null;
    private Transform targetEnemy;
    private Ray mettleRay;
    private RaycastHit mettleHit;
    private bool mettleMoving;
    //private float stopDist;
    int IdleHash = Animator.StringToHash("Idle");
    int MoveHash = Animator.StringToHash("Move");
    int AttackHash = Animator.StringToHash("Attack");
    private bool enemyTargeted;
    private float nextStrike;
    

    // Use this for initialization
    void Awake () {

    mettleAnimator = GetComponent<Animator>();
    mettleAgent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {

        //float stopDist = .Vector3.Distance(mettlePosition.position, mettleAgent.destination);

        //Animator
        Vector3 position = mettleAnimator.rootPosition;
        Vector3 move = mettleAgent.velocity;
  
        position = mettleAgent.nextPosition;
        transform.position = position;
        
        mettleAnimator.SetFloat("Forward", move.z, 0.1f, Time.deltaTime);
        mettleAnimator.SetFloat("Turn", move.x, 0.1f, Time.deltaTime);

        //--------------------------------------------------------

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1")) {


                if (Physics.Raycast(ray, out hit, 100)) {

                        CurrentState = PLAYER_STATE.MOVE;
                        //mettleMoving = true;
                        mettleAnimator.SetTrigger(MoveHash);
                        //enemyTargeted = false;
                        mettleAgent.destination = hit.point;
                        //mettleAgent.isStopped = false;
                }
           
        }

        

        // Note! Below the bool is not just a name but it is given a value defined at top of script.
        //mettleAnimator.SetBool("Moving", mettleMoving);
		
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
                mettleAnimator.SetTrigger(IdleHash);

                ThisLineSight.Sensitity = MettleSight.SightSensitivity.STRICT;

                //Stand in place
                mettleAgent.isStopped = true;
                mettleAgent.SetDestination(mettleAnimator.rootPosition);
                yield return null;

                //If we can see the target then start chasing
                if (ThisLineSight.CanSeeTarget) {
                    mettleAgent.isStopped = true;
                    CurrentState = PLAYER_STATE.MOVE;
                    yield break;
                }

                yield return null;
            }
        }


        public IEnumerator AIMove() {

            //Loop while Moveing
            while (currentState == PLAYER_STATE.MOVE) {

                //Set loose search
                ThisLineSight.Sensitity = MettleSight.SightSensitivity.LOOSE;

                //Move to last known position
                mettleAgent.isStopped = false;
                mettleAgent.SetDestination(mettleAgent.destination);

                //Wait until path is computed
                while (mettleAgent.pathPending)

                //Set Move trigger in Animator
                mettleAnimator.SetTrigger(MoveHash);

                yield return null;

                //Have we reached destination?
                float distTo = Vector3.Distance(mettlePosition.position, mettleAgent.destination);

                if (distTo <= 1.5f) {
                    //Stop agent
                    mettleAgent.isStopped = true;
                    mettleAnimator.SetTrigger(IdleHash);

                    //Reached destination but cannot see player
                    //if (!ThisLineSight.CanSeeTarget)
                    //    CurrentState = PLAYER_STATE.IDLE;
                    //else //Reached destination and can see player. Reached attacking distance
                    //    CurrentState = PLAYER_STATE.ATTACK;

                    yield return null;
                }

                //Wait until next frame
                yield return null;
            }
        }

        public IEnumerator AIAttack() {
            //Loop while chasing and attacking
            while (currentstate == PLAYER_STATE.ATTACK) {


                //Attack standing range
                float attackRange = Vector3.Distance(mettlePosition.position, mettleAgent.destination);

                mettleAgent.isStopped = false;
                //ThisAgent.SetDestination(EnemyTransform.position);

                if (ThisLineSight.CanSeeTarget && attackRange <= 2.0f) {
                    Debug.Log("Range");

                    //Stand in place
                    mettleAgent.isStopped = true;
                    mettleAgent.SetDestination(mettleAnimator.rootPosition);
                    //Set Attack trigger in Animator
                    mettleAnimator.SetTrigger(AttackHash);

                    yield return null;

                }
                //EnemyHealth.HealthPoints -= MaxDamage * Time.deltaTime;

                //Wait until next frame
                yield return null;
            }

            yield break;
        }
    }
}



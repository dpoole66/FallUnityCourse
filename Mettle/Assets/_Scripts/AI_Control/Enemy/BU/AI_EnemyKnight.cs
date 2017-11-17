using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class AI_EnemyKnight : MonoBehaviour {

    //-- Primary body before FSM

    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    public Transform PatrolDestination;
    Animator ThisAnimator;
    public Vector3 velocity;
    bool Idle;
    bool Walk;
    bool Run;
    bool Attack;

    //Hashing the Animator params
    int MovemntStageHash = Animator.StringToHash("Movement Stage");
    int IdleHash = Animator.StringToHash("Idle");
    int WalkHash = Animator.StringToHash("Walk");
    int RunHash = Animator.StringToHash("Run");
    int AttackHash = Animator.StringToHash("Attack");

    //int runStateHash = Animator.StringToHash("Base Layer.Run");


    // Use this for initialization
    void Awake() {
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisAnimator = GetComponent<Animator>();
        ThisAgent.updatePosition = false;
    }

    void FixedUpdate() {

        Vector3 position = ThisAnimator.rootPosition;
        position = ThisAgent.nextPosition;
        transform.position = position;
        velocity = ThisAgent.velocity;

        
        AnimatorStateInfo stateInfo = ThisAnimator.GetCurrentAnimatorStateInfo(0);

        if (Mathf.Abs(velocity.z) == 1.0f && stateInfo.fullPathHash != WalkHash) {    // Set Walk 
            ThisAnimator.SetInteger(MovemntStageHash, 1);
            ThisAnimator.SetBool(WalkHash, true);
        } else {
            //ThisAnimator.SetInteger(WalkHash, 0);
        }

        int roundedValue = Mathf.RoundToInt(velocity.z);
        int moveValue = Mathf.Abs(roundedValue);

        ThisAnimator.SetInteger("VeloX", Mathf.RoundToInt(velocity.x));
        ThisAnimator.SetInteger("VeloZ", Mathf.RoundToInt(velocity.z));

        if (Mathf.Abs(moveValue) == 0) {
            ThisAnimator.SetInteger(MovemntStageHash, 0);
            ThisAnimator.SetBool(IdleHash, true);
        } else {
            ThisAnimator.SetBool(IdleHash, false);
        }

        if (Mathf.Abs(moveValue) == 1) {
            ThisAnimator.SetInteger(MovemntStageHash, 1);
            ThisAnimator.SetBool(WalkHash, true);
        } else {
            ThisAnimator.SetBool(WalkHash, false);
        }

        if (Mathf.Abs(moveValue) == 2) {
            ThisAnimator.SetInteger(MovemntStageHash, 2);
            ThisAnimator.SetBool(RunHash, true);
        } else {
            ThisAnimator.SetBool(RunHash, false);
        }

        if (Mathf.Abs(moveValue) == 3) {
            ThisAnimator.SetInteger(MovemntStageHash, 3);
        }


        if (Input.GetKeyDown(KeyCode.X)) {
            ThisAnimator.SetTrigger(AttackHash);
        }

    }
}

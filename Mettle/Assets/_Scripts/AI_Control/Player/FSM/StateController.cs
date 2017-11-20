using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Mettle;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class StateController : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent MettleAgent { get; private set; }
    public State currentState;

    int AttackHash = Animator.StringToHash("Attack");

    // Enemy Player
    private float attackRange;
    public Transform EnemyTarget;
    public AI_EnemyHealth EnemyHealth;
    public float MaxDamage;

    // Mettle State Machine Animator switching
    private Animator MettleAnimator;
    private Transform MettleTransform;

    // Vision and awarness
    public Transform mettleEyes;    // for Gizmos
    private MettleSight ThisLineSight = null;

    // Mettle Approach state 
    //[HideInInspector] public int ApproachToHere;

    // Mettle FSM active
    private bool mettleOn;

    // Use this for initialization
    void Awake () {
        MettleAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        MettleAnimator = GetComponent<Animator>();  
    }
	
	// Update is called once per frame
	void Update () {

        if (!mettleOn)
            return;
        currentState.UpdateState(this);
		
	}

    void OnDrawGizmos() {
        if(currentState != null && mettleEyes != null) {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(mettleEyes.position, 3.0f);
        }
    }
}

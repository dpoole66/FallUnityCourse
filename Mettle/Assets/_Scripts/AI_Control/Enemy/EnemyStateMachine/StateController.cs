using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Mettle;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class StateController : MonoBehaviour {



    public State currentState;
    public MettleEnemyStats enemyStats;
    public Transform MettleEnemyEyes;
    [HideInInspector] public UnityEngine.AI.NavMeshAgent MettleEnemyAgent { get; private set; }
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;

    // Mettle FSM active
    private bool mettleOn;

    // Use this for initialization
    void Awake () {
        MettleEnemyAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();

    }
	
	// Update is called once per frame
	void Update () {

        if (!mettleOn)
            return;
        currentState.UpdateState(this);
		
	}

    void OnDrawGizmos() {
        if(currentState != null && MettleEnemyEyes != null) {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(MettleEnemyEyes.position, 3.0f);
        }
    }
}

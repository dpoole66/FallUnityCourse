using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class AI_Patrol : MonoBehaviour {


    //private UnityEngine.AI.NavMeshAgent agent;
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    public Transform PatrolDestination = null;

	// Use this for initialization
	void Awake () {
        ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        ThisAgent.nextPosition = PatrolDestination.position;
         
    }
}

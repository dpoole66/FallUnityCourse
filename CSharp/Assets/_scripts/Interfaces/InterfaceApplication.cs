using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceApplication : MonoBehaviour {
    public EnemyOne enemyOne;
    public EnemyTwo enemyTwo;


    // Use this for initialization
    void Start() {

        ((IKillable)enemyOne).Die(); 
        ((IKillable)enemyOne).PowerUp();
        ((IKillable)enemyTwo).Die();
        ((IKillable)enemyTwo).PowerUp();
    }

    // Further testing
    void Update() {
        if  (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Power Up via Space");
        }  
    }
   
}

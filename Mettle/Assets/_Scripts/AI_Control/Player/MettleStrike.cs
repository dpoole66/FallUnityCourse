using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mettle {


public class MettleStrike : MonoBehaviour {

        int enemyMask;
        public GameObject Mettle;
        public Transform EnemyToTarget = null;

        // Use this for initialization
        void Awake() {
            enemyMask = LayerMask.GetMask("Enemy");
            EnemyToTarget = GetComponent<Transform>();

        }

        // Update is called once per frame
        void Update() {
            if (Input.GetButtonDown("Fire2")) {

                Strike();

            }
        }

        public void Strike() {
            Mettle.SendMessage("AttackNow");

            return;

        }
    }

}

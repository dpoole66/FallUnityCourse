using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour {

    public GameObject enemyWeapon;

    void Start() {

        foreach (Transform child in transform) {

            GameObject weapon = Instantiate(enemyWeapon, child.transform.position, Quaternion.identity) as GameObject;
            weapon.transform.parent = child;

        }

    }

}

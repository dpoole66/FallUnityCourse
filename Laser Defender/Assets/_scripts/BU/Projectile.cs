using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage = 100f;
    public int hits = 4;

    public float GetDamage(){
        return damage;
    }

    public int Hits() {
        return hits;
    }
}
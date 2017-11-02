using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour, IKillable {
    private float _health = 100;
    public float Health {
        get { return _health; }
    }

    public void Die() {
        Debug.Log ("Enemy One Dying" + _health);
    }

    public void PowerUp() {
        Debug.Log("Power up Enemy One");
    }
}


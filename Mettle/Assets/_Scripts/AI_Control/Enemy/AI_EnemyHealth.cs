using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI_EnemyHealth : MonoBehaviour {

    public float HealthPoints {

        get { return healthPoints; }
        set {
            healthPoints = value;
            if (HealthPoints <= 0.0f)
                Destroy(gameObject);
        }
    }

    [SerializeField]
    private float healthPoints = 100.0f;

}

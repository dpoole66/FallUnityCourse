using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public GameObject target;
    public LightController lightController;
    public float velocity;
    public Light enemySpot;
    float damage = 5.0f;


    // Use this for initialization
    void Start() {
        enemySpot.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Vector3.Distance(transform.position, target.transform.position) < 8) {
            Vector3 targetPos = (target.transform.position - transform.position).normalized;
            enemySpot.enabled = true;
            //transform.rotation = Quaternion.LookRotation(targetPos);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos), Time.deltaTime);

        }
        if (Vector3.Distance(transform.position, target.transform.position) < 4) {
            Vector3 targetPos = (target.transform.position - transform.position).normalized;
            transform.position += targetPos * Time.deltaTime * velocity;
            
            lightController.LightMagic();

        }
    }

    // Inflict Damage
    void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<SetHealth>();



    }
}

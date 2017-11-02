using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerBUinvertedFire : MonoBehaviour {


    public GameObject playerWeapon;

    public GameObject enemyWeapon;
    public float fireRate = 0.1f;
    public float weaponSpeed = 3.0f;

    public float health = 500.0f;

    void OnTriggerEnter2D(Collider2D collision) {

        Projectile myHit = playerWeapon.GetComponent<Projectile>();

        
        //Note, This is what I found worked on v.2017. Replaceing "Projectile missile =". 
        if (myHit) {
            health -= myHit.GetComponent<Projectile>().GetDamage();
            Debug.Log(health);
            if (health <= 0) {
                Destroy(gameObject);
            }

        }
    }

    void EnemyFire() {
        GameObject enemylazer = Instantiate(enemyWeapon, transform.position, Quaternion.identity) as GameObject;
        enemylazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -weaponSpeed);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            InvokeRepeating("EnemyFire", 0.001f, fireRate);
        }

        if (Input.GetKeyUp(KeyCode.X)) {
            CancelInvoke("EnemyFire");
        }

    }


}

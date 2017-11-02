using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    
    public GameObject playerWeapon;
    public float enemySpeed;
    public GameObject enemyWeapon;
    public float fireRate = 0.1f;
    public float weaponSpeed;
    public float health = 500.0f;
    
    void OnStart() {
           
    }

    void OnTriggerEnter2D(Collider2D collider) {
       
        Projectile myHit = playerWeapon.GetComponent<Projectile>();


        //Note, This is what I found worked on v.2017. Replaceing "Projectile missile =". 
        if (myHit) {
            health -= myHit.GetComponent<Projectile>().GetDamage();
            Debug.Log(health);
            if (health <= 0){
                Destroy(gameObject);
            }
            
        }
    }

    void FireController() {
        GameObject lazer = Instantiate(enemyWeapon, transform.position, Quaternion.identity) as GameObject;
        lazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, weaponSpeed);
    }

}
    


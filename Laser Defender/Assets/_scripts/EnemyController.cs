using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour {

    public GameObject enemyWeapon;
    public float fireRate = 2.0f;
    public float weaponSpeed = 3.0f;
    public float health = 500.0f;
    public int scoreValue = 100;
    public GameObject playerWeapon;
    public AudioClip soundEnemyLaser;
    public AudioClip soundEnemyHit;
    public AudioClip soundEnemyDeath;

    private ScoreKeeper scoreKeeper;

    void Start(){
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D collision) {

        LazerOne myHit = playerWeapon.GetComponent<LazerOne>();
        
        Destroy(collision.gameObject);
        

        // Get the scoreboard menu item named "Score" above with GameObject.Find("").
        // this gets the script "ScoreKeeper" that's attached to the object.
        // This now attaches the scoreKeeper script to this function (below) with
        // scoreKeeper (my new local var).Score (the text object) and adds the scoreValue
        // into the text.
        // Note, This is what I found worked on v.2017. Replaceing "Projectile missile =". 
        if (myHit) {
            health -= myHit.GetComponent<LazerOne>().GetDamage();
            AudioSource.PlayClipAtPoint(soundEnemyHit, transform.position);

            if (health <= 0) {
                Die(); 
            }

        }
    }

    void EnemyFire() {
        GameObject enemylazer = Instantiate(enemyWeapon, transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
        enemylazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -weaponSpeed);

        AudioSource.PlayClipAtPoint(soundEnemyLaser, transform.position);
    }

    void Update() {

        float probability = Time.deltaTime * fireRate;

        if (Random.value < probability) {
            EnemyFire();
        }

        

    }

    void Die() {
        AudioSource.PlayClipAtPoint(soundEnemyDeath, transform.position);
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour   {
    //    static MusicPlayer instance = null;
    //    public GameObject enemyShip;
    public float playerSpeed;
    public GameObject playerWeapon;
    public float fireRate = 0.01f;
    public float weaponSpeed = 3.0f;
    public AudioClip soundFireLaser;
    public AudioClip soundPlayerHit;
    public AudioClip soundPlayerAlarm;
    public AudioClip soundPlayerOneDies;
    public float countdown = 4.0f;

    public GameObject enemyWeapon;

    private int delay = 3;
    private int health = 500;
    float xMin;
    float xMax;

    private HealthKeeper healthKeeper;

    void Start(){
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0.03f, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(0.97f, 0, distance));
        xMin = leftMost.x;
        xMax = rightMost.x;

        // Health UI via "HealthKeeper" script
        healthKeeper = GameObject.Find("Health").GetComponent<HealthKeeper>(); 
    }

    void Fire(){
        GameObject lazer = Instantiate(playerWeapon, transform.position, Quaternion.identity) as GameObject;
        lazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, weaponSpeed);

        AudioSource.PlayClipAtPoint(soundFireLaser, transform.position);
    }

	void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            InvokeRepeating("Fire", 0.001f, fireRate);
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            CancelInvoke("Fire");
        }

		if (Input.GetKey (KeyCode.LeftArrow)) {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }
		else if (Input.GetKey(KeyCode.RightArrow)){
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }

        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }


    void OnTriggerEnter2D(Collider2D collision) {

        LazerOne enemyHit = enemyWeapon.GetComponent<LazerOne>();

        Destroy(collision.gameObject);

        //Note, This is what I found worked on v.2017. Replaceing "Projectile missile =". 
        if (enemyHit) {

            health -= enemyHit.GetComponent<LazerOne>().GetDamage();
            AudioSource.PlayClipAtPoint(soundPlayerHit, transform.position);
            healthKeeper.HealthScore(health);
            if (health <= 100) {
                AudioSource.PlayClipAtPoint(soundPlayerAlarm, transform.position);
            }
            if (health <= 0) {
                Die();   
            }

        }
    }

    void Die() {
        
        AudioSource.PlayClipAtPoint(soundPlayerOneDies, transform.position);
        countdown -= Time.deltaTime;
        Destroy(gameObject);
        LoadLose();
   
    }

    void LoadLose() {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("Lose Screen");
    }

    public int GetHealth() {
        return health;
    }

}




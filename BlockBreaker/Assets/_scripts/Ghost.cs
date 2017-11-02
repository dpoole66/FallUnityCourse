using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public AudioClip Bones;
    public Sprite[] hitSprites;
    public static int breakableCount = 0;

    public float force;
    // public Rigidbody2D rb;

    public GameObject ghost;

    private LevelManager levelManager;

    private int timesHit;
    private int spriteIndex;
    private bool isBreakable;

    // Use this for initialization
    void Start()
    {
        isBreakable = (this.tag == "Breakable");
        // Tracking breakables 
        if (isBreakable)
        {
            breakableCount++;
        }

        timesHit = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        // rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(Bones, transform.position, 1.7f);
        if (isBreakable)
        {
            HandleHits();
        }
    }

    void HandleHits()
    {
        timesHit++;
        // Declare and Assign maxHits here 
        int maxHits = hitSprites.Length + 1;

        if (timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();

            GhostSmoke();

            Destroy(gameObject);

            // rb.AddForce(transform.up * force);
        }

        else
        {
            LoadSprites();
        }
    }

    // Sprite Loader
    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex])
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
    }

    void GhostSmoke() {

        GameObject ghostPuff = Instantiate(ghost, gameObject.transform.position, Quaternion.identity);

        ParticleSystem.MainModule main = ghostPuff.GetComponent<ParticleSystem>().main;
        main.startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void SimulateWin()
    {
        levelManager.LoadNextLevel();
    }
}

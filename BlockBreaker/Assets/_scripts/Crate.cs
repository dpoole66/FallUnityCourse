using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public AudioClip crateHit;
    public Sprite[] hitSprites;
    public static int breakableCount = 0;

    public float force;
   // public Rigidbody2D rb;

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

    void Update()   {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crateHit, transform.position);
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

    void SimulateWin()
    {
        levelManager.LoadNextLevel();
    }
}

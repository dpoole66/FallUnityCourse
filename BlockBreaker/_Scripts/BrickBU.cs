using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public int maxHits;
    public Sprite[] hitSprites;
    public bool Tombstone;

    private int timesHit;
    private LevelManager levelManager;

    private int spriteIndex;

	// Use this for initialization
	void Start () {
        if (Tombstone == false) {
            timesHit = 0;
        }
        else {
            timesHit = -1;
        }

        levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

    void Update()   {
        
    }

    void OnCollisionEnter2D(Collision2D collision)  {

        timesHit++;
        Debug.Log("Hit" + timesHit);

        if (timesHit >= maxHits)   {
            Destroy(gameObject);
        }

        else {
            LoadSprites();
        }
    }

    // Sprite Loader
    void LoadSprites() {
        spriteIndex = timesHit -1;


        this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    void SimulateWin(){
        levelManager.LoadNextLevel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour {

    public AudioClip tombstone;
    public Rigidbody2D rb;
    public Brick grave;

    private LevelManager levelManager;

    private bool isToombstone;

    // Use this for initialization
    void Start()
    {
        isToombstone = (this.tag == "Tombstone");
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        grave = GameObject.FindObjectOfType<Brick>();

    }

    void Update()   {
       
    }

    public void TombstoneDrop()    {
        if (isToombstone)   {
            rb.isKinematic = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)  {
        AudioSource.PlayClipAtPoint(tombstone, transform.position, 1.5f);
        rb.isKinematic = false;
    }


}
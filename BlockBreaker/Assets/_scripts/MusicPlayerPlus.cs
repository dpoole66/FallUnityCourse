using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerPlus : MonoBehaviour {

    //Static instance for one-off MusicPlayer
    static MusicPlayerPlus instance = null;


    // Singlton Pattern
    void Awake()    {
        if (instance != null){
            Destroy(gameObject);
           // Debug.Log("Destroying the DUPLICATE." + GetInstanceID());
        }
        
        else   {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        //Debug.Log("Music Player START!" + GetInstanceID());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerBU : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;

    private AudioSource audioSource;

    private void OnEnable() {

        LevelManager.OnSceneChange += MusicChange;
    }

    private void OnDisEnable() {

        LevelManager.OnSceneChange -= MusicChange;
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Don't destory on load " + name);
        LevelManager.OnSceneChange += MusicChange;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void MusicChange(int level) {
        AudioClip currentMusic = levelMusicChangeArray[level];

        if (currentMusic) {
            audioSource.clip = currentMusic;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Playing Clip " + currentMusic);
        }
    }
    
}


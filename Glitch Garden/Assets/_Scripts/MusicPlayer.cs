using UnityEngine;
using System.Collections;


public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip aboutClip;

    private AudioSource music;

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Music Playey: Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
		}
		
	}

    private void OnLevelWasLoaded(int level) {
        Debug.Log("MusicPlayer: loaded level " + level);
        music.Stop();

        if (level == 0) {
            music.clip = startClip;
        }

        if(level == 1) {
            music.clip = gameClip;
        }

        if(level == 2) {
            music.clip = winClip;
        }

        if(level == 3) {
            music.clip = loseClip;
        }

        if(level == 4) {
            music.clip = aboutClip;
        }
        music.loop = true;
        music.Play();
    }
}

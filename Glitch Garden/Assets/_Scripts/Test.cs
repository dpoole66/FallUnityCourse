using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefsManager.GetMasterVolume();
        //PlayerPrefsManager.SetMasterVolume(0.3f);
        //print(PlayerPrefsManager.GetMasterVolume());

        //print(PlayerPrefsManager.IsLevelUnlocked(4));
        PlayerPrefsManager.UnlockLevel(4);
        //print(PlayerPrefsManager.IsLevelUnlocked(4));

        PlayerPrefsManager.GetDifficulty();
        //PlayerPrefsManager.SetDifficulty(1);
        //print(PlayerPrefsManager.GetDifficulty());

    }
	
	// Update is called once per frame
	void Update () {
		
	}


}

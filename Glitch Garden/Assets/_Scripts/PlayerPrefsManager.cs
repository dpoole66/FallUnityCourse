using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFF_KEY = "difficulty";
    const string LEVEL_KEY = "level_unlocked_";


    // MASTER VOLUME Wrapper Get/Set

    

    public static void SetMasterVolume(float volume) {

        if (volume >= 0.0f && volume <= 1.0f) {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        } else {
            Debug.LogError("Master Volume out of range");
        } 
    }

    public static float GetMasterVolume() {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }


    // DIFFICLUTY WRAPPER
    // DIFFICULTY(Get)
    public static float GetDifficulty() {
        return PlayerPrefs.GetFloat(DIFF_KEY);
    }

    // DIFFICULTY(Set)
    public static void SetDifficulty(float difficulty) {
        if (difficulty >= 0.0f && difficulty <= 3.0f) {
            PlayerPrefs.SetFloat(DIFF_KEY, difficulty);
        } else {
            Debug.LogError("Difficulty Out of Range");
        }
    }


    // LEVEL UNLOCKING WRAPPER

    // (Set-Unlock) returns nothing only sets (chagnes) level in PlayerPrefs
    public static void UnlockLevel(int level) {
        if (level <= SceneManager.sceneCountInBuildSettings - 1) {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);  // Use 1 for true (no bools).
        } else {
            Debug.LogError("Trying to Unlock level not in Build Order");
        }
    }
    // (Get-Lock/Unlock) returns a bool to key
    public static bool IsLevelUnlocked(int level) {

        int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());
        bool IsLevelUnlocked = (levelValue == 1);

        if (level <= SceneManager.sceneCountInBuildSettings - 1) {
            return IsLevelUnlocked;
        } else {
            Debug.Log("Can't return a level not in build order");
            return false;
        }
    }
}

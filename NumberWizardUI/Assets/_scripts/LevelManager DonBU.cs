using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string name){
        Debug.Log("Level Load Requested for " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest(){
        Debug.Log("I Want to Quit!");
        Application.Quit();
    }

}
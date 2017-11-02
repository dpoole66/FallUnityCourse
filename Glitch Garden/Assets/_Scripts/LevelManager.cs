using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float autoLoadNextLevelAfter;
    private int _activeSceneBuildIndex = 0;

    public delegate void SceneChange(int level);
    public static event SceneChange OnSceneChange;

    private void Start() {
        if (autoLoadNextLevelAfter == 0) {
        } else {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }

        if(OnSceneChange != null) {
            OnSceneChange(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Initialise() {
        Scene activeScene = SceneManager.GetActiveScene();
        _activeSceneBuildIndex = activeScene.buildIndex;
    }

    public void LoadLevel(string name){
		SceneManager.LoadScene (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void LoadNextLevel() {
        SceneManager.LoadSceneAsync (++_activeSceneBuildIndex);
    }

}

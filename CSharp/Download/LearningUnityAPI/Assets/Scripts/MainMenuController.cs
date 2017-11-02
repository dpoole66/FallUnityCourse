using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

 	public void GoToGameplay ()
	{
		// change the scene
		Application.LoadLevel ("Gameplay");
	}

	public void QuitGame ()
	{
		Application.Quit ();
	}
}

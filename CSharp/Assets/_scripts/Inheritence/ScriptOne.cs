using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptOne : MonoBehaviour {
    public string text;

	// Use this for initialization
	void Start () {
        if (GetComponent<ScriptTwo>() == null) {
            GameObject.FindObjectOfType<ScriptTwo>().text = "Script One found me";
        }
    }

    public string Hello() {
       return "Hello from Script One";
    }
}

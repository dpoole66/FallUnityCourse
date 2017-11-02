using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTwo : MonoBehaviour {
    public string text;
    public ScriptOne scriptOne;

	void Start () {
        GetComponent<Text>().text = scriptOne.Hello();
    }
}

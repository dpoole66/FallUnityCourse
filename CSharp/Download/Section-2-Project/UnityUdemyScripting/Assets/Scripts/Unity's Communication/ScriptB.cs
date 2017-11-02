using UnityEngine;
using System.Collections;

public class ScriptB : MonoBehaviour {

	public ScriptA scriptA;

	// Use this for initialization
	void Start () {
		scriptA.text = scriptA.Hello ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

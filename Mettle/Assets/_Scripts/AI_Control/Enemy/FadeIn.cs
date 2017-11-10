using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    public Renderer ThisRenderer;     // the mesh below the enemy root

    // Use this for initialization
    void Start () {

        ThisRenderer = GetComponent<Renderer>();
        //FadeObject.a = 0;
        //FadeObject.enabled = true;    
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {
            StartCoroutine(FadingIn(0.5f));
        }

    }

    IEnumerator FadingIn(float time) {
        float i = 0;
        float rate = 1 / time;

        while(i < 1) {
            ThisRenderer.material.color = Color.Lerp(Color.black, Color.red, rate);
            i += Time.deltaTime * rate;
            yield return 0;
        }
    }
}

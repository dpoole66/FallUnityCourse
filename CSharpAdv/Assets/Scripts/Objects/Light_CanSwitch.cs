using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_CanSwitch : MonoBehaviour {

    public GameObject Player;
    private Light _light;
    //On/Off
    public float onDelay;
    public float offDelay;

    //public float minFlick;
    public float duration;
    public float flickTime;
    

    void Awake() {
        _light = GetComponentInParent<Light>();

        _light.enabled = false;
    }

    void Start() {
        StartCoroutine(LightsOFF(0.0f));
    }

    void Update() {
        
    }


    //--
    //--
    IEnumerator LightsON (float onDelay) {
        StartCoroutine(LightsFlicker(duration, flickTime));
        //_light.enabled = false;
        yield return new WaitForSeconds(2);
        //Debug.Log("Flicker");
       // yield return new WaitForSeconds(2.0f);
       // Debug.Log("Light");
       // _light.enabled = true;
    }

    IEnumerator LightsOFF(float offDelay) {
        
        yield return new WaitForSeconds(offDelay);
        _light.enabled = false;
    }

    IEnumerator LightsFlicker(float duration, float flickTime) {
        
        while (duration > 0f) {
            duration -= Time.deltaTime;
            _light.enabled = !_light.enabled;
            yield return new WaitForSeconds(flickTime);
        }

        _light.enabled = true;
        
        //if (true) {
        //    yield return new WaitForSeconds(Random.Range(minFlick,maxFlick));
        //    _light.enabled = !_light.enabled;
        //}
    }

    IEnumerator Counter() {
        for (int i = 0; i <= 10; i++) {
            print(i);
            yield return new WaitForSeconds(1.0f);
        }  
    }
    //--
    //--

    //-
    void OnTriggerEnter(Collider Col) {
        if (!Col.CompareTag("Witches"))
            return;

        StartCoroutine(LightsON(onDelay));

    }

    void OnTriggerExit(Collider Col) {
        if (!Col.CompareTag("Witches"))
            return;

        StartCoroutine(LightsOFF(offDelay));
    }
    //-
}

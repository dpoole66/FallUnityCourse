using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Can : MonoBehaviour {

    public static Light CanisterLight = null;
 

    void Awake() {
        CanisterLight = this.GetComponent<Light>();
        CanisterLight.enabled = false;
    }

    void Update() {
        
    }

    public static void LightSwitchOn() {
        CanisterLight.enabled = true;   
        Debug.Log("ON");

    }

    public static void LightSwitchOff() {
        CanisterLight.enabled = false;
        Debug.Log("OFF");

    }
}

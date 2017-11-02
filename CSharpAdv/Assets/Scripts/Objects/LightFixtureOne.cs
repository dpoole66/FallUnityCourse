using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFixtureOne : MonoBehaviour {

    private static Light CanisterLight = null;


    void Awake() {
        CanisterLight = this.GetComponent<Light>();
        CanisterLight.enabled = false;
    }

    public static void  LightSwitchOn() {
        CanisterLight.enabled = true;

    }

    public static void LightSwitchOff() {
        CanisterLight.enabled = false;

    }







    //void OnTriggerEnter (Collider other) {
    //    Debug.Log("Light On");
    //    if(other.gameObject.name == "TurretZone") {
    //        TurretLight.SetActive(false);
    //    }
    //}

    //void OnTriggerStay(Collider other) {
    //    Debug.Log("Light Remainging On");
    //    GunLight.enabled = true;
    //}

    //void OnTriggerExit(Collider other) {
    //    Debug.Log("Light Off");
    //    GunLight.enabled = false;
    //}
}

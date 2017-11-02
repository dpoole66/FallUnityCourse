using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour {
    public GameObject Player;


    void OnTriggerEnter(Collider Col) {
        if (!Col.CompareTag("Rachael"))
            return;

        DeerLight.LightSwitchOn();
    }

    void OnTriggerExit(Collider Col) {
        if (!Col.CompareTag("Rachael"))
            return;
        DeerLight.LightSwitchOff();
    }
}

    //    Debug.Log("Light Off");
    //    GunLight.enabled = false;
    //


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


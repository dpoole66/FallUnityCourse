using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

    public GameObject Player;

    void OnTriggerEnter(Collider Col) {
        if (!Col.CompareTag("Rachael"))
            return;

        LightFixtureOne.LightSwitchOn();
        Debug.Log("ON");
    }

    void OnTriggerExit(Collider Col) {
        if (!Col.CompareTag("Rachael"))
            return;

        LightFixtureOne.LightSwitchOff();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomB_LStrigger : MonoBehaviour {

    public GameObject Player;

    void OnTriggerEnter(Collider Col) {
        if (!Col.CompareTag("Rachael"))
            return;

        RoomB_LS.LightSwitchOn();
        Debug.Log("ON");
    }

    void OnTriggerExit(Collider Col) {
        if (!Col.CompareTag("Rachael"))
            return;

        RoomB_LS.LightSwitchOff();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomB_LS : MonoBehaviour {

    private static Light _OverHeadLight = null;


    void Awake() {
        _OverHeadLight = this.GetComponent<Light>();
        _OverHeadLight.enabled = false;
    }

    public static void LightSwitchOn() {
        _OverHeadLight.enabled = true;

    }

    public static void LightSwitchOff() {
        _OverHeadLight.enabled = false;

    }
}

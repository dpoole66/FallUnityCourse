using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour {

    void OnTriggerStay(Collider GO) {
        Debug.Log(GO.name);
    }
}

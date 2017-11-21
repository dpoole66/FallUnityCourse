using System;
using UnityEngine;

namespace Mettle {
    [RequireComponent(typeof(MettleCharacterControl))]
    public class PlaceTargetWithMove : MonoBehaviour {
        public float surfaceOffset = 0.1f;
        public GameObject setTargetOn;

           // Update is called once per frame
           private void Update() {
            if (!Input.GetMouseButtonDown(0)) {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit) ) {
                return;
            }
            transform.position = hit.point + hit.normal * surfaceOffset;
            if ((setTargetOn != null) && (hit.transform.gameObject.tag == "Ground")) {
                setTargetOn.SendMessage("SetTarget", transform);
                Debug.Log(hit.transform.gameObject.tag);
            }

        }
    }

}

    




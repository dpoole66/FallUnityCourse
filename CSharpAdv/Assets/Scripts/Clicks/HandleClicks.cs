using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandleClicks : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData EventData) {
        Debug.Log(EventData.pointerPress.name);
    } 
}

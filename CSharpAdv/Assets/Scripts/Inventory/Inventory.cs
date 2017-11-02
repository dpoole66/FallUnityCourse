using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    //Create a Singelton object to make sure there is only one Inventory
    public static Inventory Instance {

        get {
            if (ThisInstance == null) {
                GameObject InventoryObject = new GameObject("Inventory");
                ThisInstance = InventoryObject.AddComponent<Inventory>();
            }

            return ThisInstance;
        }
    }

   // Reference to singelton object
    private static Inventory ThisInstance = null;

    // Root object of item list
    public RectTransform ItemList = null;

	// Use this for initialization
	void Awake () {
		
        if(ThisInstance != null) {
            DestroyImmediate(gameObject);
            return;
        }

        ThisInstance = this;
	}
    //----------------------------------------------------------
    // Adding items to Inventory
    public static void AddItem(GameObject GO) {
        //disable colliders on added obj
        foreach (Collider C in GO.GetComponents<Collider>())
            C.enabled = false;

        //disable renderers
        foreach (MeshRenderer MR in GO.GetComponents<MeshRenderer>())
            MR.enabled = false;

        //fill the slots
        for (int i = 0; i < ThisInstance.ItemList.childCount; i++) {

            Transform Item = ThisInstance.ItemList.GetChild(i);

            if (!Item.gameObject.activeSelf) {
                Item.GetComponent<Image>().sprite = GO.GetComponent<InventoryItem>().GUI_Icon;
                Item.gameObject.SetActive(true);
                return;
            }
        }
		
	}
}

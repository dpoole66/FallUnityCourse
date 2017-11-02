using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {

    public bool ShowCursor;
    public float Sensitivity;
    private Transform ThisTransform = null;
    // Use this for initialization
    void Awake() {
        ThisTransform = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start () {
        if(ShowCursor == false ) {
            Cursor.visible = false;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        float newRotationY = transform.localEulerAngles.y + Input.GetAxis("Horizontal") * Sensitivity;
        float newRotationX = transform.localEulerAngles.x + Input.GetAxis("Vertical") * Sensitivity;

        ThisTransform.localEulerAngles = new Vector3(newRotationX, newRotationY);
	}
}

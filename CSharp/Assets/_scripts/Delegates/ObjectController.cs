using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    Renderer objRenderer;

	void Start () {
        DelegateHandler.ghostDelegate += ChangeColor;
        DelegateHandler.ghostDelegate += ChangePosition;
        DelegateHandler.ghostDelegate += PlaySound;
        objRenderer = GetComponent<Renderer>();
	}

    void ChangePosition() {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.0f);
    }

    void ChangeColor() {
        objRenderer.material.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }

    void PlaySound() {
        AudioSource soundBite = GetComponent<AudioSource>();
        soundBite.Play();
    }

    private void OnDisable() {
        DelegateHandler.ghostDelegate -= ChangeColor;
        DelegateHandler.ghostDelegate -= ChangePosition;
        DelegateHandler.ghostDelegate -= PlaySound;
    }

}

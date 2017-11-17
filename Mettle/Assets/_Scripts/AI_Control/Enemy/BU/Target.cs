using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public TextMesh text;
    public int health = 100;
    public void TakeDamage(int Amount) {
        health -= Amount;
        if (health <= 0) {
            Destroy();
        }
    }

    void Update() {
        text.GetComponent<TextMesh>().text = health.ToString("00") + "%";
    }

    void Destroy() {
        Destroy(this.gameObject);
    }
}

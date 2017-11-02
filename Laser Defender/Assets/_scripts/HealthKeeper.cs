using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthKeeper : MonoBehaviour {
    public GameObject player;
    public static int healthScore = 500;
    private Text myText;

    void onStart() {
       
    }

    private void Start() {
        myText = GetComponent<Text>();
        myText.text = healthScore.ToString();
    }

    public void HealthScore(int health) {

        healthScore += health;
        myText.text = health.ToString();
    }

    public void Reset() {
        healthScore = 0;
        myText.text = healthScore.ToString();
    }
}
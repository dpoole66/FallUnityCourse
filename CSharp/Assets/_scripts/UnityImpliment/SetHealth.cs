using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHealth : MonoBehaviour {
    public Image healthBar;
    public float max_health = 100.0f;
    public float cur_health = 50.0f;


	// Use this for initialization
	void Start () {
        cur_health = max_health;
        SetHealthBar();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetHealthBar() {
        float my_health = cur_health / max_health;
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(my_health, 0.0f, 1.0f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}

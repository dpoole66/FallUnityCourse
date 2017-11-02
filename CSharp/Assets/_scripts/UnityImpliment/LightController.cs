using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public Light stageLight;



	// Use this for initialization
	void Start () {
        stageLight.type = LightType.Point;
        stageLight.color = Color.white;
       
		
	}
	
	// Update is called once per frame
	void Update () {
	 if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("LightMagic", 0.05f, 0.05f);  
        }	

     if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("LightMagic");
        }
	}

   public  void LightMagic() {
        float red = Random.Range(0f, 1f);
        float green = Random.Range(0f, 1f);
        float blue = Random.Range(0f, 1f);

        stageLight.color = new Color(red, green, blue, 1);
    }
}

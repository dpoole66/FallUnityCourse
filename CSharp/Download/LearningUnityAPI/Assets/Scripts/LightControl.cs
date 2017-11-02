using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour {

	private Light light;
	public Camera cam;

	// Use this for initialization
	void Start () {
		light = GetComponent <Light>();
		light.intensity = light.intensity * 3;
	}
	
	// Update is called once per frame
	void Update () {
		float red = Random.Range (0f, 1f);
		float green = Random.Range (0f, 1f);
		float blue = Random.Range (0f, 1f);
		light.color = new Color (red, green, blue, 1);
	}
}

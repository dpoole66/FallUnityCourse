using UnityEngine;
using System.Collections;

public class RaycastController : MonoBehaviour {

	public Camera cam;
	public GameObject prefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray raycastRay = cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool collision = Physics.Raycast (raycastRay.origin, raycastRay.direction, out hit);
		if (collision == true)
		{
			// place a prefab in the scenario
			// change the prefab's position to be the hit point
			GameObject currentInstantiatedPrefab = (GameObject)Instantiate (prefab, hit.point, Quaternion.identity);
			Destroy (currentInstantiatedPrefab, 2);
		}
	}
}

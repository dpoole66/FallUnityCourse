using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public GameObject target;
	public float velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, target.transform.position) > 3)
		{
			// execute this if distance between target and enemy is greather than 3
			Vector3 ep = (target.transform.position - transform.position).normalized;
			transform.position += ep * Time.deltaTime * velocity;
			transform.rotation = Quaternion.LookRotation (ep);
		}
		else
		{
			//this is going to be executed if distance is less than 3
			print ("You've lost the game");
		}
	}
}

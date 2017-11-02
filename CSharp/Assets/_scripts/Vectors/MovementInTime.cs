using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInTime : MonoBehaviour {

    public Vector3 target;
    public float duration;

	// Use this for initialization
	void Start () {
        StartCoroutine(MoveTo(target, duration));
	}
	
    public IEnumerator MoveTo(Vector3 target,  float duration) {    
        Vector3 diff = (target - transform.position); 
        float counter = 0;
        while (counter <= duration) {
            float step = (diff.magnitude * Time.deltaTime) / duration;
            transform.position += diff.normalized * step;
            counter += Time.deltaTime;
            yield return null;
        }
        transform.position = target;     
    }
}

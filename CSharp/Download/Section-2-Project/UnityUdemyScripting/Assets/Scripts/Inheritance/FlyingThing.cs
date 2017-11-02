using UnityEngine;
using System.Collections;

public class FlyingThing {
	protected float velocity;
	public float currentHeight;

	public float Velocity {
		get { return velocity; }
	}

	public FlyingThing (float Velocity) {
		velocity = Velocity;
	}

	/*public virtual void Fly () {
		// A virtual method is allowed to have a body
	}*/

	public virtual void Fly ()
	{
		MonoBehaviour.print ("Generalized way of flying");
	}
}

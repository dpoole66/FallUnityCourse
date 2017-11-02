using UnityEngine;
using System.Collections;

public class Bird: FlyingThing {
	public bool isHungry;
	public bool isThirsty;

	public Bird (float Velocity): base (Velocity)
	{
	}

	public override void Fly ()
	{
		base.Fly ();
		MonoBehaviour.print ("Flying as a bird");
	}
}

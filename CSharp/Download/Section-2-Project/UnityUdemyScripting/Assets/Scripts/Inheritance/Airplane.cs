using UnityEngine;
using System.Collections;

public class Airplane: FlyingThing {
	public float currentFuel, maxFuel;
	public int passengerCapacity;
	public string company;

	public Airplane (float MaxFuel, int PassengerCapacity, string Company, float Velocity): base (Velocity) {
		maxFuel = MaxFuel;
		passengerCapacity = PassengerCapacity;
		company = Company;
	}

	public override void Fly ()
	{
		base.Fly ();
		MonoBehaviour.print ("Flying as an airplane");
	}
}

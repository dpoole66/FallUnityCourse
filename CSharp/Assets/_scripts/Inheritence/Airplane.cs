using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Airplane : Flight    {

    public float fuelCurrent, fuelMax;
    public int capacity;
    public string airline;

    public Airplane (float Fuel, float MaxFuel, int Capacity, string Airline,
        float Velocity): base(Velocity) {
        fuelCurrent = Fuel;
        fuelMax = MaxFuel;
        capacity = Capacity;
        airline = Airline;
        velocity = Velocity;
    }

   public override void Fly() {
        base.Fly();
        MonoBehaviour.print ("Airplane is flying");
  }

}

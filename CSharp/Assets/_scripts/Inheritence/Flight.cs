using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight {

    public float velocity;
    public float altitude;

    public Flight(float Velocity) {
        velocity = Velocity;
    }

    public virtual void Fly() {
        MonoBehaviour.print("Base Flight"); 
    }

}

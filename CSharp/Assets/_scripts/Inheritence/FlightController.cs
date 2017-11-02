using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController  {

    public float velocity;
    public float altitude;
    public Vector3 location;


    public FlightController(float Velocity, float Altitude, Vector3 Location) {
        velocity = Velocity;
        altitude = Altitude;
        location = Location;
    }

}
             
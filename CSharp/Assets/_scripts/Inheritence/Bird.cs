using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Bird : Flight {

    public float stamina;  
    public string type;

    public Bird(float Stamina,  string Type, float Velocity) : base (Velocity) {
        stamina = Stamina; 
        type = Type;
        velocity = Velocity;
            }

    public override void Fly() {
        base.Fly();
        MonoBehaviour.print("Bird is flying");
    }

}
 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable {
    float Health {
        get;
    }

    void Die();

    void PowerUp();
}

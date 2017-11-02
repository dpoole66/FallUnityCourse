using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateHandler : MonoBehaviour {

    public delegate void GhostDelegate();
    public static GhostDelegate ghostDelegate;

    public void Ghost() {
        ghostDelegate();
    }
}

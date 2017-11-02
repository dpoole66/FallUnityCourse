using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

    int guessRemaining;
    public int maxGuessesAllowed;

    public Text numRemaining;


    void Countdown()    {
        numRemaining.text = maxGuessesAllowed.ToString();
    }

    void Uddate()   {
        Countdown();
    }
}

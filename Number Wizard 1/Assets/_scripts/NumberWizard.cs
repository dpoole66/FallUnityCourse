using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour {

    // Use this for initialization

    int max;
    int min;
    int guess;


    void Start() {
        StartGame();

    }

    void StartGame()
    {
        // note the min/max/guess vars are declaired at init but assigned here.
        max = 1000;
        min = 1;
        guess = 500;

        print("========================");
        print("Welcome to Number Wizard");
        // ask the player for a number
        print("Pick a number, any number but do not tell me what it is.");


        // tell the player the range
        print("The higest number you can pick is " + max);
        print("The lowest number you can pick is " + min);

        // prompt the player with input choices (affordance)
        print("Is the number higher or lower than " + guess);
        print("up arrow = higher, down arrow = lower, return = equal");

        max = max + 1;

    }

    // Update is called once per frame
    void Update()   {
        if (Input.GetKeyDown(KeyCode.UpArrow))  {
           // guessing logic up
            min = guess;
            guess = (min + max) / 2;
            NextGuess();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            // guessing logic down
            max = guess;
            NextGuess();
        } else if (Input.GetKeyDown(KeyCode.Equals))    {
            print("equal key was pressed");
        } else if (Input.GetKeyDown(KeyCode.Return))    {
            print("I Won!");
           // DestroyCurtain();
            StartGame();
        }
    }

    void NextGuess()
    {
        guess = (min + max) / 2;
        print("Higher or lower than " + guess);
        print("up arrow = higher, down arrow = lower, return = equal");
    }

   
}

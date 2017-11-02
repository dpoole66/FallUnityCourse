using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour {

    // Use this for initialization

    int max = 1000;
    int min = 1;
    int guess;
    public int maxGuessesAllowed = 10;
    public Text theGuess;


    void Start()    {
        StartGame();
    }

    void StartGame()    {
        max = 1001;
        min = 1;
        NextGuess();
    }

    // Guess LOWER
    public void GuessLower()   {
        min = guess;
        NextGuess();
    }

    // Guess HIGHER
    public void GuessHigher()  {
        max = guess;
        NextGuess();
    }

    // Next Guess 
    void NextGuess()    {
        guess = Random.Range(min, max +1);
        theGuess.text = guess.ToString();
        maxGuessesAllowed = maxGuessesAllowed -1;

        if(maxGuessesAllowed <= 0)  {
            SceneManager.LoadScene("Win");
        }
    }

 }

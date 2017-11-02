using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberWizardPlus : MonoBehaviour {

    // Use this for initialization

    int max = 1000;
    int min = 1;
    int guess;
    int count;
    public int maxGuessesAllowed = 10;
    public Text guesses;
    public Text counter;


    void Start() {
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
        print("Lower");
    }

    // Guess HIGHER
    public void GuessHigher()  {
        max = guess;
        NextGuess();
        print("Higher");
    }

    // Next Guess 
    void NextGuess()    {
        guess = Random.Range(min, max +1);
        guesses.text = guess.ToString();
        maxGuessesAllowed = maxGuessesAllowed -1;

        if(maxGuessesAllowed <= 0)  {
            SceneManager.LoadScene("Win");
        }

        counter.text = count.ToString();
        print(count);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PriestLogic : MonoBehaviour {

    int min;
    int max;
    int guess;
    int remGuesses;
    public int maxGuesses = 10;
    public Text currentGuess;
    public Text remainingGuesses;



	void Start ()   {
        GameStart();
	}
	
	void GameStart()    {
        min = 1;
        max = 1001;
        NextGuess();
    }

    void NextGuess()    {
        guess = Random.Range(min, max + 1);
        currentGuess.text = guess.ToString();
        maxGuesses = maxGuesses - 1;

        if (maxGuesses <= 0)    {
            SceneManager.LoadScene("Win");
        }
    }

    public void GuessLower()   {
        max = guess;
        NextGuess();
        Counter();
    }

    public void GuessHigher()  {
        min = guess;
        NextGuess();
        Counter();
    }

    public void Counter(){
        remGuesses = maxGuesses;
        remainingGuesses.text = remGuesses.ToString();
    }
}

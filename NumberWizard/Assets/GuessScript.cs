using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuessScript : MonoBehaviour {

    int min = 1, max = 1000, guess = 500, tries = 5;
    public Text guessText;
    public Text tryText;

    void Start()
    {
        guessText.text = guess.ToString();
        tryText.text = "Tries Left: " + tries.ToString();
    }



    public void Lower()
    {
        max = guess;
        NextGuess();

    }

    public void Higher()
    {
        min = guess;
        NextGuess();
    }

    void NextGuess()
    {
        
        
        if (tries < 1)
        {
            SceneManager.LoadScene("Win");
        }
        else
        {
            guess = (max + min) / 2;
            guessText.text = guess.ToString();
            tries--;
            tryText.text = "Tries Left: "+tries.ToString();
        }
        

    }
}

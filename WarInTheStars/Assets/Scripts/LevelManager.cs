using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {


    //public  Text scoreText;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            Text levelText = GameObject.Find("LevelText").GetComponent<Text>();
            levelText.text = SceneManager.GetActiveScene().name.Replace("_", " ");
        }
    }

    public void LoadLevel(string name)
    {

        
        SceneManager.LoadScene(name);

    }

    public void QuitRequest()
    {
        Debug.Log("Quit request");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BrickDestroyed()
    {

            if (SceneManager.GetActiveScene().name == "Level_05")
            {
                LoadLevel("Win");
            }
            else
            {
                LoadNextLevel();
            }
        
    }
}

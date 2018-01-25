using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {
    public static int lifeCount = 4;
    public static int score = 0;
    public static Text lifeText;
    public static Text scoreText;
    
    static GameStats instance = null;

    void Awake() //earlier than start
    {
       
        if (instance != null)
        {
            Destroy(gameObject);
      
        }
        else
        {

            instance = this;
            GameObject.DontDestroyOnLoad(gameObject); //gameObject is the gamestats object in unity
        }
    }

    // Use this for initialization
    void Start () {

        lifeText = GameObject.Find("LifeCount").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        lifeText = GameObject.Find("LifeCount").GetComponent<Text>();
        lifeText.text = lifeCount.ToString();

        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = score.ToString();
    }
}

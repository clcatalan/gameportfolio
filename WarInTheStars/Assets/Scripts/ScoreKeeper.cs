using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    // Use this for initialization
    Text scoreText;
    public static int score = 0;
	void Start () {
        scoreText = gameObject.GetComponent<Text>();
        scoreText.text = score.ToString();
	}

    public static void AddScore(int points)
    {
        score += points;
        

    }

    void Reset()
    {
        score = 0;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {
    private LevelManager levelManager;
    private Ball ball;
    private Paddle paddle;
    private Vector3 paddleToBallVector;


    private void Start()
    {

        levelManager = GameObject.FindObjectOfType<LevelManager>();
        ball = GameObject.FindObjectOfType<Ball>();
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //because there's no collision, since rigid body an static, refer to the table
        //this will be called if one is a trigger
        print("Trigger");
        GameStats.lifeCount--;
        if (GameStats.lifeCount < 0)
        {
            levelManager.LoadLevel("Lose");
        }
        else
        {
            GameStats.lifeText.text = GameStats.lifeCount.ToString();
            ball.transform.position = paddle.transform.position + paddleToBallVector;
            ball.hasStarted = false;
        }

        //levelManager.LoadLevel("Lose");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this will be called if both aren't triggers
        print("collision");
    }
}

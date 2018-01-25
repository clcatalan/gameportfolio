using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    float mousePosInBlocks;
    public bool autoPlay = false;

    private Ball ball;

    void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update () {
        if (!autoPlay)
        {
            MoveWithMouse();
        }
        else
        {
            AutoPlay();
        }
	}

    void AutoPlay()
    {

        Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);
        Vector3 ballPos = ball.transform.position;
        paddlePos.x = Mathf.Clamp(ballPos.x, 0.5f, 15.5f);
        this.transform.position = paddlePos;
    }

    void MoveWithMouse()
    {
        //Input.mousePosition/Screen.width = [0,1] relative mouse position to the entire screen width
        //Input.mousePosition/Screen.width * 16 = tells us how many game units we have therefore mouseposition in game units

        mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;

        //this refers to the current object, the paddle
        Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);
        paddlePos.x = Mathf.Clamp(mousePosInBlocks, 0.5f, 15.5f); //these two values are the bounds in the scene
        this.transform.position = paddlePos;
    }
}

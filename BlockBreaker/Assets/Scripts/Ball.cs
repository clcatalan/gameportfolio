using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
    private Paddle paddle;
    private Vector3 paddleToBallVector;
    public bool hasStarted = false;
    //public Text lifeText;
   
    // Use this for initialization
    void Start () {
 
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
        
	}

    

    // Update is called once per frame
    void Update () {
        if (!hasStarted)
        {
            //lock ball with paddle
            this.transform.position = paddle.transform.position + paddleToBallVector;
            //if left click, start game
            if (Input.GetMouseButtonDown(0))
            {
   
                hasStarted = true;
                //trigger bounce
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);

            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 tweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f)); //to prevent infinite loops when bouncing balls
       
        if (hasStarted)
        {
            this.GetComponent<Rigidbody2D>().velocity += tweak;
            this.GetComponent<AudioSource>().Play();
        }
    }
}

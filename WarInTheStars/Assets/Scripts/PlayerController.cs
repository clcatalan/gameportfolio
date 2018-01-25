using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float padding = 1f; //makes sure the player is always seen in camera
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate = 0.2f;
    public float health;
    public AudioClip fireSound;
    public AudioClip deathSound;
    Text lifeText;

    float xmin;
    float xmax;

    // Use this for initialization
    void Start () {
        /*
         x, y coordinates relative to the size of the screen
         z distance b/w camera and object you want to find out
        cornermost:
        Q1 = 1,1
        Q2 = 0,1
        Q3 = 0,0
        Q4 = 1,0
        center = 0.5, 0.5
         */

        lifeText = GameObject.Find("Life").GetComponent<Text>();
        lifeText.text = health.ToString();
        float distance = transform.position.z - Camera.main.transform.position.z; // the z value
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance)); //x = 0 because left most is 0, y = 0, because we dont care, not moving along y-axis
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)); //x = 0 because left most is 0, y = 0, because we dont care, not moving along y-axis
        xmin = leftmost.x + padding;
        xmax = rightmost.x -  padding;
    }

    // Update is called once per frame
    void Update () {
        if (EnemySpawner.countdown <= 0)
        {
            movePlayer();
        }
    }

    void Fire()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject beam = Instantiate(projectile, transform.position+offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void movePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //newPosition = new Vector3(-speed * Time.deltaTime, y, 0);
            transform.position += Vector3.left * speed * Time.deltaTime; //same effect as above code

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

        }
        //transform.position += newPosition; //transform is this attached object's transform component

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, -4, 0);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Projectile missile = collision.GetComponent<Projectile>(); //see if it is of type projectile
        if (missile) //if it exists
        {
            health -= missile.GetDamage(); //decrement health of this (enemy)
            lifeText.text = health.ToString();
            missile.Hit();
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                Destroy(gameObject); //destroy this (enemy)
                LevelManager lm = new LevelManager();
                lm.LoadLevel("Win");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour { //EnemyFormation
    Projectile missile;
    public float health = 150f;
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate = 50f;
    public float shotsPerSeconds = 0.5f;
    public int scoreValue = 150;
    public AudioClip fireSound;
    public AudioClip deathSound;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //p(of firing at this frame) = time elapsed * frequency;
        float probability = shotsPerSeconds * Time.deltaTime;
        if (Random.value < probability)
        {
            EnemyFire();
        }
       
    }

    void EnemyFire()
    {
        //Vector3 startPosition = transform.position + new Vector3(0, -1, 0); //offset, to make sure, missile is spawned below ships
        //GameObject enemyMissile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        GameObject enemyMissile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;

        enemyMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed); //changes direction already
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision); //object this collided with
        missile = collision.GetComponent<Projectile>(); //see if it is of type projectile
        if (missile) //if it exists
        {
            health -= missile.GetDamage(); //decrement health of this (enemy)
            missile.Hit();
            if(health <= 0)
            {
                ScoreKeeper.AddScore(scoreValue);
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                
                Destroy(gameObject); //destroy this (enemy)
                

            }
        }
    }
}

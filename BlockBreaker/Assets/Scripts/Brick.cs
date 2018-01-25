using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour {
    public int brickPoints;
    public AudioClip crack;
    public Sprite[] hitSprites;
    private int timesHit; //number of times it has been hit
    private LevelManager levelManager;
    public static int breakableCount = 0;
    private bool isBreakable;
    public GameObject smoke;
    
    
    // Use this for initialization
    void Start () {
        isBreakable = (this.tag == "Breakable");
        if (isBreakable)
        {
            breakableCount++;
            
        }
        timesHit = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        AudioSource.PlayClipAtPoint(crack, transform.position); //transform.position is position of brick
        if (isBreakable)
        {
            HandleHits();
        }
    }



    void HandleHits()
    {
        int maxHits = hitSprites.Length + 1; //allowable hits
        timesHit++;
        if (timesHit >= maxHits)
        {
            if (this.name.Contains("purple"))
            {
                GameStats.lifeCount++;
                GameStats.lifeText.text = GameStats.lifeCount.ToString();
            }
            breakableCount--;
            levelManager.BrickDestroyed();
            //print(breakableCount);
            GameObject smokePuff = Instantiate(smoke, gameObject.transform.position, Quaternion.identity);

            ParticleSystem.MainModule settings = smokePuff.GetComponent<ParticleSystem>().main;
            settings.startColor = new ParticleSystem.MinMaxGradient(gameObject.GetComponent<SpriteRenderer>().color);
            GameStats.score += brickPoints;
            //levelManager.scoreText.text = GameStats.score.ToString();
            Destroy(gameObject);
            
        }
        else
        {
            LoadSprites();
        }
    }

    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex])
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else
        {
            Debug.LogError("Brick sprite missing");
        }
    }




}

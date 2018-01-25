using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour { //FormationController
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public int speed = 3;
    float xmin;
    float xmax;
    public float padding = 5;
    bool moveRight, moveLeft;
    public float spawnDelay = 0.5f;
    public static int  countdown = 3;
    Text countdownText;

    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z; //distance to camera
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)); //x = 0 because left most is 0, y = 0, because we dont care, not moving along y-axis
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)); //x = 0 because left most is 0, y = 0, because we dont care, not moving along y-axis
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
        moveRight = true;
        moveLeft = false;
        countdownText = GameObject.Find("Countdown").GetComponent<Text>();
        countdownText.text = countdown.ToString();
        StartCoroutine("Countdown");
        //SpawnUntilFull();

	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height)); //adds an outline around the formation
    }

    // Update is called once per frame
    void Update () {
   
        countdownText.text = countdown.ToString();
        
        if (countdown <= 0)
        {
            countdownText.text = "START!";   
            if(countdown<0)
            {
                StopCoroutine("Countdown");
                countdownText.text = "";
            }
 
            if (!moveLeft && moveRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if (moveLeft && !moveRight)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }


            float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
            transform.position = new Vector3(newX, 0, 0);

            if (transform.position.x <= xmin || transform.position.x >= xmax)
            {

                moveRight = !moveRight;
                moveLeft = !moveLeft;
            }


            if (AllMembersDead())
            {
                SpawnUntilFull();
            }
        }
        


    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform) //transform is the child of this EnemyFormation object
        {

            if (childPositionGameObject.childCount == 0) //if position has no child, we should add an enemy here
            {
                return childPositionGameObject; //so we return the vacant position
            }
        }

        return null;
    }

    bool AllMembersDead()
    {
 
        foreach(Transform childPositionGameObject in transform) //transform is the child of this EnemyFormation object
        {
            
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }



    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition(); //if there's a freeposition returned
        if (freePosition) //spawn enemy
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition; //enemy object will be child of enemyFormation object
            Invoke("SpawnUntilFull", spawnDelay);
        }

        /*if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }*/


    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            countdown--;
        }
    }
}

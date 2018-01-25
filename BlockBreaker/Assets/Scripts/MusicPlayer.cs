using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    static MusicPlayer instance = null;

    void Awake() //earlier than start
    {
       
        if (instance != null)
        {
            Destroy(gameObject);
            print("game object destroyed");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject); //gameObject is the musicplayer object in unity
        }
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

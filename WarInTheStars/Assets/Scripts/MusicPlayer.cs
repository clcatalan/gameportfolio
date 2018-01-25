using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip instructionsClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;
    void Awake() //earlier than start
    {
       
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            print("game object destroyed");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject); //gameObject is the musicplayer object in unity
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }

    private void OnLevelWasLoaded(int level) //the level that has been loaded
    { //when you load the first level the first time, this does not get called
        Debug.Log("MusicPlayer: loaded level "+level);
        music.Stop(); //stop the current music playing
        if (level == 0)
        {
            music.clip = startClip;
        }
        if (level == 1)
        {
            music.clip = instructionsClip;
        }
        if (level == 2)
        {
            music.clip = gameClip;
            music.volume = 0.1f;
        }
        if (level == 3)
        {
            music.clip = endClip;
        }
        music.loop = true;
        music.Play();
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

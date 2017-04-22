using UnityEngine;
using System.Collections;

public class Buttonmanager : MonoBehaviour {

    public AudioClip A_clip1,A_clip2;
    public AudioSource Audio;
	// Use this for initialization
	void Start () {        
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void back()
    {
        Audio.PlayOneShot(A_clip2);
        Application.LoadLevel(0);
    }
    public void start()
    {        
        Audio.PlayOneShot(A_clip1);
        Application.LoadLevel(1);
    }

}

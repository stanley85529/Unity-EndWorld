using UnityEngine;
using System.Collections;

public class cam : MonoBehaviour {

    public Camera camera;
    public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        camera.transform.position = new Vector3(35.8f, 18f+player.transform.position.y/3f, player.transform.position.z / 1f);
        if (camera.transform.position.z<-30)
        {
            camera.transform.position = new Vector3(35.8f, 18f + player.transform.position.y / 3f, -30);
        }
        if(camera.transform.position.z>17)
        {
            camera.transform.position = new Vector3(35.8f, 18f + player.transform.position.y / 3f, 17);
        }
       // camera.transform.position = new Vector3(35.8f, 19.09f, player.transform.position.z / 1f);
	}

}

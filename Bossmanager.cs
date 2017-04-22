using UnityEngine;
using System.Collections;

public class Bossmanager : MonoBehaviour {
    public static Bossmanager Instance;
    public GameObject boss1, boss2;
    public int t = 0;
    public bool b1=false;
	// Use this for initialization
	void Start () {
        Instance = this;
        boss1.SetActive(false);
        boss2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (t == 100)
        {
            if (b1)
            {
                boss2.SetActive(true);
            }
            else
            {
                boss1.SetActive(true);
            }
        }
        else if(t<100)
        {
            t++;
        }
        

    }
    public void bos1()
    {
        boss1.SetActive(false);
        b1 = true;
        t = 0;
    }
}

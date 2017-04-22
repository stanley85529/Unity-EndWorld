using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {
    public static Boss1 Instance;
    public GameObject funnel;
    public GameObject par;
    public GameObject[] ful = new GameObject[8];
    public int[] ran = new int[8];
    public int t;
    public int fun_count;
	// Use this for initialization
	void Start () {
        Instance = this;
        fun_count = 8;        
        for (int i = 0; i < 8; i++)
        {
            Vector3 point = transform.position + new Vector3(0, 0, -50 + 15 * i);
            GameObject go = (GameObject)Instantiate(funnel, point, Quaternion.identity);
            go.transform.parent = transform;           
            go.transform.rotation = Quaternion.Euler(0, 90, 90);           
            go.transform.localScale = go.transform.localScale * 20;
            ful[i] = go;
        }
        t = 1;
        forRandom();
    }
	
	// Update is called once per frame
	void Update () {
        if (t == 100)
        {
            call(2);            
        }       
        else if (t % 200 == 0)
        {
            if (PlayerCtrl1.Instance.funnel_count <= 1 && fun_count>0)
            {
                call(1);
            }
            else if(PlayerCtrl1.Instance.funnel_count==0 && fun_count == 0)
            {
                Bossmanager.Instance.bos1();
            }
        }
        t++;
        
    }
    void call(int count)
    {
        for(int i = 0; i < count; i++)
        {
            fun_count--;
            PlayerCtrl1.Instance.funnel(ful[ran[fun_count]]);
            PlayerCtrl1.Instance.funnel_count++;
            
        }
    }
    void forRandom()
    {
        for(int i = 0; i < 8; i++)
        {
            ran[i] = i;
        }
        for (int i = 0; i < 8; i++)
        {
            int c = ran[i];
            int t = Random.Range(0, 7);
            ran[i] = ran[t];
            ran[t] = c;
        }
    }

}

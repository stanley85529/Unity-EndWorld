using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public static Enemy Instance;
    public float hp;
    public GameObject funnel;
    public GameObject[] ful = new GameObject[8]; // 儲存浮游砲
    public int interval;    //浮游砲距離本體距離 供日後調整方便用
    public int ful_count; // 當前浮游砲到哪個
    public int t; //發射時間控制
    //public Vector3[] point = new Vector3[8];
    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start () {
        hp = 50;
        ful_count = 0;
        for (int i = 0; i < 8; i++)
        {
            Vector3 point = this.transform.position + new Vector3(0, Mathf.Sin(-Mathf.PI + Mathf.PI / 4 * i) * interval+2f, Mathf.Cos(-Mathf.PI + Mathf.PI / 4 * i) * interval);
            GameObject go = (GameObject)Instantiate(funnel, point , Quaternion.identity);
            go.transform.parent = transform;
            if (transform.position.z > 0)
            {
                go.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                go.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            go.transform.localScale = go.transform.localScale * 10;
            ful[i] = go;
        }
    }
	
	// Update is called once per frame
	void Update () {

        /*if (Vector3.Distance(gameObject.transform.position,PlayerCtrl1.Instance.transform.position)<20 && PlayerCtrl1.Instance.funnel_count<5 && t%200==0)
        {
            PlayerCtrl1.Instance.funnel(ful[ful_count++]);
        }
        t++;*/
        if (Input.GetKeyDown(KeyCode.F) && PlayerCtrl1.Instance.funnel_count<5)
        {
            for(int i = 0;i< 8; i++)
            {
                if (ful[i] != null && ful[i].transform.parent==transform)
                {
                    PlayerCtrl1.Instance.funnel(ful[i]);
                    PlayerCtrl1.Instance.funnel_count++;
                    break;
                }
            }
           /* PlayerCtrl1.Instance.funnel(ful[ful_count++]);
            PlayerCtrl1.Instance.funnel_count++;*/
        }

    }
    public void setdamage()
    {

    }
}

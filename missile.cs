using UnityEngine;
using System.Collections;

public class missile : MonoBehaviour {
    
    public float atk;
    public int t;
    void Start()
    {
        atk = PlayerCtrl1.Instance.missile_atk;
        if (atk > 2)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void Update()
    {
        if (t < 50)
        {
            t++;
        }
        else
        {
            PlayerCtrl1.Instance.lv2.SetActive(false);
            Destroy(gameObject);
        }
    }
   /* void OnCollisionEnter(Collision col)
    {
        PlayerCtrl1.Instance.lv2.SetActive(false);
        if (col.collider.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy" || col.tag=="Funnel")
        {
            return;
        }
        PlayerCtrl1.Instance.lv2.SetActive(false);
        Destroy(this.gameObject);
           
    }*/
}

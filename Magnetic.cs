using UnityEngine;
using System.Collections;

public class Magnetic : MonoBehaviour {
    public Line line;
    private int t;
	// Use this for initialization
	void Start () {
        t = 0;
	}
	
	// Update is called once per frame
	void Update () {
        t++;
        if (t > 100)
        {
            line.go = null;
            Destroy(this.gameObject);
        }
	}
    void OnTriggerEnter(Collider col)
    {           
        if (col.tag == "hook_point")
        {
            Debug.Log("Hook");
            line.go = null;
            line.Target = col.gameObject;
            PlayerCtrl1.Instance.rb.useGravity = false;
            PlayerCtrl1.Instance.hook_status = true;
            PlayerCtrl1.Instance.hook_point = col.transform.position;
           // PlayerCtrl1.Instance.magnetic(col.transform.position);
            Destroy(this.gameObject);
        }
        /*else if (col.tag == "Funnel")
        {
            Debug.Log("Funnel");
            /*if (col.transform.parent == null)
            {
                return;
            }*/
           /* col.GetComponent<Funnel>().enabled = true;
            col.GetComponent<Funnel>().Go_Magnetic();
            line.Target = col.gameObject;
            line.T = 0;
            line.go = null;
            Destroy(this.gameObject);
        }   */
        else if(col.tag=="Ground")
        {
            Destroy(gameObject);
        }     
    }
}

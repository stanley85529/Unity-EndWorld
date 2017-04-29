using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RO11 : MonoBehaviour {
    public GameObject Target;
    public float MoveSpeed;
    public float FindRange;
    public float MoveRange;
    public int track;
    public bool Lock;
    public bool Patrol=false;
    public bool ATK;
    public bool stop;
    private Rigidbody Rib;
    private Animator anim;
	// Use this for initialization
	void Start () {
        Rib = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        setmoverang();
    }

    // Update is called once per frame
    void Update()
    {
        search();
        
        if (track < MoveRange && !stop)
        {
            if (Patrol)
            {
                Rib.velocity = new Vector3(0, 0, MoveSpeed);
            }
            else
            {
                Rib.velocity = new Vector3(0, 0, -MoveSpeed);
            }

        }
        else if (track > MoveRange)
        {            
            setmoverang();
        } 
        
        track++;
    }
    void RotationRO11()
    {
        Rib.velocity = Vector3.zero;
        Patrol = !Patrol;
        anim.Play("Return");
       // track = 0;
        stop = true;
    }
   /* void OnTriggerEnter(Collider col)
    {
        if (col.tag == "wall")
        {
            RotationRO11();
        }
    }*/
    void setmoverang()
    {
        RotationRO11();
        MoveRange = Random.Range(300, 600);
        track = -100;        
    }
    void search()
    {
        if (transform.position.z < 55)
        {
            RotationRO11();
            transform.position = new Vector3(0, 4, 55);
        }
        else if(transform.position.z>100)
        {
            RotationRO11();
            transform.position = new Vector3(0, 4, 100);
        }
    }
    void return_Fin()
    {
        stop = false;
        gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.y == 1 ? 0 : 180, 0);
       // Debug.Log (gameObject.transform.rotation.y);
    }
    float findPlayer()
    {
        return Mathf.Abs(PlayerCtrl1.Instance.transform.position.z - gameObject.transform.position.z);
    }
}

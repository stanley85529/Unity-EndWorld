using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RO11 : MonoBehaviour {
    public GameObject Target;
    public float MoveSpeed;
    public float FindRange;
    public bool Lock;
    public bool Patrol=false;
    private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Patrol)
        {
            rigidbody.velocity = new Vector3(0, 0, MoveSpeed);
        }
        else
        {
            rigidbody.velocity = new Vector3(0, 0, -MoveSpeed);
        }
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "wall")
        {
            Patrol = !Patrol;
        }
    }
}

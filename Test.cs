using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    public Material[] mater = new Material[2];

    int i = 0;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "knife")
        {
            //Debug.Log("knife");
            //GetComponent<SkinnedMeshRenderer>().sharedMaterial = mater[i];
            GetComponent<Animator>().Play("xxx");

        }
    }
}

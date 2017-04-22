using UnityEngine;
using System.Collections;
public class BackGroundLoop : MonoBehaviour {
	public float speed = 0.2f;
	void Update () {
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0f, Time.time * speed);
	}
}
using UnityEngine;
using System.Collections;

public class PlayerCtrl2 : MonoBehaviour {
	public Animator Anim;
	public AnimatorStateInfo BS;
	static int Idle = Animator.StringToHash("Base Layer.Idle");
	static int RunR = Animator.StringToHash("Base Layer.RunR");
	static int Run = Animator.StringToHash("Base Layer.Run");
	static int Jump = Animator.StringToHash("Base Layer.Jump");
	static int AirFire = Animator.StringToHash("Base Layer.Air Fire");
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//跳躍
		Anim.SetBool( "Jump", false );
		if( Input.GetKeyDown (KeyCode.W))
		{
				Anim.SetBool( "Jump", true);
		}
		//跑步

		Anim.SetBool( "RunR", false );
		if( Input.GetKey( KeyCode.D ))
		{
			Anim.SetBool( "RunR", true );
		}
		Anim.SetBool( "Run", false );
		if(Input.GetKey( KeyCode.A ))
		{
			Anim.SetBool( "Run", true );
		}


		Anim.SetBool( "AirFire", false);
		if( Input.GetKeyDown (KeyCode.Space))
		{
			Anim.SetBool( "AirFire", true);
		}
	}
}
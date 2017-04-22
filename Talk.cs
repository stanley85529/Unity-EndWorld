using UnityEngine;
using System.Collections;

public class Talk : MonoBehaviour
{
	void OnTriggerEnter(Collider aaa) //aaa為自定義碰撞事件
	{    
		if (aaa.gameObject.name == "Talk1") //如果aaa碰撞事件的物件名稱是CubeA
		{    
			print("OK"); //在除錯視窗中顯示OK
		}
	}
}
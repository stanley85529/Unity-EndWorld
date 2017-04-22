using UnityEngine;
using System.Collections;

public class BoosAI : MonoBehaviour
{
	
	//敌人状态
	
	//敌人站立状态
	public const int STATE_STAND = 0;
	//敌人行走
	public const int STATE_WALK = 1;
	//敌人奔跑状态
	public const int STATE_RUN = 2;
	//记录敌人的当前状态
	private int enemyState;
	//主角对象
	private GameObject hero;
	//备份上一次的敌人思考时间
	private float backUptime;
	//敌人思考下一次行为的时间
	public const int AI_THINK_TIME = 2;
	//敌人的巡逻范围
	public const int AI_ATTACK_DISTANCE = 10;
	
	// Use this for initialization
	void Start()
	{
		//得到主角对象
		hero = GameObject.Find("Cube");
		//设置敌人的默认状态站立
		enemyState = STATE_STAND;
	}
	
	// Update is called once per frame
	void Update()
	{
		//判断敌人与主角的距离
		if (Vector3.Distance(transform.position, hero.transform.position) <
		    (AI_ATTACK_DISTANCE))
		{
			//敌人进入奔跑状态
			gameObject.GetComponent<Animation>().Play("run");
			enemyState = STATE_RUN;
			//设计敌人面朝主角方向
			transform.LookAt(hero.transform);
		}
		//敌人进入巡逻状态
		else
		{
			//计算敌人的思考时间
			if (Time.time - backUptime >= AI_THINK_TIME)
			{
				//敌人开始思考
				backUptime = Time.time;
				
				//取得0~2之间的随机数
				int rand = Random.Range(0,2);
				
				if (rand == 0)
				{
					//敌人进入站立状态
					gameObject.GetComponent<Animation>().Play("idle");
					enemyState = STATE_STAND;
				}
				
				else if (rand == 1)
				{
					//敌人进入站立状态
					//敌人随机旋转角度
					Quaternion rotate = Quaternion.Euler(0,Random.Range(1,5) * 90,0);
					//1秒内完成敌人旋转
					transform.rotation = Quaternion.Slerp(transform.rotation,rotate,Time.deltaTime * 1000);
					//播放行走动画
					gameObject.GetComponent<Animation>().Play("walk");
					enemyState = STATE_WALK;
				}
			}
		}
		switch (enemyState)
		{
		case STATE_STAND:
			break;
		case STATE_WALK:
			//敌人行走
			transform.Translate(Vector3.forward * Time.deltaTime);
			break;
		case STATE_RUN:
			//敌人朝向主角奔跑
			if (Vector3.Distance(transform.position, hero.transform.position) > 3)
			{
				transform.Translate(Vector3.forward * Time.deltaTime * 3);
			}
			break;
			
		}
	}
}
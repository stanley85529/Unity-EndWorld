using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCtrl1 : MonoBehaviour
{
    public static PlayerCtrl1 Instance;
    public Animator Anim;
   // public AnimatorStateInfo BS;
  //  public Collider col;
    public bool OnGround;
    public Rigidbody rb;
    public int atk_Combo;
    public int atk_stage;
    public bool direction; // false = 右 true =左
    public float power;
    public GameObject projectile;
    public GameObject Shield;
    public GameObject lv2,lv3R,lv3L;
    public bool shield_status;
    public int shield_t;
    public Vector3[] funnel_point = new Vector3[5]; //儲存玩家周圍浮游砲Vector3
    public bool[] funnel_status = new bool[5];      //該點位是否有浮游砲
    public GameObject funnel_parent;                //浮游砲參考點 定位跟砲擊使用
    public int funnel_count;                        //玩家身邊浮游砲數量
    public int interval;                            //浮游砲座標計算時距離中心的距離
    public BoxCollider Knife;
    public float hp;
    public float ATK;
    public bool hook_status;
    public Vector3 hook_point;
    public int t,t1;
    public int m;
    public int missile_atk;
    public bool down;
    public bool stop;
   // public int jump_stage;
   // public int stay_ground; // 黏滯時間
    public int jump;
    public bool missile_direction;

    public GameObject[] mi = new GameObject[3];
    static int IdleR = Animator.StringToHash("Base Layer.IdleR");
    static int IdleL = Animator.StringToHash("Base Layer.IdleL");
    static int RunR = Animator.StringToHash("Base Layer.RunR");
    static int RunL = Animator.StringToHash("Base Layer.RunL");
    static int JumpR = Animator.StringToHash("Base Layer.JumpR");
    static int JumpL = Animator.StringToHash("Base Layer.JumpL");
    static int AirFireR = Animator.StringToHash("Base Layer.Air FireR");
    static int AirFireL = Animator.StringToHash("Base Layer.Air FireL");
    public AudioSource Audio;
    public AudioClip[] A_clip=new AudioClip[10];
    // Use this for initialization    
   // public ArrayList Enemys = new ArrayList();

    /*public Image[] HpBar = new Image[6];
    private Color32[] Hp_Color = new Color32[6];
    public GameObject PowerBar;
    public int[] Power_point = { 850, 720, 650, 450, 330, 150, 0 };*/
    private Color32[] Power_Color = new Color32[3];
    private int[] sword = { 1, 1, 3 }; //進戰武器傷害
    private float[] projectile_atk = { 0.5f, 2, 5 }; //飛彈蓄能傷害


    /*void Awake()
    {
        
        LoadEnemys();
    }*/
    void Start()
    {        
        Instance = this;
        power = 0;
        funnel_count = 0;
        hp = 3;
        missile_atk = 0;
        //jump_stage = 0;
        rb = GetComponent<Rigidbody>();
        LoadEnemys();
        for(int i=0;i<5; i++)
        {
            funnel_point[i]= new Vector3(5.8f, Mathf.Sin(Mathf.PI/2f + Mathf.PI / 2.5f * i) * interval + 8, Mathf.Cos(Mathf.PI/2f + Mathf.PI / 2.5f * i) * interval);
        }
       /* Hp_Color[0] = new Color32(178, 255, 0, 255);
        Hp_Color[1] = new Color32(204, 255, 0, 255);
        Hp_Color[2] = new Color32(255, 255, 0, 255);
        Hp_Color[3] = new Color32(255, 204, 0, 255);
        Hp_Color[4] = new Color32(255, 148, 0, 255);
        Hp_Color[5] = new Color32(255, 20, 0, 255);

        Power_Color[0] = new Color32(0, 255, 255,255);
        Power_Color[1] = new Color32(0, 0, 255, 255);
        Power_Color[2] = new Color32(255, 0, 255, 255);*/




        /*funnel_point[0] = funnel_parent.transform.position + new Vector3(0, 3f, 0);
        funnel_point[1] = funnel_parent.transform.position + new Vector3(0, 2.7f, 0.5f);
        funnel_point[2] = funnel_parent.transform.position + new Vector3(0, 1.75f, 0.75f);
        funnel_point[3] = funnel_parent.transform.position + new Vector3(0, -2.7f, -0.5f);
        funnel_point[4] = funnel_parent.transform.position + new Vector3(0, -1.75f, -0.75f);*/
        // funnel_point[0] += new Vector3(0, 3f, 0);
        funnel_point[1] += new Vector3(0, 2.7f, 0.5f);
        funnel_point[2] += new Vector3(0, 7f, -4f);
        funnel_point[3] += new Vector3(0, 7f, 4f);
        funnel_point[4] += new Vector3(0, 2.7f, -0.5f);
        load_HP();
        load_Power();
    }

    // Update is called once per frame
    void Update()
    {
        Anim.SetBool("direction", direction);
        if (hook_status)
        {
            hook();
            return;
        }
        if (Anim.GetCurrentAnimatorStateInfo(0).nameHash == IdleR || Anim.GetCurrentAnimatorStateInfo(0).nameHash == IdleL)
        {
            Knife.enabled = false;
            lv3L.SetActive(false);
            lv3R.SetActive(false);
            stop = false;
            Anim.applyRootMotion = false;
            atk_Combo = 0;
            Anim.SetInteger("Atk_Combo", atk_Combo);
        }
        if (t - t1 >= 50 && !dead())
        {
            down = false;
            GetComponent<BoxCollider>().center = new Vector3(0, 0.5f, 0);
        }
        //跳躍        
        if (rb.velocity.y < 0)
        {
            Anim.SetBool("Drop", true);
        }
        else
        {            
            Anim.SetBool("Drop", false);
        }
        if (Input.GetKeyDown(KeyCode.W) && OnGround && !stop)
        {
            Audio.PlayOneShot(A_clip[7]);
            Anim.applyRootMotion = false;
            rb.velocity += Vector3.up * jump;
            OnGround = false;
            if (!direction)
            {
                //transform.position += new Vector3(0, 3f, 0);
                Anim.SetBool("JumpR", true);
            }
            else
            {
               // transform.position += new Vector3(0, 3f, 0);
                Anim.SetBool("JumpL", true);                
            }

        }
        else
        {            
            Anim.SetBool("JumpR", false);
            Anim.SetBool("JumpL", false);
        }
       /* if (!OnGround && jump_stage == 0)
        {
            rb.velocity += Vector3.up * jump;
            jump_stage++;
        }*/
        /*Anim.SetBool( "JumpL", false );
		if( Input.GetKeyDown (KeyCode.W))
		{
			Anim.SetBool( "JumpL", true);           
        }*/
        //跑步

        
        if (Input.GetKey(KeyCode.D) && atk_Combo == 0 && !shield_status && !down && !stop)
        {
            // transform.position += new Vector3(0, 0, stay_ground < 20 ? 0.3f : 0.5f);// * Time.deltaTime;
            if(rb.velocity.z < 25)
                rb.velocity += Vector3.forward * 4f;
            direction = false;
            Anim.SetBool("RunR", true);
        }
        else
        {
            Anim.SetBool("RunR", false);
        }

        
        if (Input.GetKey(KeyCode.A) && atk_Combo == 0 && !shield_status && !down && !stop)
        {            
            // transform.position -= new Vector3(0, 0, stay_ground < 20 ? 0.3f : 0.5f);// * Time.deltaTime;
            if (rb.velocity.z > -25)
                rb.velocity += Vector3.back * 4f;
            direction = true;
            Anim.SetBool("RunL", true);
        }
        else
        {
            Anim.SetBool("RunL", false);            
        }


        /* Anim.SetBool("AirFireR", false);
         if (Input.GetKeyDown(KeyCode.Space))
         {
             Anim.SetBool("AirFireR", true);
         }

         Anim.SetBool("AirFireL", false);
         if (Input.GetKeyDown(KeyCode.Space))
         {
             Anim.SetBool("AirFireL", true);
         }*/
        if (Input.GetMouseButtonDown(1) && atk_Combo > 0)
        {
            
            //stay_ground = 0;                 
            atk_Combo++;
            Anim.SetInteger("Atk_Combo", atk_Combo);
        }
        if (Input.GetMouseButtonDown(1) && atk_Combo == 0)
        {
            //stay_ground = 0;                 
            atk_stage = 0;
            atk_Combo++;
            Anim.SetInteger("Atk_Combo", atk_Combo);       
            Anim.SetBool("Atk", true);
            if (OnGround)
            {
                Anim.applyRootMotion = true;
            }  
        }
        else
        {
            Anim.SetBool("Atk", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            m = 0;
            missile_atk = 0;
           /* Anim.SetBool("Fire",true);
            missile_direction = direction;*/
        }
        else
        {
            Anim.SetBool("Fire", false);
        }
        if (Input.GetMouseButton(0))
        {
            m++;
            if (m % 50 == 0 && power > 0)
            {
                power -= 0.5f;
                missile_atk += 1;
                load_Power();
                m_status();
            }
        }
        if (Input.GetMouseButtonUp(0) && missile_atk>0)
        {
            stop = true;
            if (direction)
            {
                lv2.transform.localScale = new Vector3(1, 1, -1);
            }
            else
            {
                lv2.transform.localScale = new Vector3(1, 1, 1);
            }
            if (missile_atk <= 2)
            {

            }
            else if (missile_atk <= 4)
            {
                lv2.SetActive(true);
            }

            else if (missile_atk == 6)
            {
                if (!direction)
                {
                    lv3R.SetActive(true);
                    Anim.Play("Stagnge_fire_Lv3_Pose_R");
                }
                else
                {
                    lv3L.SetActive(true);
                    Anim.Play("Stagnge_fire_Lv3_Pose_L");
                }               
            }
            Anim.SetBool("Fire", true);
            missile_direction = direction;
            for(int i = 0; i < 3; i++)
            {
                mi[i].SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.Space) && shield_t>100 && !shield_status)
        {
            shield_t = 0;
            //Shield.GetComponent<MeshRenderer>().enabled = false;
            Shield.transform.position = new Vector3(-1000, -1000, -1000);
            
            Anim.SetBool("Defense", true);      
        }
        else
        {
            Anim.SetBool("Defense", false);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (shield_status)
            {
                shield_t = 0;
            }
            Shield.SetActive(false);
            shield_status = false;

        }
        if (Input.GetKeyDown(KeyCode.E) && Line.Instance.go == null && !down)//&& rb.velocity.z==0
        {
            /* if (Line.Instance.hook() == Vector3.zero)
             {
                 return;
             }*/
            if (gethook())
            {
                return;
            }
            if (!direction)
            {
                if (!OnGround)
                {
                    Anim.Play("Floating_magnetic_R");
                }                
                else if(rb.velocity.z==0)
                {
                    Anim.Play("Stagnge_magnetic_R");
                }
                
            }
            else
            {               
                if (!OnGround)
                {
                    Anim.Play("Floating_magnetic_L");
                }                
                else if(rb.velocity.z==0)
                {
                    Anim.Play("Stagnge_magnetic_L");
                }                
            }
            
        }
        t++;
        shield_t++;
    }
    void m_status()
    {
        switch (missile_atk)
        {
            case 1:
            case 2:
                
                mi[0].SetActive(true); break;
            case 3:
            case 4:
                
                mi[0].SetActive(false);
                mi[1].SetActive(true);break;
            case 5:
            case 6:
                
                mi[1].SetActive(false);
                mi[2].SetActive(true);break;
        }
        
    }
    bool gethook()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "hook_point")
            {
                return false;
            }
            /*else if (hit.collider.tag == "Funnel") 勾浮游砲
            {
                return false;
            }*/
        }
        return true;
    }

    void fire()//到時候用來存放發射用
    {
        power--;
    }
    void atk()//攻擊 採用動畫內置Eveents來呼叫
    {
        atk_stage++;
        Knife.enabled = true;
        if (atk_stage < 3)
        {
            Audio.PlayOneShot(A_clip[0]);
        }
        else
        {
            Audio.PlayOneShot(A_clip[1]);
        }
        
        /*ArrayList _Enemys = Enemys;
        foreach(GameObject g in _Enemys) //搜尋所有敵人來判斷距離
        {
            if (g == null)
            {
                continue;
            }            
            Vector3 mPos = transform.position;
            Vector3 ePos = g.transform.position;
            if (!direction && mPos.z > ePos.z)
            {
                return;
            }
            else if(direction && mPos.z < ePos.z)
            {
                return;
            }
            float pos = Vector2.Distance(new Vector2(mPos.y, mPos.z), new Vector2(ePos.y, ePos.z));
            if (g.tag == "Funnel" && pos < 20f)
            {
                g.GetComponent<Funnel>().setDamage(sword[atk_stage - 1]);
            }
            else if (pos < 20f)
            {
                g.GetComponent<Enemy>().hp -= sword[atk_stage-1];
            }

        }*/
        ATK = sword[atk_stage - 1];

    }   
    void Fire()
    {
        int go = missile_direction == false ? 1 : -1;
        GameObject missile = (GameObject) Instantiate(projectile, transform.position + new Vector3(0,8,6.82f*go), Quaternion.identity);
        missile.transform.Rotate(new Vector3(0, 90, 0) * go);
        missile.GetComponent<Rigidbody>().velocity += Vector3.forward * 100 * go;
        if (missile_atk < 3)
        {
            Audio.PlayOneShot(A_clip[4]);
        }
        else if (missile_atk < 5)
        {
            Audio.PlayOneShot(A_clip[5]);
        }
        else
        {
            Audio.PlayOneShot(A_clip[6]);
        }
        //給予飛彈傷害
        // missile.GetComponent<missile>().atk = projectile_atk[0];
        //missile.GetComponent<missile>().atk = 2;
    }
    public void LoadEnemys()//載入所有tag為"Enemy"的物體
    {
       /* GameObject[] all = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject game in all)
        {
            // Enemys.Add(game.GetComponent<Enemy>());
            Enemys.Add(game);
        }*/
    }

    void Defense()
    {
        Audio.PlayOneShot(A_clip[2]);
        shield_status = true; 
        Shield.SetActive(true);
        Shield.transform.position = new Vector3(0.4f, 0.7f, 0);
        if (direction)
        {
            Shield.GetComponentInChildren<Animator>().Play("Shield_L");
        }
        
    }
    
    public void magnetic()//Vector3 force
    {
        if (Line.Instance.go != null)
        {
            return;
        }
        Line.Instance.hook();
        Audio.PlayOneShot(A_clip[3]);
        /* if (Line.Instance.hook().z > transform.position.z)
         {
             direction = false;
         }
         else
         {
             direction = true;
         }*/
        /*if (Line.Instance.Target.tag == "hook_point")
        {
            hook_status = true;
            rb.useGravity = false;
        }*/
    }
    void hook()
    {        
        Vector3 Tpos = hook_point;
        rb.velocity = new Vector3(0, Tpos.y - transform.position.y, Tpos.z - transform.position.z)*2;
        if (Vector3.Distance(Tpos, transform.position) < 6f)
        {
            hook_status = false;
            rb.useGravity = true;
            Line.Instance.Target = null;
        }
    }
    public void funnel(GameObject ful)
    {
        if (funnel_count > 4)
        {
            return;
        }
        int num = Mathf.FloorToInt(Random.value * 5);        
        if (funnel_status[num])
        {
            funnel(ful);//使用遞迴找出空的位置 進入點先判斷是否已滿
        }
        else
        {
            //Debug.Log(num);
            //ful.transform.parent = funnel_parent.transform;
            //ful.transform.position = funnel_parent.transform.position + funnel_point[num];
            ful.GetComponent<Funnel>().enabled = true;
            ful.GetComponent<Funnel>().point_num = num;
            funnel_status[num] = true;            
        }

    }

    public void SetDamage(float dmg)
    {
        if (t - t1 < 100)
        {
            return;
        }
        else
        {
            t1 = t;
        }
        if (shield_status)
        {
            Audio.PlayOneShot(A_clip[8]);
            power += dmg;
            if (power > 3)
            {
                //   hp -= power - 3;
                hp = 0;
                power = 3;
                if (dead())
                {
                    return;
                }
                hit();
            }
            load_Power();
        }
        else
        {
            hp -= dmg;
            if (dead())
            {
                return;
            }
            hit();
        }
        load_HP();
        
    }

    void hit()
    {
       // GetComponent<BoxCollider>().center = new Vector3(0, .9f, 0);
        down = true;
        if (stop)
        {
            return;
        }
        if (!direction)
        {
            rb.velocity = new Vector3(0, 0, -40);
            Anim.Play("Hit_fly_R");           
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 40);
            Anim.Play("Hit_fly_L");
        }
    }

    bool dead()
    {
        if (hp > 0)
        {
            return false;
        }
        else if (hp <= 0)
        {
            if (!direction)
            {
                Anim.Play("fall_R");
            }
            else
            {
                Anim.Play("fall_L");
            }
            
        }
        load_HP();
        
        //GetComponent<BoxCollider>().center = new Vector3(0, 1f, 0);
      /*  if (!direction)
        {
            Anim.Play("Exhausted_R");
        }
        else
        {
            Anim.Play("Exhausted_L");
        }*/
        return true;
    }
    void load_HP()
    {
        UImanager.Instance.hp = hp;
        /*for(int i=0; i <6; i++)
        {
            if (hp * 2 > 5-i)
            {
                HpBar[i].color = Hp_Color[i];
            }
            else
            {
                HpBar[i].color = new Color32(0, 0, 0, 0);
            }
        }*/
    }
    void load_Power()
    {
        UImanager.Instance.power = power;
        /*
        PowerBar.transform.localPosition = new Vector3(-Power_point[(int)(power*2)]/.8f, 0, 0);
        if (power <= 1)
        {
            PowerBar.GetComponent<Image>().color = Power_Color[0];
        }
        else if (power <= 2)
        {
            PowerBar.GetComponent<Image>().color = Power_Color[1];
        }
        else if(power <= 3)
        {
            PowerBar.GetComponent<Image>().color = Power_Color[2];
        }*/
    }    
   void addF()
    {
        rb.velocity = new Vector3(0, 0, direction == false ? 40 : -40);
    }
    void OnCollisionEnter(Collision col)
    {
        // stay_ground = 0;
        if (col.collider.tag == "Ground")
        {
            Anim.SetBool("OnGround", true);
            OnGround = true;
            atk_Combo = 0;
        }
    }    

    void OnCollisionStay(Collision col)
    {
        //this.col = col.collider;
        /*
        if (stay_ground > 20)
        {
            jump_stage = 0;
        }
        else
        {
            stay_ground++;
        }*/

        if (col.collider.tag == "Ground")
        {
            Anim.SetBool("OnGround", true);
            OnGround = true;
            //jump_stage = 0;
        }

    }
    void OnCollisionExit(Collision col)
    {        
        OnGround = false;
        Anim.SetBool("OnGround", false);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Wall")
        {
            Debug.Log("wall");
        }
        else if(col.tag== "BossKnife")
        {
            SetDamage(1);
        }
    }   
}
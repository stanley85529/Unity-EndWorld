using UnityEngine;
using System.Collections;

public class Boss2 : MonoBehaviour {
    public static Boss2 Instace;
    public float hp;
    public GameObject player;
    public float m;
    public GameObject missile,missile_point;
    public int atk_t;
    public GameObject funnel, funnel_par;    
    public int interval;
    public GameObject[] ful = new GameObject[6];
    public int[] fun_ran = new int[6];
    public int funcount;
    private Rigidbody rb;
    private Animator Anim;
    public GameObject[] eq = new GameObject[5];
    public float mos;
    public int status;
    public int level;   //boss階段
    public int MaxFunnel=6;
    // public BoxCollider[] game = new BoxCollider[3];
    public GameObject[] game = new GameObject[3];
    public int st,lv3_st;
    private bool fire_s;

    public AudioClip[] A_clip = new AudioClip[3];
    public AudioSource Audio;
    int[] fire = new int[4];
    int fire1 = Animator.StringToHash("Base Layer.Lv2_fire1");
    int fire2 = Animator.StringToHash("Base Layer.Lv2_fire2");
    int fire3 = Animator.StringToHash("Base Layer.Lv2_fire3");
    // Use this for initialization
    void Awake()
    {
        funcount = 6;
        level = 2;
        Instace = this;
    }
    void Start () {
        
        status = 0;
        atk_t = 301;
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        for (int i = 0; i < MaxFunnel; i++)
        { 
            GameObject go = (GameObject)Instantiate(funnel, funnel_par.transform.position, Quaternion.identity);
            go.transform.parent = funnel_par.transform;
            go.transform.localScale = go.transform.localScale * 20;            
            ful[i] = go;
        }
        fire[0] = Animator.StringToHash("Base Layer.Lv2_fire1");
        fire[1] = Animator.StringToHash("Base Layer.Lv2_fire2");
        fire[2] = Animator.StringToHash("Base Layer.Lv2_fire3");
        fire[3] = Animator.StringToHash("Base Layer.Lv3_atk01");
        forran();
        Debug.Log(transform.rotation.y);
        rb.isKinematic = true;
    }
	void forran()
    {   for(int i = 0; i < 6; i++)
        {
            fun_ran[i] = i;
        }
        for (int i = 0; i < 6; i++)
        {
            int c = fun_ran[i];
            int t = Random.Range(0, 5);
            fun_ran[i] = fun_ran[t];
            fun_ran[t] = c;
        }       
    }
	// Update is called once per frame
	void Update () {

        /*  if (player.transform.position.z < transform.position.z && atk_t>500)
          {
              transform.localRotation = Quaternion.Euler(0, 175, 0);
          }
          else if(player.transform.position.z > transform.position.z && atk_t>500)
          {
              transform.localRotation = Quaternion.Euler(0, -5, 0);
          }*/

        if (level==3)
        {
            lv3();
        }
        else if (checkful() == 0)
        {
            if (game[0].gameObject == true)
            {
                game[0].GetComponent<bossM>().status = true;
            }
            if (transform.position.y == 36)
            {
                transform.position -= new Vector3(0, 36 - 3.55f, 0);
            }
            rb.isKinematic = false;
            move();
        }        
        if (PlayerCtrl1.Instance.funnel_count < 1 && funcount>0)
        {
            call(2);
        }
        atk_t++;
    }
    int checkful()
    {
        int check= MaxFunnel;
        for(int i = 0; i < MaxFunnel; i++)
        {
            if (ful[i] == null)
            {
                check--;
            }
        }
        return check;
    }
    void lv3()
    {
        if ((lv3_st % 3) != 2)
        {
            rb.velocity = (player.transform.position - transform.position) / 3;
            
        }
        mos = Vector3.Distance(player.transform.position, transform.position);        

        if (mos > 20 && (lv3_st % 3) != 2)
        {
            Rotation();
            Anim.SetBool("Run", true);
        }
        else if((lv3_st % 3) != 2)
        {
            rb.velocity = Vector3.zero;
            Anim.SetBool("Run", false);
        }

        if((lv3_st%3)<2 && atk_t > 600)
        {
            Rotation();
            atk_t = 0;            
            Anim.SetBool("atk", true);
            Anim.SetBool("Run", false);
        }
        else if((lv3_st % 3) == 2)
        {
            Anim.SetBool("Run", false);
            firemove();
        }
        
        

        atk_t++;
    }
    void Rotation()
    {
        if (player.transform.position.z < transform.position.z)
        {
            transform.localRotation = Quaternion.Euler(0, 175, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, -5, 0);
        }
    }
    void firemove()
    {
        if (player.transform.position.z < transform.position.z && !fire_s)
        {
            transform.localRotation = Quaternion.Euler(0, 175, 0);
        }
        else if(!fire_s)
        {
            transform.localRotation = Quaternion.Euler(0, -5, 0);
        }
        Vector3 point = new Vector3(8.5f, 3.55f, 14.4f);
        if (!fire_s)
        {            
            rb.velocity = (point - transform.position);
        }

        mos = Vector3.Distance(transform.position, point);
        if (mos < 3 )
        {
            fire_s = true;
            Anim.SetBool("fire", true);
            rb.velocity = Vector3.zero;
        }
    }
    void move()
    {
        mos = Vector3.Distance(player.transform.position, transform.position);
        if (mos > m && !OnFire())
        {
            rb.velocity = (player.transform.position - transform.position)/5;
            Anim.SetBool("Run", true);
            if (player.transform.position.z < transform.position.z)
            {
                transform.localRotation = Quaternion.Euler(0, 175, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, -5, 0);
            }
        }
        else
        {            
            atk();
            Anim.SetBool("Run", false);
            rb.velocity = Vector3.zero;
        }
    }
    bool OnFire()
    {
        for(int i = 0; i < 4; i++)
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).nameHash == fire[i])
            {
                return true;
            }
        }
        return false;
    }
    void atk()
    {
        if (atk_t < 500)
        {
            return;
        }        
        Anim.SetBool("atk", true);
        if (Anim.GetCurrentAnimatorStateInfo(0).nameHash == fire3)
        {
            Anim.SetBool("atk", false);
            atk_t = 0;
            st++;
        }
        
        
    }
    void fire_m()
    {
        Audio.PlayOneShot(A_clip[0]);
        GameObject go = (GameObject)Instantiate(missile, missile_point.transform.position, Quaternion.Euler(0, 90, 0));
        go.GetComponent<Rigidbody>().velocity = (player.transform.position+new Vector3(0,5,0) - missile_point.transform.position); // *.4f
        go.GetComponent<missile_boss>().atk = 0.5f;
        //go.transform.localRotation = Quaternion.Euler(0, 90, 0);
    }
    void call(int count)
    {
        for (int i = 0; i < count; i++)
        {
            funcount--;
            PlayerCtrl1.Instance.funnel(ful[fun_ran[funcount]]);
            PlayerCtrl1.Instance.funnel_count++;
        }
    }
    void fire_over()
    {
        st++;
        fire_s = false;
    }
    void setdmg(float dmg)
    {
        hp -= dmg;       
    }
    public void next()
    {
        status++;
        if (status == 2)
        {
            NextLevel();
           // return;
        }
        else if (status == 3)
        {
            Anim.Play("Down");
        }
        game[status].GetComponent<bossM>().status = true;
    }
    public void NextLevel()
    {
        level++;
        st = 0;
        Anim.Play("Lv2_Lv3");
    }
    public void Atk_fin()
    {
        lv3_st++;
        Anim.SetBool("atk", false);
    }
    public void Fire_fin()
    {
        atk_t = 500;
        lv3_st++;
        fire_s = false;
        Anim.SetBool("fire", false);
    }
    public void fire_A()
    {
        Debug.Log(player.transform.position);
        Debug.Log(transform.position);
        if (transform.rotation.y >  0.9 && transform.rotation.y<1)
        {
            if (player.transform.position.z < transform.position.z && player.transform.position.y<20)
            {
                Debug.Log("LV3砲擊中");
                PlayerCtrl1.Instance.SetDamage(1);
            }
        }
        else
        {
            if (player.transform.position.z > transform.position.z && player.transform.position.y < 20)
            {
                Debug.Log("LV3砲擊中");
                PlayerCtrl1.Instance.SetDamage(1);
            }
        }
        Audio.PlayOneShot(A_clip[2]);
    }
    public void atk_A()
    {
        Audio.PlayOneShot(A_clip[1]);
    }
    public void End()
    {
        Application.LoadLevel(2);
    }
}

using UnityEngine;
using System.Collections;

public class Funnel : MonoBehaviour
{
    public Animator Anim;
    public GameObject halo;
    public LineRenderer line;
    public int length;
    public int point_num;   //目標點的編號
    public Vector3 Target, point;
    public float run;
    public Rigidbody rb;
    public bool fire, magnetic;
    public int T;
    public int atk_T;
    public int M, M_time;
    public Vector3 par;
    public Vector3 laser_Target;
    public float hp = 2;
    private BoxCollider BoxCol;
    public AudioClip[] A_clip = new AudioClip[2];
    public AudioSource Audio;
    // Use this for initialization
    void Start()
    {
        BoxCol = GetComponent<BoxCollider>();
        hp = 2;
        T = 0;
        atk_T = 250;
        M = 0;
        M_time = 200;
        /* fire = false;
         magnetic = false;*/
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.X))
        {
            rb.velocity = new Vector3(0,0,0);
            fire = true;
            Anim.SetBool("Fire", true);
        }*/
        if (hp == 0)
        {
            return;
        }
        else if (magnetic)
        {
            Magnetic();
        }
        else if (T < atk_T - 50)
        {
            T++;
            go();
        }
        else if (T < atk_T)
        {
            BoxCol.enabled = true;
            T++;
            halo.SetActive(true);
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
        else
        {
            Anim.SetBool("Fire", true);
        }


    }
    void go()//攻擊飛行
    {
        par = PlayerCtrl1.Instance.funnel_parent.transform.position;
        Target = PlayerCtrl1.Instance.transform.position + PlayerCtrl1.Instance.funnel_point[point_num];
        point = transform.position;
        run = Mathf.Atan2(par.y - point.y, par.z - point.z) * Mathf.Rad2Deg;
        /*switch (point_num)
        {
            case 0:
                run -= 180;
                break;
            case 4:
                run -= 90;
                break;
            case 1:
                run += 90;
                break;
        }*/

        rb.velocity = new Vector3(0, Target.y - point.y, Target.z - point.z) * 3;
        transform.rotation = Quaternion.Euler(180, 90, run);
        transform.parent = null;
    }

    public void E_light()
    {
        halo.SetActive(true);
        laser_Target = par;
    }
    public void laser()
    {
        if (hp <= 0)
        {
            return;
        }
        Audio.PlayOneShot(A_clip[0]);
        //  PlayerCtrl1.Instance.Enemys.Add(gameObject);      
        BoxCol.enabled = true;
        //GetComponent<Collider>().enabled = false;
        line.SetPosition(0, point);
        line.SetPosition(1, laser_Target + (laser_Target - point));
        Ray ray = new Ray(point, laser_Target + (laser_Target - point) - point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Vector3.Distance(hit.point, transform.position) > 20f)
            {
                return;
            }
            Debug.Log(hit.collider);
            if (hit.collider.tag == "Player")
            {
                PlayerCtrl1.Instance.SetDamage(0.5f);
            }
        }
    }
    public void Out_laser()
    {
        line.SetPosition(0, new Vector3(0, 0, 0));
        line.SetPosition(1, new Vector3(0, 0, 0));
    }
    public void Go_Magnetic()
    {
        transform.parent = null;
        //PlayerCtrl1.Instance.Enemys.Add(this.gameObject);        
        BoxCol.enabled = true;
        magnetic = true;
        M = 0;
        //rb.isKinematic = true;
        //  PlayerCtrl1.Instance.LoadEnemys();
    }
    public void Magnetic()//勾索飛行
    {
        //Target = PlayerCtrl1.Instance.transform.position + PlayerCtrl1.Instance.funnel_point[PlayerCtrl1.Instance.direction == false ? 3 : 2];
        point = transform.position;
        Target = PlayerCtrl1.Instance.funnel_parent.transform.position + new Vector3(0, 2f, PlayerCtrl1.Instance.direction == false ? 3f : -3f);
        rb.velocity = Target - point;
        /*
        if (Vector3.Distance(Target, point) < 0.5f)
        {
            PlayerCtrl1.Instance.Enemys.Remove(gameObject);
            magnetic = false;
            PlayerCtrl1.Instance.funnel(gameObject);
        }*/
        if (M < M_time)
        {
            M++;
        }
        else
        {
            magnetic = false;
            // PlayerCtrl1.Instance.Enemys.Remove(gameObject);
            BoxCol.enabled = false;
            PlayerCtrl1.Instance.funnel(gameObject);
        }
    }
    public void onrush()
    {
        //  PlayerCtrl1.Instance.Enemys.Remove(gameObject);
        BoxCol.enabled = false;
        PlayerCtrl1.Instance.funnel_status[point_num] = false;
        halo.SetActive(false);
        // atk_T -= 50;
        T = 0;
        rb.isKinematic = false;
        Anim.SetBool("Fire", false);
        Anim.Play("stop");
        PlayerCtrl1.Instance.funnel(gameObject);
    }
    public void setDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Audio.volume = .5f;
            Audio.PlayOneShot(A_clip[1]);
            //PlayerCtrl1.Instance.Enemys.Remove(gameObject);
            PlayerCtrl1.Instance.funnel_status[point_num] = false;
            
            Line.Instance.Remove_line();
            Anim.Play("Dead");
        }
    }
    public void dead()
    {
        PlayerCtrl1.Instance.funnel_count--;
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "knife")
        {
            setDamage(PlayerCtrl1.Instance.ATK);
        }
    }

}

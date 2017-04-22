using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {
    public static Line Instance;
    public LineRenderer line;
    public Vector3 mouse = new Vector3();
    public Vector3 test;
    public float run;
    public Camera cam;
    public GameObject player;
    public GameObject missile;
    public GameObject go;
    public GameObject Target;
    public int speed;
    public int T, T_max;
	// Use this for initialization
	void Start () {
        // test = new Vector3(0, 8.18f, 3.26f);
        Instance = this;
        T = 100;
        T_max = 101;
    }
	
	// Update is called once per frame
	void Update () {
        if (T < T_max && Target!=null)
        {
            T++;
            draw_line();
        }
        else if (T == T_max)
        {
            Remove_line();
            if (Target != null)
            {
                if (Target.tag == "Funnel")
                {

                   // PlayerCtrl1.Instance.Enemys.Remove(Target.gameObject);
                }
                
            }
            Target = null;
        }         
    }
    public Vector3 hook()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
          /*  if (PlayerCtrl1.Instance.direction)
            {
                if (hit.point.z > PlayerCtrl1.Instance.transform.position.z)
                {
                    return Vector3.zero;
                }
            }
            else
            {
                if (hit.point.z < PlayerCtrl1.Instance.transform.position.z)
                {
                    return Vector3.zero;
                }
            }*/
            test = player.transform.position + new Vector3(0, 8.18f, 3.26f * (PlayerCtrl1.Instance.direction == false ? 1 : -1));
                mouse = new Vector3(-0.2f, hit.point.y, hit.point.z);
                /*line.SetPosition(0, test);
                //line.SetPosition(1, mouse);
                t = 0;*/
                run = Mathf.Atan2(test.y - mouse.y, test.z - mouse.z) * Mathf.Rad2Deg;
                go = (GameObject)Instantiate(missile, test, Quaternion.identity);
                go.transform.rotation = Quaternion.Euler(180 - run, 0, 0);
                go.GetComponent<Rigidbody>().AddForce(0, (mouse.y - test.y) * speed, (mouse.z - test.z) * speed);
                go.GetComponent<Magnetic>().line = this;            
        }
        return mouse;
    }

    void draw_line()
    {
       /* line.SetPosition(0, player.transform.position + new Vector3(0, 8.18f, 3.26f));
        line.SetPosition(1, Target.transform.position);*/
    }
    public void Remove_line()
    {
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
    }
}


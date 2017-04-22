using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {
    public static UImanager Instance;
    public Image[] HpBar = new Image[6];
    private Color32[] Hp_Color = new Color32[6];
    public GameObject PowerBar;
    public int[] Power_point = { 850, 720, 650, 450, 330, 150, 0 };
    private Color32[] Power_Color = new Color32[3];

    public float hp,power;




    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        
        PowerBar.transform.localPosition = new Vector3(-Power_point[0]/.8f, 0, 0);
        Hp_Color[0] = new Color32(178, 255, 0, 255);
        Hp_Color[1] = new Color32(204, 255, 0, 255);
        Hp_Color[2] = new Color32(255, 255, 0, 255);
        Hp_Color[3] = new Color32(255, 204, 0, 255);
        Hp_Color[4] = new Color32(255, 148, 0, 255);
        Hp_Color[5] = new Color32(255, 20, 0, 255);
        Power_Color[0] = new Color32(0, 255, 255, 255);
        Power_Color[1] = new Color32(0, 0, 255, 255);
        Power_Color[2] = new Color32(255, 0, 255, 255);

    }
	
	// Update is called once per frame
	void Update () {
        loadHP();
        loadPower();
    }

    void loadHP()
    {
        for (int i = 0; i < 6; i++)
        {
            if (hp * 2 > 5 - i)
            {
                HpBar[i].color = Hp_Color[i];
            }
            else
            {
                HpBar[i].color = new Color32(0, 0, 0, 0);
            }
        }
    }
    void loadPower()
    {
        if (power <= 1)
        {
            PowerBar.GetComponent<Image>().color = Power_Color[0];
        }
        else if (power <= 2)
        {
            PowerBar.GetComponent<Image>().color = Power_Color[1];
        }
        else if (power <= 3)
        {
            PowerBar.GetComponent<Image>().color = Power_Color[2];
        }
        float point = -Power_point[(int)(power * 2)] / .8f ;
        float P = point - PowerBar.transform.localPosition.x;
        PowerBar.transform.localPosition += new Vector3(P/30, 0, 0);
    }


}

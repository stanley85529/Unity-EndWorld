using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    void CloseShield()
    {
        gameObject.SetActive(false);
        PlayerCtrl1.Instance.shield_status = false;
        
            PlayerCtrl1.Instance.shield_t = 0;
        
    }
}

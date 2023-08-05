using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponManager wm;
    public WeaponData wd;
    // Start is called before the first frame update
    void Awake()
    {
        wd = GetComponentInChildren<WeaponData>();
    }

    public float GetATK_Value()
    {
        moveController mc = wm.transform.parent.GetComponent<moveController>();
        return wd.ATK + wm.am.sm.Player_ATK 
            * (mc.OneOrTwo[mc.Hands_index] == "Two"?1.5f:1.0f);
    }
    //// Update is called once per frame
    //void Update()
    //{

    //}
}

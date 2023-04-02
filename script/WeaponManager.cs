using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IAcyorManagerInterface
{

    //public ActorManager am;
    private Collider weaponColL;
    private Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;
    private void Start()
    {
        whL = transform.DeepFind("weaponHandleL").gameObject;
        if (whL == null) print("whL是null");
        whR = transform.DeepFind("weaponHandleR").gameObject;
        if (whR == null) print("whR是null");
        wcL = BindWeaponController(whL);
        wcR = BindWeaponController(whR);
        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR = whR.GetComponentInChildren<Collider>();
        //weaponCol = whR.GetComponentInChildren<Collider>();
        //print(transform.DeepFind("weaponHandleR"));


    }

    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWc;
        tempWc = targetObj.GetComponent<WeaponController>();
        if (tempWc == null)
        {
            tempWc = targetObj.AddComponent<WeaponController>();
        }
        tempWc.wm = this;
        return tempWc;
    }
    public void WeaponEnable()
    {
        if (am.ac.CheckStateTag("attackL"))
        {
            weaponColL.enabled = true;
        }
        else
        {
            //weaponColL.enabled = false;
            weaponColR.enabled = true;
        }

        //if (am.ac.CheckStateTag("attackR"))
        //{
        //    weaponColR.enabled = true;
        //}
        
       
        //print("WeaponEnable");
    }

    public void WeaponDisable()
    {
        weaponColL.enabled = false;
        weaponColR.enabled = false;

        //print("WeaponDisable");
    }

    public void CounterBackEnable()
    {
        am.SetIsCounterBack(true);
    }
    public void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }

}

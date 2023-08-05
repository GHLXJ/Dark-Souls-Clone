using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IAcyorManagerInterface
{

    //public ActorManager am;
    [SerializeField]
    private Collider weaponColL;
    [SerializeField]
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
    public void unloadWeapon(string side)
    {
        if (side == "L")
        {
            weaponColL = null;
            wcL.wd = null;
            foreach(Transform tran in whL.transform)
            {
                Destroy(tran.gameObject);
            }
        }
        else if (side == "R")
        {
            weaponColR = null;
            wcR.wd = null;
            foreach (Transform tran in whR.transform)
            {
                Destroy(tran.gameObject);
            }
        }
    }
    public void UpdateWeaponCollider(string side,Collider col)
    {
        if (side == "L")
        {
            weaponColL = col;
        }
        else if (side == "R")
        {
            weaponColR = col;
        }
        else
        {
            return;
        }
    }
    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController? tempWc;
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
            if(weaponColL!=null)
                weaponColL.enabled = true;
        }
        else
        {
            //weaponColL.enabled = false;
            if (weaponColR != null)
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
        if(weaponColL!=null)
        weaponColL.enabled = false;
        if(weaponColR!=null)
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

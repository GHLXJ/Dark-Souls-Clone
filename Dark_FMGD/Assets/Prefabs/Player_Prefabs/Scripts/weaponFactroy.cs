using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponFactroy
{
    private Data_Base weaponDB;
	public weaponFactroy(Data_Base _weaponDB)
	{
        weaponDB = _weaponDB;
    }
   /* public GameObject createWeapon(string weaponName,Vector3 vec3,Quaternion qua)
    {
        GameObject pre = Resources.Load(weaponName) as GameObject;
        GameObject obj = GameObject.Instantiate(pre, vec3, qua);

        WeaponData weaponData = obj.AddComponent<WeaponData>();
        weaponData.ATK = weaponDB.WeaponJSONObject[weaponName]["ATK"].floatValue;

        return obj;
    }*/
    public bool createWeapon(string weaponName, string side, WeaponManager wm)
    {
        //Ð¶ÏÂ×°±¸
        wm.unloadWeapon(side);
        WeaponController wc;
        if (side == "L")
        {
            wc = wm.wcL;
        }
        else if (side == "R")
        {
            wc = wm.wcR;
        }
        else
        {
            return false;
        }
        
        GameObject pre = Resources.Load(weaponName) as GameObject;
        GameObject obj = GameObject.Instantiate(pre);
        obj.transform.parent = wc.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        WeaponData weaponData = obj.AddComponent<WeaponData>();
        weaponData.ATK = weaponDB.WeaponJSONObject[weaponName]["ATK"].floatValue;
        wc.wd = weaponData;


        wm.UpdateWeaponCollider(side,obj.transform.GetChild(0).GetComponent<Collider>());

        return true;
    }
}

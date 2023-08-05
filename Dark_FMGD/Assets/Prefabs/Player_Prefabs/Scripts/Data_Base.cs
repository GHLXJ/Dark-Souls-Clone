using Defective.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Base
{
    private string weaponDataBaseFileName = "Weapon";
    private JSONObject weaponJSONObject;
    public JSONObject WeaponJSONObject { get => weaponJSONObject; }
    public Data_Base()
	{
        TextAsset WeaponTextAsset = Resources.Load(weaponDataBaseFileName) as TextAsset;
        weaponJSONObject = new JSONObject(WeaponTextAsset.text);
    }

    
}

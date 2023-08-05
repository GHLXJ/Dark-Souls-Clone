using Defective.JSON;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private Data_Base weaponDB;
    private weaponFactroy WF;

    public static GameManager Instance { get => instance; private set => instance = value; }
    public weaponFactroy _WF { get => WF;private set => WF = value; }

    private void Awake()
    {
        CheckGameObjectTag();
        CheckSingle();
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("finishPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
        InitData_Base();
        InitWeaponFactory();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void InitWeaponFactory()
    {
        _WF = new weaponFactroy(weaponDB);
    }
    private void InitData_Base()
    {
        weaponDB = new Data_Base();
    }
    private void CheckGameObjectTag()
    {
        if (tag == "GM") return;
        Destroy(this);
    }
    private void CheckSingle()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }
}

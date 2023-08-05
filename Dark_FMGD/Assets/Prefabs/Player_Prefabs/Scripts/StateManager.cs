using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IAcyorManagerInterface
{
    //public ActorManager am;


    public float HPMax = 80.0f;
    public float HP = 15.0f;
    public float Player_ATK = 10.0f;

    [Header("===1st order state flags===")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    public bool isCounterBack;//只和状态有关 related to state
    public bool isCounterBackEnable;// related to animation events

    [Header("===2nd order state flags===")]
    public bool isAllowDefense;
    public bool isImmortal;
    public bool isCounterBackSuccess;
    public bool isCounterBackFailure;

    void Start()
    {
        HP = HPMax;
    }
    void Update()
    {
        /*if ((!photonView.IsMine || !PhotonNetwork.IsConnected)
        ) return;*/
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("Jump");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isJab = am.ac.CheckState("jab");
        isAttack = am.ac.CheckStateTag("attackR")|| am.ac.CheckStateTag("attackL");
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        //isDefense = am.ac.CheckState("defense1h", "defense");
        isCounterBack = am.ac.CheckState("counterBack");
        //isCounterBack = true;
        isCounterBackSuccess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable;

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckState("defense1h", "defense");
        isImmortal = isRoll || isJab;

    }
    public void AddHp(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);
        
    }
    

    //public void Test()
    //{
    //    print("");
    //}
 
}

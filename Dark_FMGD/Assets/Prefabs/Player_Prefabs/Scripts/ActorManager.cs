using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public moveController ac;

    [Header("===Auto Generate if Null===")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public InteractionManager im;
    public AnimatorOverrideController OneHand;
    public AnimatorOverrideController TwoHand;

    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<moveController>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;
        //bm = sensor.GetComponent<BattleManager>();
        //if(bm == null)
        //{
        //    bm = sensor.AddComponent<BattleManager>();
        //}
        //bm.am = this;

        //wm = model.GetComponent<WeaponManager>();
        //if(wm == null)
        //{
        //    wm = model.AddComponent<WeaponManager>();
        //}
        //wm.am = this;

        //sm = gameObject.GetComponent<StateManager>();
        //if (sm == null)
        //{
        //    sm = gameObject.AddComponent<StateManager>();
        //}
        //sm.am = this;
        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);
        im = Bind<InteractionManager>(sensor);

        ac.OnAction += DoAction;
    }
    public void DoAction()
    {
        if (im.overlapEcastms.Count != 0)
        {
            //应该根据对应的eventName播放对应的Timeline
            if (im.overlapEcastms[0].eventName == "frontStab")
            {
                //im.overlapEcastms[0].eventName = "";
                //Debug.Log("Yes");
                dm.PlayFrontStab("frontStab", this, im.overlapEcastms[0].am);
                im.overlapEcastms.Remove(im.overlapEcastms[0]);
            }
        }
        else
        {
            //Debug.Log("OK");
        }
    }
    private T Bind<T>(GameObject go)where T : IAcyorManagerInterface
    {
        T tempInstance;
        tempInstance = go.GetComponent<T>();
        if(tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;
    }

    public void TryDoDamage(WeaponController targetWc,bool attackValid, bool countValid)
    {
        //sm.HP -= 5;
        //if(sm.HP > 0)
        //{
        //    sm.AddHp(-5);
        //}
        
        if(sm.isCounterBackSuccess)
        {
            if (countValid)
            {
                targetWc.wm.am.Stunned();
            }
        }
        else if (sm.isCounterBackFailure)
        {
            if (attackValid)
            {
                HitOrDie(targetWc, false);
            }
        }
        else if (sm.isImmortal)//无敌状态
        {
            //Do nothing
        }
        else if (sm.isDefense)//举盾状态
        {
            //Attack should be blocked!
            Blocked();
        }
        else
        {
            if (attackValid)
            {
                HitOrDie(targetWc,true);
            }
        }

    }

    public void Stunned()
    {
        ac.IssueTrigger("stunned");
    }

    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }

    public void HitOrDie(WeaponController targetWc,bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {
            //Alreadly dead
        }
        else
        {
            sm.AddHp(-1*targetWc.GetATK_Value());
            if (sm.HP > 0)
            {
                if (doHitAnimation)
                {
                    Hit();
                }
                // do some VFX:like splatter blood...
            }

            else
            {
                Die();
            }
               
        }
    }
    public void Hit()
    {
        ac.IssueTrigger("hit");
    }
    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnabled = false;
        if(ac.camcon.lockState == true)
        {
            ac.camcon.LockUnlock();
            ac.camcon.enabled = false;
        }
    }
    public void LockUnlockActorController(bool value)
    {
        //Debug.Log("LockUnlockActorController" + ac.name);
        if (ac != null)
        {
            ac.SetBooll("lock", value);
            ac.pi.inputEnabled = !value;
            dm.victim = null;
        }   
    }
}

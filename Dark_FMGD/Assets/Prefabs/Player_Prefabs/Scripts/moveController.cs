using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveController : MonoBehaviourPun

{
    public GameObject model;
    public CameraController camcon;

    public IUserInput pi;
    public float walkSpeed = 2.4f;
    public float runMu = 4.0f;
    public float tar;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 3.0f;

    [Space(10)]
    [Header("=====  Friction Settings =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    public Animator anim;
    private Rigidbody rigid;
    private Vector3 thrustVec;
    private bool canAttack;
    private CapsuleCollider col;
    [SerializeField]
    private Vector3 Psave;
    [SerializeField]
    private bool losk = false;
    private bool trackDirection = false;
    private Vector3 deltaPos;


    public bool leftIsShield = true;

    public delegate void OnActionDelegate();
    public event OnActionDelegate OnAction;

    private float lerpTarget;
    private int weapon_index_L;
    private int weapon_index_R;
    private int weapon_side;
    private int hands_index;
    private List<string> list_L;
    private List<string> list_R;
    private List<string> sides;
    private List<string> oneOrTwo;

    public List<string> OneOrTwo { get => oneOrTwo; private set => oneOrTwo = value; }
    public int Hands_index { get => hands_index;private set => hands_index = value; }

    void Awake()
    {
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if(input.enabled == true)
            {
                pi = input;
                break;
            }
        }
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        InitWeaponAsset();
    }

    void Update()
    {
        /*if ((!photonView.IsMine || !PhotonNetwork.IsConnected)
        || gameObject.layer != LayerMask.NameToLayer("Enemy")) return;*/
        if (pi.lockon)
        {
            camcon.LockUnlock();
        }

        if (camcon.lockState == false)
        {
            tar = ((GetComponent<IUserInput>().run) ? 2.0f : 1.0f);
            anim.SetFloat("forword", pi.Dforword * Mathf.Lerp(anim.GetFloat("forword"), tar, 0.05f));
            anim.SetFloat("right", 0);
            
        }
        else
        {
           
                Vector3 localDvec =transform.InverseTransformVector(pi.Dvec) ;
                anim.SetFloat("forword", localDvec.z * ((pi.run) ? 2.0f : 1.0f));
                anim.SetFloat("right", localDvec.x * ((pi.run) ? 2.0f : 1.0f));
        }
        //anim.SetBool("defense", pi.defense);

        if (pi.roll || (rigid.velocity.magnitude > 10.0f && !CheckState("ground")))//刚体的速度还要调一调
        {
            anim.SetTrigger("roll");
        }
        
        if (pi.jump)
        { 
            anim.SetTrigger("forjump");
            canAttack = false;
        }

        //if (pi.attack && (CheckState("ground") || CheckStateTag("attack")) && canAttack)
        //{
        //    anim.SetTrigger("attack");
        //}
        if ((pi.rb||pi.lb) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (pi.rb)
            {
                anim.SetBool("R0L1",false);
                anim.SetTrigger("attack");
            }
            else if (pi.lb && !leftIsShield)
            {
                anim.SetBool("R0L1", true);
                anim.SetTrigger("attack");
            }
        }

        if ((pi.rt || pi.lt) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (pi.rt)//do right heavy attack
            {
                //anim.SetBool("R0L1", false);
                //anim.SetTrigger("attack");
            }
            else
            {
                if (pi.lt && !leftIsShield)
                {
                    //anim.SetBool("R0L1", true);
                    //anim.SetTrigger("attack");
                }
                else if (pi.lt && leftIsShield)
                {
                    //print("pi.lt && leftIsShield");
                    anim.SetTrigger("counterBack");
                }
            }
        }

        if (pi.action && transform.GetComponent<DirectorManager>().victim!=null&&
            transform.GetComponent<DirectorManager>().victim.ac.CheckState("stunned"))
        {
            OnAction.Invoke();
        }

        if (leftIsShield)
        {
            if (CheckState("ground")||CheckState("blocked"))
            {
                anim.SetBool("defense", pi.defense);
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);
            }
            else
            {
                anim.SetBool("defense", false);
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
        }
        

        if (camcon.lockState == false)
        {
            if (pi.Dforword > 0.01f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.05f);
            }
            if (losk == false)
                Psave = pi.Dforword * model.transform.forward * walkSpeed * ((pi.run) ? runMu : 1.0f);
        }
        else
        {
            if(trackDirection == false)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = Psave.normalized;
            }
            
            if (losk == false)
            Psave = pi.Dvec * walkSpeed * ((pi.run) ? runMu : 1.0f);
        }
        if (pi.changeOneRoTwo)
        {
            Animator am = model.GetComponent<Animator>();
            ActorManager actorManager= GetComponent<ActorManager>();
            WeaponManager wm = model.GetComponent<WeaponManager>();
            Hands_index = (Hands_index+1)%OneOrTwo.Count;
            if (OneOrTwo[Hands_index] == "One")
            {
                am.runtimeAnimatorController = actorManager.OneHand;
                //sides[(weapon_side+1)%sides.Count]这一侧武器需要还原
                if(sides[(weapon_side + 1) % sides.Count] == "L")
                {
                    wm.whL.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if(sides[(weapon_side + 1) % sides.Count] == "R")
                {
                    wm.whR.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else if (OneOrTwo[Hands_index] == "Two")
            {
                am.runtimeAnimatorController = actorManager.TwoHand;
                //sides[(weapon_side+1)%sides.Count]这一侧武器需要关闭
                if (sides[(weapon_side + 1) % sides.Count] == "L")
                {
                    wm.whL.transform.GetChild(0).gameObject.SetActive(false);
                }
                else if (sides[(weapon_side + 1) % sides.Count] == "R")
                {
                    wm.whR.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        //如果为单手状态，那么两手都可以持有武器，此时可以调变weapon_side
        if (pi.changeSide && OneOrTwo[Hands_index] == "One")
        {
            weapon_side = (weapon_side + 1) % sides.Count;
        }
        if (pi.changeWeapon)
        {
            //切换武器
            if (sides[weapon_side] == "L")
            {
                GameManager.Instance._WF.createWeapon(list_L[(++weapon_index_L) % list_L.Count],
            sides[weapon_side], model.GetComponent<WeaponManager>());
            }
            else if (sides[weapon_side] == "R")
            {
                GameManager.Instance._WF.createWeapon(list_R[(++weapon_index_R) % list_R.Count],
            sides[weapon_side], model.GetComponent<WeaponManager>());
            }
        }

    }
    /*private void RuleForOneOrTwohands(string OneOrTwoHands)
    {

    }*/
    private void InitWeaponAsset()
    {
        list_L= new List<string>();
        list_L.Add("DEF_A");
        list_L.Add("Falchion");
        list_L.Add("sword");
        list_L.Add("bolas");
        list_R = new List<string>();
        list_R.Add("DEF_A");
        list_R.Add("Falchion");
        list_R.Add("sword");
        list_R.Add("bolas");
        sides = new List<string>();
        sides.Add("L");
        sides.Add("R");
        OneOrTwo = new List<string>();
        OneOrTwo.Add("One");
        OneOrTwo.Add("Two");
        weapon_index_L = 0;
        weapon_index_R = 0;
        weapon_side = 0;
        Hands_index = 0;
    }

    public bool CheckState(string stateName,string layerName = "Action")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }
    public bool CheckStateTag(string tagName, string layerName = "Action")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
        return result;
    }
    void FixedUpdate()
    {
        /*if ((!photonView.IsMine || !PhotonNetwork.IsConnected)
        || gameObject.layer != LayerMask.NameToLayer("Enemy")) return;*/
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(Psave.x, rigid.velocity.y, Psave.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }
    public void Onjump()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnabled = false;
        losk = true;
        trackDirection = true;
    }
    public void IsGround()
    {

        anim.SetBool("isGround", true);
    }
    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }
    public void OnGroundEnter()
    {
        pi.inputEnabled = true;
        losk = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
    }
    public void OnFallEnter()
    {
        pi.inputEnabled = false;
        losk = true;
    }
    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnabled = false;
        losk = true;
        trackDirection = true;
    }
    public void OnJabOnter()
    {
        pi.inputEnabled = false;
        losk = true;
    }
    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");
    }

    public void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;

    }
    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity"); 
    }

    public void OnAttackExit()
    {
        //print("OnAttackExit");
        model.SendMessage("WeaponDisable");
    }
    public void OnHitEnter()
    {
        pi.inputEnabled = false;
        Psave = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnDieEnter()
    {
        pi.inputEnabled = false;
        Psave = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }
    public void OnBlockedEnter()
    {
        pi.inputEnabled = false;
    }
    public void OnStunnedEnter()
    {
        pi.inputEnabled = false;
        Psave = Vector3.zero;
    }

    public void OnCounterBackEnter()
    {
        pi.inputEnabled = false;
        Psave = Vector3.zero;
    }
    public void OnCounterBackExit()
    {
        model.SendMessage("CounterBackDisable");
    }
    public void OnLockEnter()
    {
        pi.inputEnabled = false;
        Psave = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }


    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("attack1hC"))
        {

            deltaPos += (0.2f * deltaPos + 0.8f * (Vector3)_deltaPos) / 1.0f;
        }

    }
    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
    public void SetBooll(string boolName, bool value)
    {
        if (anim != null)
            anim.SetBool(boolName, value);
    }
    public void OnAttackIdleEnter()
    {
        pi.inputEnabled = true;
        losk = false;

        lerpTarget = 0;
    }
    public void OnAttackIdleUpdate()
    {
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.2f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }

}

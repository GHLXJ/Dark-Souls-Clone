using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviourPun
{
    [Header("===== Output signals =====")]

    public float Dup;
    public float Dright;
    public float Dforword;
    public Vector3 Dvec;//应该是物体移动方向
    public float Jup;
    public float Jright;

    public bool run;
    public bool defense;

    public bool action;
    public bool jump;
    protected bool lastjump = false;
    protected bool lastAttack = false;
    public bool roll;
    public bool lockon;
    public bool lb;
    public bool lt;
    public bool rb;
    public bool rt;
    public bool changeWeapon;
    public bool changeSide;
    public bool changeOneRoTwo;

    [Header("===== Others =====")]

    public bool inputEnabled = true;

    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
    protected void UpdateDmagDvec(float Dup2,float Dright2)
    {
        Dforword = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));

        Dvec = Dright2 * transform.right + Dup2 * transform.forward;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("===== Joystick Settings =====")]
    public string axisX = "axisX";
    public string axisY = "axisY";

    [Header("===== Output signals =====")]
    public float Dup;
    public float Dright;
    public float Dforword;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;


    public bool run;
    public bool jump;
    public bool lastjump = false;
    public bool attack;
    public bool lastAttack = false;

    [Header("===== Others =====")]

    public bool inputEnabled = true;

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;





    // Update is called once per frame
    void Update()
    {
        targetDup = Input.GetAxis(axisY);
        targetDright = Input.GetAxis(axisX);

        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 temp = SquareToCircle(new Vector2(Dright, Dup));

        float Dright2 = temp.x;
        float Dup2 = temp.y;

        Dforword = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));//平方开根
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;//



    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
}



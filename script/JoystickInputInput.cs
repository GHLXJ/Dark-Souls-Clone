using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInputInput : IUserInput
{
    [Header("===== Joystick Settings =====")]
    //public string axisX = "axisX";
    //public string axisY = "axisY";
    //public string axisJright = "axis3";
    //public string axisJup = "axis5";
    //public string btnA = "btn0";//run
    //public string btnB = "btn1";
    //public string btnC = "btn2";
    public string btnD = "btn3";
    public string btnLB = "btn4";
    public string btnLT = "btn6";
    public string btnRB = "btn5";
    public string btnRT = "btn7";
    public string btnJstick = "btn11";

    //public MyButton buttonA = new MyButton();
    //public MyButton buttonB = new MyButton();
    //public MyButton buttonC = new MyButton();
    public MyButton buttonD = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();
    public MyButton buttonRB = new MyButton();
    public MyButton buttonRT = new MyButton();
    public MyButton buttonJstick = new MyButton();

    void Update()
    {
        //buttonA.Tick(Input.GetButton(btnA));//
        //buttonB.Tick(Input.GetButton(btnB));
        //buttonC.Tick(Input.GetButton(btnC));
        buttonD.Tick(Input.GetButton(btnD));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonLT.Tick(Input.GetButton(btnLT));
        buttonRB.Tick(Input.GetButton(btnRB));
        buttonRT.Tick(Input.GetButton(btnRT));
        buttonJstick.Tick(Input.GetButton(btnJstick));


        //Jup = -1*Input.GetAxis(axisJup);
        //Jright = Input.GetAxis(axisJright);

        //targetDup = Input.GetAxis(axisY);
        //targetDright = Input.GetAxis(axisX);

        //if (!inputEnabled)
        //{
        //    targetDup = 0;
        //    targetDright = 0;
        //}

        //Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.3f);
        //Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.3f);

        //Vector2 temp = SquareToCircle(new Vector2(Dright, Dup));

        //float Dright2 = temp.x;
        //float Dup2 = temp.y;


        //Dforword = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));

        //Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        //run = (buttonA.IsPressing && !buttonA.IsDelaying)|| buttonA.IsExtending;

        //roll = buttonA.OnReleased && buttonA.IsDelaying;

        //defense = buttonB.IsPressing;
        //defense = buttonLB.IsPressing;

        //jump = buttonA.OnPressed && buttonA.IsExtending;


        //attack = buttonC.OnPressed;
        rb = buttonRB.OnPressed;
        rt = buttonRT.OnPressed;
        lb = buttonLB.OnPressed;
        lt = buttonLT.OnPressed;
        lockon = buttonJstick.OnPressed;


    }

}

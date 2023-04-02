using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{

    public string keyUp = "w";//public string axisY = "axisY";
    public string keyDown = "s";//public string axisY = "axisY";
    public string keyLeft = "a";//public string axisX = "axisX";
    public string keyRight = "d";//public string axisX = "axisX";

    public string axisJright = "Mouse X";//public string axisJup = "axis5";//axis���ֱ���Jright�ǿ����ӽǣ�
    public string axisJup = "Mouse Y";//public string axisJup = "axis5";

    public string KeyA = "left shift"; //public string btnA = "btn0";
    public string KeyB = "j";
    
    public string KeyD = "mouse 1";//������ public string btnLB = "btn4";

    public string keyJup = "up";
    public string keyJdown = "down";
    public string keyJright = "right";
    public string keyJleft = "left";


    public string btnLB = "i";
    public string btnLT = "e";//���ع���
    public string KeyC = "mouse 0";//������ public string btnRB = "o";
    public string btnRT = "q";//���ع���
    public string btnJstick = "p";//��������


    [Header("===== Output signals =====")]
    public bool mouseEnable = false;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;


    public MyButton buttonA = new MyButton();
    //public MyButton buttonB = new MyButton();
    //public MyButton buttonC = new MyButton();
    //public MyButton buttonD = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();
    public MyButton buttonRB = new MyButton();
    public MyButton buttonRT = new MyButton();
    public MyButton buttonJstick = new MyButton();


    //�Լ���ӵı���
    //public string KeyRoll = "b";
    void Update()
    {
        buttonA.Tick(Input.GetKey(KeyA));
        //buttonB.Tick(Input.GetButton(btnB));
        //buttonC.Tick(Input.GetButton(btnC));
        //buttonD.Tick(Input.GetButton(btnD));
        buttonLB.Tick(Input.GetKey(KeyD));
        buttonLT.Tick(Input.GetKey(btnLT));
        buttonRB.Tick(Input.GetKey(KeyC));
        buttonRT.Tick(Input.GetKey(btnRT));
        buttonJstick.Tick(Input.GetKey(btnJstick));


        if (mouseEnable == true)
        {
            Jup = Input.GetAxis(axisJup) * 3.0f * mouseSensitivityY;
            Jright = Input.GetAxis(axisJright) * 2.5f * mouseSensitivityX;
        }
        else
        {
            Jup = (Input.GetKey(keyJup) ? 1.0f : 0) - (Input.GetKey(keyJdown) ? 1.0f : 0);
            Jright = (Input.GetKey(keyJright) ? 1.0f : 0) - (Input.GetKey(keyJleft) ? 1.0f : 0);
        }


        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

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



        Dforword = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));

        Dvec = Dright2 * transform.right + Dup2 * transform.forward;


        //run = Input.GetKey(KeyA);
        run = (buttonA.IsPressing && !buttonA.IsDelaying) || buttonA.IsExtending;

        roll = buttonA.OnReleased && buttonA.IsDelaying;//IsExtending��OnReleased(�ͷ�˲��)����չ
                                                        //IsDelaying��OnPressing����չ

        jump = buttonA.OnPressed && buttonA.IsExtending;

        action = buttonRT.OnPressed;
        //defense = Input.GetKey(KeyD);
        defense = buttonLB.IsPressing;//IsPressingһֱ��ѹ

        //bool tempjump = Input.GetKey(KeyB);
        //if (tempjump != lastjump && tempjump == true)
        //{
        //    jump = true;
        //}
        //else
        //{
        //    jump = false;
        //}
        //lastjump = tempjump;

        //bool newAttack = Input.GetKey(KeyC);
        //if (newAttack != lastAttack && newAttack == true)
        //{
        //    rb = true;
        //}
        //else
        //{
        //    rb = false;
        //}

        //lastAttack = newAttack;

        rb = buttonRB.OnPressed;//OnPressed����˲��
        rt = buttonRT.OnPressed;
        lb = buttonLB.OnPressed;
        lt = buttonLT.OnPressed;
        lockon = buttonJstick.OnPressed;

    }
    
}
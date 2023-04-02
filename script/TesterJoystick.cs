using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterJoystick : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        //print(Input.GetAxis("Horizontal"));
        //输出（默认设置左）蘑菇头左右移动时的量
        //print(Input.GetAxis("Vertical"));
        //输出（默认设置左）蘑菇头上下移动时的量
        print("RT:" + Input.GetAxis("RT"));


    }
}

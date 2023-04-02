using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FSMonEXIT : StateMachineBehaviour
{
    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    public string[] Exitmsg;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var msg in Exitmsg)
        {
            animator.gameObject.SendMessageUpwards(msg);
        }
    }
}

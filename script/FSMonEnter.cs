using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMonEnter : StateMachineBehaviour
{
    public string[] onEnter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var msg in onEnter)
        {
            animator.gameObject.SendMessageUpwards(msg);
        }
    }

}

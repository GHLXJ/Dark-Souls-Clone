using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorIKSetting : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Vector3 a;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void OnAnimatorIK()
    {
        // anim:private Animator
        Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        leftLowerArm.localEulerAngles += a;
        print(leftLowerArm.localEulerAngles);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
    }
}

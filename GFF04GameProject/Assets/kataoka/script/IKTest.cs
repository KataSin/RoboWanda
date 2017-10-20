using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest : MonoBehaviour
{
    private Animator avator;
    public Transform lookAtObj = null;

    [SerializeField, Range(0, 1)]
    private float lookAtWeight = 1.0f;
    [SerializeField, Range(0, 1)]
    private float bodyWeight = 0.4f;
    [SerializeField, Range(0, 1)]
    private float headWeight = 0.7f;
    [SerializeField, Range(0, 1)]
    private float eyesWeight = 0.5f;
    [SerializeField, Range(0, 1)]
    private float clampWeight = 0.5f;

    // Use this for initialization
    void Start()
    {
        avator = GetComponent<Animator>();
        if (lookAtObj == null)
        {
            lookAtObj = Camera.main.transform;
        }
    }

    void OnAnimatorIK(int layorIndex)
    {
        if (avator)
        {
            avator.SetLookAtWeight(lookAtWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
            avator.SetLookAtPosition(lookAtObj.position);
            //avator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            //avator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            //avator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            //avator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            
            //avator.SetIKPosition(AvatarIKGoal.LeftHand, lookAtObj.position);
            //avator.SetIKRotation(AvatarIKGoal.LeftHand, lookAtObj.rotation);
            //avator.SetIKPosition(AvatarIKGoal.RightHand, lookAtObj.position);
            //avator.SetIKRotation(AvatarIKGoal.RightHand, lookAtObj.rotation);
        }
    }
}

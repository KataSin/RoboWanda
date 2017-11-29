using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSample : MonoBehaviour
{

    private Animator animator_;

    [SerializeField]
    private GameObject destination_;

    // Use this for initialization
    void Start()
    {
        animator_=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimatorIK()
    {
        animator_.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator_.SetIKPosition(AvatarIKGoal.LeftHand, destination_.transform.position);

        //animator_.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        //animator_.SetIKRotation(AvatarIKGoal.LeftHand, destination_.transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeepSwat : MonoBehaviour
{
    private Animator m_Animator;

    [SerializeField]
    private GameObject L_ik_;
    [SerializeField]
    private GameObject R_ik_;

    // Use this for initialization
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAnimatorIK()
    {
        m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        m_Animator.SetIKPosition(AvatarIKGoal.LeftHand, L_ik_.transform.position);

        m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        m_Animator.SetIKPosition(AvatarIKGoal.RightHand, R_ik_.transform.position);
    }
}

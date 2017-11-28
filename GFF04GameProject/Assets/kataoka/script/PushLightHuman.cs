using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PushLightHuman : MonoBehaviour
{
    public GameObject m_RightHandPointIK;
    public GameObject m_LeftHandPointIK;

    private NavMeshAgent m_Agent;
    //アニメーター
    private Animator m_Animator;

    private float m_RotateVelocityY;

    private float m_RotateVelocitySeveY;
    // Use this for initialization
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Agent = transform.parent.GetComponent<NavMeshAgent>();

        m_RotateVelocityY = 0.0f;
        m_RotateVelocitySeveY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //アニメーションパラメーターに渡す
        m_Animator.SetFloat("speed", m_Agent.velocity.magnitude);

        m_RotateVelocityY = transform.rotation.eulerAngles.y-m_RotateVelocitySeveY;
        m_Animator.SetFloat("speedRotate", m_RotateVelocityY);
        m_RotateVelocitySeveY = transform.eulerAngles.y;
    }

    public void LateUpdate()
    {
    }

    public void OnAnimatorIK(int layerIndex)
    {
        m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        m_Animator.SetIKPosition(AvatarIKGoal.RightHand, m_RightHandPointIK.transform.position);
        m_Animator.SetIKPosition(AvatarIKGoal.LeftHand, m_LeftHandPointIK.transform.position);

        m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        m_Animator.SetIKRotation(AvatarIKGoal.RightHand, m_RightHandPointIK.transform.rotation);
        m_Animator.SetIKRotation(AvatarIKGoal.LeftHand, m_LeftHandPointIK.transform.rotation);

    }


}

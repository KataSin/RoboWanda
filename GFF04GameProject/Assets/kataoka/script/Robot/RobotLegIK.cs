using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLegIK : MonoBehaviour
{
    //アニメーター
    private Animator m_Animator;
    //右足のウェイト
    private float m_RightLegWeight;
    //左足のウェイト
    private float m_LeftLegWeight;
    //右足の位置
    private Vector3 m_RightLegPos;
    //左足の位置
    private Vector3 m_LeftLegPos;
    //右足の角度
    private Quaternion m_RightLegRotation;
    //左足の角度
    private Quaternion m_LeftLegRotation;
    //右足Rayを飛ばす位置
    private GameObject m_RightLegRay;
    //左足Rayを飛ばす位置
    private GameObject m_LeftLegRay;


    Ray left;
    Ray right;
    // Use this for initialization
    void Start()
    {
        //アニメーター
        m_Animator = GetComponent<Animator>();
        //各種初期化
        m_RightLegWeight = 0.0f;
        m_LeftLegWeight = 0.0f;
        //Rayを取得
        m_RightLegRay = GameObject.FindGameObjectWithTag("RightLegRay").gameObject;
        m_LeftLegRay = GameObject.FindGameObjectWithTag("LeftLegRay").gameObject;
        //ポジション設定
        m_RightLegPos = m_RightLegRay.transform.position;
        m_LeftLegPos = m_LeftLegRay.transform.position;
        //角度設定
        m_LeftLegRotation = m_LeftLegRay.transform.rotation;
        m_RightLegRotation = m_RightLegRay.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //ロボットとは当たらない
        int layer = ~(1 << 8);
        //右足
        left = new Ray(m_LeftLegRay.transform.position, Vector3.down*2.0f);
        RaycastHit leftHit;
        if (Physics.Raycast(left, out leftHit, 5.0f, layer))
        {
            m_LeftLegPos = leftHit.point;
            m_LeftLegRotation = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
        }

        //左足
        right = new Ray(m_RightLegRay.transform.position, Vector3.down * 2.0f);
        RaycastHit rightHit;
        if (Physics.Raycast(right, out rightHit, 5.0f, layer))
        {
            m_RightLegPos = rightHit.point;
            m_RightLegRotation = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
        }

        Debug.Log(m_LeftLegWeight);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(left);
        Gizmos.DrawRay(right);
    }

    public void OnAnimatorIK(int layerIndex)
    {
        m_LeftLegWeight = m_Animator.GetFloat("RobotLeftLeg");
        m_RightLegWeight = m_Animator.GetFloat("RobotRightLeg");
        //座標
        m_Animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, m_RightLegWeight);
        m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, m_LeftLegWeight);
        //回転
        m_Animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, m_RightLegWeight);
        m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, m_LeftLegWeight);
        //IK設定
        m_Animator.SetIKPosition(AvatarIKGoal.RightFoot, m_RightLegPos + new Vector3(0, 1, 0));
        m_Animator.SetIKPosition(AvatarIKGoal.LeftFoot, m_LeftLegPos + new Vector3(0, 1, 0));
        m_Animator.SetIKRotation(AvatarIKGoal.RightFoot, m_RightLegRotation);
        m_Animator.SetIKRotation(AvatarIKGoal.LeftFoot, m_LeftLegRotation);
    }

}

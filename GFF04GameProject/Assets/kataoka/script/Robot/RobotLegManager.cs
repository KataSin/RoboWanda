using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLegManager : MonoBehaviour
{
    public enum Leg
    {
        NO,
        RIGHT,
        LEFT
    }
    //アニメーター
    private Animator m_Animator;
    //ロボットマネージャー
    private RobotManager m_Manager;
    //足のオブジェクト
    private GameObject m_RobotRightLeg;
    private GameObject m_RobotLeftLeg;
    //現在沈んでいる足
    private Leg m_Leg;
    //IKのウェイト
    private float m_IkLeftWeight;
    private float m_IkRightWeight;
    //Rayが当たっている場所
    private Vector3 m_LeftLegRayPoint;
    private Vector3 m_RightLegRayPoint;
    // Use this for initialization
    void Start()
    {
        m_Animator = GameObject.FindGameObjectWithTag("Robot").GetComponent<Animator>();
        m_Manager = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>();
        m_RobotLeftLeg = GameObject.FindGameObjectWithTag("RobotLeftLeg");
        m_RobotRightLeg = GameObject.FindGameObjectWithTag("RobotRightLeg");
        m_Leg = Leg.NO;
        m_IkLeftWeight = 0.0f;
        m_IkRightWeight = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Leg == Leg.NO)
        {
            if (m_RobotLeftLeg.GetComponent<RobotLeg>().GetLegFlag())
            {
                m_Leg = Leg.LEFT;
                m_LeftLegRayPoint = m_RobotLeftLeg.GetComponent<RobotLeg>().GetIKPoint();
                m_Manager.SetAction(RobotAction.RobotState.ROBOT_FALL_DOWN,false,true);
            }
            else if (m_RobotRightLeg.GetComponent<RobotLeg>().GetLegFlag())
            {
                m_Leg = Leg.RIGHT;
                m_RightLegRayPoint = m_RobotRightLeg.GetComponent<RobotLeg>().GetIKPoint();
                m_Manager.SetAction(RobotAction.RobotState.ROBOT_FALL_DOWN, false,true);
            }
        }

        if (m_Leg == Leg.LEFT)
        {
            m_IkLeftWeight += Time.deltaTime;
            m_IkRightWeight -= Time.deltaTime;
        }
        else if (m_Leg == Leg.RIGHT)
        {
            m_IkLeftWeight -= Time.deltaTime;
            m_IkRightWeight += Time.deltaTime;
        }
        else
        {
            m_IkLeftWeight -= Time.deltaTime;
            m_IkRightWeight -= Time.deltaTime;
        }
        //ウェイトクランプ
        m_IkLeftWeight = Mathf.Clamp(m_IkLeftWeight, 0.0f, 1.0f);
        m_IkRightWeight = Mathf.Clamp(m_IkRightWeight, 0.0f, 1.0f);

        ////Ray関係
        //Ray leftLegRay = new Ray(m_RobotLeftLeg.transform.position, Vector3.down);
        //Ray rightLegRay = new Ray(m_RobotRightLeg.transform.position, Vector3.down);
        //RaycastHit leftHit;
        //RaycastHit rightHit;
        //int layer = ~(1 << 8 | 1 << 2);
        //if (Physics.Raycast(leftLegRay, out leftHit, 100, layer))
        //{
        //    m_LeftLegRayPoint = leftHit.point;
        //}
        //if (Physics.Raycast(rightLegRay, out rightHit, 100, layer))
        //{
        //    m_RightLegRayPoint = rightHit.point;
        //}

    }

    public void OnAnimatorIK(int layerIndex)
    {
        m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, m_IkLeftWeight);
        m_Animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, m_IkRightWeight);
        m_Animator.SetIKPosition(AvatarIKGoal.LeftFoot, m_LeftLegRayPoint);
        m_Animator.SetIKPosition(AvatarIKGoal.RightFoot, m_RightLegRayPoint);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(m_LeftLegRayPoint, new Vector3(10,10,10));
        Gizmos.DrawCube(m_RightLegRayPoint, new Vector3(10, 10, 10));

    }



    /// <summary>
    /// 埋まっている足を設定
    /// </summary>
    /// <param name="leg">足</param>
    public void LegSet(Leg leg)
    {
        m_Leg = leg;
    }
}

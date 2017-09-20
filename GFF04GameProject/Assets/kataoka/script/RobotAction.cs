using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;


public class RobotAction : MonoBehaviour
{

    public enum RobotState
    {
        ROBOT_NULL,
        ROBOT_IDLE,
        ROBOT_MOVE,
        ROBOT_ARM_ATTACK,
        ROBOT_LEG_ATTACK,
        ROBOT_SEARCH
    }

    [SerializeField, Tooltip("ロボットのスピード"), HeaderAttribute("ロボット移動関係")]
    public float m_RobotSpeed;
    [SerializeField, Tooltip("ロボットがどのくらいプレイヤーの近くによるか")]
    public float m_Player_Enemy_Distance;

    //ナビエージェント
    private NavMeshAgent m_NavAgent;
    //ロボット
    private GameObject m_Robot;
    //プレイヤー
    private GameObject m_Player;
    //現在の状態
    private RobotState m_RobotState;
    //アニメーター
    private Animator m_Animator;
    //ロボットのY軸速度
    private float m_VelocityY;
    private float m_SeveVelocityY;

    private bool flag = true;
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
        m_Animator = GetComponent<Animator>();
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_RobotState = RobotState.ROBOT_NULL;
        m_NavAgent.destination = m_Player.transform.position;
        m_VelocityY = 0.0f;
        m_SeveVelocityY = 0.0f;
    }
    /// <summary>
    /// ロボットがプレイヤーに向かって動く
    /// </summary>
    /// <returns></returns>
    public Func<bool> RobotMove()
    {
        Func<bool> move = () =>
            {
                m_NavAgent.isStopped = false;
                m_NavAgent.speed = m_RobotSpeed;
                m_NavAgent.stoppingDistance = m_Player_Enemy_Distance;

                //Y軸をロボットに合わせる
                m_NavAgent.destination = m_Player.transform.position;
                m_RobotState = RobotState.ROBOT_MOVE;
                m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
                m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);

                //m_VelocityY = transform.rotation.eulerAngles.y - m_SeveVelocityY;
                //m_Animator.SetFloat("RobotRotateSpeed", 5);

                return true;
            };
        return move;
    }
    /// <summary>
    /// ロボットが止まる
    /// </summary>
    /// <returns>終わったかどうか</returns>
    public Func<bool> RobotIdle()
    {
        Func<bool> idle = () =>
            {
                m_NavAgent.isStopped = true;
                m_RobotState = RobotState.ROBOT_IDLE;
                m_NavAgent.destination = m_Player.transform.position;
                m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
                m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);
                //どうせループなので
                return true;
            };
        return idle;
    }

    /// <summary>
    /// ロボットの腕の攻撃
    /// </summary>
    /// <returns>終わったかどうか</returns>
    public Func<bool> RobotArmAttack()
    {
        Func<bool> armAttack = () =>
        {
            bool endAnim = true;
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "Attack")
            {
                endAnim = m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
            }
            m_RobotState = RobotState.ROBOT_ARM_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        return armAttack;
    }
    /// <summary>
    /// ロボットが探すアニメーションだけ
    /// </summary>
    /// <returns></returns>
    public Func<bool> RobotSearch()
    {
        Func<bool> armAttack = () => 
        {
            bool endAnim = true;
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "Search")
            {
                endAnim = m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
            }
            m_RobotState = RobotState.ROBOT_ARM_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        return armAttack;
    }
}

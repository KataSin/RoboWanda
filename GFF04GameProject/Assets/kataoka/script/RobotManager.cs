using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RobotManager : MonoBehaviour
{
    ////アクションたち
    //struct ActionState
    //{
    //    //アクション
    //    public Func<bool> action;
    //    //割り込みかどうか
    //    public bool interrupt;
    //    //継続するか
    //    public bool continuation;
    //    //アクションの種類
    //    public RobotAction.RobotState state;
    //}
    //アクションたち
    private Dictionary<RobotAction.RobotState,Func<bool>> m_Actions;
    //アニメーション
    private Animation m_Animation;
    //ロボットアクション
    private RobotAction m_RobotAction;
    //現在アクションしているかどうか
    private bool m_IsAction;
    //現在のロボットアクション
    private RobotAction.RobotState m_RobotState;
    // Use this for initialization
    void Start()
    {
        m_Actions = new Dictionary<RobotAction.RobotState, Func<bool>>();
        m_RobotAction = GetComponent<RobotAction>();
        m_IsAction = true;
        m_RobotState = RobotAction.RobotState.ROBOT_MOVE;

        AddAction(RobotAction.RobotState.ROBOT_IDLE, m_RobotAction.RobotIdle());
        AddAction(RobotAction.RobotState.ROBOT_MOVE, m_RobotAction.RobotMove());
        AddAction(RobotAction.RobotState.ROBOT_ARM_ATTACK, m_RobotAction.RobotArmAttack());


    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Actions[m_RobotState]())
        {
            m_RobotState = RobotAction.RobotState.ROBOT_IDLE;
        }

    }

    /// <summary>
    /// アクションをセットする
    /// </summary>
    /// <param name="state">アクションステート</param>
    /// <param name="loop">ループするかどうか</param>
    public void SetAction(RobotAction.RobotState state,bool loop)
    {
        m_RobotState = state;
    }

    /// <summary>
    /// アクションを追加
    /// </summary>
    /// <param name="state">アクションの種類</param>
    /// <param name="action">アクション</param>
    private void AddAction(RobotAction.RobotState state, Func<bool> action)
    {
        m_Actions[state] = action;
    }

}

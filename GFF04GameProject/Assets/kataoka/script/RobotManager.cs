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
    private Dictionary<RobotAction.RobotState, Func<bool>> m_Actions;
    //アニメーション
    private Animation m_Animation;
    //ロボットアクション
    private RobotAction m_RobotAction;
    //現在アクションしているかどうか
    private bool m_IsAction;
    //現在のロボットアクション
    private RobotAction.RobotState m_RobotState;
    //現在のアクションがループするかどうか
    private bool m_IsLoop;
    // Use this for initialization
    void Start()
    {
        m_Actions = new Dictionary<RobotAction.RobotState, Func<bool>>();
        m_RobotAction = GetComponent<RobotAction>();
        m_IsAction = true;
        m_RobotState = RobotAction.RobotState.ROBOT_IDLE;

        AddAction(RobotAction.RobotState.ROBOT_IDLE, m_RobotAction.RobotIdle());
        AddAction(RobotAction.RobotState.ROBOT_MOVE, m_RobotAction.RobotMove());
        AddAction(RobotAction.RobotState.ROBOT_ARM_ATTACK, m_RobotAction.RobotArmAttack());
        AddAction(RobotAction.RobotState.ROBOT_SEARCH, m_RobotAction.RobotSearch());
        AddAction(RobotAction.RobotState.ROBOT_SEARCH_MOVE, m_RobotAction.RobotSearchMove());
        m_IsLoop = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_IsAction = m_Actions[m_RobotState]();
        //行動が終わったら待機に戻る
        if (m_IsAction)
        {
            SetAction(RobotAction.RobotState.ROBOT_IDLE, true);
        }

    }

    /// <summary>
    /// アクションをセットする
    /// </summary>
    /// <param name="state">アクションステート</param>
    /// <param name="loop">ループするかどうか</param>
    public void SetAction(RobotAction.RobotState state, bool loop)
    {
        if (!m_IsLoop && !m_IsAction) return;
        m_RobotState = state;
        m_IsLoop = loop;
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
    /// <summary>
    /// ロボットの状態を取得する
    /// </summary>
    /// <returns>ロボットの状態</returns>
    public RobotAction.RobotState GetRobotState()
    {
        return m_RobotState;
    }

}

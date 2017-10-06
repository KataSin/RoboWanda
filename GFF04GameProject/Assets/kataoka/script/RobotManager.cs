using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class RobotManager : MonoBehaviour
{
   [SerializeField, Tooltip("ロボットのHP")]
    public int m_RobotHp;


    //アクションたち
    public struct ActionFunc
    {
        //アクションスターと
        public Action actionStart;
        //アクションアップデート
        public Func<bool> actionUpdate;
    }
    //アクションたち
    private Dictionary<RobotAction.RobotState, ActionFunc> m_Actions;
    //アニメーション
    private Animation m_Animation;
    //ロボットアクション
    private RobotAction m_RobotAction;
    //現在アクションしているかどうか
    private bool m_IsAction;
    //現在のロボットアクション
    private RobotAction.RobotState m_RobotState;
    //直前のロボットアクション
    private RobotAction.RobotState m_PreState;
    //現在のアクションがループするかどうか
    private bool m_IsLoop;
    // Use this for initialization
    void Start()
    {
        m_Actions = new Dictionary<RobotAction.RobotState, ActionFunc>();
        m_RobotAction = GetComponent<RobotAction>();
        m_IsAction = true;
        m_RobotState = RobotAction.RobotState.ROBOT_IDLE;
        m_PreState = RobotAction.RobotState.ROBOT_NULL;
        //アクションの追加
        AddAction(RobotAction.RobotState.ROBOT_IDLE, m_RobotAction.RobotIdle());
        AddAction(RobotAction.RobotState.ROBOT_MOVE, m_RobotAction.RobotMove());
        AddAction(RobotAction.RobotState.ROBOT_ARM_ATTACK, m_RobotAction.RobotArmAttack());
        AddAction(RobotAction.RobotState.ROBOT_SEARCH, m_RobotAction.RobotSearch());
        AddAction(RobotAction.RobotState.ROBOT_SEARCH_MOVE, m_RobotAction.RobotSearchMove());
        AddAction(RobotAction.RobotState.ROBOT_GOOL_MOVE, m_RobotAction.RobotGoolMove());
        AddAction(RobotAction.RobotState.ROBOT_BILL_BREAK, m_RobotAction.RobotBillBreak());
        m_IsLoop = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_RobotState);
        //ロボット仮HPUI
        Text text = GameObject.FindGameObjectWithTag("RobotHp").GetComponent<Text>();
        text.text = "RobotHp:" + m_RobotHp.ToString();

        //ロボット死んだ処理
        if (m_RobotHp <= 0)
        {
            Destroy(gameObject);
        }

        //アクションのスタート
        if (m_RobotState != m_PreState)
        {
            m_Actions[m_RobotState].actionStart();
            m_PreState = m_RobotState;
        }
        //アクションアップデート(trueが帰ったら終了)
        m_IsAction = m_Actions[m_RobotState].actionUpdate();
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
    private void AddAction(RobotAction.RobotState state, ActionFunc action)
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
    /// <summary>
    /// ロボットにダメージを与える
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        m_RobotHp -= damage;
    }
}

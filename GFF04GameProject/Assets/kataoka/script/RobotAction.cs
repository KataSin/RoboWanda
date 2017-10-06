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
        ROBOT_SEARCH,
        ROBOT_SEARCH_MOVE,
        ROBOT_GOOL_MOVE,
        ROBOT_BILL_BREAK
    }
    [SerializeField, Tooltip("ビルコリジョン")]
    public GameObject m_BillCollision;

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
    //特定のポイントに当たったかどうか
    private bool m_SearchFlag;
    //サーチポイントたち
    private List<GameObject> m_SearchPoints;
    //ランダム
    private System.Random m_Random;
    //ランダムインデックス
    private int m_RandomIndex;
    //ゴールポイント
    private GameObject m_GoalPoint;
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
        //ランダム生成
        m_Random = new System.Random();
        //サーチ系
        m_SearchFlag = true;
        m_SearchPoints = new List<GameObject>();
        m_SearchPoints.AddRange(GameObject.FindGameObjectsWithTag("SearchPoint"));
        if (m_SearchPoints.Count <= 0) m_SearchPoints.Add(new GameObject());
        m_RandomIndex = m_Random.Next(0, m_SearchPoints.Count-1);
        m_GoalPoint = GameObject.FindGameObjectWithTag("GoalPoint");
    }
    /// <summary>
    /// ロボットがプレイヤーに向かって動く
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotMove()
    {

        Action moveStart = () =>
        {

        };

        //アクションアップデート
        Func<bool> move = () =>
            {
                m_NavAgent.isStopped = false;
                m_NavAgent.speed = m_RobotSpeed;
                m_NavAgent.stoppingDistance = m_Player_Enemy_Distance;

                //ロボットからゴールのベクトル計算
                Vector3 robotToGoalVec3 = (m_GoalPoint.transform.position - m_Robot.transform.position).normalized;
                //前か後か判定するために横ベクトルに変換
                Vector3 robotToGoalLeftVec3 = Vector3.Cross(robotToGoalVec3, Vector3.up).normalized;
                //さっき計算したやつをVector2に変換
                Vector2 robotToGoalVec2 = new Vector2(robotToGoalLeftVec3.x, robotToGoalLeftVec3.z).normalized;

                //ロボットからプレイヤーのベクトル計算
                Vector3 robotToPlayerVec3 = (m_Player.transform.position - m_Robot.transform.position).normalized;
                //それをVector2に変換
                Vector2 robotToPlayerVec2 = new Vector2(robotToPlayerVec3.x, robotToPlayerVec3.z);

                if (Vector2Cross(robotToGoalVec2, robotToPlayerVec2) < 0.0f)
                {
                    //Y軸をロボットに合わせる
                    m_NavAgent.destination = m_Player.transform.position;
                    m_RobotState = RobotState.ROBOT_MOVE;
                    m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
                    m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);
                }
                else
                {
                    RobotGoolMove().actionUpdate();
                }


                //m_VelocityY = transform.rotation.eulerAngles.y - m_SeveVelocityY;
                //m_Animator.SetFloat("RobotRotateSpeed", 5);

                return false;
            };

        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = moveStart;
        func.actionUpdate = move;

        return func;
    }
    /// <summary>
    /// ロボットが止まる
    /// </summary>
    /// <returns>終わったかどうか</returns>
    public RobotManager.ActionFunc RobotIdle()
    {
        Action idleStart = () =>
        {

        };

        Func<bool> idle = () =>
            {
                m_NavAgent.isStopped = true;
                m_RobotState = RobotState.ROBOT_IDLE;
                m_NavAgent.destination = m_Player.transform.position;
                m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
                m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);
                //どうせループなので
                return false;
            };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = idleStart;
        func.actionUpdate = idle;

        return func;
    }

    /// <summary>
    /// ロボットの腕の攻撃
    /// </summary>
    /// <returns>終わったかどうか</returns>
    public RobotManager.ActionFunc RobotArmAttack()
    {
        Action robotArmAttackStart = () =>
        {

        };


        Func<bool> armAttack = () =>
        {
            bool endAnim = false;
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "Attack")
            {
                endAnim = (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            }
            m_RobotState = RobotState.ROBOT_ARM_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotArmAttackStart;
        func.actionUpdate = armAttack;

        return func;
    }
    /// <summary>
    /// ロボットが探すアニメーションだけ
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotSearch()
    {
        Action robotSearchStart = () =>
        {

        };
        Func<bool> robotSearch = () =>
        {
            bool endAnim = false;
            m_NavAgent.isStopped = true;
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "Search")
            {
                endAnim = (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            }
            m_RobotState = RobotState.ROBOT_SEARCH;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };

        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotSearchStart;
        func.actionUpdate = robotSearch;

        return func;
    }
    /// <summary>
    /// ロボットが探す移動
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotSearchMove()
    {
        Action robotSearchMoveStart = () =>
        {
            m_NavAgent.isStopped = true;
            m_NavAgent.speed = m_RobotSpeed;
            m_NavAgent.stoppingDistance = 0.0f;
            m_NavAgent.velocity = Vector3.zero;
            m_RobotState = RobotState.ROBOT_SEARCH;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);

        };


        Func<bool> robotSerchMove = () =>
        {
            //探すアニメーションが終わったら次に行く
            bool endAnim = false;
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "Search")
            {
                endAnim = (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            }
            if (!endAnim) return false;

            Vector3 pos = m_NavAgent.destination;
            //当たったらポジションを更新
            if (m_SearchFlag)
            {
                m_RandomIndex = m_Random.Next(0, m_SearchPoints.Count - 1);
                m_NavAgent.destination = m_SearchPoints[m_RandomIndex].transform.position;
                m_SearchFlag = false;
            }

            m_RobotState = RobotState.ROBOT_MOVE;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);

            //m_VelocityY = transform.rotation.eulerAngles.y - m_SeveVelocityY;
            //m_Animator.SetFloat("RobotRotateSpeed", 5);

            return false;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotSearchMoveStart;
        func.actionUpdate = robotSerchMove;

        return func;
    }
    /// <summary>
    /// ロボットがゴールに向かって歩く
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotGoolMove()
    {
        Action robotGoolMoveStart = () =>
        {
        };


        Func<bool> robotGoolMove = () =>
        {
            m_NavAgent.isStopped = false;
            m_NavAgent.speed = m_RobotSpeed;
            m_NavAgent.stoppingDistance = 0.0f;

            m_NavAgent.destination = m_GoalPoint.transform.position;
            m_RobotState = RobotState.ROBOT_MOVE;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);

            //m_VelocityY = transform.rotation.eulerAngles.y - m_SeveVelocityY;
            //m_Animator.SetFloat("RobotRotateSpeed", 5);
            return false;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotGoolMoveStart;
        func.actionUpdate = robotGoolMove;

        return func;
    }

    public RobotManager.ActionFunc RobotBillBreak(GameObject bill)
    {
        Action robotBillBreakStart = () =>
        {
            m_NavAgent.isStopped = true;
        };


        Func<bool> robotBillBreak = () =>
        {
            bool endAnim = false;
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "Attack")
            {
                endAnim = (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            }
            m_RobotState = RobotState.ROBOT_ARM_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotBillBreakStart;
        func.actionUpdate = robotBillBreak;

        return func;
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_SearchPoints[m_RandomIndex])
        {
            m_SearchFlag = true;
        }
    }


    public float Vector2Cross(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x * rhs.y - rhs.x * lhs.y;
    }


}

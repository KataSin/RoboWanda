﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;


public class RobotAction : MonoBehaviour
{
    public GameObject testObj;

    public enum RobotState
    {
        ROBOT_NULL = 0,
        ROBOT_IDLE,
        ROBOT_TO_PLAYER_MOVE,
        ROBOT_TO_BILL_MOVE,
        ROBOT_ARM_ATTACK,
        ROBOT_BEAM_ATTACK,
        ROBOT_LEG_ATTACK,
        ROBOT_SEARCH,
        ROBOT_SEARCH_MOVE,
        ROBOT_GOOL_MOVE,
        ROBOT_BILL_BREAK
    }

    [SerializeField, Tooltip("ロボットのスピード"), HeaderAttribute("ロボット移動関係")]
    public float m_RobotSpeed;
    [SerializeField, Tooltip("ロボットがどのくらいプレイヤーの近くによるか")]
    public float m_Player_Enemy_Distance;

    [SerializeField, Tooltip("ロボットビームのクールダウン")]
    public float m_BeamCoolDownTime = 30.0f;


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
    //ロボットが見る補間タイム
    private float m_LookAtLerpTime;
    //ロボットが見る場所
    private Vector3 m_RobotLookAtPosition;
    //ロボットがプレイヤーを見ているかどうか
    private bool m_IsRobotLookAtPlayerFlag;
    //ビルたち
    private List<GameObject> m_Bills;
    void Start()
    {
        m_Bills = new List<GameObject>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        m_Bills.AddRange(GameObject.FindGameObjectsWithTag("Tower"));


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
        m_RandomIndex = m_Random.Next(0, m_SearchPoints.Count - 1);
        m_GoalPoint = GameObject.FindGameObjectWithTag("GoalPoint");

        m_LookAtLerpTime = 0.0f;
    }
    /// <summary>
    /// ロボットがプレイヤーに向かって動く
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotToPlayerMove()
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

                //Y軸をロボットに合わせる
                m_NavAgent.destination = m_Player.transform.position;
                m_RobotState = RobotState.ROBOT_TO_PLAYER_MOVE;
                m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
                m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);
                ////ロボットからゴールのベクトル計算
                //Vector3 robotToGoalVec3 = (m_GoalPoint.transform.position - m_Robot.transform.position).normalized;
                ////前か後か判定するために横ベクトルに変換
                //Vector3 robotToGoalLeftVec3 = Vector3.Cross(robotToGoalVec3, Vector3.up).normalized;
                ////さっき計算したやつをVector2に変換
                //Vector2 robotToGoalVec2 = new Vector2(robotToGoalLeftVec3.x, robotToGoalLeftVec3.z).normalized;

                ////ロボットからプレイヤーのベクトル計算
                //Vector3 robotToPlayerVec3 = (m_Player.transform.position - m_Robot.transform.position).normalized;
                ////それをVector2に変換
                //Vector2 robotToPlayerVec2 = new Vector2(robotToPlayerVec3.x, robotToPlayerVec3.z);

                //if (Vector2Cross(robotToGoalVec2, robotToPlayerVec2) < 0.0f)
                //{
                //    //Y軸をロボットに合わせる
                //    m_NavAgent.destination = m_Player.transform.position;
                //    m_RobotState = RobotState.ROBOT_MOVE;
                //    m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
                //    m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);
                //}
                //else
                //{
                //    RobotGoolMove().actionUpdate();
                //}


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
    /// ロボットのビームの攻撃
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotBeamAttack()
    {
        Action robotBeamAttackStart = () =>
        {
            m_NavAgent.isStopped = true;
        };


        Func<bool> robotBeamAttack = () =>
        {
            bool endAnim = false;
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "AttackHame")
            {
                endAnim = (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            }
            m_RobotState = RobotState.ROBOT_BEAM_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotBeamAttackStart;
        func.actionUpdate = robotBeamAttack;

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

            m_RobotState = RobotState.ROBOT_SEARCH_MOVE;
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
            m_RobotState = RobotState.ROBOT_TO_PLAYER_MOVE;
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
    /// <summary>
    /// ロボットが近くのビルに向かって歩く
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotBillMove()
    {
        Action robotBillMoveStart = () =>
        {
        };


        Func<bool> robotBillMove = () =>
        {
            m_NavAgent.isStopped = false;
            m_NavAgent.speed = m_RobotSpeed;
            m_NavAgent.stoppingDistance = 0.0f;
            //一番近いビル
            GameObject disObj = m_Bills[0];
            foreach (var i in m_Bills)
            {
                if (Vector3.Distance(disObj.transform.position, m_Robot.transform.position) >=
                    Vector3.Distance(i.transform.position, m_Robot.transform.position))
                {
                    disObj = i;
                }
            }
            m_NavAgent.destination = disObj.transform.position;

            m_RobotState = RobotState.ROBOT_TO_PLAYER_MOVE;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);

            //m_VelocityY = transform.rotation.eulerAngles.y - m_SeveVelocityY;
            //m_Animator.SetFloat("RobotRotateSpeed", 5);
            return false;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotBillMoveStart;
        func.actionUpdate = robotBillMove;

        return func;
    }


    /// <summary>
    /// ロボットがビルを壊す
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotBillBreak()
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
    /// <summary>
    /// ロボットがプレイヤーを見るかどうか
    /// </summary>
    /// <param name="flag">見るかどうかフラグ</param>
    private void SetRobotLookAt(bool flag)
    {
        m_IsRobotLookAtPlayerFlag = flag;
    }

    public void RobotLookAtIKUpdate()
    {
        if (m_IsRobotLookAtPlayerFlag)
        {
            m_LookAtLerpTime += Time.deltaTime;
        }
        else
        {
            m_LookAtLerpTime -= Time.deltaTime;
        }
        //基本ここを見てる
        Vector3 robotFront = m_Robot.transform.position + (m_Robot.transform.forward.normalized * 2000.0f) + new Vector3(0, 3000, 0);
        //プレイヤー座標
        Vector3 playerPos = m_Player.transform.position;

        testObj.transform.position = Vector3.Lerp(robotFront, playerPos, m_LookAtLerpTime);
    }
    /// <summary>
    /// IK系
    /// </summary>
    /// <param name="layorIndex"></param>
    void OnAnimatorIK(int layorIndex)
    {
        m_Animator.SetLookAtWeight(1.0f, 0.4f, 0.7f, 0.0f, 0.5f);
        m_Animator.SetLookAtPosition(testObj.transform.position);





        //avator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        //avator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        //avator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        //avator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        //avator.SetIKPosition(AvatarIKGoal.LeftHand, lookAtObj.position);
        //avator.SetIKRotation(AvatarIKGoal.LeftHand, lookAtObj.rotation);
        //avator.SetIKPosition(AvatarIKGoal.RightHand, lookAtObj.position);
        //avator.SetIKRotation(AvatarIKGoal.RightHand, lookAtObj.rotation);

    }

    /// <summary>
    /// Vector2の外積
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns>外積の結果</returns>
    public float Vector2Cross(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x * rhs.y - rhs.x * lhs.y;
    }


}

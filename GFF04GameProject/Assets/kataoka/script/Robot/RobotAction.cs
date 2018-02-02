using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;


public class RobotAction : MonoBehaviour
{
    public GameObject m_LookAtObj;
    public GameObject goPoint;
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
        ROBOT_BILL_BREAK,
        ROBOT_FALL_DOWN,
        ROBOT_MISSILE_ATTACK,
        ROBOT_HELI_ATTACK,
        ROBOT_DEAD,
        ROBOT_BOMBING_ATTACK,
        ROBOT_MISSILE_BEAM_ATTACK,
        ROBOT_TANK_ATTACK
    }

    [SerializeField, Tooltip("ロボットのスピード"), HeaderAttribute("ロボット移動関係")]
    public float m_RobotSpeed;
    [SerializeField, Tooltip("ロボットがどのくらいプレイヤーの近くによるか")]
    public float m_Player_Enemy_Distance;

    [SerializeField, Tooltip("ロボットビームのクールダウン")]
    public float m_BeamCoolDownTime = 30.0f;

    //ミサイルスポーン
    private GameObject m_MissileSpawn;
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

    //ロボットが見る場所
    private Vector3 m_RobotLookAtPosition;
    //ロボットが見る場所保存
    private Vector3 m_SeveRobotLokAtPosition;
    //ロボットが見るフラグ
    private bool m_RobotLookAtFlag;

    //ロボットが見ているかどうか
    private bool m_IsRobotLookAtFlag;
    //ロボットの見る場所の速度
    private Vector3 m_SpringVelo;
    //ビルたち
    private List<GameObject> m_Bills;
    //ロボットが壊すビル
    private GameObject m_BreakBill;
    //IKを有効にするかどうか
    private bool m_IsIK;
    //ビル壊すときと足踏み付けの球面補間用
    private float m_LerpTime = 0.0f;
    private Quaternion m_BillQuaternion;
    private Quaternion m_RobotQuaternion;
    private Quaternion m_PlayerQuaternion;
    //ロボット足マネージャー
    private RobotLegManager m_LegManager;
    //転ぶとき前に進む時間
    private float m_RobotFallDownTime;
    //ミサイルが発射するフラグ
    private bool m_SpawnMissileFlag;
    public GameObject m_RobotEye;

    //首
    private Vector3 m_StartLookPos;
    private Vector3 m_EndLookPos;
    private float m_LookLerpTime;

    //ビーム補間用
    private Vector3 m_BeamStartPos;
    private Vector3 m_BeamEndPos;
    private float m_BeamLerpTime;

    //バネ補間のパラメーター
    float m_Stiffness;
    float m_Friction;
    float m_Mass;

    //IKのウェイト設定
    private float m_IKWeight;

    private Vector3 m_BombingBeamPoint;

    private bool m_FirstTankFlag;
    private GameObject m_LookTank;
    //矢野実装
    private AudioSource[] boss_se_;

    void Start()
    {
        m_Bills = new List<GameObject>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
        m_LegManager = GetComponent<RobotLegManager>();
        m_Bills.AddRange(GameObject.FindGameObjectsWithTag("Tower"));

        m_Animator = GetComponent<Animator>();
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_RobotState = RobotState.ROBOT_NULL;
        m_NavAgent.destination = m_Player.transform.position;
        m_VelocityY = 0.0f;
        m_SeveVelocityY = 0.0f;

        m_SpringVelo = Vector3.zero;

        m_IsIK = true;
        //ランダム生成
        m_Random = new System.Random();
        //サーチ系
        m_SearchFlag = true;
        m_SearchPoints = new List<GameObject>();
        m_SearchPoints.AddRange(GameObject.FindGameObjectsWithTag("SearchPoint"));
        if (m_SearchPoints.Count <= 0) m_SearchPoints.Add(new GameObject());
        m_RandomIndex = m_Random.Next(0, m_SearchPoints.Count - 1);
        m_GoalPoint = GameObject.FindGameObjectWithTag("GoalPoint");

        m_MissileSpawn = GameObject.FindGameObjectWithTag("MissileSpawn");

        m_RobotFallDownTime = 0.0f;

        m_BreakBill = m_Bills[0];
        //バネ補間系
        m_RobotLookAtPosition = m_Player.transform.position;
        m_RobotLookAtFlag = true;
        m_SeveRobotLokAtPosition = m_Player.transform.position;


        m_SpringVelo = Vector3.zero;
        SetSpringParameter(0.05f, 0.5f, 2.0f);

        m_SpawnMissileFlag = true;


        m_BeamStartPos = m_Player.transform.position;
        m_BeamEndPos = m_Player.transform.position;
        m_BeamLerpTime = 0.0f;

        m_IKWeight = 0.0f;
        boss_se_ = GetComponents<AudioSource>();


        m_StartLookPos = m_Player.transform.position;
        m_EndLookPos = m_Player.transform.position;

        m_LookLerpTime = 0.0f;

        m_BombingBeamPoint = Vector3.zero;

        m_FirstTankFlag = true;
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

                m_IsIK = true;
                m_NavAgent.isStopped = false;
                m_NavAgent.speed = m_RobotSpeed;
                m_NavAgent.stoppingDistance = m_Player_Enemy_Distance;
                //ロボットが何を見ているか設定
                m_RobotLookAtPosition = m_Player.transform.position;

                //Y軸をロボットに合わせる
                m_NavAgent.destination = m_Player.transform.position;
                m_RobotState = RobotState.ROBOT_TO_PLAYER_MOVE;
                m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
                m_Animator.SetFloat("RobotSpeed", m_NavAgent.velocity.magnitude);

                if (!boss_se_[0].isPlaying)
                    boss_se_[0].Play();

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

                m_IsIK = true;
                m_RobotLookAtPosition = m_Player.transform.position;
                m_NavAgent.isStopped = true;
                m_RobotState = RobotState.ROBOT_IDLE;
                //見てる場所設定
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

            m_IsIK = false;
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
            m_RobotEye.SetActive(true);
            m_IsIK = true;
            m_LerpTime = 0.0f;

            m_PlayerQuaternion = Quaternion.LookRotation(m_Player.transform.position - m_Robot.transform.position);
            m_PlayerQuaternion = Quaternion.Euler(0.0f, m_PlayerQuaternion.eulerAngles.y, 0.0f);

            m_RobotQuaternion = transform.rotation;

            m_BeamLerpTime = 0.0f;
            Vector3 vec = m_Player.transform.position - m_Robot.transform.position;
            vec = vec.normalized * 50.0f;

            m_BeamStartPos = m_Player.transform.position - vec * 1.5f;
            m_BeamEndPos = m_Player.transform.position + vec;
        };


        Func<bool> robotBeamAttack = () =>
        {
            m_NavAgent.isStopped = true;
            m_NavAgent.velocity = Vector3.zero;
            bool endAnim = false;

            m_LerpTime += 0.5f * Time.deltaTime;

            transform.rotation = Quaternion.Lerp(m_RobotQuaternion, m_PlayerQuaternion, m_LerpTime);


            m_LerpTime = Mathf.Clamp(m_LerpTime, 0.0f, 1.0f);
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];

            m_BeamLerpTime += 0.1f * Time.deltaTime;
            m_RobotLookAtPosition = Vector3.Lerp(m_BeamStartPos, m_BeamEndPos, m_BeamLerpTime);
            m_RobotEye.GetComponent<RobotBeam>().SetBeamFlag(true);
            if (m_BeamLerpTime >= 1.0f)
            {
                m_RobotEye.GetComponent<RobotBeam>().SetBeamFlag(false);
                endAnim = true;
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
    /// ふみつけ
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotLegAttack()
    {
        Action robotLefAttackStart = () =>
        {
            m_NavAgent.isStopped = true;
            m_LerpTime = 0.0f;
            Vector3 vec = (m_Player.transform.position - m_Robot.transform.position);
            vec.y = 0.0f;
            m_PlayerQuaternion = Quaternion.LookRotation(vec);
            m_RobotQuaternion = transform.rotation;
        };

        Func<bool> robotLegAttack = () =>
        {
            m_IsIK = true;
            bool endAnim = false;
            m_RobotLookAtPosition = m_Player.transform.position;
            m_LerpTime += 0.5f * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(m_RobotQuaternion, m_PlayerQuaternion, m_LerpTime);

            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "LegAttack")
            {
                endAnim = (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            }
            m_RobotState = RobotState.ROBOT_LEG_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotLefAttackStart;
        func.actionUpdate = robotLegAttack;

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

            m_IsIK = false;
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

            m_IsIK = false;
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

            m_IsIK = false;
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

            m_IsIK = true;
            m_NavAgent.isStopped = false;
            m_NavAgent.speed = m_RobotSpeed;
            m_NavAgent.stoppingDistance = 0.0f;
            //ビル設定
            //if (!NearBill())
            //{
            //    return false;
            //}
            m_NavAgent.destination = m_BreakBill.transform.position;
            //壊すビルを見る
            m_RobotLookAtPosition = m_BreakBill.transform.position;

            m_RobotState = RobotState.ROBOT_TO_BILL_MOVE;
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
            m_LerpTime = 0.0f;
            Vector3 vec = (m_BreakBill.transform.position - m_Robot.transform.position);
            vec.y = 0.0f;
            m_BillQuaternion = Quaternion.LookRotation(vec);
            m_RobotQuaternion = transform.rotation;
        };


        Func<bool> robotBillBreak = () =>
        {
            m_IsIK = false;
            m_NavAgent.isStopped = true;
            bool endAnim = false;
            m_NavAgent.velocity = Vector3.zero;
            m_LerpTime += 0.5f * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(m_RobotQuaternion, m_BillQuaternion, m_LerpTime);
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
    /// <summary>
    /// ロボットが転ぶ
    /// </summary>
    /// <returns></returns>
    public RobotManager.ActionFunc RobotFallDown()
    {
        Action robotFallDownStart = () =>
        {
            m_RobotFallDownTime = 0.0f;
        };


        Func<bool> robotFallDown = () =>
        {

            m_IsIK = false;
            m_NavAgent.isStopped = true;

            m_RobotFallDownTime += Time.deltaTime;

            if (m_RobotFallDownTime <= 10.0f)
            {
                m_Robot.transform.position += m_Robot.transform.forward * 5.0f * Time.deltaTime;
            }
            bool endAnim = false;
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "Standing Up")
            {
                if ((m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                {
                    endAnim = true;
                    m_LegManager.LegSet(RobotLegManager.Leg.NO);
                    GameObject.FindGameObjectWithTag("RobotRightLeg").GetComponent<RobotLeg>().SetLegFlag(false);
                    GameObject.FindGameObjectWithTag("RobotLeftLeg").GetComponent<RobotLeg>().SetLegFlag(false);

                }
            }
            m_RobotState = RobotState.ROBOT_FALL_DOWN;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotFallDownStart;
        func.actionUpdate = robotFallDown;

        return func;
    }
    public RobotManager.ActionFunc RobotMissileAttack()
    {
        Action missileAttackStart = () =>
        {
            m_SpawnMissileFlag = true;
        };


        Func<bool> missileAttack = () =>
        {
            m_IsIK = true;
            m_NavAgent.isStopped = true;
            m_RobotLookAtPosition = m_Player.transform.position;
            bool endAnim = false;
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];
            if (clipInfo.clip.name == "Idle")
            {
                if (m_SpawnMissileFlag)
                {
                    m_MissileSpawn.GetComponent<MissileSpawn>().SpawnFlag(true);
                    m_SpawnMissileFlag = false;
                }
                endAnim = !(m_MissileSpawn.GetComponent<MissileSpawn>().GetSpawnFlag());
            }
            m_RobotState = RobotState.ROBOT_MISSILE_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = missileAttackStart;
        func.actionUpdate = missileAttack;

        return func;
    }

    public RobotManager.ActionFunc RobotHeliAttack()
    {
        Action robotHeliAttackStart = () =>
        {
            m_BeamLerpTime = 0.0f;


            Vector3 robotLeft = transform.right.normalized;
            List<GameObject> heli = new List<GameObject>();
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Helicopter");
            foreach (var i in objs)
            {

                float dis = Vector3.Distance(i.transform.position, m_Robot.transform.position);

                if (dis <= 70.0f)
                {
                    //前のいる
                    heli.Add(i);
                }
            }
            if (heli.Count == 0)
            {
                m_BeamStartPos = Vector3.zero;
                return;
            }

            m_BeamStartPos = heli[UnityEngine.Random.Range(0, heli.Count - 1)].transform.position;
            m_RobotEye.SetActive(true);
        };


        Func<bool> robotHeliAttack = () =>
        {
            m_NavAgent.isStopped = true;
            m_NavAgent.velocity = Vector3.zero;
            bool endAnim = false;
            m_RobotState = RobotState.ROBOT_BEAM_ATTACK;

            if (m_BeamStartPos == Vector3.zero) return true;

            m_BeamLerpTime += Time.deltaTime;
            m_RobotLookAtPosition = m_BeamStartPos;
            if (m_BeamLerpTime >= 5.0f)
            {
                m_RobotEye.SetActive(false);
                endAnim = true;
                m_BeamLerpTime = 0.0f;
            }

            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotHeliAttackStart;
        func.actionUpdate = robotHeliAttack;

        return func;
    }
    public RobotManager.ActionFunc RobotBombingAttack()
    {
        Action robotBombingAttackStart = () =>
        {
            m_RobotEye.SetActive(true);
            m_IsIK = true;
            m_LerpTime = 0.0f;
            GameObject bomber = GameObject.FindGameObjectWithTag("BombingBeamPoint");
            m_RobotLookAtPosition = bomber.transform.position;
            m_PlayerQuaternion = Quaternion.LookRotation(bomber.transform.position - m_Robot.transform.position);
            m_PlayerQuaternion = Quaternion.Euler(0.0f, m_PlayerQuaternion.eulerAngles.y, 0.0f);

            m_RobotQuaternion = transform.rotation;
        };


        Func<bool> robotBombingAttack = () =>
        {
            m_LerpTime += 0.2f * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(m_RobotQuaternion, m_PlayerQuaternion, m_LerpTime);
            if (GameObject.FindGameObjectWithTag("Bomber") == null) return false;

            m_NavAgent.isStopped = true;
            m_NavAgent.velocity = Vector3.zero;
            bool endAnim = false;


            m_LerpTime = Mathf.Clamp(m_LerpTime, 0.0f, 1.0f);
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];

            m_BeamLerpTime += 0.1f * Time.deltaTime;
            m_RobotEye.GetComponent<RobotBeam>().SetBeamFlag(true);
            if (m_BeamLerpTime >= 2.0f)
            {
                m_RobotEye.GetComponent<RobotBeam>().SetBeamFlag(false);
                endAnim = true;
            }

            m_RobotState = RobotState.ROBOT_BEAM_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotBombingAttackStart;
        func.actionUpdate = robotBombingAttack;

        return func;
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_SearchPoints[m_RandomIndex])
        {
            m_SearchFlag = true;
        }
    }


    public RobotManager.ActionFunc RobotDead()
    {
        Action robotDeadStart = () =>
        {
        };


        Func<bool> robotDead = () =>
        {
            m_IsIK = false;
            m_NavAgent.isStopped = true;
            m_NavAgent.velocity = Vector3.zero;

            bool endAnim = false;

            //死んだらもう何も移行しない
            m_RobotState = RobotState.ROBOT_DEAD;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };



        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotDeadStart;
        func.actionUpdate = robotDead;
        return func;
    }
    //ビームミサイル攻撃
    public RobotManager.ActionFunc RobotBeamAndMissileAttack()
    {
        Action robotBeamAttackStart = () =>
        {
            m_RobotEye.SetActive(true);
            m_IsIK = true;
            m_LerpTime = 0.0f;

            m_PlayerQuaternion = Quaternion.LookRotation(m_Player.transform.position - m_Robot.transform.position);
            m_PlayerQuaternion = Quaternion.Euler(0.0f, m_PlayerQuaternion.eulerAngles.y, 0.0f);

            m_RobotQuaternion = transform.rotation;

            m_BeamLerpTime = 0.0f;
            Vector3 vec = m_Player.transform.position - m_Robot.transform.position;
            vec = vec.normalized * 50.0f;

            m_BeamStartPos = m_Player.transform.position - vec * 1.5f;
            m_BeamEndPos = m_Player.transform.position + vec;

            m_SpawnMissileFlag = true;
        };


        Func<bool> robotBeamAttack = () =>
        {
            m_NavAgent.isStopped = true;
            m_NavAgent.velocity = Vector3.zero;
            bool endAnim = false;

            //ミサイル攻撃
            if (m_SpawnMissileFlag)
            {
                m_MissileSpawn.GetComponent<MissileSpawn>().SpawnFlag(true);
                m_SpawnMissileFlag = false;
            }



            m_LerpTime += 0.5f * Time.deltaTime;

            transform.rotation = Quaternion.Lerp(m_RobotQuaternion, m_PlayerQuaternion, m_LerpTime);


            m_LerpTime = Mathf.Clamp(m_LerpTime, 0.0f, 1.0f);
            AnimatorClipInfo clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0)[0];

            m_BeamLerpTime += 0.1f * Time.deltaTime;
            m_RobotLookAtPosition = Vector3.Lerp(m_BeamStartPos, m_BeamEndPos, m_BeamLerpTime);
            m_RobotEye.GetComponent<RobotBeam>().SetBeamFlag(true);
            if (m_BeamLerpTime >= 1.0f)
            {
                m_RobotEye.GetComponent<RobotBeam>().SetBeamFlag(false);
                endAnim = true;
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

    //スポーンポイントを見る
    public RobotManager.ActionFunc RobotLookBombingSpawn()
    {
        Action robotLookBombingSpawnStart = () =>
        {
            m_RobotEye.SetActive(true);
            m_IsIK = true;
            m_LerpTime = 0.0f;

        };


        Func<bool> robotLookBombingSpawn = () =>
        {
            m_NavAgent.isStopped = true;
            m_NavAgent.velocity = Vector3.zero;
            bool endAnim = false;

            m_LerpTime += 0.3f * Time.deltaTime;

            transform.rotation = Quaternion.Lerp(m_RobotQuaternion, m_PlayerQuaternion, m_LerpTime);

            if (m_BeamLerpTime >= 1.0f)
            {
                m_RobotEye.GetComponent<RobotBeam>().SetBeamFlag(false);
                endAnim = true;
            }

            m_RobotState = RobotState.ROBOT_BEAM_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);
            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotLookBombingSpawnStart;
        func.actionUpdate = robotLookBombingSpawn;

        return func;
    }

    //Tankを攻撃
    public RobotManager.ActionFunc RobotTankAttack()
    {
        Action robotTankAttackStart = () =>
        {
            m_RobotEye.SetActive(true);
            m_IsIK = true;
            m_LerpTime = 0.0f;
            m_BeamLerpTime = 0.0f;
            m_FirstTankFlag = true;
        };


        Func<bool> robotTankAttack = () =>
        {
            m_NavAgent.isStopped = true;
            m_NavAgent.velocity = Vector3.zero;
            bool endAnim = false;
            m_RobotState = RobotState.ROBOT_BEAM_ATTACK;
            m_Animator.SetInteger("RobotAnimNum", (int)m_RobotState);

            if (m_FirstTankFlag)
            {
                List<GameObject> lookTank = new List<GameObject>();
                var tanks = GameObject.FindGameObjectsWithTag("GameTank");
                //タンクがいなかったら終わる
                if (tanks.Length <= 0)
                {
                    return true;
                }
                foreach (var i in tanks)
                {
                    Vector3 vec = i.transform.position - m_RobotEye.transform.position;
                    Ray ray = new Ray(m_RobotEye.transform.position, vec);
                    RaycastHit tank;
                    if (Physics.Raycast(ray, out tank, 20000))
                    {
                        if (tank.collider.tag == "GameTank")
                        {
                            lookTank.Add(i.gameObject);
                        }
                    }
                }
                //見えてなかったら終わる
                if (lookTank.Count <= 0) return true;

                m_LookTank = lookTank[UnityEngine.Random.Range(0, lookTank.Count - 1)];
                m_FirstTankFlag = false;
            }
            m_RobotEye.GetComponent<RobotBeam>().SetBeamFlag(true);
            m_RobotLookAtPosition = m_LookTank.transform.position;
            m_LerpTime += 0.3f * Time.deltaTime;
            m_BeamLerpTime += 0.5f * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(m_RobotQuaternion, m_PlayerQuaternion, m_LerpTime);

            if (m_BeamLerpTime >= 3.0f)
            {
                m_RobotEye.GetComponent<RobotBeam>().SetBeamFlag(false);
                endAnim = true;
            }


            return endAnim;
        };
        RobotManager.ActionFunc func = new RobotManager.ActionFunc();
        func.actionStart = robotTankAttackStart;
        func.actionUpdate = robotTankAttack;

        return func;
    }


    /// <summary>
    /// プレイヤーを見るアップデート
    /// </summary>
    public void RobotLookAtIKUpdate()
    {
        //毎フレーム近いビルを検索
        NearBill();

        if (!m_IsIK) m_IKWeight -= Time.deltaTime;
        else m_IKWeight += Time.deltaTime;
        m_IKWeight = Mathf.Clamp(m_IKWeight, 0.0f, 1.0f);


        if (m_SeveRobotLokAtPosition != m_RobotLookAtPosition)
        {
            //変わってまたタイムが0になってまた変わってるけど元のポジションは変わってないため
            m_StartLookPos = m_LookAtObj.transform.position;
            m_EndLookPos = m_RobotLookAtPosition;
            m_LookLerpTime = 0.0f;
            m_SeveRobotLokAtPosition = m_RobotLookAtPosition;
        }
        m_LookLerpTime += 0.7f * Time.deltaTime;
        m_LookAtObj.transform.position = Vector3.Lerp(m_StartLookPos, m_EndLookPos, m_LookLerpTime);



        //if (m_IsRobotLookAtPlayerFlag)
        //{
        //    m_LookAtLerpTime += Time.deltaTime;
        //}
        //else
        //{
        //    m_LookAtLerpTime -= Time.deltaTime;
        //}
        //m_LookAtLerpTime = Mathf.Clamp(m_LookAtLerpTime, 0.0f, 1.0f);
        ////基本ここを見てる(ローカル座標)
        //Vector3 robotFront = m_Robot.transform.position + m_Robot.transform.forward * 150.0f + new Vector3(0, 180, 0);
        ////プレイヤー座標
        //Vector3 playerPos = m_Player.transform.position;

        //testObj.transform.position = Vector3.Lerp(robotFront, playerPos, m_LookAtLerpTime);
    }
    /// <summary>
    /// IK系
    /// </summary>
    /// <param name="layorIndex"></param>
    void OnAnimatorIK(int layorIndex)
    {
        m_Animator.SetLookAtWeight(Mathf.Lerp(0.0f, 1.0f, m_IKWeight),
            Mathf.Lerp(0.0f, 0.4f, m_IKWeight),
            Mathf.Lerp(0.0f, 0.7f, m_IKWeight),
            0.0f,
            Mathf.Lerp(0.0f, 0.5f, m_IKWeight));

        m_Animator.SetLookAtPosition(m_LookAtObj.transform.position);


        //if (m_RobotState == RobotState.ROBOT_LEG_ATTACK)
        //{
        //    m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
        //}


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
    /// ロボットが壊すビルの取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetBillBreakObject()
    {
        return m_BreakBill;
    }
    /// <summary>
    /// ロボットから一番近いビルを取得する
    /// </summary>
    /// <returns>ビルがあるかどうか</returns>
    private bool NearBill()
    {
        m_Bills.Clear();
        //一番近いビル
        GameObject[] bill = GameObject.FindGameObjectsWithTag("TowerPoint");


        for (int i = 0; i < bill.Length; i++)
        {
            if (!(bill[i].GetComponent<tower_point>().Get_BreakFlag()))
            {
                m_Bills.Add(bill[i]);
            }
        }
        m_BreakBill = m_Bills[0];
        //もしビルが無かったらプレイヤーに向かって歩く
        if (m_Bills.Count <= 0)
        {
            RobotToPlayerMove().actionUpdate();
            m_BreakBill = null;
            return false;
        }
        int count = 0;
        foreach (var i in m_Bills)
        {
            count++;
            if (Vector3.Distance(m_BreakBill.transform.position, m_Robot.transform.position) >=
                Vector3.Distance(i.transform.position, m_Robot.transform.position))
            {
                m_BreakBill = i;
                goPoint.transform.position = m_BreakBill.transform.position;
            }
        }
        return true;
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

    /// <summary>
    /// バネのパラメーターを設定する
    /// </summary>
    /// <param name="stiffness"></param>
    /// <param name="friction"></param>
    /// <param name="mass"></param>
    private void SetSpringParameter(float stiffness, float friction, float mass)
    {
        m_Stiffness = stiffness;
        m_Friction = friction;
        m_Mass = mass;
    }


    /// <summary>
    /// バネ補間をする
    /// </summary>
    /// <param name="resPos">行きたい座標</param>
    /// <param name="pos">現在の座標</param>
    /// <param name="velo">速度</param>
    /// <param name="stiffness">なんか</param>
    /// <param name="friction">なんか</param>
    /// <param name="mass">重さ</param>
    private void Spring(Vector3 resPos, ref Vector3 pos, ref Vector3 velo, float stiffness, float friction, float mass)
    {
        // バネの伸び具合を計算
        Vector3 stretch = (pos - resPos);
        // バネの力を計算
        Vector3 force = -stiffness * stretch;
        // 加速度を追加
        Vector3 acceleration = force / mass;
        // 移動速度を計算
        velo = friction * (velo + acceleration);
        // 座標の更新
        pos += velo;
    }
}

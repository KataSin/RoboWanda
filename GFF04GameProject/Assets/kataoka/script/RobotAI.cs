using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotAI : MonoBehaviour
{
    private RobotAction action;
    private RobotManager manager;
    private GameObject player;
    private NavMeshAgent agent;
    private GameObject robotEye;
    private float attackTime;
    [SerializeField, Tooltip("ビームのクールタイム")]
    public float m_RobotBeamCoolTime = 30.0f;
    //ビームのカウント
    private float m_RobotBeamCount;

    [SerializeField, Tooltip("ビルコリジョン")]
    public GameObject m_BillCollision;


    public GameObject m_Beamtest;

    bool m_FirstCol;
    // Use this for initialization
    void Start()
    {
        action = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotAction>();
        manager = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>();

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GameObject.FindGameObjectWithTag("Robot").GetComponent<NavMeshAgent>();


        robotEye = GameObject.FindGameObjectWithTag("RobotEye");

        m_FirstCol = true;

        attackTime = 0.0f;
        m_RobotBeamCount = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //カウント
        m_RobotBeamCount += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(m_Beamtest, robotEye.transform.position, Quaternion.Euler(0, 0, 0));
        }
        //見えてたら
        GameObject player;
        if (PlayerToRobotRay("Player", 0, out player))
        {
            //見えててかつ遠かったらビームアタック
            if (m_RobotBeamCount >= m_RobotBeamCoolTime)
            {
                manager.SetAction(RobotAction.RobotState.ROBOT_BEAM_ATTACK, false);
                m_RobotBeamCount = 0.0f;
            }
            manager.SetAction(RobotAction.RobotState.ROBOT_TO_PLAYER_MOVE, true);
        }
        //見えてなかったらビル壊す
        else if (agent.gameObject.GetComponent<RobotAction>().GetBillBreakObject() != null)
        {
            if (Vector3.Distance(agent.gameObject.GetComponent<RobotAction>().GetBillBreakObject().transform.position, agent.transform.position) <= 100.0f)
            {
                manager.SetAction(RobotAction.RobotState.ROBOT_BILL_BREAK, false);
                return;
            }
            manager.SetAction(RobotAction.RobotState.ROBOT_TO_BILL_MOVE, true);
        }
        else
        {
            manager.SetAction(RobotAction.RobotState.ROBOT_IDLE, true);
        }
    }


    /// <summary>
    /// 特定のオブジェクトとロボットが当たっているかどうか
    /// </summary>
    /// <param name="objectName">オブジェクトの名前</param>
    /// <param name="layerMask">レイヤー</param>
    /// <param name="collision">当たったオブジェクト</param>
    /// <returns>当たったかどうか</returns>
    private bool PlayerToRobotRay(string objectName, int layerMask, out GameObject collision)
    {
        Vector3 vec = (player.transform.position - robotEye.transform.position).normalized * 2000.0f;
        Vector3 start = robotEye.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(start, vec, out hit, 20000.0f))
        {
            string name = hit.collider.tag;
            collision = hit.collider.gameObject;
            if (name == objectName)
            {
                return true;
            }
        }
        collision = null;
        return false;
    }
    /// <summary>
    /// プレイヤーとロボットが指定した距離以内かどうか
    /// </summary>
    /// <param name="dis">距離</param>
    /// <returns>指定した距離以内か</returns>
    private bool Player_Robot_Distance(float dis)
    {
        return Vector3.Distance(agent.transform.position, player.transform.position) < dis;
    }




    public void OnDrawGizmos()
    {
        if (player == null) return;
        Vector3 vec = (player.transform.position - robotEye.transform.position).normalized * 2000.0f;
        Vector3 start = robotEye.transform.position;
        Gizmos.DrawLine(robotEye.transform.position, player.transform.position);
    }


}

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
    public float m_RobotBeamCoolTime = 50.0f;

    [SerializeField, Tooltip("ビルコリジョン")]
    public GameObject m_BillCollision;
    // Use this for initialization
    void Start()
    {
        action = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotAction>();
        manager = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>();

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GameObject.FindGameObjectWithTag("Robot").GetComponent<NavMeshAgent>();


        robotEye = GameObject.FindGameObjectWithTag("RobotEye");



        attackTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //見えてたら
        GameObject player;
        if(PlayerToRobotRay("Player",0,out player)){
            manager.SetAction(RobotAction.RobotState.ROBOT_TO_PLAYER_MOVE, true);
        }
            //見えてなかったら
        else
        {
            manager.SetAction(RobotAction.RobotState.ROBOT_TO_PLAYER_MOVE, true);

        }


        //if (m_BillCollision.GetComponent<RobotBillCollision>().GetCollisionFlag())
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_BILL_BREAK, false);
        //}

        //Debug.Log(agent.remainingDistance);
        //attackTime += Time.deltaTime;

        //manager.SetAction(RobotAction.RobotState.ROBOT_MOVE,true);



        //if ((manager.GetRobotState()!=RobotAction.RobotState.ROBOT_SEARCH_MOVE)
        //    &&attackTime >= 10.0f && Player_Robot_Distance(400.0f))
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_ARM_ATTACK, false);
        //    attackTime = 0.0f;
        //}
        //else
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_MOVE, true);
        //     int mask = ~(1 << 8);
        //    GameObject collisionObject;
        //    if (!PlayerToRobotRay("TestPlayer", mask, out collisionObject))
        //    {
        //        manager.SetAction(RobotAction.RobotState.ROBOT_SEARCH_MOVE,true);
        //    }

        //}


        //if (attackTime >= 10.0f&&agent.velocity.magnitude<=1.0f&&agent.remainingDistance<=1000.0f)
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_ARM_ATTACK,false);
        //    attackTime = 0.0f;
        //}
        //else if (agent.remainingDistance >= 1000.0f)
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_MOVE, false);
        //}
        //if (Player_Robot_Distance(500.0f) && attackTime>10.0f && agent.velocity.magnitude < 10.0f)
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_ARM_ATTACK,false);
        //    attackTime = 0.0f;
        //}
        //else
        //{
        //    GameObject colObject;
        //    int mask = ~(1 << 8);
        //    if (PlayerToRobotRay("Player", mask, out colObject))
        //    {
        //        manager.SetAction(RobotAction.RobotState.ROBOT_MOVE, true);
        //    }
        //}

        //Vector3 vec = (player.transform.position - robotEye.transform.position).normalized * 2000.0f;
        //Vector3 start = robotEye.transform.position;
        //RaycastHit hit;
        //int mask = ~(1 << 8);
        //if (Physics.Raycast(start, vec, out hit, 20000.0f,mask))
        //{
        //    string name = hit.collider.name;
        //    if (hit.collider.tag == "Player")
        //    {
        //        manager.SetAction(RobotAction.RobotState.ROBOT_MOVE, false);
        //    }
        //    else
        //    {
        //        manager.SetAction(RobotAction.RobotState.ROBOT_SEARCH, false);
        //    }
        //}


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
        
        if (Physics.Raycast(start, vec, out hit, 20000.0f, layerMask))
        {
            string name = hit.collider.name;
            collision = hit.collider.gameObject;
            if (hit.collider.name == objectName)
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
        return agent.remainingDistance < dis;
    }
}

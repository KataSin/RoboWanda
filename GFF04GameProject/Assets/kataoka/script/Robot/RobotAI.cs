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
    public GameObject robotEye;


    private float beamAttackTime;
    private float missileAttackTime;

    private float billBreakTime;
    //見つかったかどうか
    private bool m_IsLookFlag;
    //ヘリ攻撃クールタイム
    private float m_HeliAttackCoolTime;
    //ビル破壊クールタイム
    private float m_BillBreakCoolTime;
    //戦車破壊のクールタイム
    private float m_TankBreakCoolTime;

    //[SerializeField, Tooltip("ビルコリジョン")]
    //public GameObject m_BillCollision;

    // Use this for initialization
    void Start()
    {
        action = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotAction>();
        manager = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>();

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GameObject.FindGameObjectWithTag("Robot").GetComponent<NavMeshAgent>();



        beamAttackTime = 10.0f;
        missileAttackTime = 10.0f;
        billBreakTime = 10.0f;


        m_BillBreakCoolTime = 20.0f;
        m_HeliAttackCoolTime = 0.0f;
        m_TankBreakCoolTime = 0.0f;
        m_IsLookFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (manager.GetRobotHP() <= 0)
        {
            manager.SetAction(RobotAction.RobotState.ROBOT_DEAD, false, true);
            return;
        }


        ////ここから
        //beamAttackTime += Time.deltaTime;
        //if (beamAttackTime >= 10.0f)
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_HELI_ATTACK, false);
        //    beamAttackTime = 0.0f;
        //}
        ////ここまで
        m_HeliAttackCoolTime += Time.deltaTime;
        m_BillBreakCoolTime += Time.deltaTime;
        m_TankBreakCoolTime += Time.deltaTime;
        switch (manager.GetBehavior())
        {
            case RobotManager.RobotBehavior.ROBOT_ONE:
                {
                    GameObject player;

                    //プレイヤーが見えていたら
                    if (PlayerToRobotRay("Player",0, out player) ||
                        Player_Robot_Distance() <= 20.0f)
                    {
                        float dis = Player_Robot_Distance();
                        if (dis <= 20.0f)
                        {
                            manager.SetAction(RobotAction.RobotState.ROBOT_MISSILE_BEAM_ATTACK, false);
                        }
                        else if (dis < 50.0f)
                        {
                            manager.SetAction(RobotAction.RobotState.ROBOT_MISSILE_ATTACK, false);
                        }
                        else if (dis < 75.0f)
                        {
                            manager.SetAction(RobotAction.RobotState.ROBOT_BEAM_ATTACK, false);
                        }
                    }
                    else if (m_HeliAttackCoolTime >= 30.0f)
                    {
                        manager.SetAction(RobotAction.RobotState.ROBOT_HELI_ATTACK, false);
                        m_HeliAttackCoolTime = 0.0f;
                    }
                    else
                    {
                        GameObject billObj = agent.gameObject.GetComponent<RobotAction>().GetBillBreakObject();
                        if (billObj != null)
                        {
                            if ((Vector3.Distance(billObj.transform.position, agent.transform.position) <= 30.0f) &&
                                m_BillBreakCoolTime >= 10.0f)
                            {
                                manager.SetAction(RobotAction.RobotState.ROBOT_BILL_BREAK, false);
                                m_BillBreakCoolTime = 0.0f;
                                return;
                            }
                            manager.SetAction(RobotAction.RobotState.ROBOT_TO_BILL_MOVE, true);
                        }
                    }
                    break;
                }
            case RobotManager.RobotBehavior.ROBOT_TWO:
                {
                    GameObject player;
                    //プレイヤーが見えていたら
                    if (PlayerToRobotRay("Player", 0, out player) ||
                        Player_Robot_Distance() <= 20.0f)
                    {
                        float dis = Player_Robot_Distance();
                        if (dis <= 20.0f)
                        {
                            manager.SetAction(RobotAction.RobotState.ROBOT_MISSILE_BEAM_ATTACK, false);
                        }
                        else if (dis < 50.0f)
                        {
                            manager.SetAction(RobotAction.RobotState.ROBOT_MISSILE_ATTACK, false);
                        }
                        else if (dis < 75.0f)
                        {
                            manager.SetAction(RobotAction.RobotState.ROBOT_BEAM_ATTACK, false);
                        }
                    }
                    //見えてなかったら爆撃機攻撃
                    else
                    {
                        manager.SetAction(RobotAction.RobotState.ROBOT_BOMBING_ATTACK, false);
                    }
                    break;
                }
            case RobotManager.RobotBehavior.ROBOT_THREE:
                {
                    GameObject player;

                    //プレイヤーが見えていたら
                    if (PlayerToRobotRay("Player", 0, out player) ||
                        Player_Robot_Distance() <= 20.0f)
                    {
                        float dis = Player_Robot_Distance();
                        if (dis <= 20.0f)
                        {
                            manager.SetAction(RobotAction.RobotState.ROBOT_MISSILE_BEAM_ATTACK, false);
                        }
                        else if (dis < 50.0f)
                        {
                            manager.SetAction(RobotAction.RobotState.ROBOT_MISSILE_ATTACK, false);
                        }
                        else if (dis < 75.0f)
                        {
                            manager.SetAction(RobotAction.RobotState.ROBOT_BEAM_ATTACK, false);
                        }
                    }
                    else if (RobotToTankRay()&&m_TankBreakCoolTime>=20.0f)
                    {
                        manager.SetAction(RobotAction.RobotState.ROBOT_TANK_ATTACK, false);
                        m_TankBreakCoolTime = 0.0f;
                    }
                    else
                    {
                        GameObject billObj = agent.gameObject.GetComponent<RobotAction>().GetBillBreakObject();
                        if (billObj != null)
                        {
                            if ((Vector3.Distance(billObj.transform.position, agent.transform.position) <= 30.0f) &&
                                m_BillBreakCoolTime >= 10.0f)
                            {
                                manager.SetAction(RobotAction.RobotState.ROBOT_BILL_BREAK, false);
                                m_BillBreakCoolTime = 0.0f;
                                return;
                            }
                            manager.SetAction(RobotAction.RobotState.ROBOT_TO_BILL_MOVE, true);
                        }
                    }
                    break;
                }
                //case 3:
                //    {
                //        break;
                //    }
        }


        ////見えてたら
        //GameObject player;
        //missileAttackTime += Time.deltaTime;
        //beamAttackTime += Time.deltaTime;
        //legAttackTime += Time.deltaTime;
        //if (PlayerToRobotRay("Player", 0, out player))
        //{
        //    if (Player_Robot_Distance(10.0f))
        //    {
        //        if (legAttackTime >= 5.0f)
        //        {
        //            manager.SetAction(RobotAction.RobotState.ROBOT_LEG_ATTACK, false);
        //            legAttackTime = 0.0f;
        //        }
        //        else
        //            manager.SetAction(RobotAction.RobotState.ROBOT_IDLE, true);

        //    }
        //    //見えててかつ遠かったらビームアタック
        //    else if (!Player_Robot_Distance(30.0f))
        //    {
        //        if (missileAttackTime >= 6.0f)
        //        {
        //            manager.SetAction(RobotAction.RobotState.ROBOT_MISSILE_ATTACK, false);
        //            missileAttackTime = 0.0f;
        //        }
        //        else
        //            manager.SetAction(RobotAction.RobotState.ROBOT_IDLE, true);
        //    }
        //    else
        //    {
        //        manager.SetAction(RobotAction.RobotState.ROBOT_BEAM_ATTACK, false);
        //    }
        //    m_IsLookFlag = true;
        //}
        ////見えてなかったらビル壊す
        //else if (agent.gameObject.GetComponent<RobotAction>().GetBillBreakObject() != null)
        //{
        //    if (m_IsLookFlag)
        //    {
        //        manager.SetAction(RobotAction.RobotState.ROBOT_BEAM_ATTACK, false);
        //        m_IsLookFlag = false;
        //    }

        //    if (Vector3.Distance(agent.gameObject.GetComponent<RobotAction>().GetBillBreakObject().transform.position, agent.transform.position) <= 100.0f)
        //    {
        //        if (billBreakTime >= 10.0f)
        //        {
        //            manager.SetAction(RobotAction.RobotState.ROBOT_BILL_BREAK, false);
        //            billBreakTime = 0.0f;
        //        }
        //        return;
        //    }
        //    manager.SetAction(RobotAction.RobotState.ROBOT_TO_BILL_MOVE, true);
        //}
        //else
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_IDLE, true);
        //}



    }


    private bool RobotToTankRay()
    {
        var tanks = GameObject.FindGameObjectsWithTag("GameTank");
        foreach(var i in tanks)
        {
            Vector3 vec = (i.transform.position - robotEye.transform.position).normalized * 2000.0f;
            Vector3 start = robotEye.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(start, vec, out hit, 20000.0f))
            {
                string name = hit.collider.tag;
                if (name == "GameTank")
                {
                    return true;
                }
            }
        }
        return false;
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
    /// プレイヤーとロボットの距離を返す
    /// </summary>
    private float Player_Robot_Distance()
    {
        return Vector3.Distance(agent.transform.position, player.transform.position);
    }




    public void OnDrawGizmos()
    {
        if (player == null) return;
        Vector3 vec = (player.transform.position - robotEye.transform.position).normalized * 2000.0f;
        Vector3 start = robotEye.transform.position;
        Gizmos.DrawLine(robotEye.transform.position, player.transform.position);
    }


}

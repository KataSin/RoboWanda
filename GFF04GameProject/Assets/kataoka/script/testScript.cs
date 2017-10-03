using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class testScript : MonoBehaviour
{

    public GameObject m_Robot;
    public GameObject m_GoalPoint;
    private GameObject m_Player;
    // Use this for initialization
    void Start()
    {
        m_Player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(Vector2Cross(robotToGoalVec2, robotToPlayerVec2));

    }
    public float Vector2Cross(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x * rhs.y - rhs.x * lhs.y;
    }
}

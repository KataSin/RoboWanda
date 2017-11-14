using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAircraft : MonoBehaviour
{
    //ロボット
    private GameObject m_Robot;
    //通過した後のタイム
    private float m_PassingThroughTime;
    //直進したかどうかフラグ
    private bool m_StraightFlag;
    //直進のタイム
    private float m_StraightTime;
    //スピー度
    private float m_Speed;
    private float m_SpeedVelo;
    // Use this for initialization
    void Start()
    {
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
        m_PassingThroughTime = 0.0f;
        m_StraightTime = 0.0f;
        m_Speed = 300.0f;
        m_SpeedVelo = 0.0f;
        m_StraightFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 robotPos = m_Robot.transform.position;
        Vector3 fighterPos = transform.position;
        Vector3 fighterFront = transform.forward;



        Vector3 vec = (robotPos - fighterPos).normalized;
        //垂直なベクトル
        Vector3 robotToFighterLeftVec = new Vector3(vec.z, 0.0f, -vec.x);

        float cross = Vector2Cross(vec, fighterFront);
        float cross2 = Vector2Cross(vec, transform.right);

        float cross3 = Vector2Cross(vec, robotToFighterLeftVec);

        float speed = 400.0f;
        if (m_StraightFlag)
        {
            m_StraightTime += Time.deltaTime;
            if (m_StraightTime >= 5.0f)
            {
                m_StraightFlag = false;
                m_StraightTime = 0.0f;
            }

        }
        else if (cross2 > 0)
        {
            speed = 150.0f;
            if (cross2 <= 0.0f)
                transform.Rotate(Vector3.up, -1.0f);
            else if (cross2 > 0.0f)
                transform.Rotate(Vector3.up, 1.0f);
        }
        else if (cross < -0.01f)
        {
            speed = 150.0f;
            transform.Rotate(Vector3.up, -1.0f);
        }
        else if (cross > 0.01f)
        {
            speed = 150.0f;
            transform.Rotate(Vector3.up, 1.0f);
        }
        //直進の場合
        else if (!m_StraightFlag)
        {
            m_StraightFlag = true;
        }


        Spring(speed, ref m_Speed, ref m_SpeedVelo, 0.1f, 0.3f, 2.0f);
        transform.position += m_Speed * transform.forward * Time.deltaTime;

    }
    public float Vector2Cross(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.z - rhs.x * lhs.z;
    }

    public void OnDrawGizmos()
    {
        //Vector3 robotPos = m_Robot.transform.position;
        //Vector3 fighterPos = transform.position;
        //Vector3 fighterFront = transform.forward;
        //Vector3 vec = (robotPos - fighterPos).normalized;
        ////垂直なベクトル
        //Vector3 robotToFighterLeftVec = new Vector3(vec.z, 0.0f, -vec.x);
        //Gizmos.DrawLine(robotPos, robotPos + robotToFighterLeftVec * 100.0f);
    }

    /// <summary>
    /// バネ補間をする
    /// </summary>
    /// <param name="resPos">行きたい</param>
    /// <param name="pos">現在</param>
    /// <param name="velo">速度</param>
    /// <param name="stiffness">なんか</param>
    /// <param name="friction">なんか</param>
    /// <param name="mass">重さ</param>
    private void Spring(float resNum, ref float num, ref float velo, float stiffness, float friction, float mass)
    {
        // バネの伸び具合を計算
        float stretch = (num - resNum);
        // バネの力を計算
        float force = -stiffness * stretch;
        // 加速度を追加
        float acceleration = force / mass;
        // 移動速度を計算
        velo = friction * (velo + acceleration);
        // 座標の更新
        num += velo;
    }
}

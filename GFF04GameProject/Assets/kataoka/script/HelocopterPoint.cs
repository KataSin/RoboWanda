using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelocopterPoint : MonoBehaviour
{
    //ロボットからどのくらいはなれるか
    private float m_RobotDis;
    //回転スピード
    public float m_PointSpeed = 10.0f;

    private GameObject m_Robot;
    private float m_Count;

    private Vector3 serve;
    // Use this for initialization
    void Start()
    {
        m_Count = Random.Range(0,180);
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
        Vector2 pos1=new Vector2(transform.position.x,transform.position.z);
        Vector2 pos2=new Vector2(m_Robot.transform.position.x,m_Robot.transform.position.z);
        serve = transform.position;
        m_RobotDis = Vector2.Distance(pos1, pos2);
    }

    // Update is called once per frame
    void Update()
    {
        m_Count += m_PointSpeed * Time.deltaTime;

        transform.position =
            new Vector3(Mathf.Sin(m_Count * Mathf.Deg2Rad)*m_RobotDis, serve.y, Mathf.Cos(m_Count * Mathf.Deg2Rad)*m_RobotDis)+
            m_Robot.transform.position;
    }
}

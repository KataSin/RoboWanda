using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank2 : MonoBehaviour
{
    //切り離されたかどうか
    private bool m_IsFree;

    public GameObject m_GunRotateY;
    public GameObject m_GunRotateX;

    public GameObject m_SpawnBullet;

    private GameObject m_Robot;


    private NavMeshAgent m_Agent;
    private float m_Time;

    private Vector3 m_go_point1;
    private Vector3 m_go_point2;

    [SerializeField]
    private int m_pointCount;

    private float t0, t1, t2, t3;

    private Vector3 m_origin_pos;

    private bool isMoveClear;

    // Use this for initialization
    void Start()
    {
        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        m_Agent = GetComponent<NavMeshAgent>();
        m_Time = 0.0f;

        t0 = 0f;
        t1 = 0f;
        t2 = 0f;
        t3 = 0f;
        m_origin_pos = transform.position;

        isMoveClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        //gunの動き
        Vector3 vec = (m_Robot.transform.position + new Vector3(0, 30, 0)) - transform.position;
        Quaternion lookY = Quaternion.LookRotation(vec);
        if (isMoveClear)
        {
            //m_GunRotateY.transform.rotation = Quaternion.Euler(0.0f, lookY.eulerAngles.y, 0.0f);
            m_GunRotateY.transform.rotation = 
                Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0.0f, lookY.eulerAngles.y, 0.0f), t3 / 2f);
            m_GunRotateX.transform.rotation = lookY;

            t3 += 1.0f * Time.deltaTime;
        }

        bool colFlag = false;
        var lightCol = GameObject.FindGameObjectsWithTag("LightCollision");
        if (lightCol.Length > 0)
        {
            foreach (var i in lightCol)
            {
                if (i.GetComponent<LightCollision>().GetCollisionFlag())
                {
                    colFlag = true;
                }
            }
            if (colFlag) m_Time += Time.deltaTime;
            if (m_Time >= 1.0f)
            {
                m_SpawnBullet.GetComponent<TankGunSpawn>().SpawnBullet();
                m_Time = 0.0f;
            }
        }

        if (t0 >= 4f)
            PointMove();

        MoveClearCheck();

        t0 += 1.0f * Time.deltaTime;
    }

    private void PointMove()
    {
        if (t1 <= 6f && m_pointCount == 1)
            transform.position = Vector3.Lerp(m_origin_pos, m_go_point1, t1 / 6f);

        else if (t1 <= 3f && m_pointCount == 2)
            transform.position = Vector3.Lerp(m_origin_pos, m_go_point1, t1 / 3f);

        if (t1 >= 3f && m_pointCount == 2)
        {
            transform.position = Vector3.Lerp(m_go_point1, m_go_point2, t2 / 3f);

            t2 += 1.0f * Time.deltaTime;
        }

        t1 += 1.0f * Time.deltaTime;
    }

    private void MoveClearCheck()
    {
        if (m_pointCount == 1)
        {
            if (t1 >= 3f)
                isMoveClear = true;
        }
        else if (m_pointCount == 2)
        {
            if (t2 >= 3f)
                isMoveClear = true;
        }
    }


    public void SetGoTankPos(Vector3 pos)
    {
        if (!m_Agent.enabled) return;
        m_Agent.destination = pos;
    }

    public void Free()
    {
        Destroy(GetComponent<Rigidbody>());
        m_Agent.enabled = true;
    }

    public void Set_GPoint1(Vector3 l_goPoint1, int l_pointCount)
    {
        m_go_point1 = l_goPoint1;
        m_pointCount = l_pointCount;
    }

    public void Set_GPoint2(Vector3 l_goPoint2)
    {
        m_go_point2 = l_goPoint2;
    }
}

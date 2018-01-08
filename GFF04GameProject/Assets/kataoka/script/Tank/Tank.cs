using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : MonoBehaviour
{
    //切り離されたかどうか
    private bool m_IsFree;

    public GameObject m_GunRotateY;
    public GameObject m_GunRotateX;

    public GameObject m_SpawnBullet;

    private GameObject m_Robot;


    private NavMeshAgent m_Agent;
    private float m_Time;
    // Use this for initialization
    void Start()
    {
        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        m_Agent = GetComponent<NavMeshAgent>();
        m_Time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //gunの動き
        Vector3 vec = (m_Robot.transform.position + new Vector3(0, 30, 0)) - transform.position;
        Quaternion lookY = Quaternion.LookRotation(vec);
        m_GunRotateY.transform.rotation = Quaternion.Euler(0.0f, lookY.eulerAngles.y, 0.0f);
        m_GunRotateX.transform.rotation = lookY;

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
}

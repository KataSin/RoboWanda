using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : MonoBehaviour
{
    public GameObject m_BreakTank;
    public GameObject m_GunRotateY;
    public GameObject m_GunRotateX;

    public GameObject m_SpawnBullet;

    public bool m_IsBreak;

    private GameObject m_Robot;

    private bool m_IsAttack;

    private NavMeshAgent m_Agent;
    private float m_Time;
    // Use this for initialization
    void Start()
    {
        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        m_Agent = GetComponent<NavMeshAgent>();

        m_IsAttack = false;
        m_Time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsBreak)
        {
            Instantiate(m_BreakTank, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        //gunの動き
        Vector3 vec = (m_Robot.transform.position + new Vector3(0, 30, 0)) - transform.position;
        Quaternion lookY = Quaternion.LookRotation(vec);
        m_GunRotateY.transform.rotation = Quaternion.Euler(0.0f, lookY.eulerAngles.y, 0.0f);
        m_GunRotateX.transform.rotation = lookY;

        m_IsAttack = false;
        var lightCol = GameObject.FindGameObjectsWithTag("LightCollision");
        if (lightCol.Length > 0)
        {
            foreach (var i in lightCol)
            {
                m_IsAttack = i.GetComponent<LightCollision>().GetCollisionFlag();
            }
            if (m_IsAttack) m_Time += Time.deltaTime;
            if (m_Time >= 3.0f)
            {
                m_SpawnBullet.GetComponent<TankGunSpawn>().SpawnBullet();
                m_Time = 0.0f;
            }
        }



    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ExplosionCollision")
        {
            m_IsBreak = true;
        }
    }

    public void SetGoTankPos(Vector3 pos)
    {
        if (m_Agent == null) return;
        if (!m_Agent.enabled) return;
        m_Agent.destination = pos;
    }

    public void Free()
    {
        if (GetComponent<Rigidbody>() != null)
            Destroy(GetComponent<Rigidbody>());
        m_Agent.enabled = true;
    }

    public bool GetIsAttack()
    {
        return m_IsAttack;
    }
}

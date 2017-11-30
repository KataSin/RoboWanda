using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LightSerch : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    public GameObject m_LightRotateY;
    public GameObject m_LightRotateZ;

    private GameObject m_Robot;
    // Use this for initialization
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
    }

    // Update is called once per frame
    void Update()
    {

        Quaternion lookRobot = Quaternion.LookRotation(transform.position - m_Robot.transform.position);

        m_LightRotateY.transform.eulerAngles =
            new Vector3(m_LightRotateY.transform.eulerAngles.x,
            lookRobot.eulerAngles.y,
            m_LightRotateY.transform.eulerAngles.z);

        m_LightRotateZ.transform.eulerAngles =
            new Vector3(lookRobot.eulerAngles.x,
                m_LightRotateZ.transform.eulerAngles.y,
                m_LightRotateZ.transform.eulerAngles.z
                );
    }
}

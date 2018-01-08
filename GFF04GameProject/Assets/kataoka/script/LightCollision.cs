using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollision : MonoBehaviour
{
    private bool m_IsRobotCol;
    // Use this for initialization
    void Start()
    {
        m_IsRobotCol = false;
    }

    // Update is called once per frame
    void Update()
    {
        //m_IsRobotCol = false;
    }
    public bool GetCollisionFlag()
    {
        return m_IsRobotCol;
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<RobotDamage>() != null)
        {
            m_IsRobotCol = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<RobotDamage>() != null)
        {
            m_IsRobotCol = false;
        }
    }
}

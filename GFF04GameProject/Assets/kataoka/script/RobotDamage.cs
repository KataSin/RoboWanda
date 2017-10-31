using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDamage : MonoBehaviour
{
    //ロボットマネージャー
    private RobotManager m_Manager;
    // Use this for initialization
    void Start()
    {
        m_Manager = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bom")
        {
            m_Manager.Damage(10);
        }
    }

}

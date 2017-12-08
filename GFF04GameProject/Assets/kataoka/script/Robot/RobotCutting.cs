using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCutting : MonoBehaviour
{
    private List<GameObject> m_LeftArms;
    private List<GameObject> m_RightArms;

    private GameObject m_Robot;
    // Use this for initialization
    void Start()
    {
        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        var arms = transform.GetComponentsInChildren<Transform>();
        var robotcols=
        m_LeftArms = new List<GameObject>();
        m_RightArms = new List<GameObject>();
        foreach (var i in arms)
        {
            if (i.name == "LeftArm")
            {
                m_LeftArms.Add(i.gameObject);
            }
            if (i.name == "RightArm")
            {
                m_RightArms.Add(i.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Robot.GetComponent<RobotManager>().GetRobotHP() <= 50)
        {
            foreach(var i in m_RightArms)
            {
                i.SetActive(false);
            }
        }
    }
}

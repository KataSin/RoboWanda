using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyRobotLightManager : MonoBehaviour
{
    public bool m_IsLight;
    private List<GameObject> m_Lights;

    private GameObject m_Robot;
    // Use this for initialization
    void Start()
    {
        m_IsLight = false;
        m_Lights = new List<GameObject>();

        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        foreach(var i in transform.GetComponentsInChildren<Transform>())
        {
            if(name!=i.name)
            m_Lights.Add(i.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach(var i in m_Lights)
        {
            //ライトつけるかどうか
            i.GetComponent<Light>().enabled = m_IsLight;
            i.GetComponent<LightShafts>().enabled = m_IsLight;
            i.transform.LookAt(m_Robot.transform.position+new Vector3(0,20,0));
        }
    }
}

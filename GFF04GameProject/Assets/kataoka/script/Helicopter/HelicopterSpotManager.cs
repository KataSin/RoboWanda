using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpotManager : MonoBehaviour
{
    private List<GameObject> m_Points;
    private List<GameObject> m_Helis;


    private GameObject m_Player;
    // Use this for initialization
    void Start()
    {
        m_Points = new List<GameObject>();
        m_Helis = new List<GameObject>();

        foreach (var i in transform.GetComponentsInChildren<Transform>())
        {
            if (i.name == "SpotPoint")
            {
                m_Points.Add(i.gameObject);
            }
            else if (i.name == "HeicopterBreakBill")
            {
                m_Helis.Add(i.gameObject);
            }
        }

        m_Player = GameObject.FindGameObjectWithTag("Player");

        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject disObject = m_Points[0];
        foreach (var i in m_Points)
        {
            float dis = Vector3.Distance(disObject.transform.position, m_Player.transform.position);
            float dis2 = Vector3.Distance(i.transform.position, m_Player.transform.position);
            if (dis > dis2)
            {
                disObject = i;
            }
        }

        foreach(var i in m_Helis)
        {
            if (i.gameObject == null) return;
            i.GetComponent<HelicopterBreakBillSpot>().m_ToPoint = disObject;
        }

    }
}

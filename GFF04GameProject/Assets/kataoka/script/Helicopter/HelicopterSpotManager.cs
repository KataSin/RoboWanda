using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpotManager : MonoBehaviour
{
    struct SpotState
    {
        public GameObject m_Point;
        public List<GameObject> m_GoPoints;
    }

    private List<SpotState> m_Points;
    private List<GameObject> m_Helis;

    private GameObject m_Player;
    // Use this for initialization
    void Start()
    {
        m_Points = new List<SpotState>();
        m_Helis = new List<GameObject>();

        foreach (var i in transform.GetComponentsInChildren<Transform>())
        {
            if (i.name == "SpotPoint")
            {
                SpotState state = new SpotState();
                state.m_Point = i.gameObject;
                state.m_GoPoints = new List<GameObject>();
                foreach (var j in i.GetComponentsInChildren<Transform>())
                {
                    if (j.name != i.name)
                        state.m_GoPoints.Add(j.gameObject);
                }
                m_Points.Add(state);
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
        SpotState disObject = m_Points[0];
        foreach (var i in m_Points)
        {
            float dis = Vector3.Distance(disObject.m_Point.transform.position, m_Player.transform.position);
            float dis2 = Vector3.Distance(i.m_Point.transform.position, m_Player.transform.position);
            if (dis > dis2)
            {
                disObject = i;
            }
        }

        int count = 0;
        foreach (var i in m_Helis)
        {
            if (i.gameObject == null) return;
            i.GetComponent<HelicopterBreakBillSpot>().m_ToPoint = disObject.m_GoPoints[count];
            i.GetComponent<HelicopterBreakBillSpot>().m_Light.transform.LookAt(disObject.m_Point.transform);
            count++;
        }

    }
}

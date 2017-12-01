using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerchPointCenter : MonoBehaviour
{

    public struct SerchPointState
    {
        public GameObject m_SerchPoint;
        public GameObject m_Point;
        public float m_Distance;
    }

    private List<SerchPointState> m_SerchPoints;


    private GameObject m_Robot;
    // Use this for initialization
    void Start()
    {
        m_SerchPoints = new List<SerchPointState>();

        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        var objs = GetComponentsInChildren<Transform>();
        foreach (Transform i in objs)
        {
            //自身だったら入れない
            if (i.gameObject.name == gameObject.name) continue;
            SerchPointState point;
            point.m_Distance = Vector3.Distance(i.transform.position, transform.position);
            point.m_SerchPoint = i.gameObject;
            GameObject pointObj = Instantiate(new GameObject());
            pointObj.transform.position = i.gameObject.transform.position;
            pointObj.transform.parent = transform;
            point.m_Point = pointObj;
            m_SerchPoints.Add(point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Robot.transform.position;
        foreach (var i in m_SerchPoints)
        {
            Ray ray = new Ray(transform.position + new Vector3(0, 2, 0), (i.m_Point.transform.position - transform.position) + new Vector3(0, 2, 0));
            RaycastHit hit;
            int layer = ~(1 << 8);
            if (Physics.Raycast(ray, out hit, i.m_Distance, layer))
            {
                Vector3 hitPoint = hit.point;
                string name = hit.collider.gameObject.name;
                i.m_SerchPoint.transform.position = hitPoint;
            }
            else
            {
                i.m_SerchPoint.transform.position = i.m_Point.transform.position;
            }
        }
    }

    public List<SerchPointState> GetSerchPointState()
    {
        return m_SerchPoints;
    }

    public void OnDrawGizmos()
    {
        //foreach (var i in m_SerchPoints)
        //{
        //    Ray ray = new Ray(transform.position, i.m_SerchPoint.transform.position - transform.position);
        //    Gizmos.DrawRay(ray);
        //}
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCenterManager : MonoBehaviour
{
    public GameObject m_CenterPointPrefab;
    struct TankPoint
    {
        public GameObject centerPoint;　//センターオブジェクト
        public GameObject posRay; //Ray用の座標
    }
    private List<TankPoint> m_CenterPoint;

    private float m_Angle;

    private int m_PointSize;

    private GameObject m_Robot;
    // Use this for initialization
    void Start()
    {
        m_CenterPoint = new List<TankPoint>();
        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        m_PointSize = 0;
        m_Angle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var tanks = GameObject.FindGameObjectsWithTag("Tank");
        int pointsize = tanks.Length;
        if (m_PointSize != pointsize)
        {
            foreach (var i in m_CenterPoint)
            {
                Destroy(i.centerPoint);
                Destroy(i.posRay);
            }
            m_CenterPoint.Clear();

            for (int i = 0; i <= pointsize - 1; i++)
            {
                TankPoint point = new TankPoint();
                point.centerPoint = Instantiate(m_CenterPointPrefab, Vector3.zero, Quaternion.identity);
                Vector3 vec = new Vector3(Mathf.Sin(360.0f * ((float)i / (float)pointsize) * Mathf.PI / 180.0f),
                    0.0f,
                    Mathf.Cos(360.0f * ((float)i / (float)pointsize) * Mathf.PI / 180.0f));

                point.posRay = Instantiate(new GameObject(), transform.position + (vec * 50.0f), Quaternion.identity);
                point.posRay.transform.parent = transform;
                m_CenterPoint.Add(point);
            }
            m_PointSize = pointsize;
        }

        foreach (var i in m_CenterPoint)
        {
            Ray ray = new Ray(transform.position, i.posRay.transform.position - transform.position);
            RaycastHit hit;
            int layer = ~(1 << 8 | 1 << 16);
            //ボムのあたり判定に現在当たる2018/1/8
            if (Physics.Raycast(ray, out hit, Vector3.Distance(i.posRay.transform.position, transform.position), layer))
            {
                i.centerPoint.transform.position = hit.point;
            }
            else
            {
                i.centerPoint.transform.position = i.posRay.transform.position;
            }

        }
        for (int i = 0; i <= pointsize - 1; i++)
        {
            tanks[i].GetComponent<Tank>().SetGoTankPos(m_CenterPoint[i].centerPoint.transform.position);
        }

        transform.position = m_Robot.transform.position+new Vector3(0,10,0);
    }
}

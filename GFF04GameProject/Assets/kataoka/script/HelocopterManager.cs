using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelocopterManager : MonoBehaviour
{
    //最大何機出すか
    public int m_MaxHelicopter = 4;
    //ポイント
    private List<GameObject> m_Points;
    //ヘリがどのポイントにいるか
    private Dictionary<int, GameObject> m_Helicopter;
    //ヘリ回るセンターの場所
    private GameObject m_PointCenter;
    //ロボット
    private GameObject m_Robot;
    private float m_Count;
    // Use this for initialization
    void Start()
    {
        m_PointCenter = transform.Find("HelicopterPointCenter").gameObject;
        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        m_Points = new List<GameObject>();
        m_Points.AddRange(GameObject.FindGameObjectsWithTag("HelicopterPoint"));


    }

    // Update is called once per frame
    void Update()
    {
        m_PointCenter.transform.Rotate(Vector3.up, 10.0f);
        m_PointCenter.transform.position = m_Robot.transform.position;

    }

    private float SortDis(GameObject left, GameObject right)
    {
        return Vector3.Distance(left.transform.position, m_Robot.transform.position) - Vector3.Distance(right.transform.position, m_Robot.transform.position);
    }
}

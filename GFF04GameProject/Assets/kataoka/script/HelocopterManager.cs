using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelocopterManager : MonoBehaviour
{
    //最大何機出すか
    public int m_MaxHelicopter = 4;

    public GameObject m_HelicopterPrefab;
    //ポイント
    private List<GameObject> m_Points;
    //ヘリがどのポイントにいるか
    private List<GameObject> m_Helicopter;
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
        m_Helicopter = new List<GameObject>();
        m_Points = new List<GameObject>();
        m_Points.AddRange(GameObject.FindGameObjectsWithTag("HelicopterPoint"));


        for (int i = 0; i <= 3; i++)
        {
            m_Helicopter.Add(Instantiate(m_HelicopterPrefab,m_Points[i].transform.position,Quaternion.identity));

        }
    }

    // Update is called once per frame
    void Update()
    {
        m_PointCenter.transform.position = m_Robot.transform.position;
        int count = 0;

        for (int i = 0; i <= m_Helicopter.Count - 1; i++)
        {
            //死んでたらヘリコプター追加
            if (m_Helicopter[i] == null)
            {
                m_Helicopter.Remove(m_Helicopter[i]);
                m_Helicopter.Add(Instantiate(m_HelicopterPrefab, transform.position, Quaternion.identity));

            }
            m_Helicopter[i].GetComponent<Helicopter>().SetPosition(m_Points[count].transform.position);
            count++;
        }

    }
    private bool SortDis(GameObject left, GameObject right)
    {
        return Vector3.Distance(left.transform.position, m_Robot.transform.position) < Vector3.Distance(right.transform.position, m_Robot.transform.position);
    }


}

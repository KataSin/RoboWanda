using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelocopterManager : MonoBehaviour
{
    //最大何機出すか
    public int m_MaxHelicopter = 4;
    //帰還させるか
    public bool m_ReturnHeli;

    public GameObject m_HelicopterPrefab;
    public GameObject m_MissileHelicopterPrefab;
    //ポイント
    private List<GameObject> m_Points;
    //スポーンポイント
    private List<GameObject> m_SpawnPoints;
    //ヘリがどのポイントにいるか
    private List<GameObject> m_Helicopter;
    //ミサイルを積んだヘリコプター
    private List<GameObject> m_MissileHelicopter;
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
        m_SpawnPoints = new List<GameObject>();
        m_Helicopter = new List<GameObject>();
        m_Points = new List<GameObject>();
        m_Points.AddRange(GameObject.FindGameObjectsWithTag("HelicopterPoint"));

        m_MissileHelicopter = new List<GameObject>();

        Transform spawnPointTrans = transform.Find("SpawnPoints");
        var points = spawnPointTrans.GetComponentsInChildren<Transform>();
        foreach (var i in points)
        {
            if (i.name != spawnPointTrans.name)
                m_SpawnPoints.Add(i.gameObject);
        }

        for (int i = 0; i <= 3; i++)
        {
            m_MissileHelicopter.Add(Instantiate(m_MissileHelicopterPrefab, m_SpawnPoints[i].transform.position, Quaternion.identity));
        }

        for (int i = 0; i <= 3; i++)
        {
            m_Helicopter.Add(Instantiate(m_HelicopterPrefab, m_Points[i].transform.position, Quaternion.identity));

        }

        m_ReturnHeli = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ReturnHeli)
        {
            for(int i = 0; i <= m_Helicopter.Count - 1; i++)
            {
                if (m_Helicopter[i] != null)
                {
                    m_Helicopter[i].GetComponent<Helicopter>().m_ReturnFlag = true;
                    m_Helicopter[i] = null;
                }
            }
            for (int i = 0; i <= m_MissileHelicopter.Count - 1; i++)
            {
                if (m_MissileHelicopter[i] != null)
                {
                    m_MissileHelicopter[i].GetComponent<HelicopterMissile>().m_ReturnFlag = true;
                    m_MissileHelicopter[i] = null;
                }
            }
            return;
        }


        m_PointCenter.transform.position = m_Robot.transform.position;
        int count = 0;



        //攻撃するヘリコプター
        for (int i = 0; i <= m_MissileHelicopter.Count - 1; i++)
        {
            //死んだり攻撃し終わってたら追加
            if (m_MissileHelicopter[i] == null)
            {
                m_MissileHelicopter.Remove(m_MissileHelicopter[i]);
                m_MissileHelicopter.Insert(i, Instantiate(m_MissileHelicopterPrefab, m_SpawnPoints[i].transform.position, Quaternion.identity));
            }
        }
        //回り回っているヘリコプター
        for (int i = 0; i <= m_Helicopter.Count - 1; i++)
        {
            //死んでたらヘリコプター追加
            if (m_Helicopter[i] == null)
            {
                m_Helicopter.Remove(m_Helicopter[i]);

                int rand = Random.Range(0, m_SpawnPoints.Count - 1);
                m_Helicopter.Insert(0,Instantiate(m_HelicopterPrefab, m_SpawnPoints[rand].transform.position, Quaternion.identity));
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

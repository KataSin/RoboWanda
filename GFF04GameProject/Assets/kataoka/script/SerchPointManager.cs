using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SerchPointManager : MonoBehaviour
{
    //サーチライトプレハブ
    public GameObject m_SerchLightPrefab;

    //サーチライトたち
    private List<GameObject> m_SerchLights;
    //サーチポイントセンター
    private SerchPointCenter m_SerchPointCenter;
    //スポーンポイントたち
    private List<Vector3> m_SpawnPoints;

    // Use this for initialization
    void Start()
    {
        m_SerchPointCenter = transform.Find("SerchPointCenter").gameObject.GetComponent<SerchPointCenter>();
        m_SpawnPoints = new List<Vector3>();

        m_SerchLights = new List<GameObject>();


        Transform trans = transform.Find("SpawnPoints");

        var points = trans.GetComponentsInChildren<Transform>();
        //スポーンポイント設定
        foreach (var i in points)
        {
            if (i.name == trans.gameObject.name) continue;
            m_SpawnPoints.Add(i.position);
        }
        //サーチライト生成
        for (int i = 0; i <= 3; i++)
        {
            int random = Random.Range(0, m_SpawnPoints.Count - 1);
            m_SerchLights.Add(Instantiate(m_SerchLightPrefab, m_SpawnPoints[random], Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //サーチポイントを代入
        List<SerchPointCenter.SerchPointState> points = m_SerchPointCenter.GetSerchPointState();

        for (int i = 0; i <= m_SerchLights.Count - 1; i++)
        {
            if (m_SerchLights[i].GetComponent<NavMeshAgent>() == null)
            {
                int random = Random.Range(0, m_SpawnPoints.Count - 1);
                m_SerchLights[i] = Instantiate(m_SerchLightPrefab, m_SpawnPoints[random], Quaternion.identity);
            }
            m_SerchLights[i].GetComponent<NavMeshAgent>().destination = points[i].m_SerchPoint.transform.position;
        }
    }
}

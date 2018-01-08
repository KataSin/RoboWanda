using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyDrop : MonoBehaviour
{
    public float m_StrategyTime;
    public GameObject m_HeliDropPrefab;

    public enum DropItem
    {
        SMOKE_BULLET,
        LIGHT_BULLET
    }
    public DropItem m_Drop;
    private List<GameObject> m_DropPoints;
    private List<GameObject> m_Helis;

    private float m_Time;
    //投下したか
    private bool m_IsDrop;
    //スポーンしたか
    private bool m_IsSpawn;
    private Vector3 m_SpawnPoint;

    // Use this for initialization
    void Start()
    {
        m_DropPoints = new List<GameObject>();
        m_Helis = new List<GameObject>();
        foreach(var i in GetComponentsInChildren<Transform>())
        {
            if (i.name =="DropPoint")
                m_DropPoints.Add(i.gameObject);
            if (i.name == "DropSpawnPoint")
                m_SpawnPoint = i.position;
        }
        m_Time = 0.0f;
        m_IsSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsSpawn)
        {
            m_Time += Time.deltaTime;
            if (m_Time >= m_StrategyTime)
            {
                for (int i = 0; i <= m_DropPoints.Count - 1; i++)
                {
                    GameObject heli = Instantiate(m_HeliDropPrefab, m_SpawnPoint, Quaternion.identity);
                    heli.GetComponent<HelicopterDrop>().SetPoint(m_DropPoints[i].transform.position);
                    m_Helis.Add(heli);
                    m_IsSpawn = true;
                }
            }
        }
        else
        {
            bool flag = true;
            foreach(var i in m_Helis)
            {
                if (i.GetComponent<HelicopterDrop>().GetDropPoint()) continue;
                flag = false;
            }
            if (flag)
            {
                foreach(var i in m_Helis)
                {
                    i.GetComponent<HelicopterDrop>().SetDrop(true);
                    i.GetComponent<HelicopterDrop>().DropBox();
                }
                Destroy(gameObject);
            }
        }
    }
}

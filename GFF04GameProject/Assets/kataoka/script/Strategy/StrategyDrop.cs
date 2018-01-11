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

    struct HeliState
    {
        public GameObject heli;//ヘリ
        public float arrivalTime;//スポーンポイントからドロップポイントにかかる時間
        public bool spawnFlag;
    }

    //デバッグコマンドの実装

    public DropItem m_Drop;
    private List<GameObject> m_DropPoints;
    private List<HeliState> m_Helis;

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
        m_Helis = new List<HeliState>();
        foreach (var i in GetComponentsInChildren<Transform>())
        {
            if (i.name == "DropPoint")
                m_DropPoints.Add(i.gameObject);
            if (i.name == "DropSpawnPoint")
                m_SpawnPoint = i.position;
        }

        for (int i = 0; i <= m_DropPoints.Count - 1; i++)
        {
            HeliState state = new HeliState();
            float dis = Vector2.Distance(new Vector2(m_DropPoints[i].transform.position.x, m_DropPoints[i].transform.position.z),
                        new Vector2(m_SpawnPoint.x, m_SpawnPoint.z));
            state.arrivalTime = m_StrategyTime - (dis / 10.0f);
            state.spawnFlag = false;
            m_Helis.Add(state);
        }


        m_Time = 0.0f;
        m_IsSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        Debug.Log(m_Time);
        for (int i = 0; i <= m_Helis.Count - 1; i++)
        {
            if (m_Helis[i].spawnFlag) continue;
            if (m_Helis[i].arrivalTime <= m_Time)
            {
                HeliState state = new HeliState();
                GameObject heli = Instantiate(m_HeliDropPrefab, m_SpawnPoint, Quaternion.identity);
                heli.GetComponent<HelicopterDrop>().SetPoint(m_DropPoints[i].transform.position);
                state.heli = heli;
                state.spawnFlag = true;
                m_Helis[i] = state;
            }
        }

        if (m_Time >= m_StrategyTime)
        {
            foreach (var i in m_Helis)
            {
                i.heli.GetComponent<HelicopterDrop>().SetDrop(true);
                i.heli.GetComponent<HelicopterDrop>().DropBox();
            }
            Destroy(gameObject);
        }
    }
}

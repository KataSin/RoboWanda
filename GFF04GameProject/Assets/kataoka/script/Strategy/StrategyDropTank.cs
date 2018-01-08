using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyDropTank : MonoBehaviour
{

    public float m_StrategyTime;
    private float m_Time;
    private List<GameObject> m_TankPoints;
    private GameObject m_SpawnPoint;
    public GameObject m_HeliTankPrefab;


    // Use this for initialization
    void Start()
    {
        m_TankPoints = new List<GameObject>();
        m_Time = 0.0f;
        m_TankPoints.AddRange(GameObject.FindGameObjectsWithTag("TankPoint"));
        m_SpawnPoint = transform.Find("HeliTankSpawnPoint").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= m_StrategyTime)
        {
            foreach (var i in m_TankPoints)
            {
                GameObject heli = Instantiate(m_HeliTankPrefab, m_SpawnPoint.transform.position, Quaternion.identity);
                heli.GetComponent<HelicopterTank>().SetPoint(i);
            }
            Destroy(gameObject);
        }
    }
}

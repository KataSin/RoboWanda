using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategySpawnTank : MonoBehaviour
{
    //作戦の時間
    public float m_StrategyTime;
    public GameObject m_TankPrefab;

    public AudioClip m_WirelessClip;

    private float m_Time;

    private List<GameObject> m_SpawnPoints;

    private AudioSource m_AudioSource;
    
    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;

        m_SpawnPoints = new List<GameObject>();

        var trans = GetComponentsInChildren<Transform>();
        foreach(var i in trans)
        {
            if (i.name != name)
                m_SpawnPoints.Add(i.gameObject);
        }

        m_AudioSource = GameObject.FindGameObjectWithTag("StrategySound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= m_StrategyTime)
        {
            foreach(var i in m_SpawnPoints)
            {
                Instantiate(m_TankPrefab, i.transform.position, Quaternion.identity);
            }
            m_AudioSource.clip = m_WirelessClip;
            if (m_WirelessClip != null)
                m_AudioSource.PlayOneShot(m_AudioSource.clip);
            Destroy(gameObject);
        }
    }
}

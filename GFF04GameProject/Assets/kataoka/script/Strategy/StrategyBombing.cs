using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyBombing : MonoBehaviour
{
    

    //作戦の時間
    public float m_StrategyTime;
    //時間
    private float m_Time;
    //作戦音声
    public AudioClip m_WirelessClip;
    //爆撃機プレハブ
    public GameObject m_BomberPrefab;

    private AudioSource m_AudioSource;
    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
        m_AudioSource = GameObject.FindGameObjectWithTag("StrategySound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= m_StrategyTime)
        {
            Vector3 spawnPos = transform.Find("BomBingPointStart").position;
            Vector3 goalPos= transform.Find("BomBingPointEnd").position;
            GameObject bomber=Instantiate(m_BomberPrefab,spawnPos,Quaternion.identity);
            bomber.GetComponent<Bombing>().SetStartEnd(spawnPos, goalPos);
            //音声を流す
            m_AudioSource.clip = m_WirelessClip;
            if (m_WirelessClip != null)
                m_AudioSource.PlayOneShot(m_AudioSource.clip);
            Destroy(gameObject);
        }
    }
}

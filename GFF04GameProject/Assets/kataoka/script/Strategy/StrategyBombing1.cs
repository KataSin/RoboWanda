using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyBombing1 : MonoBehaviour
{
    

    //作戦の時間
    public float m_StrategyTime;
    //時間
    private float m_Time;
    //作戦音声
    public AudioClip m_Clips;
    //爆撃機プレハブ
    public GameObject m_BomberPrefab;
    
    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;

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
            bomber.GetComponent<Bombing1>().SetStartEnd(spawnPos, goalPos);
            //音声を流す

            Destroy(gameObject);
        }
    }
}

﻿using System.Collections;
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

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        //m_Time += Time.deltaTime;
        //if (m_Time >= m_StrategyTime)
        //{
        //    Vector3 spawnPos = transform.Find("BomBingPointStart").position;
        //    Vector3 goalPos = transform.Find("BomBingPointEnd").position;
        //    GameObject bomber = Instantiate(m_BomberPrefab, spawnPos, Quaternion.identity);
        //    bomber.GetComponent<Bombing1>().SetStartEnd(spawnPos, goalPos);
        //    //音声を流す
        //    Destroy(gameObject);
        //}
    }

    public void SpawnBomber()
    {
        Vector3 spawnPos = transform.Find("BomBingPointStart").position;
        Vector3 goalPos = transform.Find("BomBingPointEnd").position;
        GameObject bomber = Instantiate(m_BomberPrefab, spawnPos, Quaternion.identity);
        bomber.GetComponent<Bombing1>().SetStartEnd(spawnPos, goalPos);
        bomber.GetComponent<Bombing1>().SetType(Bombing1.BrieBomberType.Normal);
    }

    public void SpawnBomberBreak()
    {
        Vector3 spawnPos = transform.Find("BomBingPointStart").position;
        Vector3 goalPos = transform.Find("BomBingPointEnd").position;
        GameObject bomber = Instantiate(m_BomberPrefab, spawnPos, Quaternion.identity);
        bomber.GetComponent<Bombing1>().SetStartEnd(spawnPos, goalPos);
        bomber.GetComponent<Bombing1>().SetType(Bombing1.BrieBomberType.Break);
    }
}

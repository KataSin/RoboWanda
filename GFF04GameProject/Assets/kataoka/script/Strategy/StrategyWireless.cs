﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyWireless : MonoBehaviour
{
    //無線の時間
    public float m_WirelessTime;
    public AudioClip m_WirelessClip;
    //ヘリを帰らせるかどうか
    public bool m_HeliReturn;
    //ライトをつけるかどうか
    public bool m_IsLight;
    //時間
    private float m_Time;
    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= m_WirelessTime)
        {
            //無線再生
            var audio = GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
            //ヘリを帰らせるかどうか
            GameObject.FindGameObjectWithTag("HeliManager").GetComponent<HelocopterManager>().m_ReturnHeli = m_HeliReturn;
            //ライトを付けるかどうか
            GameObject.FindGameObjectWithTag("RobotLightManager").GetComponent<StrategyRobotLightManager>().m_IsLight = m_IsLight;
            Destroy(gameObject);
        }
    }
}

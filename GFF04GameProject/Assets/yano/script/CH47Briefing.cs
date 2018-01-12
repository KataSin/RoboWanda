﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CH47Briefing : MonoBehaviour
{
    private Color m_origin_color;

    private float t, t2;

    private bool isTarget;
    private bool isClear;

    // Use this for initialization
    void Start()
    {
        m_origin_color = GetComponent<Image>().color;
        m_origin_color.a = 0f;
        GetComponent<Image>().color = m_origin_color;

        t = 0f;
        t2 = 0f;

        isTarget = false;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp01(t);
        t2 = Mathf.Clamp(t2, 0f, 2f);

        GetComponent<Image>().color =
            Color.Lerp(m_origin_color, new Color(m_origin_color.r, m_origin_color.g, m_origin_color.b, 1f), t / 1f);
    }

    public void ActiveMark()
    {
        t += 2.0f * Time.deltaTime;
    }

    public void NotActiveMark()
    {
        t -= 2.0f * Time.deltaTime;
    }

    public float Get_T1()
    {
        return t;
    }
}

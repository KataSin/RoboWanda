using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text m_timer_text;

    [SerializeField]
    private float m_Timer;

    private int minute;
    private float second;
    private string second_string;

    [SerializeField]
    private float m_elapsedTimer;

    // Use this for initialization
    void Start()
    {
        m_timer_text = GetComponent<Text>();

        m_Timer = 300f;
        minute = 5;
        second = 60f;
        second_string = "00";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimerUpdate()
    {
        m_timer_text.text = "0" + minute.ToString() + ":" + second_string;

        m_elapsedTimer += 1.0f * Time.deltaTime;
        m_Timer -= 1.0f * Time.deltaTime;
        second -= 1.0f * Time.deltaTime;
        if (second <= 0f)
            second = 60f;
        if (m_Timer >= 300f)
            minute = 5;
        else if (m_Timer < 300f && m_Timer >= 240f)
            minute = 4;
        else if (m_Timer < 240f && m_Timer >= 180f)
            minute = 3;
        else if (m_Timer < 180f && m_Timer >= 120f)
            minute = 2;
        else if (m_Timer < 120f && m_Timer >= 60f)
            minute = 1;
        else if (m_Timer < 60f && m_Timer >= 0f)
            minute = 0;

        if (second >= 60f)
            second_string = "00";

        else if (second < 60f)
        {
            if (second >= 10f)
                second_string = ((int)second).ToString();
            if (second < 10f)
                second_string = "0" + ((int)second).ToString();
        }

       
    }

    public float Get_ElapsedTimer()
    {
        return m_elapsedTimer;
    }
}

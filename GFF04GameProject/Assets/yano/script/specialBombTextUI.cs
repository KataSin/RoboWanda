using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class specialBombTextUI : MonoBehaviour
{
    private Text text_;
    private char[] m_text_char;

    private float m_interval;

    private int m_charNum;

    // Use this for initialization
    void Start()
    {
        text_ = GetComponent<Text>();
        m_text_char = new char[6];

        m_text_char[0] = 'B';
        m_text_char[1] = 'o';
        m_text_char[2] = 'm';
        m_text_char[3] = 'b';

        m_interval = 1f;
        m_charNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_charNum <= 3)
        {
            if (m_interval <= 0f)
            {
                text_.text += m_text_char[m_charNum];
                m_charNum++;
                m_interval = 1f;
            }
        }

        m_interval -= 10.0f * Time.deltaTime;
    }
}

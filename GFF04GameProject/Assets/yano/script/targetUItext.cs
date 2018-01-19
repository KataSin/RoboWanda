using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class targetUItext : MonoBehaviour
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

        m_text_char[0] = 'T';
        m_text_char[1] = 'a';
        m_text_char[2] = 'r';
        m_text_char[3] = 'g';
        m_text_char[4] = 'e';
        m_text_char[5] = 't';

        m_interval = 1f;
        m_charNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_charNum <= 5)
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

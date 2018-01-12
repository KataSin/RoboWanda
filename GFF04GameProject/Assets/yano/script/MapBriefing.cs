using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBriefing : MonoBehaviour
{
    private RectTransform m_rect;

    private Renderer color_;
    private Color m_originColor;

    private float t1, m_speed, t2;

    private bool isClear;
    private bool isBreath;

    // Use this for initialization
    void Start()
    {
        m_rect = GetComponent<RectTransform>();

        GetComponent<Image>().material.SetColor("_TintColor", new Color(128f / 255f, 128f / 255f, 128f / 255f));
        m_originColor = GetComponent<Image>().material.GetColor("_TintColor");

        m_rect.localScale = new Vector3(0f, 5f, 1f);
        t1 = 0f;
        m_speed = 1f;

        isClear = false;
        isBreath = false;
    }

    // Update is called once per frame
    void Update()
    {
        t1 = Mathf.Clamp01(t1);

        if (t1 >= 1f)
            isClear = true;

        m_rect.localScale =
            Vector3.Lerp(new Vector3(0f, 5f, 1f), new Vector3(8.8f, 5f, 1f), t1 / 1f);

        GetComponent<Image>().material.SetColor("_TintColor", Color.Lerp(m_originColor, new Color(128f / 255f, 200f / 255f, 128f / 255f), t2 / 2f));

        if (!isBreath)
        {
            BreathUp();

            if (t2 >= 2f)
                isBreath = true;
        }
        else
        {
            BreathDown();

            if (t2 <= 0f)
                isBreath = false;
        }   
    }

    public void Open()
    {
        t1 += 1.0f * Time.deltaTime * m_speed;
        m_speed++;
    }

    public void Close()
    {
        t1 -= 1.0f * Time.deltaTime * m_speed;
        m_speed++;
    }

    public void BreathUp()
    {
        t2 += 1.0f * Time.deltaTime;
    }

    public void BreathDown()
    {
        t2 -= 1.0f * Time.deltaTime;
    }

    public bool Get_Clear()
    {
        return isClear;
    }
}

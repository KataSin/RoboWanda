using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScan : MonoBehaviour
{
    private Color m_origin_color;

    private float t, m_interval;
    [SerializeField]
    private bool isScanAlpha;
    private bool isScan;

    // Use this for initialization
    void Start()
    {
        t = 0f;
        m_interval = 4f;

        isScanAlpha = false;
        isScan = false;

        m_origin_color = GetComponent<Image>().color;
        m_origin_color.a = 0f;
        GetComponent<Image>().color = m_origin_color;

        transform.localPosition = new Vector3(-610f, 76f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isScanAlpha)
            GetComponent<Image>().color =
                Color.Lerp(m_origin_color, new Color(m_origin_color.r, m_origin_color.g, m_origin_color.b, 1f), t / 1f);
        else
            GetComponent<Image>().color =
                Color.Lerp(new Color(m_origin_color.r, m_origin_color.g, m_origin_color.b, 1f), m_origin_color, t / 2f);

        transform.localPosition = Vector3.Lerp(new Vector3(-610f, 76f, 0f), new Vector3(260f, 76f, 0f), t / 2f);
    }

    public void ScanFead()
    {
        if (!isScan)
        {
            t += 2.0f * Time.deltaTime;

            if (t >= 1f)
            {
                isScanAlpha = true;

                if (t >= 2f)
                {
                    isScanAlpha = false;
                    t = 0f;
                    isScan = true;
                }
            }
        }

        else
        {
            if (m_interval <= 0f)
            {
                isScan = false;
                m_interval = 2f;
            }

            m_interval -= 1.0f * Time.deltaTime;
        }
    }
}

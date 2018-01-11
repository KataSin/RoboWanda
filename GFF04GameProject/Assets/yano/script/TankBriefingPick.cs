using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankBriefingPick : MonoBehaviour
{
    [SerializeField]
    private GameObject tank_;

    private Color m_origin_color;

    [SerializeField]
    private Vector3 m_origin_Lpos;

    [SerializeField]
    private Vector3 m_after_Lpos;

    private float t, t2, t3;

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
        t3 = 0f;

        isTarget = false;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp01(t);
        t2 = Mathf.Clamp(t2, 0f, 2f);
        t3 = Mathf.Clamp(t3, 0f, 2f);

        GetComponent<Image>().color =
            Color.Lerp(m_origin_color, new Color(m_origin_color.r, m_origin_color.g, m_origin_color.b, 1f), t / 1f);

        transform.localPosition =
            Vector3.Lerp(m_origin_Lpos, m_after_Lpos, t3 / 2f);

        tank_.transform.localPosition =
            Vector3.Lerp(new Vector3(-290f, 139f, 0f), new Vector3(-86f, 215f, 0f), t2 / 1f);

        if (!isClear)
        {
            if (!isTarget)
                tank_.transform.localScale =
                    Vector3.Lerp(Vector3.zero, new Vector3(0.2f, 1.5f, 0f), t2 / 1f);
            else
                tank_.transform.localScale =
                    Vector3.Lerp(new Vector3(0.2f, 1.5f, 0f), new Vector3(2.5f, 1.5f, 0f), t2 - 1f / 1f);
        }
        else
        {
            if (!isTarget)
                tank_.transform.localScale =
                    Vector3.Lerp(new Vector3(0.2f, 1.5f, 0f), new Vector3(2.5f, 1.5f, 0f), t2 - 1f / 1f);
            else
                tank_.transform.localScale =
                    Vector3.Lerp(Vector3.zero, new Vector3(0.2f, 1.5f, 0f), t2 / 1f);
        }
    }

    public void ActiveMark()
    {
        t += 2.0f * Time.deltaTime;
    }

    public void MoveMark()
    {
        t3 += 1.0f * Time.deltaTime;
    }

    public void ActiveTank()
    {
        t2 += 3.0f * Time.deltaTime;

        if (t2 >= 1f)
        {
            isTarget = true;
            if (t2 >= 2f)
            {
                isTarget = false;
                isClear = true;
            }
        }
    }

    public void CloseTank()
    {
        t2 -= 3.0f * Time.deltaTime;

        if (t2 <= 1f)
        {
            isTarget = true;
        }
    }

    public void NotActiveMark()
    {
        t -= 2.0f * Time.deltaTime;
    }

    public float Get_T1()
    {
        return t;
    }

    public float Get_T2()
    {
        return t2;
    }

    public bool Get_Clear()
    {
        return isClear;
    }
}

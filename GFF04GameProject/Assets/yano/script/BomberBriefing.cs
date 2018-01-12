using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomberBriefing : MonoBehaviour
{
    [SerializeField]
    private GameObject bomber_;

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

        bomber_.transform.localPosition =
            Vector3.Lerp(new Vector3(-465f, 66f, 0f), new Vector3(-296f, 203f, 0f), t2 / 1f);

        if (!isClear)
        {
            if (!isTarget)
                bomber_.transform.localScale =
                    Vector3.Lerp(Vector3.zero, new Vector3(0.2f, 1.5f, 0f), t2 / 1f);
            else
                bomber_.transform.localScale =
                    Vector3.Lerp(new Vector3(0.2f, 1.5f, 0f), new Vector3(2.5f, 1.5f, 0f), t2 - 1f / 1f);
        }
        else
        {
            if (!isTarget)
                bomber_.transform.localScale =
                    Vector3.Lerp(new Vector3(0.2f, 1.5f, 0f), new Vector3(2.5f, 1.5f, 0f), t2 - 1f / 1f);
            else
                bomber_.transform.localScale =
                    Vector3.Lerp(Vector3.zero, new Vector3(0.2f, 1.5f, 0f), t2 / 1f);
        }
    }

    public void ActiveMark()
    {
        t += 2.0f * Time.deltaTime;
    }

    public void ActiveBomber()
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

    public void CloseBomber()
    {        
        t2 -= 3.0f * Time.deltaTime;

        if (t2 <= 1f)
        {
            isTarget = true;
            if(t2<=0f)
                t -= 2.0f * Time.deltaTime;
        }
    }

    public float Get_T1()
    {
        return t;
    }
}

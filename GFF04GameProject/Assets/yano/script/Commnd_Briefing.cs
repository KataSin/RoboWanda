using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commnd_Briefing : MonoBehaviour
{
    private RectTransform m_rect;

    private float t1, t2;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        m_rect = GetComponent<RectTransform>();
        RectInit();
        t1 = 0f;
        t2 = 1f;
        isClear = false;
    }

    private void RectInit()
    {
        m_rect.localPosition = new Vector3(-80f, 0f, 0f);
        m_rect.pivot = new Vector2(0f, 0.5f);
        m_rect.localScale = new Vector3(0f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        t1 = Mathf.Clamp(t1, 0f, 1f);

        if (t1 >= 1f)
            isClear = true;

        m_rect.localScale = Vector3.Lerp(new Vector3(0f, 1f, 1f), Vector3.one, t1 / 1f);
    }

    public void LeftFead()
    {
        t1 += 1.0f * Time.deltaTime * t2;
        t2 ++;
    }

    public void RightFead()
    {
        t1 -= 1.0f * Time.deltaTime * t2;
        t2 ++;
    }

    public void PivotChange()
    {
        m_rect.localPosition = new Vector3(80f, 0f, 0f);
        m_rect.pivot = new Vector2(1f, 0.5f);
    }

    public bool Get_Clear()
    {
        return isClear;
    }
}

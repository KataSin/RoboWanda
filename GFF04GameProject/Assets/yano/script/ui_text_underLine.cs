using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_text_underLine : MonoBehaviour
{
    private RectTransform rect_;

    private float t;

    private bool isClear;

    [SerializeField]
    private Vector3 m_before_scale;

    [SerializeField]
    private Vector3 m_after_scale;

    // Use this for initialization
    void Start()
    {
        rect_ = GetComponent<RectTransform>();
        rect_.localScale = m_before_scale;
        t = 0f;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        rect_.localScale = Vector3.Lerp(m_before_scale, m_after_scale, t / 1f);
        LineUpdate();
    }

    public void LineUpdate()
    {
        t += 8.0f * Time.deltaTime;

        if (t >= 1f) isClear = true;
    }

    public bool Get_Clear()
    {
        return isClear;
    }
}

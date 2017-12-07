using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_imageScale : MonoBehaviour
{
    private RectTransform m_rect;

    [SerializeField]
    private Vector3 m_afterSize;

    private Vector3 m_originSize;

    [SerializeField]
    [Header("変位にかかる時間")]
    private float m_displacementTime;

    private float t;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        m_rect = GetComponent<RectTransform>();

        m_originSize = m_rect.localScale;
        t = 0f;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp(t, 0f, m_displacementTime);

        isClear = (t >= m_displacementTime) ? true : false;

        m_rect.localScale =
           Vector3.Lerp(m_originSize, m_afterSize, t / m_displacementTime);
    }

    public void ScaleChange()
    {
        t += 1.0f * Time.deltaTime;       
    }

    public void ScaleBack()
    {
        t -= 1.0f * Time.deltaTime;
    }

    public bool GetClear()
    {
        return isClear;
    }
}

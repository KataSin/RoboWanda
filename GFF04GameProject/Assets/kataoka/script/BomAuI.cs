using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BomAuI : MonoBehaviour
{
    private Vector3 m_Target;
    private RectTransform m_Rect;
    private bool m_IsDraw;

    private CanvasGroup m_Group;
    // Use this for initialization
    void Start()
    {
        m_Rect = GetComponent<RectTransform>();
        m_Group = GetComponent<CanvasGroup>();
        m_Target = Vector3.zero;
        m_IsDraw = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsDraw)
        {
            m_Group.alpha -= Time.deltaTime;
            return;
        }
        m_Group.alpha += Time.deltaTime;
        m_Rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, m_Target);
    }

    public void SetTarget(Vector3 pos)
    {
        m_Target = pos;
    }

    public void SetIsDraw(bool flag)
    {
        m_IsDraw = flag;
    }
}

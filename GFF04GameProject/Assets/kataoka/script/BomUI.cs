using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BomUI : MonoBehaviour
{
    private CanvasGroup m_Group;

    private RectTransform m_Rect;

    private GameObject m_DrawObj;

    private bool m_DrawFlag;
    private float m_Alpha;
    // Use this for initialization
    void Start()
    {
        m_Group = GetComponent<CanvasGroup>();
        m_Rect = GetComponent<RectTransform>();

        m_DrawFlag = false;

        m_Alpha = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_DrawFlag) m_Alpha += Time.deltaTime;
        else m_Alpha -= Time.deltaTime;
        m_Alpha = Mathf.Clamp(m_Alpha, 0.0f, 1.0f);


        m_Group.alpha = m_Alpha;
        if (m_DrawObj != null)
            m_Rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, m_DrawObj.transform.position);

    }
    //ワールド座標をスクリーン座標に
    public void SetPosition(GameObject obj)
    {
        m_DrawObj = obj;
    }
    //表示するかどうか
    public void SetDraw(bool flag)
    {
        m_DrawFlag = flag;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOut_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject black_curtain_obj_;

    private Image black_curtain_;

    private Color m_lerp_color;

    private float t, t1, t2;

    [SerializeField]
    private float m_blackFeadTime;

    [SerializeField]
    private float m_blackFeadTime2;

    [SerializeField]
    private bool isClear, isClearGO,isClearGC;

    // Use this for initialization
    void Start()
    {
        black_curtain_ = black_curtain_obj_.GetComponent<Image>();

        t = 0f;
        t1 = 0f;
        t2 = 0f;

        m_lerp_color = black_curtain_.color;


        isClear = false;
        isClearGO = false;
        isClearGC = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BlackOut()
    {
        t += 1.0f * Time.deltaTime;

        m_lerp_color.a = Mathf.Lerp(0.0f, 1.0f, t / m_blackFeadTime);

        black_curtain_.color = m_lerp_color;

        CheackClear();

    }

    public void FeadIn()
    {
        t += 1.0f * Time.deltaTime;

        m_lerp_color.a = Mathf.Lerp(1.0f, 0.0f, t / m_blackFeadTime);

        black_curtain_.color = m_lerp_color;

        CheackClear();
    }

    public void GameClearFead()
    {
        t1 += 1.0f * Time.deltaTime;

        m_lerp_color.a = Mathf.Lerp(0.0f, 1.0f, t1 / m_blackFeadTime2);

        black_curtain_.color = m_lerp_color;

        CheackClear();
    }

    public void GameOverFead()
    {
        t2 += 1.0f * Time.deltaTime;

        m_lerp_color.a = Mathf.Lerp(0.0f, 0.3f, t2 / m_blackFeadTime2);

        black_curtain_.color = m_lerp_color;

        CheackClear();
    }

    private void CheackClear()
    {
        if (t >= m_blackFeadTime)
            isClear = true;

        if (t1 >= m_blackFeadTime2)
            isClearGC = true;

        if (t2 >= m_blackFeadTime2)
            isClearGO = true;
    }

    public float GetT()
    {
        return t;
    }

    public float GetBFeadTime()
    {
        return m_blackFeadTime;
    }

    public void ResetT()
    {
        t = 0f;
        isClear = false;
    }

    public bool Get_Clear()
    {
        return isClear;
    }

    public bool Get_ClearGO()
    {
        return isClearGO;
    }

    public bool Get_ClearGC()
    {
        return isClearGC;
    }
}

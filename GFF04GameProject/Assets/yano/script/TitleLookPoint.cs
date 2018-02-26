using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLookPoint : MonoBehaviour
{
    private Vector3 m_origin_pos;

    private Vector3 m_yes_pos;

    private Vector3 m_no_pos1;
    private Vector3 m_no_pos2;

    private float t, t1;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        m_origin_pos = transform.localPosition;

        m_yes_pos = new Vector3(0.359f, 1.8f, -1.4f);
        m_no_pos1 = new Vector3(0.195f, transform.localPosition.y, -1.38f);
        m_no_pos2 = new Vector3(0.55f, transform.localPosition.y, -1.5f);

        t = 0f;
        t1 = 0f;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp01(t);
    }

    public void YesMove()
    {
        transform.localPosition = Vector3.Lerp(m_origin_pos, m_yes_pos, t / 1f);

        if (t >= 1f)
            isClear = true;

        if (!isClear)
            t += 3.0f * Time.deltaTime;

        else t -= 3.0f * Time.deltaTime;
    }

    public void NoMove()
    {
        if (t1 < 1f)
            transform.localPosition = Vector3.Lerp(m_origin_pos, m_no_pos1, t1 / 1f);
        if (t1 >= 1f && t1 < 2f)
            transform.localPosition = Vector3.Lerp(m_no_pos1, m_no_pos2, t1 - 1f / 1f);
        if (t1 >= 2f)
            transform.localPosition = Vector3.Lerp(m_no_pos2, m_origin_pos, t1 - 2f / 1f);

        t1 += 3.0f * Time.deltaTime;
    }

    public float GetT()
    {
        return t;
    }

    public bool GetClear()
    {
        return isClear;
    }
}

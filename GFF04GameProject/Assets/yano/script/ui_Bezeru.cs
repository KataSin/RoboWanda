using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_Bezeru : MonoBehaviour
{
    [SerializeField]
    private GameObject topBezeru_;
    private RectTransform top_rect_;

    [SerializeField]
    private GameObject bottomBezeru_;
    private RectTransform bottom_rect_;

    private float t;

    [SerializeField]
    private float m_feadTime;

    // Use this for initialization
    void Start()
    {
        top_rect_ = topBezeru_.GetComponent<RectTransform>();
        bottom_rect_ = bottomBezeru_.GetComponent<RectTransform>();

        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        top_rect_.localPosition = Vector3.Lerp(new Vector3(0f, 360f, 0f), new Vector3(0f, 440f, 0f), t / m_feadTime);
        bottom_rect_.localPosition = Vector3.Lerp(new Vector3(0f, -360f, 0f), new Vector3(0f, -440f, 0f), t / m_feadTime);

        t = Mathf.Clamp(t, 0f, m_feadTime);
    }

    public void FeadOut()
    {
        t += 1.0f * Time.deltaTime;
    }

    public void FeadIn()
    {
        t -= 1.0f * Time.deltaTime;
    }

    public float GetT()
    {
        return t;
    }

    public void SetT(float l_t)
    {
        t = l_t;
    }

    public float GetFeadTime()
    {
        return m_feadTime;
    }
}

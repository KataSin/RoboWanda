using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOut_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject balck_curtain_onj_;

    private Image balck_curtain_;

    private Color m_lerp_color;

    private float t;

    [SerializeField]
    private float m_blackFeadTime;

    // Use this for initialization
    void Start()
    {
        balck_curtain_ = balck_curtain_onj_.GetComponent<Image>();

        t = 0f;

        m_lerp_color = balck_curtain_.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlackOut()
    {
        t += 1.0f * Time.deltaTime;

        m_lerp_color.a = Mathf.Lerp(0.0f, 1.0f, t / m_blackFeadTime);

        balck_curtain_.color = m_lerp_color;
    }
}

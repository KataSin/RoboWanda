using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Flashing : MonoBehaviour
{
    //アルファ値
    private enum AlphaValue
    {
        Increase,   //増加
        Decrease,   //減少
    }

    [SerializeField]
    [Header("点滅間隔")]
    private float m_flsh_timer;

    [SerializeField]
    private GameObject canvas_;

    private float t;

    private Image ui_text_;

    private AlphaValue alphaValue_ = AlphaValue.Decrease;

    private Color m_lerp_color;

    // Use this for initialization
    void Start()
    {
        ui_text_ = GetComponent<Image>();
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        t += 1.0f * Time.deltaTime;

        Alpha_Flashing();
    }

    private void Alpha_Flashing()
    {
        if (canvas_ != null)
        {
            if (canvas_.GetComponent<TitleManager>().titleState_ == TitleManager.TitleState.Start)
            {
                m_flsh_timer = 0.1f;
            }
        }

        m_lerp_color = ui_text_.color;

        switch (alphaValue_)
        {
            case AlphaValue.Increase:

                m_lerp_color.a = Mathf.Lerp(0.0f, 1.0f, t / m_flsh_timer);
                if (t >= m_flsh_timer)
                {
                    t = 0f;
                    alphaValue_ = AlphaValue.Decrease;
                }

                break;

            case AlphaValue.Decrease:

                m_lerp_color.a = Mathf.Lerp(1.0f, 0.0f, t / m_flsh_timer);
                if (t >= m_flsh_timer)
                {
                    t = 0f;
                    alphaValue_ = AlphaValue.Increase;
                }

                break;
        }

        ui_text_.color = m_lerp_color;
    }
}
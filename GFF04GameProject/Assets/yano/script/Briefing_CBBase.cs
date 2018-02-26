using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Briefing_CBBase : MonoBehaviour
{
    [SerializeField]
    private GameObject cb_text_;

    private RectTransform rect_;

    private float m_alpha;
    private float m_flashingTime;
    private float t0, t1;

    private int m_flashingCnt;

    // Use this for initialization
    void Start()
    {
        cb_text_.SetActive(false);
        rect_ = GetComponent<RectTransform>();

        m_alpha = 0f;
        m_flashingTime = 1f;
        t0 = 0f;
        t1 = 0f;
        m_flashingCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(1f, 1f, 1f, m_alpha);
    }

    public void FlashingBase()
    {
        if (m_flashingTime <= 0f)
        {
            m_alpha = (m_alpha == 0f) ? 0.6f : 0f;

            m_flashingCnt++;
            m_flashingTime = 1f;
            if (m_flashingCnt >= 5)
                m_alpha = 1f;

            if (m_alpha >= 0.6f)
                GetComponents<AudioSource>()[0].PlayOneShot(GetComponents<AudioSource>()[0].clip);
        }

        if (m_flashingCnt <= 4)
            m_flashingTime -= 12.0f * Time.deltaTime;
    }

    public void FeadOutBase()
    {
        if (m_flashingCnt >= 5)
        {
            cb_text_.SetActive(true);
            if (t0 >= 2f)
            {
                rect_.localScale =
                    Vector3.Lerp(new Vector3(4f, 0.5f, 1f), new Vector3(0f, 0.5f, 1f), t1 / 1f);
                cb_text_.GetComponent<RectTransform>().localScale =
                    Vector3.Lerp(Vector3.one, new Vector3(0f, 1f, 1f), t1 - 4f / 1f);

                if (t1 >= 7f)
                    gameObject.SetActive(false);

                t1 += 8.0f * Time.deltaTime;

                if (!GetComponents<AudioSource>()[1].isPlaying)
                    GetComponents<AudioSource>()[1].PlayOneShot(GetComponents<AudioSource>()[1].clip);
            }
            t0 += 4.0f * Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missionComp : MonoBehaviour
{
    private RectTransform m_rect;

    [SerializeField]
    private float t, t2;

    [SerializeField]
    private Vector3 m_scale;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        m_rect = GetComponent<RectTransform>();

        t = 0f;
        t2 = 0f;

        m_scale = Vector3.zero;

        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Image>().enabled)
        {
            m_scale.z = 1f;
            m_scale.x = Mathf.Lerp(0f, 5f, t / 0.3f);
            if (t >= 0.2f)
            {
                m_scale.y = Mathf.Lerp(0f, 4f, t2 / 0.2f);
                t2 += 1.0f * Time.deltaTime;
                if (!isClear)
                {
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                    isClear = true;
                }
            }

            m_rect.localScale = m_scale;

            t += 1.0f * Time.deltaTime;

            
        }

        else
        {
            m_scale = Vector3.zero;
            m_rect.localScale = Vector3.zero;
            t = 0f;
            t2 = 0f;
            isClear = false;
        }
    }
}

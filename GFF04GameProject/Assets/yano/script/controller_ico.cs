using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller_ico : MonoBehaviour
{
    private Image ico_;

    private float m_intervalTimer;

    // Use this for initialization
    void Start()
    {
        ico_ = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        m_intervalTimer += 1.0f * Time.deltaTime;

        if (m_intervalTimer >= 0.6f)
        {
            ico_.enabled = !ico_.enabled;
            m_intervalTimer = 0f;
        }
    }
}

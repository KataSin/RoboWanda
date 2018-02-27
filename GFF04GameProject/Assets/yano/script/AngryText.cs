using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngryText : MonoBehaviour
{
    private int m_randValue;

    private float m_activeTimer;

    // Use this for initialization
    void Start()
    {
        m_randValue = 0;
        GetComponent<Text>().enabled = false;
        m_activeTimer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Text>().enabled)
        {
            if (m_activeTimer <= 0f)
                GetComponent<Text>().enabled = false;

            m_activeTimer -= 1.0f * Time.deltaTime;
        }
    }

    public void AngryCall()
    {
        GetComponent<Text>().enabled = true;

        m_randValue = Random.Range(0, 2);

        if (m_randValue == 0)
        {
            transform.localPosition = new Vector3(-247f, -238f, 0f);
            GetComponent<Text>().text =
                "どこを狙っている！お前にはそこが建物に見えるのか！";
        }
        else if (m_randValue == 1)
        {
            transform.localPosition = new Vector3(-78f, -238f, 0f);
            GetComponent<Text>().text =
                "ちゃんと建物を狙え！";
        }

        m_activeTimer = 2f;
    }
}

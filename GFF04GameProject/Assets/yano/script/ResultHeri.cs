using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultHeri : MonoBehaviour
{
    private float t;

    private float m_speed;

    // Use this for initialization
    void Start()
    {
        m_speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FinishHeriMove()
    {
        t += 1.0f * Time.deltaTime;
        m_speed += 1.0f * Time.deltaTime;
        if (m_speed >= 5f)
            m_speed = 5f;

        transform.rotation = Quaternion.Slerp(Quaternion.Euler(0f, -38.685f, 0f), Quaternion.Euler(0f, -145, 0f), t / 3f);
        transform.position -= (transform.forward * m_speed) / 10f;
    }
}

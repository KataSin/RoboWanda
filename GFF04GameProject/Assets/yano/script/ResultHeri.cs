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
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void FinishHeriMove()
    {
        m_speed += 1.0f * Time.deltaTime;
        if (m_speed >= 5f)
            m_speed = 5f;

        if (m_speed >= 1f)
        {
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(0f, -38.685f, 0f), Quaternion.Euler(-10f, -145, -20f), t / 5f);
            t += 1.0f * Time.deltaTime;
        }
        transform.position -= (transform.forward * m_speed) / 10f;
    }

    public float Get_Speed()
    {
        return m_speed;
    }

    public float Get_T()
    {
        return t;
    }
}

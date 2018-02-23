using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleHeri : MonoBehaviour
{
    private Transform m_origin;

    [SerializeField]
    private float t;

    private float m_rotationTime;

    private float m_speed;

    private float m_speed_dampinf;

    // Use this for initialization
    void Start()
    {
        m_origin = transform;

        m_rotationTime = 5.2f;

        m_speed = 0f;

        m_speed_dampinf = 8f;

        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void TitleHeriMove()
    {
        t += 1.0f * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(Quaternion.Euler(0f, 19.624f, 0f), Quaternion.Euler(-2f, 100f, 20f), t / m_rotationTime);

        m_speed += 1f * Time.deltaTime;

        transform.position -= (transform.forward * m_speed) / m_speed_dampinf;
    }
}

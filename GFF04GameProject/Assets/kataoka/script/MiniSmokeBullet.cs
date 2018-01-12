using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSmokeBullet : MonoBehaviour
{
    public float m_lifeTime;
    private float m_Time;
    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= m_lifeTime)
        {
            Destroy(gameObject);
        }
    }
}

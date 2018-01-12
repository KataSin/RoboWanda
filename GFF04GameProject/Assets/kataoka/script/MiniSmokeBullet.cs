using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSmokeBullet : MonoBehaviour
{
    public float m_lifeTime;
    private float m_Time;

    private ParticleSystem m_Ps;
    // Use this for initialization
    void Start()
    {
        m_Ps = transform.Find("SmokeBom").GetComponent<ParticleSystem>();
        m_Time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= m_lifeTime)
        {
            var e=m_Ps.emission;
            e.enabled = false;
            if (m_Ps.particleCount <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMissileObj : MonoBehaviour
{
    public bool m_FiringMissileFlag;

    public GameObject m_ExprosionEffect;

    public GameObject m_FireEffect;

    private AudioSource missile_se_;
    private bool isClear;

    private Vector3 m_Vec;
    // Use this for initialization
    void Start()
    {
        m_FiringMissileFlag = false;
        m_Vec = Vector3.zero;

        missile_se_ = GetComponent<AudioSource>();
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FiringMissileFlag)
        {
            m_FireEffect.SetActive(true);
            transform.parent = null;
            transform.position += m_Vec * 40.0f * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(m_Vec) * Quaternion.Euler(0, 90, 0);
            if (!isClear)
                missile_se_.Play();
            isClear = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Instantiate(m_ExprosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    /// <summary>
    /// ミサイルを打つ
    /// </summary>
    public void FiringMissile()
    {
        m_FiringMissileFlag = true;
    }
    /// <summary>
    /// ミサイルを打つ方向
    /// </summary>
    /// <param name="vec"></param>
    public void MissileVec(Vector3 vec)
    {
        m_Vec = vec;
    }
}

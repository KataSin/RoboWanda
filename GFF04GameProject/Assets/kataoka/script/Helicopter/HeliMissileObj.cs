using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMissileObj : MonoBehaviour
{
    public bool m_FiringMissileFlag;

    public GameObject m_ExprosionEffect;

    public GameObject m_FireEffect;

    private Vector3 m_Vec;

    private float m_SpawnTime;

    [SerializeField]
    private AudioClip missile_se_;

    // Use this for initialization
    void Start()
    {
        m_FiringMissileFlag = false;
        m_Vec = Vector3.zero;
        m_SpawnTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FiringMissileFlag)
        {
            m_SpawnTime += Time.deltaTime;
            m_FireEffect.SetActive(true);
            transform.parent = null;
            transform.position += m_Vec * 40.0f * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(m_Vec) * Quaternion.Euler(0, 90, 0);

            GetComponent<AudioSource>().PlayOneShot(missile_se_);
        }

        if (m_SpawnTime >= 6.0f) Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Helicopter" || other.tag == "SandSmoke" || other.tag == "ExplosionCollision") return;
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

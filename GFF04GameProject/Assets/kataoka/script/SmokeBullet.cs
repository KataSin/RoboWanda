using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBullet : MonoBehaviour
{
    public GameObject m_MiniSmokeBullet;

    private Rigidbody m_rb;

    private ParticleSystem m_Ps;
    private bool m_IsExprosion;
    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_Ps = transform.Find("SmokeBom").GetComponent<ParticleSystem>();
        m_IsExprosion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsExprosion)
        {
            if (m_Ps.particleCount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Ground" || other.tag == "TowerCollision") && !m_IsExprosion)
        {
            //ベクトルを取る
            List<Vector3> vecs = new List<Vector3>();
            Vector3 velo = m_rb.velocity;
            Vector3 front = Vector3.Cross(velo, Vector3.left).normalized;
            Vector3 left = Vector3.Cross(velo, front).normalized;
            //left
            vecs.Add(left);
            //right
            vecs.Add(-left);
            //front
            vecs.Add(front);
            //back
            vecs.Add(-front);
            foreach (var i in vecs)
            {
                GameObject miniBullet = Instantiate(m_MiniSmokeBullet, (transform.position + i * 0.1f) - (velo.normalized), Quaternion.identity);
                miniBullet.GetComponent<Rigidbody>().AddForce(i * 500.0f);
            }
            var e = m_Ps.GetComponent<ParticleSystem>().emission;
            e.enabled = false;

            m_IsExprosion = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamExplosion : MonoBehaviour
{
    [SerializeField, Tooltip("爆発エフェクト")]
    public GameObject m_Exprosion;
    //爆発あたり判定の時間
    private float m_Timer;
    // Use this for initialization
    void Start()
    {
        m_Timer = 0.0f;
        Instantiate(m_Exprosion, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= 0.5f)
        {
            Destroy(gameObject.GetComponent<SphereCollider>());
        }
        if (m_Timer >= 4.0f)
        {
            Destroy(gameObject);
        }
    }
}

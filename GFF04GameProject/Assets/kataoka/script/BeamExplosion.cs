using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamExplosion : MonoBehaviour
{
    [SerializeField, Tooltip("爆発エフェクト")]
    public GameObject m_Exprosion;
    [SerializeField, Tooltip("爆発の遅延時間")]
    public float m_ExprosionDelay;
    //爆発あたり判定の時間
    private float m_Timer;
    //爆発フラグ
    private bool m_ExprosionFlag;
    // Use this for initialization
    void Start()
    {
        m_Timer = 0.0f;
        m_ExprosionFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_ExprosionDelay&&m_ExprosionFlag)
        {
            Instantiate(m_Exprosion, transform.position, Quaternion.identity);
            gameObject.AddComponent<SphereCollider>();
            m_ExprosionFlag = false;
        }

        if (m_Timer >= m_ExprosionDelay + 0.5f)
        {
            Destroy(gameObject);
        }



    }
}

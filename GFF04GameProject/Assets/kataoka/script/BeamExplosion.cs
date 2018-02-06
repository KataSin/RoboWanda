using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamExplosion : MonoBehaviour
{
    [SerializeField, Tooltip("爆発エフェクト")]
    public GameObject m_Exprosion;
    //爆発あたり判定の時間
    private float m_Timer;
    //爆発フラグ
    private bool m_ExprosionFlag;
    // Use this for initialization
    void Start()
    {
        m_Timer = 0.0f;
        m_ExprosionFlag = true;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ExprosionFlag)
        {
            Instantiate(m_Exprosion, transform.position, Quaternion.identity);
            //gameObject.AddComponent<SphereCollider>();
            //gameObject.GetComponent<SphereCollider>().isTrigger = true;
            m_ExprosionFlag = false;
        }

        if (m_Timer >= 0.5f)
        {
            Destroy(gameObject);
        }

        m_Timer += Time.deltaTime;
    }
}

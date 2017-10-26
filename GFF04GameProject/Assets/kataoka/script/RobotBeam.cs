using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBeam : MonoBehaviour
{
    //ベクトル
    private Vector3 m_Vec;
    [SerializeField, Tooltip("爆発エフェクト")]
    public GameObject m_BonEffect;
    // Use this for initialization
    void Start()
    {
        m_Vec = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(m_Vec);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_Vec * 200.0f * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        Instantiate(m_BonEffect, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }


}

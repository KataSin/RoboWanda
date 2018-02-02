﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField, Tooltip("爆発エフェクト")]
    public GameObject m_Exprosion;

    //プレイヤー
    private GameObject m_Player;
    //時間
    private float m_Time;
    //ベクトル
    private Vector3 m_Vec;
    //頂点の位置
    private Vector3 m_VertexPos;
    //発射される位置
    private Vector3 m_SpawnPos;
    //スポーンから頂点のベクトル
    private Vector3 m_SpawnToVertexVec;
    //初期化用フラグ
    private bool m_InitializeFlag;
    //球面補間用
    private Quaternion m_StartRotate;
    private Quaternion m_EndRotate;
    private float m_LerpTime;

    private GameObject m_OneFire;
    private GameObject m_TowFire;
    //ベクトル
    private Vector3 m_PreVec;

    private float m_LifeTime;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Time = 0.0f;
        m_LerpTime = 0.0f;
        //頂点座標は
        m_VertexPos = ((transform.position + m_Player.transform.position) / 2.0f) +
            new Vector3(Random.Range(-5, 5), 0.0f, Random.Range(-5, 5));
        m_VertexPos.y = 60.0f + Random.Range(-10, 10);

        m_SpawnPos = transform.position;
        m_SpawnToVertexVec = m_VertexPos - m_SpawnPos;


        m_OneFire = transform.Find("Smoke").gameObject;
        m_TowFire = transform.Find("Fire_Two").gameObject;

        m_OneFire.SetActive(true);


        m_StartRotate = Quaternion.identity;
        m_EndRotate = Quaternion.identity;
        m_InitializeFlag = true;

        m_LifeTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Time >= 5.0f) Destroy(gameObject);

        m_Time += Time.deltaTime;
        float speedOne = 0.8f;
        float speedTwo = 1.0f;
        float InitilizeVec = Mathf.Sqrt((m_VertexPos.y - m_SpawnPos.y) * 2.0f * 9.8f);
        float vertexTime = InitilizeVec / 9.8f;


        if (vertexTime <= m_Time * speedOne)
        {
            if (m_InitializeFlag)
            {
                m_OneFire.SetActive(false);
                m_TowFire.SetActive(true);

                m_InitializeFlag = false;
                m_StartRotate = transform.rotation;
                Vector3 randPos = m_Player.transform.position + new Vector3(Random.Range(-10, 10), 0.0f, Random.Range(-10, 10));
                m_EndRotate = Quaternion.LookRotation(randPos - transform.position);
            }
            m_LerpTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(m_StartRotate, m_EndRotate, m_LerpTime);

            if (m_LerpTime >= 1.0f)
            {
                transform.position += transform.forward * 100.0f * speedTwo * Time.deltaTime;
            }
            return;
        }

        m_Vec = new Vector3(
            m_SpawnToVertexVec.x / vertexTime * speedOne,
            0.0f,
            m_SpawnToVertexVec.z / vertexTime * speedOne
            );
        transform.position = new Vector3(
            transform.position.x,
            InitilizeVec * m_Time * speedOne - 9.8f / 2.0f * Mathf.Pow(m_Time * speedOne, 2) + m_SpawnPos.y,
            transform.position.z);
        transform.position += m_Vec * Time.deltaTime;

        //フレームの差でベクトルを求める
        m_PreVec = transform.position - m_PreVec;
        transform.rotation = Quaternion.LookRotation(m_PreVec);
        m_PreVec = transform.position;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Ground"||other.tag=="TowerCollision"||other.tag== "GroundCollisionRigid")
        {
            Instantiate(m_Exprosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "TowerCollision" || collision.collider.tag == "GroundCollisionRigid")
        {
            Instantiate(m_Exprosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "TowerCollision" || other.tag == "GroundCollisionRigid")
        {
            Instantiate(m_Exprosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }



}

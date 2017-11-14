using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Time = 0.0f;
        m_LerpTime = 0.0f;
        //頂点座標は
        m_VertexPos = ((transform.position + m_Player.transform.position) / 2.0f) +
            new Vector3(Random.Range(-30, 30), 0.0f, Random.Range(-30, 30));
        m_VertexPos.y = 300.0f + Random.Range(-50, 50);

        m_SpawnPos = transform.position;
        m_SpawnToVertexVec = m_VertexPos - m_SpawnPos;


        m_OneFire = transform.Find("Fire_One").gameObject;
        m_TowFire = transform.Find("Fire_Two").gameObject;

        m_OneFire.SetActive(true);


        m_StartRotate = Quaternion.identity;
        m_EndRotate = Quaternion.identity;
        m_InitializeFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        float speed = 2.0f;
        float InitilizeVec = Mathf.Sqrt((m_VertexPos.y - m_SpawnPos.y) * 2.0f * 9.8f);
        float vertexTime = InitilizeVec / 9.8f;


        if (vertexTime <= m_Time * speed)
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
                transform.position += transform.forward * 100.0f * speed * Time.deltaTime;
            }
            return;
        }

        m_Vec = new Vector3(
            m_SpawnToVertexVec.x / vertexTime * speed,
            0.0f,
            m_SpawnToVertexVec.z / vertexTime * speed
            );
        transform.position = new Vector3(
            transform.position.x,
            InitilizeVec * m_Time * speed - 9.8f / 2.0f * Mathf.Pow(m_Time * speed, 2) + m_SpawnPos.y,
            transform.position.z);
        transform.position += m_Vec * Time.deltaTime;

        //フレームの差でベクトルを求める
        m_PreVec = transform.position - m_PreVec;
        transform.rotation = Quaternion.LookRotation(m_PreVec);
        m_PreVec = transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Missile" && other.tag != "ExplosionCollision")
        {
            Instantiate(m_Exprosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }



}

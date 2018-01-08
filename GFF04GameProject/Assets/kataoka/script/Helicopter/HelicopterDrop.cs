using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterDrop : MonoBehaviour
{
    //壊れたかどうか
    public bool m_IsBreak;
    //ヘリコプターの速度
    private Vector3 m_Velo;
    //ヘリコプターのベクトル
    private Vector3 m_Vec;
    //ヘリコプターセーブ
    private Vector3 m_SevePos;
    //プロペラ
    public GameObject m_Propeller;
    //火
    private GameObject m_FireEffect;

    public GameObject m_ItemBox;
    //壊れるヘリ
    public GameObject m_HeliBreakPrefab;
    private GameObject m_Robot;

    private bool m_IsDrop;

    //投下ポイント
    private Vector3 m_DropPoint;
    private Vector3 m_DropVec;
    //地点に到着したかどうか
    private bool m_IsDropArrival;

    private Vector3 m_ResPos;
    private Vector3 m_Pos;
    private Vector3 m_SpringVelo;


    // Use this for initialization
    void Start()
    {
        m_Velo = Vector3.zero;
        m_SevePos = transform.position;
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
        m_FireEffect = transform.Find("FireEffect").gameObject;
        m_FireEffect.SetActive(false);

        m_ResPos = transform.position;
        m_Pos = transform.position;
        m_SpringVelo = Vector3.zero;

        m_IsBreak = false;

        m_IsDrop = false;
        m_IsDropArrival = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsBreak)
        {
            m_FireEffect.SetActive(true);
            transform.Rotate(new Vector3(0, 1, 0.4f), 10.0f);
            m_Velo.y = -4.0f;
            transform.position += m_Velo * 10.0f * Time.deltaTime;
            return;
        }
        //着地地点についているか
        m_IsDropArrival = Vector3.Distance(transform.position, m_DropPoint) <= 3.0f;

        //ヘリコプターの回転処理
        m_Velo.y = 0.0f;
        Vector3 rotateVec = Vector3.Cross(Vector3.up, m_Velo);
        transform.rotation =
        Quaternion.AngleAxis(m_Velo.magnitude * 50.0f, rotateVec) *
        Quaternion.LookRotation(m_DropVec);

        m_Propeller.transform.localEulerAngles += new Vector3(0, 0, 1000) * Time.deltaTime;

        if (!m_IsDrop)
        {
            m_ResPos = m_DropPoint;
        }
        else
        {
            m_ResPos += 5.0f * m_DropVec * Time.deltaTime;
        }

        Spring(m_ResPos, ref m_Pos, ref m_Velo, 0.04f, 0.2f, 2.0f);
        transform.position = m_Pos;

    }

    public void LateUpdate()
    {
        m_Velo = transform.position - m_SevePos;
        m_SevePos = transform.position;
    }

    /// <summary>
    /// 透過地点
    /// </summary>
    /// <param name="point"></param>
    public void SetPoint(Vector3 point)
    {
        m_DropPoint = new Vector3(point.x, transform.position.y, point.z);
        m_DropVec = (m_DropPoint - transform.position).normalized;
    }

    /// <summary>
    /// バネ補間をする
    /// </summary>
    /// <param name="resPos">行きたい</param>
    /// <param name="pos">現在</param>
    /// <param name="velo">速度</param>
    /// <param name="stiffness">なんか</param>
    /// <param name="friction">なんか</param>
    /// <param name="mass">重さ</param>
    private void Spring(Vector3 resPos, ref Vector3 pos, ref Vector3 velo, float stiffness, float friction, float mass)
    {
        // バネの伸び具合を計算
        Vector3 stretch = (pos - resPos);
        // バネの力を計算
        Vector3 force = -stiffness * stretch;
        // 加速度を追加
        Vector3 acceleration = force / mass;
        // 移動速度を計算
        velo = friction * (velo + acceleration);
        // 座標の更新
        pos += velo;
    }

    public void SetDrop(bool flag)
    {
        m_IsDrop = flag;
    }
    public void DropBox()
    {
        Instantiate(m_ItemBox, transform.position - new Vector3(0, 2, 0), Quaternion.identity);
    }
    /// <summary>
    /// 投下地点についているか
    /// </summary>
    /// <returns></returns>
    public bool GetDropPoint()
    {
        return m_IsDropArrival;
    }
}

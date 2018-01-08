using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterTank : MonoBehaviour
{
    public GameObject m_LeftPro;
    public GameObject m_RightPro;
    public GameObject m_FireEffect;
    public GameObject m_BreakHeli;
    //行くべきタンクポイント
    private GameObject m_TankPoint;

    private Vector3 m_ResPos;
    private Vector3 m_Pos;
    private Vector3 m_Velo;

    private Vector3 m_PointVec;

    private float m_PosY;

    //スピード重視
    private bool m_DropPointDisFlag;
    private bool m_ReturnFlag;
    private bool m_FrontFlag;

    //着いてからの時間
    private float m_ArrivalTime;


    private bool m_IsBreak;
    // Use this for initialization
    void Start()
    {
        m_Velo = Vector3.zero;
        m_Pos = transform.position;

        m_DropPointDisFlag = false;
        m_ReturnFlag = false;
        m_FrontFlag = false;
        m_PosY = transform.position.y;

        m_ArrivalTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_LeftPro.transform.Rotate(new Vector3(0.0f, 50.0f, 0.0f));
        m_RightPro.transform.Rotate(new Vector3(0.0f, -50.0f, 0.0f));

        if (m_IsBreak)
        {
            transform.position += new Vector3(0, -5, 0) * Time.deltaTime;
            transform.Rotate(new Vector3(0.2f, 0.8f, 0.0f), 3.0f);
            m_FireEffect.SetActive(true);
            transform.Find("HelicopterTank").Find("RopeJoint").GetComponent<HeliRope>().TankDestroy();
            return;
        }


        Vector3 pointPos = new Vector3(m_TankPoint.transform.position.x, m_PosY, m_TankPoint.transform.position.z);
        m_ResPos = pointPos;

        //これはやばい
        if (Vector3.Distance(transform.position, pointPos) < 9.0f)
        {
            if (m_TankPoint.GetComponent<StrategyTankPoint>().GetTankDownFlag())
                m_DropPointDisFlag = true;
            else
            {
                //到着してから一定時間で壊れる
                m_ArrivalTime += Time.deltaTime;
                if (m_ArrivalTime >= 1.0f)
                {
                    m_IsBreak = true;
                    return;
                }
            }
        }
        if (m_DropPointDisFlag)
        {
            Vector3 point = m_TankPoint.transform.position + new Vector3(0, 3, 0);
            m_ResPos = point;
            if (Vector3.Distance(point, transform.position) < 2.0f)
            {
                transform.Find("HelicopterTank").Find("RopeJoint").GetComponent<HeliRope>().JointFree();
                m_ReturnFlag = true;
            }
        }
        if (m_ReturnFlag)
        {
            m_ResPos = pointPos;
            if (Vector3.Distance(transform.position, pointPos) < 10.0f)
                m_FrontFlag = true;
        }
        if (m_FrontFlag)
        {
            m_ResPos = pointPos + m_PointVec * 2.5f;
        }

        Spring(m_ResPos, ref m_Pos, ref m_Velo, 0.04f, 0.2f, 5.0f);

        transform.rotation = Quaternion.LookRotation(m_PointVec);
        transform.position = m_Pos;
    }

    public void SetPoint(GameObject point)
    {
        m_TankPoint = point;
        m_PointVec = point.transform.position - transform.position;
        m_PointVec.y = 0.0f;
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

    public void OnCollisionEnter(Collision collision)
    {
        if (m_IsBreak)
        {
            Instantiate(m_BreakHeli, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (m_IsBreak)
        {
            Instantiate(m_BreakHeli, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

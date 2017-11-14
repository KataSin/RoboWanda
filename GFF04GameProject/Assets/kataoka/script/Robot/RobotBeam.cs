using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBeam : MonoBehaviour
{
    //ベクトル
    private Vector3 m_Vec;
    [SerializeField, Tooltip("爆発エフェクト")]
    public GameObject m_BonEffect;
    //ラインレンダラー
    private LineRenderer m_LineRenderer;
    //Lineのスタートする場所
    private Vector3 m_LineStartPos;
    //Lineの終わる場所
    private Vector3 m_LineEndPos;

    //フレーム数
    private int m_Frame;

    //撃っているか
    private bool m_IsBeamFlag;
    //距離
    private float m_BeamLen;
    // Use this for initialization
    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_BeamLen = 0.0f;
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, transform.position);

        m_Frame = 0;
        m_IsBeamFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsBeamFlag)
        {
            m_BeamLen += 2000.0f * Time.deltaTime;
        }
        else
        {
            m_BeamLen -= 2000.0f * Time.deltaTime;
        }

        //ビームクランプ
        m_BeamLen = Mathf.Clamp(m_BeamLen, 0.0f, 1000.0f);


        Vector3 vec = transform.position + transform.forward * m_BeamLen;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, vec);

        //ビームのあたり判定
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int layer = ~(1 << 10);
        if (Physics.Raycast(ray, out hit, m_BeamLen, layer))
        {
            m_Frame++;
            if (m_Frame % 3 == 0)
                Instantiate(m_BonEffect, hit.point, Quaternion.identity);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
    }
    /// <summary>
    /// ビーム撃つかどうか
    /// </summary>
    /// <param name="flag">true:撃つfalse:撃たない</param>
    public void SetBeamFlag(bool flag)
    {
        m_IsBeamFlag = flag;
    }
}

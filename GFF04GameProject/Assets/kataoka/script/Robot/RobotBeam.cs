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
    //ビームエフェクト
    private BeamParam m_BeamParam;
    //フレーム数
    private int m_Frame;

    //撃っているか
    private bool m_IsBeamFlag;
    //距離
    private float m_BeamLen;

    //ビームの太さ
    private float m_BeamScale;
    //ビームの長さ
    private float m_BeamLength;
    // Use this for initialization
    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_BeamLen = 1000.0f;
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, transform.position);

        m_BeamParam = GetComponent<BeamParam>();

        m_Frame = 0;
        m_IsBeamFlag = false;
        m_BeamScale = 0.0f;
        m_BeamLength = 0.0f;

        m_BeamParam.Scale = m_BeamScale;
        m_BeamParam.MaxLength = m_BeamLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsBeamFlag)
        {
            m_BeamLength += 300.0f * Time.deltaTime;
            m_BeamScale += 3.0f * Time.deltaTime;
            m_BeamLen = 1000.0f;
        }
        else
        {
            m_BeamScale -= 2.0f * Time.deltaTime;
            m_BeamLen = 0.0f;
            if (m_BeamScale <= 0.0f)
            {
                m_BeamLength = 0.0f;
                m_IsBeamFlag = true;
                gameObject.SetActive(false);

            }
        }
        //ビームのあたり判定
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int layer = ~(1 << 10 | 1 << 15);
        if (Physics.Raycast(ray, out hit, m_BeamLen, layer))
        {
            m_Frame++;
            if (m_Frame % 3 == 0)
            {
                Instantiate(m_BonEffect, hit.point, Quaternion.identity);
            }
        }
        m_BeamLength = Mathf.Clamp(m_BeamLength, 0.0f, 1000.0f);
        m_BeamScale = Mathf.Clamp(m_BeamScale, 0.0f, 3.0f);
        m_BeamParam.Scale = m_BeamScale;
        m_BeamParam.MaxLength = m_BeamLength;
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

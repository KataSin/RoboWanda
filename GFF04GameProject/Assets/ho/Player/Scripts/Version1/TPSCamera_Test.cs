using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：TPSカメラ位置制御
/// 製作者：Ho Siu Ki（何　兆祺）
/// </summary> 

public class TPSCamera_Test : MonoBehaviour
{
    [SerializeField]
    private float m_YPosition_Normal;   // 普段のy軸位置
    [SerializeField]
    private float m_ZPosition_Normal;   // 普段のz軸位置
    [SerializeField]
    private float m_YPosition_Bomb;     // 着弾点表示時のy軸位置
    [SerializeField]
    private float m_ZPosition_Bomb;     // 着弾点表示時のz軸位置
    [SerializeField]
    private float m_Speed;              // カメラ位置の移動速度

    GameObject m_Origin;                // 原点
    Vector3 forward;                    // 原点の正面向きベクトル

    CameraMode m_Mode;                  // カメラモード

    // Use this for initialization
    void Start()
    {
        m_Origin = gameObject.transform.parent.gameObject;
        m_Mode = CameraMode.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        // 原点の正面向きベクトルを取得
        forward = m_Origin.transform.forward;
        // y成分を無視する
        forward.y = 0;
        // 正規化する
        forward.Normalize();

        switch (m_Mode)
        {
            case CameraMode.Normal:
                Normal();
                break;
            case CameraMode.Bomb:
                Bomb();
                break;
            default:
                break;
        }

        if (Input.GetButton("Bomb_Hold"))
        {
            m_Mode = CameraMode.Bomb;
        }
        else
        {
            m_Mode = CameraMode.Normal;
        }
    }

    // 通常時の位置
    void Normal()
    {
        Vector3 NormalPosition = new Vector3(0.0f, m_YPosition_Normal, m_ZPosition_Normal);
        transform.localPosition = NormalPosition;
    }

    // 着弾点表示時の位置
    void Bomb()
    {
        Vector3 BombPosition = new Vector3(0.0f, m_YPosition_Bomb, m_ZPosition_Bomb);
        transform.localPosition = BombPosition;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：カメラ位置制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>
public class CameraPosition : MonoBehaviour
{
    // 相対位置を使用
    // 通常時の位置座標
    [SerializeField]
    private float m_NormalPositionX = 0.0f;     // 通常時のx軸位置
    [SerializeField]
    private float m_NormalPositionY = 2.5f;     // 通常時のy軸位置
    [SerializeField]
    private float m_NormalPositionZ = -5.0f;    // 通常時のz軸位置
    // 照準時の位置座標
    [SerializeField]
    private float m_AimingPositionX = 0.0f;     // 照準時のx軸位置
    [SerializeField]
    private float m_AimingPositionY = 2.5f;     // 照準時のy軸位置
    [SerializeField]
    private float m_AimingPositionZ = -5.0f;    // 照準時のz軸位置
    // 現在の位置座標
    float current_pos_X;                        // 現在のx軸座標
    float current_pos_Y;                        // 現在のy軸座標
    float current_pos_Z;                        // 現在のz軸座標

    [SerializeField]
    private float m_SpeedX = 0.0f;              // x軸座標の移動速度
    [SerializeField]
    private float m_SpeedY = 0.0f;              // y軸座標の移動速度
    [SerializeField]
    private float m_SpeedZ = 0.0f;              // z軸座標の移動速度

    private GameObject m_Player;                // プレイヤー
    private bool m_IsAiming = false;            // プレイヤーは照準状態であるか

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Player != null)
        {
            m_IsAiming = m_Player.GetComponent<PlayerController>().IsAiming();
        }

        current_pos_X = m_NormalPositionX;
        current_pos_Y = m_NormalPositionY;
        current_pos_Z = m_NormalPositionZ;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの状態を取得
        if (m_Player != null)
        {
            m_IsAiming = m_Player.GetComponent<PlayerController>().IsAiming();
        }

        // 照準状態であれば
        if (m_IsAiming)
        {
            // 照準時の座標まで移動
            if (current_pos_X < m_AimingPositionX)
            {
                current_pos_X += m_SpeedX * Time.deltaTime;
            }
            if (current_pos_Y > m_AimingPositionY)
            {
                current_pos_Y -= m_SpeedY * Time.deltaTime;
            }
            if (current_pos_Z < m_AimingPositionZ)
            {
                current_pos_Z += m_SpeedZ * Time.deltaTime;
            }
        }
        else
        {
            // 通常時の座標まで移動
            if (current_pos_X > m_NormalPositionX)
            {
                current_pos_X -= m_SpeedX * Time.deltaTime;
            }
            if (current_pos_Y < m_NormalPositionY)
            {
                current_pos_Y += m_SpeedY * Time.deltaTime;
            }
            if (current_pos_Z > m_NormalPositionZ)
            {
                current_pos_Z -= m_SpeedZ * Time.deltaTime;
            }
        }

        // カメラの座標制限
        current_pos_X = Mathf.Clamp(current_pos_X, m_NormalPositionX, m_AimingPositionX);
        current_pos_Y = Mathf.Clamp(current_pos_Y, m_AimingPositionY, m_NormalPositionY);
        current_pos_Z = Mathf.Clamp(current_pos_Z, m_NormalPositionZ, m_AimingPositionZ);

        // カメラの位置を更新（相対位置を使用）
        transform.localPosition = new Vector3(current_pos_X, current_pos_Y, current_pos_Z);
    }
}

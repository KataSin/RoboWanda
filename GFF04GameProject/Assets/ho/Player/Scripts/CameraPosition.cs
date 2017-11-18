using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：カメラ位置制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

// カメラ距離
enum CameraDistance
{
    VeryClose,  // とても近い
    Close,      // 近い
    Normal,     // 普通
    Far         // 遠い
}

public class CameraPosition : MonoBehaviour
{
    /*// 相対位置を使用
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

    [SerializeField]
    private float m_Speed = 0.0f;

    private GameObject m_Player;                // プレイヤー
    private bool m_IsAiming = false;            // プレイヤーは照準状態であるか*/

    // とても近い時の位置座標
    [SerializeField]
    private float m_VeryClosePositionX = 1.0f;
    [SerializeField]
    private float m_VeryClosePositionY = 1.9f;
    [SerializeField]
    private float m_VeryClosePositionZ = -2.0f;

    // 近い時の位置座標
    [SerializeField]
    private float m_ClosePositionX = 0.0f;
    [SerializeField]
    private float m_ClosePositionY = 2.5f;
    [SerializeField]
    private float m_ClosePositionZ = -3.0f;

    // 通常時の位置座標
    [SerializeField]
    private float m_NormalPositionX = 0.0f;
    [SerializeField]
    private float m_NormalPositionY = 2.5f;
    [SerializeField]
    private float m_NormalPositionZ = -5.0f;

    // 遠い時の位置座標
    [SerializeField]
    private float m_FarPositionX = 0.0f;
    [SerializeField]
    private float m_FarPositionY = 6.0f;
    [SerializeField]
    private float m_FarPositionZ = -15.0f;

    [SerializeField]
    private float m_Speed = 10.0f;

    private GameObject m_Player;                // プレイヤー
    private bool m_IsAiming = false;            // プレイヤーは照準状態であるか
    private Camera m_Camera;

    private CameraDistance m_Distance;          // カメラとプレイヤーの距離
    private int m_DistanceSelect;
    bool m_IsTriggered;                         // 十字キーの操作判定

    // Use this for initialization
    void Start()
    {
        /*m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Player != null)
        {
            m_IsAiming = m_Player.GetComponent<PlayerController>().IsAiming();
        }

        current_pos_X = m_NormalPositionX;
        current_pos_Y = m_NormalPositionY;
        current_pos_Z = m_NormalPositionZ;*/

        m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Player != null)
        {
            m_IsAiming = m_Player.GetComponent<PlayerController>().IsAiming();
        }

        m_Camera = Camera.main;
        m_DistanceSelect = 2;
        m_IsTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*// プレイヤーの状態を取得
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
        transform.localPosition = new Vector3(current_pos_X, current_pos_Y, current_pos_Z);*/

        // カメラの距離を変更
        switch (m_DistanceSelect)
        {
            case 0:
                m_Distance = CameraDistance.VeryClose;
                break;
            case 1:
                m_Distance = CameraDistance.Close;
                break;
            case 2:
                m_Distance = CameraDistance.Normal;
                break;
            case 3:
                m_Distance = CameraDistance.Far;
                break;
            default:
                m_Distance = CameraDistance.Normal;
                break;
        }

        // 十字キーの入力が無い場合、操作判定を解除
        if (Input.GetAxisRaw("Camera_Distance") == 0)
        {
            m_IsTriggered = false;
        }
        // 十字キーでカメラの距離を選択
        if (m_IsTriggered == false)
        {
            // 上ボタン
            if (Input.GetAxisRaw("Camera_Distance") < -0.9)
            {
                m_IsTriggered = true;
                m_DistanceSelect = m_DistanceSelect + 1;
            }
            // 下ボタン
            if (Input.GetAxisRaw("Camera_Distance") > 0.9)
            {
                m_IsTriggered = true;
                m_DistanceSelect = m_DistanceSelect - 1;
            }
        }
        m_DistanceSelect = Mathf.Clamp(m_DistanceSelect, 0, 3);

        // カメラの座標を変更
        Vector3 new_position;
        switch (m_Distance)
        {
            case CameraDistance.VeryClose:
                new_position.x = m_VeryClosePositionX;
                new_position.y = m_VeryClosePositionY;
                new_position.z = m_VeryClosePositionZ;
                break;
            case CameraDistance.Close:
                new_position.x = m_ClosePositionX;
                new_position.y = m_ClosePositionY;
                new_position.z = m_ClosePositionZ;
                break;
            case CameraDistance.Normal:
                new_position.x = m_NormalPositionX;
                new_position.y = m_NormalPositionY;
                new_position.z = m_NormalPositionZ;
                break;
            case CameraDistance.Far:
                new_position.x = m_FarPositionX;
                new_position.y = m_FarPositionY;
                new_position.z = m_FarPositionZ;
                break;
            default:
                new_position.x = m_NormalPositionX;
                new_position.y = m_NormalPositionY;
                new_position.z = m_NormalPositionZ;
                break;
        }
        // カメラの位置を更新（相対座標を使用）
        transform.localPosition = Vector3.Lerp(transform.localPosition, new_position, m_Speed * Time.deltaTime);
        // transform.localPosition = Vector3.Slerp(transform.position, new_position, m_Speed * Time.deltaTime);
        // transform.localPosition = new Vector3(new_position.x, new_position.y, new_position.z);

        // カメラがフィールドや他のオブジェクトに透過しないようにする

    }
}
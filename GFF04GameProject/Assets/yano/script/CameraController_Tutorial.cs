using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：カメラ支点制御（正式バージョン）
/// 製作者：Ho Siu Ki（何　兆祺）
/// </summary>

public class CameraController_Tutorial : MonoBehaviour
{
    enum T_PlayerCameraMode
    {
        Normal,     // 通常
        Event,       // イベント
    }

    [SerializeField]
    private float m_RotateSpeedY = 180.0f;  // y軸回転速度
    [SerializeField]
    private float m_RotateSpeedX = 90.0f;  // x軸回転速度

    float pitch = 0.0f;                     // 仰角
    [SerializeField]
    private float m_PitchMax = 30.0f;       // 仰角最大値
    [SerializeField]
    private float m_PitchMin = -40.0f;      // 仰角最小値

    Vector3 offset;                         // プレイヤーとカメラ間のオフセット距離
    GameObject m_Player;                    // プレイヤーオブジェクト

    [SerializeField]
    private T_PlayerCameraMode mode_;

    [SerializeField]
    private GameObject cameraPos_;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");

        if (m_Player != null)
        {
            offset = transform.position - m_Player.transform.position;
        }

        mode_ = T_PlayerCameraMode.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Player != null)
        {
            //CheckMode();

            if (mode_ == T_PlayerCameraMode.Normal)
            {
                if (!m_Player.GetComponent<PlayerController_Tutorial>().IsDead())
                {
                    // 回転
                    // 方向入力を取得
                    float axisVertical = Input.GetAxisRaw("Vertical_R");        // x軸
                    float axisHorizontal = Input.GetAxisRaw("Horizontal_R");    // y軸

                    transform.Rotate(Vector3.up, axisHorizontal * Time.deltaTime * m_RotateSpeedY, Space.World);  // y軸回転

                    // x軸回転
                    pitch += axisVertical * Time.deltaTime * m_RotateSpeedX;
                    pitch = Mathf.Clamp(pitch, m_PitchMin, m_PitchMax);        // 角度を制限する

                    Vector3 rotation = transform.rotation.eulerAngles;
                    rotation.x = pitch;
                    transform.rotation = Quaternion.Euler(rotation);
                    rotation.z = 0.0f;
                }
            }
        }
    }

    void LateUpdate()
    {
        if (mode_ == T_PlayerCameraMode.Normal)
            transform.position = m_Player.transform.position + offset;
    }

    private void CheckMode()
    {
        //switch (cameraPos_.GetComponent<CameraPosition_Tutorial>().GetMode())
        //{
        //    case 1:
        //        mode_ = T_PlayerCameraMode.Normal;
        //        break;

        //    default:
        //        mode_ = T_PlayerCameraMode.Event;
        //        break;
        //}
    }
}
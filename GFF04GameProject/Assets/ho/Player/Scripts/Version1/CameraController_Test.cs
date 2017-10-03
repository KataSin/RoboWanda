using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：3Dアクションゲーム カメラ制御（追随式）
/// 製作者：Ho Siu Ki（何　兆祺）
/// </summary>

// カメラモード
enum CameraMode
{
    Normal,     // 通常
    Bomb        // 着弾点表示時
}

public class CameraController_Test : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;             // 追随目標
    [SerializeField]
    private float m_Speed = 3.0f;           // 機敏さ
    [SerializeField]
    private float m_RotateSpeed = 180.0f;   // 回転速度

    float pitch = 0.0f;                     // 仰角
    [SerializeField]
    private float m_PitchMax = 30.0f;       // 仰角最大値
    [SerializeField]
    private float m_PitchMin = -20.0f;      // 仰角最大値

    CameraMode m_Mode;                      // カメラモード

    // Use this for initialization
    void Start()
    {
        m_Mode = CameraMode.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーに追随して移動
        transform.position = Vector3.Lerp(transform.position, m_Target.position, m_Speed * Time.deltaTime);
        // transform.position = m_Target.position;

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

    // 通常時の挙動
    void Normal()
    {
        // 回転
        // 方向入力を取得
        float axisVertical = Input.GetAxisRaw("Vertical_R");        // x軸
        float axisHorizontal = Input.GetAxisRaw("Horizontal_R");    // y軸

        transform.Rotate(Vector3.up, axisHorizontal * Time.deltaTime * m_RotateSpeed, Space.World);  // y軸回転

        // x軸回転
        pitch += axisVertical * Time.deltaTime * m_RotateSpeed;
        pitch = Mathf.Clamp(pitch, m_PitchMin, m_PitchMax);        // 角度を制限する

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = pitch;
        transform.rotation = Quaternion.Euler(rotation);
        rotation.z = 0.0f;
    }

    // 着弾点表示時の挙動
    void Bomb()
    {
        // プレイヤーの背後に固定
        transform.rotation = Quaternion.Slerp(transform.rotation, m_Target.rotation, m_Speed * Time.deltaTime);

        // 回転
        // 方向入力を取得
        float axisVertical = Input.GetAxisRaw("Vertical_R");        // x軸
        float axisHorizontal = Input.GetAxisRaw("Horizontal_R");    // y軸

        transform.Rotate(Vector3.up, axisHorizontal * Time.deltaTime * m_RotateSpeed, Space.World);  // y軸回転

        // x軸回転
        pitch += axisVertical * Time.deltaTime * m_RotateSpeed;
        pitch = Mathf.Clamp(pitch, -m_PitchMax, m_PitchMax);        // 角度を制限する

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = pitch;
        transform.rotation = Quaternion.Euler(rotation);
        rotation.z = 0.0f;


    }
}

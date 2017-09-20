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
    Bomb        // 爆弾投擲
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
    private float m_PitchMax = 90.0f;       // 仰角最大値

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーに追随して移動
        transform.position = Vector3.Lerp(transform.position, m_Target.position, m_Speed * Time.deltaTime);

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

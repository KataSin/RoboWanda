﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：カメラ支点制御（α版）
/// 製作者：Ho Siu Ki（何　兆祺）
/// </summary>

public class CameraControllerAlpha : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;             // 追跡対象
    [SerializeField]
    private float m_RotateSpeed = 180.0f;   // 回転速度

    float pitch = 0.0f;                     // 仰角
    [SerializeField]
    private float m_PitchMax = 30.0f;       // 仰角最大値
    [SerializeField]
    private float m_PitchMin = -40.0f;      // 仰角最小値

    Vector3 offset;                         // プレイヤーとカメラ間のオフセット距離

    GameObject m_Player;                    // プレイヤー

    // Use this for initialization
    void Start()
    {
        if (m_Target != null)
        {
            offset = transform.position - m_Target.transform.position;
        }

        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが死亡していない場合、操作可能
        // if (m_Target != null)
        if (m_Player != null && !m_Player.GetComponent<PlayerControllerAlpha>().IsDead())
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
    }

    void LateUpdate()
    {
        if (m_Target != null)
        {
            //カメラの transform 位置をプレイヤーのものと等しく設定します。ただし、計算されたオフセット距離によるずれも加えます。
            transform.position = m_Target.transform.position + offset;
        }
    }
}

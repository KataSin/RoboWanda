using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：3Dアクションゲーム プレイヤー制御（移動慣性付き）
/// 製作者：Ho Siu Ki（何　兆祺）
/// </summary>
public class PlayerController_Test : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 4.0f;       // 移動速度
    [SerializeField]
    private float m_RotateSpeed = 4.0f; // 回転速度
    [SerializeField]
    private float m_AccelPower = 2.0f;  // 加速度（メートル/秒/秒）
    [SerializeField]
    private float m_BrakePower = 6.0f;  // ブレーキ速度（メートル/秒/秒）
    [SerializeField]
    private float m_Gravity = 18.0f;    // 重力加速度（メートル/秒/秒）

    float m_VelocityY = 0.0f;           // y軸方向の移動量
    float m_SpeedX = 0.0f;              // X軸速度（右はプラス、左はマイナス）
    float m_SpeedZ = 0.0f;              // Z軸速度（前進はプラス、後退はマイナス）

    CharacterController m_Controller;

    // Use this for initialization
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // カメラの正面向きのベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        // y成分を無視する
        forward.y = 0;
        // 正規化する
        forward.Normalize();

        // 方向入力を取得
        float axisHorizontal = Input.GetAxisRaw("Horizontal_L");    // x軸
        float axisVertical = Input.GetAxisRaw("Vertical_L");        // z軸

        // 減速する（入力が無い場合 or 進行方向と逆に入力がある場合）
        // X軸（左右キー）
        if ((axisHorizontal == 0) || (m_SpeedX * axisHorizontal < 0))
        {
            if (m_SpeedX > 0)
            {
                m_SpeedX = Mathf.Max(m_SpeedX - m_BrakePower * Time.deltaTime, 0);
            }
            else {
                m_SpeedX = Mathf.Min(m_SpeedX + m_BrakePower * Time.deltaTime, 0);
            }
        }
        // Z軸（上下キー）
        if ((axisVertical == 0) || (m_SpeedZ * axisVertical < 0))
        {
            if (m_SpeedZ > 0)
            {
                m_SpeedZ = Mathf.Max(m_SpeedZ - m_BrakePower * Time.deltaTime, 0);
            }
            else {
                m_SpeedZ = Mathf.Min(m_SpeedZ + m_BrakePower * Time.deltaTime, 0);
            }
        }

        // 左右キーで加速
        m_SpeedX +=
            m_AccelPower
            * axisHorizontal
            * Time.deltaTime;
        // 上下キーで加速
        m_SpeedZ +=
            m_AccelPower
            * axisVertical
            * Time.deltaTime;

        // 速度制限
        m_SpeedX = Mathf.Clamp(m_SpeedX, -m_Speed, m_Speed);
        m_SpeedZ = Mathf.Clamp(m_SpeedZ, -m_Speed, m_Speed);

        // 移動処理
        Vector3 velocity = forward * m_SpeedZ + Camera.main.transform.right * m_SpeedX;

        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // CharacterControllerに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // キャラクターの向きを進行方向に
        if (axisHorizontal != 0 || axisVertical != 0)
        {
            Vector3 direction = forward * axisVertical + Camera.main.transform.right * axisHorizontal;
            transform.localRotation = Quaternion.LookRotation(direction);
        }
    }
}

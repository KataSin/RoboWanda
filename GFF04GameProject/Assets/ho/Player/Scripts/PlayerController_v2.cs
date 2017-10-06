using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤー移動（慣性なし）
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>
public class PlayerController_v2 : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 15.0f;          // 移動速度
    [SerializeField]
    private float m_Gravity = 20.0f;        // 重力
    [SerializeField]
    private float m_RotateSpeed = 360.0f;   // 回転速度

    Vector3 velocity = Vector3.zero;        // 移動量
    float vY = 0;                           // y軸速度
    Vector3 m_PrevPosition;                 // 前回の位置（回転処理用）

    CharacterController m_Controller;
    PlayerState m_State;                    // プレイヤーの状態

    // Use this for initialization
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_PrevPosition = transform.position;
        m_State = PlayerState.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case PlayerState.Normal:
                Normal();
                break;
            case PlayerState.Bomb:
                Bomb();
                break;
            case PlayerState.Evasion:
                break;
            case PlayerState.Damage:
                break;
            default:
                break;
        }
    }

    // 通常時の挙動
    void Normal()
    {
        // 通常時の移動処理
        NormalMove();

        // RBキーを押しで着弾点を表示
        if (Input.GetButtonDown("Bomb_Hold"))
        {
            m_State = PlayerState.Bomb;
        }
    }

    // 通常時の移動処理
    void NormalMove()
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

        // 移動量を計算
        if (m_Controller.isGrounded)
        {
            velocity = forward * axisVertical * m_Speed + Camera.main.transform.right * axisHorizontal * m_Speed;
            vY = 0;
        }

        vY -= m_Gravity * Time.deltaTime;
        velocity.y = vY;

        // CharacterControllerに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // 移動方向に向ける
        Vector3 direction = transform.position - m_PrevPosition;

        if (direction.sqrMagnitude > 0)
        {
            Vector3 orientiation = Vector3.Slerp(transform.forward,
                new Vector3(direction.x, 0.0f, direction.z),
                m_RotateSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            transform.LookAt(transform.position + orientiation);
            m_PrevPosition = transform.position;
        }
    }

    // 着弾点表示時の挙動
    void Bomb()
    {
        // RBキーを放すと通常状態に戻る
        if (!Input.GetButton("Bomb_Hold"))
        {
            m_State = PlayerState.Normal;
        }

        // 着弾点表示時の移動処理
        BombMove();
    }

    // 着弾点表示時の移動処理
    void BombMove()
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

        // 移動量を計算
        if (m_Controller.isGrounded)
        {
            velocity = forward * axisVertical * m_Speed + Camera.main.transform.right * axisHorizontal * m_Speed;
            vY = 0;
        }

        vY -= m_Gravity * Time.deltaTime;
        velocity.y = vY;

        // CharacterControllerに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // カメラと同じ方向に向く
        Vector3 camera_direction = Camera.main.transform.eulerAngles;
        Quaternion next_direction = Quaternion.Euler(0.0f, camera_direction.y, 0.0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, next_direction, Time.deltaTime * m_RotateSpeed);
    }

    // 回避
}

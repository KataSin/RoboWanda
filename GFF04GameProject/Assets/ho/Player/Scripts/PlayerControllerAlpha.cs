using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤー制御（α版）
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

// プレイヤーの状態
enum PlayerStateAlpha
{
    Normal,     // 通常
    Bomb,       // 爆弾投げ
    Evasion,    // 回避
    Damage,     // 被弾
    Dead        // 死亡
}

public class PlayerControllerAlpha : MonoBehaviour
{
    [SerializeField]
    private float m_MaxSpeed = 6.0f;            // 最高速度（メートル/秒）
    [SerializeField]
    private float m_AccelPower = 2.0f;          // 加速度（メートル/秒/秒）
    [SerializeField]
    private float m_BrakePower = 6.0f;          // ブレーキ速度（メートル/秒/秒）
    [SerializeField]
    private float m_RotateSpeed = 180.0f;       // 回転速度（度/秒）
    [SerializeField]
    private float m_Gravity = 18.0f;            // 重力加速度（メートル/秒/秒）

    [SerializeField]
    private float m_MaxSpeedDash = 8.0f;        // ダッシュ時の最高速度（メートル/秒）
    [SerializeField]
    private float m_AccelPowerDash = 3.5f;      // ダッシュ時の加速度（メートル/秒/秒）

    [SerializeField]
    private float m_MaxSpeedBomb = 4.0f;        // 爆弾投げ時の最高速度（メートル/秒）
    [SerializeField]
    private float m_MaxBackSpeed = 2.0f;        // 爆弾投げ時の最高後退速度（メートル/秒）
    [SerializeField]
    private float m_AccelPowerBomb = 1.5f;      // 爆弾投げ時の加速度（メートル/秒/秒）
    [SerializeField]
    private float m_RotateSpeedBomb = 20.0f;    // 爆弾投げ時の回転速度（度/秒）

    float m_VelocityY = 0.0f;                   // y軸方向の移動量
    float m_SpeedX = 0.0f;                      // X軸速度（右はプラス、左はマイナス）
    float m_SpeedZ = 0.0f;                      // Z軸速度（前進はプラス、後退はマイナス）
    Vector3 m_PrevPosition;                     // 前回の位置（回転処理用）

    [SerializeField]
    private GameObject m_BombThrow;             // 爆弾の出現ポイント
    [SerializeField]
    private GameObject m_Bomb;                  // 爆弾のプレハブ
    int m_BombKeep = 3;                         // 爆弾の保持数
    int m_ThrowAmount = 1;                      // 爆弾の投擲数
    bool m_IsTriggered;                         // 十字キーの操作判定

    CharacterController m_Controller;           // キャラクターコントローラー
    PlayerStateAlpha m_State;                   // プレイヤーの状態
    bool m_IsDash;                              // ダッシュしているか


    // Use this for initialization
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_State = PlayerStateAlpha.Normal;
        m_PrevPosition = transform.position;
        m_IsDash = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case PlayerStateAlpha.Normal:
                Normal();
                break;
            case PlayerStateAlpha.Bomb:
                Bomb();
                break;
            case PlayerStateAlpha.Evasion:
                Evasion();
                break;
            case PlayerStateAlpha.Damage:
                Damage();
                break;
            case PlayerStateAlpha.Dead:
                Dead();
                break;
            default:
                break;
        }

        // 十字キーで爆弾の投擲数を調節
        // 十字キーの入力が無い場合、操作判定を解除
        if (Input.GetAxisRaw("Throw_Amount") == 0)
        {
            m_IsTriggered = false;
        }

        // 十字ボタンの上下で投擲数を選択
        if (m_IsTriggered == false)
        {
            // 上ボタン
            if (Input.GetAxisRaw("Throw_Amount") < -0.9)
            {
                m_IsTriggered = true;
                m_ThrowAmount = m_ThrowAmount + 1;
            }
            // 下ボタン
            if (Input.GetAxisRaw("Throw_Amount") > 0.9)
            {
                m_IsTriggered = true;
                m_ThrowAmount = m_ThrowAmount - 1;
            }
        }

        // 爆弾の数を１～３の範囲内に限定
        m_ThrowAmount = Mathf.Clamp(m_ThrowAmount, 1, 3);

        // 爆弾が全て起爆した場合、爆弾の保持数を3に戻す
        if (GameObject.FindGameObjectsWithTag("Bomb").Length == 0)
        {
            m_BombKeep = 3;
        }
    }

    // 通常時の挙動
    void Normal()
    {
        // 通常時の移動処理
        NormalMove();

        // Aボタンを押すとダッシュ
        m_IsDash = (Input.GetButton("Dash")) ? true : false;

        // RBボタンを押すと爆弾投げ状態に
        if (Input.GetButton("Bomb_Hold"))
        {
            m_State = PlayerStateAlpha.Bomb;
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

        // 加速する
        float accel;
        accel = (m_IsDash) ? accel = m_AccelPowerDash : accel = m_AccelPower;
        // 左右キーで加速
        m_SpeedX +=
            accel
            * axisHorizontal
            * Time.deltaTime;
        // 上下キーで加速
        m_SpeedZ +=
            accel
            * axisVertical
            * Time.deltaTime;

        // 速度制限
        float speed_limit;
        speed_limit = (m_IsDash) ? speed_limit = m_MaxSpeedDash : speed_limit = m_MaxSpeed;
        m_SpeedX = Mathf.Clamp(m_SpeedX, -speed_limit, speed_limit);
        m_SpeedZ = Mathf.Clamp(m_SpeedZ, -speed_limit, speed_limit);

        // 移動処理
        Vector3 velocity = forward * m_SpeedZ + Camera.main.transform.right * m_SpeedX;

        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
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

    // 爆弾投げ時の挙動
    void Bomb()
    {
        // 爆弾投げ時の移動処理
        BombMove();

        // 着弾点を表示
        BombShowThrowPoint();

        // 爆弾を投擲
        BombThrow();

        // RBボタンを放すと通常状態に戻る
        if (!Input.GetButton("Bomb_Hold"))
        {
            m_State = PlayerStateAlpha.Normal;
        }
    }

    // 爆弾投げ時の移動処理
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

        // 加速する
        // 左右キーで加速
        m_SpeedX +=
            m_AccelPowerBomb
            * axisHorizontal
            * Time.deltaTime;
        // 上下キーで加速
        m_SpeedZ +=
            m_AccelPowerBomb
            * axisVertical
            * Time.deltaTime;

        // 速度制限
        m_SpeedX = Mathf.Clamp(m_SpeedX, -m_MaxSpeedBomb, m_MaxSpeedBomb);
        m_SpeedZ = Mathf.Clamp(m_SpeedZ, -m_MaxBackSpeed, m_MaxSpeedBomb);

        // 移動処理
        Vector3 velocity = forward * m_SpeedZ + Camera.main.transform.right * m_SpeedX;

        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // CharacterControllerに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // カメラと同じ方向に向く
        Vector3 camera_direction = Camera.main.transform.eulerAngles;
        Quaternion next_direction = Quaternion.Euler(0.0f, camera_direction.y, 0.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, next_direction, Time.deltaTime * m_RotateSpeedBomb);
    }

    // 着弾点を表示
    void BombShowThrowPoint()
    {

    }

    // 爆弾を投擲
    void BombThrow()
    {
        // LBボタンを押すと、爆弾を投擲
        if (Input.GetButtonDown("Bomb_Throw"))
        {
            if (m_BombKeep <= 0) return;

            switch (m_ThrowAmount)
            {
                case 3:
                    if (m_BombKeep == 1)
                    {
                        ThrowOne();
                        break;
                    }
                    if (m_BombKeep == 2)
                    {
                        ThrowTwo();
                        break;
                    }
                    ThrowThree();
                    break;
                case 2:
                    if (m_BombKeep == 1)
                    {
                        ThrowOne();
                        break;
                    }
                    ThrowTwo();
                    break;
                case 1:
                    ThrowOne();
                    break;
                default:
                    break;
            }
        }
    }

    // 爆弾を投擲（1個）
    void ThrowOne()
    {
        m_BombKeep -= 1;
        GameObject bomb = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
    }

    // 爆弾を投擲（2個）
    void ThrowTwo()
    {
        m_BombKeep -= 2;
        GameObject bomb1 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        GameObject bomb2 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);

        bomb1.transform.Rotate(new Vector3(0, 1, 0), -15.0f);
        bomb2.transform.Rotate(new Vector3(0, 1, 0), +15.0f);
    }

    // 爆弾を投擲（3個）
    void ThrowThree()
    {
        m_BombKeep -= 3;
        GameObject bomb1 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        GameObject bomb2 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        GameObject bomb3 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);

        bomb2.transform.Rotate(new Vector3(0, 1, 0), -15.0f);
        bomb3.transform.Rotate(new Vector3(0, 1, 0), +15.0f);
    }

    // 回避行動
    void Evasion()
    {

    }

    // 被弾リアクション
    void Damage()
    {

    }

    // 死亡アクション
    void Dead()
    {

    }
}
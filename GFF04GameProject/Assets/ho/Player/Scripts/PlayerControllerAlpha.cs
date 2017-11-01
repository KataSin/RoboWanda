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
    Prone,      // 
    Evasion,    // 回避
    Damage,     // 被弾
    Dead        // 死亡
}

public class PlayerControllerAlpha : MonoBehaviour
{
    [SerializeField]
    private int m_MaxHP = 100;                  // プレイヤーの最大体力値
    int m_CurrentHP;                            // 現在の体力値

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
    private float m_MaxSpeedDash = 12.0f;       // ダッシュ時の最高速度（メートル/秒）
    [SerializeField]
    private float m_AccelPowerDash = 6.0f;      // ダッシュ時の加速度（メートル/秒/秒）

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
    float m_CurrentSpeedLimit;                  // 現在の速度制限
    Vector3 m_PrevPosition;                     // 前回の位置（回転処理用）

    [SerializeField]
    private GameObject m_BombThrow;             // 爆弾の出現ポイント
    [SerializeField]
    private GameObject m_Bomb;                  // 爆弾のプレハブ
    [SerializeField]
    private GameObject m_ThrowPrediction;       // 投擲予測
    [SerializeField]
    private float m_Force = 6.0f;               // 爆弾を投げる力

    int m_BombKeep = 3;                         // 爆弾の所持数
    int m_ThrowAmount = 1;                      // 爆弾の投擲数
    bool m_IsTriggered;                         // 十字キーの操作判定
    float m_CurrentForce;                       // 現在の投げる力

    [SerializeField]
    private float m_MaxProneSpeed = 1.0f;       // 
    [SerializeField]
    private float m_AccelPowerProne = 0.5f;     // 
    [SerializeField]
    private float m_RotateSpeedProne = 45.0f;  // 

    CharacterController m_Controller;           // キャラクターコントローラー
    Animator m_Animator;                        // アニメーター
    PlayerStateAlpha m_State;                   // プレイヤーの状態
    bool m_IsDash;                              // ダッシュしているか
    bool m_IsBomb;                              // 爆弾投げ状態であるか
    bool m_IsCrouch;                            // 

    // Use this for initialization
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
        m_CurrentHP = m_MaxHP;
        m_State = PlayerStateAlpha.Normal;
        m_CurrentSpeedLimit = m_MaxSpeed;
        m_PrevPosition = transform.position;
        m_CurrentForce = m_Force;
        m_IsDash = false;
        m_IsBomb = false;
        m_IsCrouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case PlayerStateAlpha.Normal:
                Normal();
                m_IsBomb = false;
                m_IsCrouch = false;
                break;
            case PlayerStateAlpha.Bomb:
                Bomb();
                m_IsBomb = true;
                m_IsCrouch = false;
                break;
            case PlayerStateAlpha.Prone:
                Prone();
                m_IsBomb = false;
                m_IsCrouch = true;
                break;
            case PlayerStateAlpha.Evasion:
                Evasion();
                m_IsBomb = false;
                m_IsCrouch = false;
                break;
            case PlayerStateAlpha.Damage:
                Damage();
                m_IsBomb = false;
                m_IsCrouch = false;
                break;
            case PlayerStateAlpha.Dead:
                Dead();
                m_IsBomb = false;
                m_IsCrouch = false;
                break;
            default:
                m_IsBomb = false;
                m_IsCrouch = false;
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

        // 爆弾が全て起爆した場合、爆弾の所持数を3に戻す
        if (GameObject.FindGameObjectsWithTag("Bomb").Length == 0)
        {
            m_BombKeep = 3;
        }

        // 体力が0以下になったら死亡
        if (m_CurrentHP <= 0)
        {
            m_State = PlayerStateAlpha.Dead;
        }

        // Animatorにプレイヤーの状態を知らせる
        m_Animator.SetBool("IsBomb", m_IsBomb);
        m_Animator.SetBool("IsCrouch", m_IsCrouch);
    }

    // 接触判定

    // 通常時の挙動
    void Normal()
    {
        // 通常時の移動処理
        NormalMove();

        // RTボタンを押すとダッシュ
        m_IsDash = (Input.GetAxis("Dash") > 0.5f) ? true : false;

        // RBボタンを押すと爆弾投げ状態に
        if (Input.GetButton("Bomb_Hold"))
        {
            m_State = PlayerStateAlpha.Bomb;
        }

        // Bボタンを押すとしゃがむ
        if (Input.GetButtonDown("Crouch"))
        {
            m_State = PlayerStateAlpha.Prone;
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
        if (m_IsDash)
        {
            m_CurrentSpeedLimit = m_MaxSpeedDash;
        }
        else
        {
            if (m_CurrentSpeedLimit > m_MaxSpeed)
            {
                m_CurrentSpeedLimit -= 0.1f;
            }
        }

        m_SpeedX = Mathf.Clamp(m_SpeedX, -m_CurrentSpeedLimit, m_CurrentSpeedLimit);
        m_SpeedZ = Mathf.Clamp(m_SpeedZ, -m_CurrentSpeedLimit, m_CurrentSpeedLimit);

        // 移動処理
        Vector3 velocity = forward * m_SpeedZ + Camera.main.transform.right * m_SpeedX;

        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // CharacterControllerに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);
        // Animatorに命令して、アニメーションを再生する
        // プレイヤー現在の移動量を取得
        float current_speed;
        current_speed = m_Controller.velocity.magnitude;
        m_Animator.SetFloat("NormalSpeed", current_speed);

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
        // カメラの角度に応じて、爆弾の投げる角度を変える（擲弾点の角度を変える）
        m_BombThrow.transform.rotation = Camera.main.transform.rotation;

        // 投擲角度によって、投げる力を変える（水平以上になると、力が増える）
        var throw_angle = m_BombThrow.transform.localEulerAngles.x;
        float elevation_angle = 0.0f;
        if (throw_angle >= 270.0f) elevation_angle = 360.0f - throw_angle;
        float new_force = m_Force * (1 + elevation_angle / 90.0f);
        m_CurrentForce = new_force;

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
        // Animatorに命令して、アニメーションを再生する
        float current_speed_X = m_SpeedX;
        float current_speed_Z = m_SpeedZ;

        m_Animator.SetFloat("BombSpeedX", current_speed_X);
        m_Animator.SetFloat("BombSpeedZ", current_speed_Z);

        // カメラと同じ方向に向く
        Vector3 camera_direction = Camera.main.transform.eulerAngles;
        Quaternion next_direction = Quaternion.Euler(0.0f, camera_direction.y, 0.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, next_direction, Time.deltaTime * m_RotateSpeedBomb);
    }

    // 着弾点を表示
    void BombShowThrowPoint()
    {
        switch (m_ThrowAmount)
        {
            case 1:
                //ShowThrowDirectionOne();
                break;
            case 2:
                //ShowThrowDirectionTwo();
                break;
            case 3:
                //ShowThrowDirectionThree();
                break;
            default:
                //ShowThrowDirectionOne();
                break;
        }
    }

    // 着弾点を表示（1個投擲時）
    void ShowThrowDirectionOne()
    {
        // 予測用オブジェクトを生成
        GameObject prediction = Instantiate(m_ThrowPrediction, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        // Rigidbodyに力を加えて投げる
        prediction.GetComponent<Rigidbody>().AddForce(prediction.transform.forward * m_CurrentForce, ForceMode.Impulse);
    }

    // 着弾点を表示（2個投擲時）
    void ShowThrowDirectionTwo()
    {
        // 予測用オブジェクトを生成
        GameObject prediction1 = Instantiate(m_ThrowPrediction, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        GameObject prediction2 = Instantiate(m_ThrowPrediction, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        // 予測用オブジェクトの角度を調整
        prediction1.transform.Rotate(new Vector3(0, 1, 0), -15.0f);
        prediction2.transform.Rotate(new Vector3(0, 1, 0), +15.0f);
        // Rigidbodyに力を加えて投げる
        prediction1.GetComponent<Rigidbody>().AddForce(prediction1.transform.forward * m_CurrentForce, ForceMode.Impulse);
        prediction2.GetComponent<Rigidbody>().AddForce(prediction2.transform.forward * m_CurrentForce, ForceMode.Impulse);
    }

    // 着弾点を表示（3個投擲時）
    void ShowThrowDirectionThree()
    {
        // 予測用オブジェクトを生成
        GameObject prediction1 = Instantiate(m_ThrowPrediction, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        GameObject prediction2 = Instantiate(m_ThrowPrediction, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        GameObject prediction3 = Instantiate(m_ThrowPrediction, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        // 予測用オブジェクトの角度を調整
        prediction2.transform.Rotate(new Vector3(0, 1, 0), -15.0f);
        prediction3.transform.Rotate(new Vector3(0, 1, 0), +15.0f);
        // Rigidbodyに力を加えて投げる
        prediction1.GetComponent<Rigidbody>().AddForce(prediction1.transform.forward * m_CurrentForce, ForceMode.Impulse);
        prediction2.GetComponent<Rigidbody>().AddForce(prediction2.transform.forward * m_CurrentForce, ForceMode.Impulse);
        prediction3.GetComponent<Rigidbody>().AddForce(prediction3.transform.forward * m_CurrentForce, ForceMode.Impulse);
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
                    ThrowOne();
                    break;
            }
        }
    }

    // 爆弾を投擲（1個）
    void ThrowOne()
    {
        // 爆弾の所持数が減る
        m_BombKeep -= 1;
        // 爆弾を生成
        GameObject bomb = Instantiate(m_Bomb, m_BombThrow.transform.position, Quaternion.identity);
        // Rigidbodyに力を加えて投げる
        bomb.GetComponent<Rigidbody>().AddForce(transform.forward * m_CurrentForce, ForceMode.Impulse);
    }

    // 爆弾を投擲（2個）
    void ThrowTwo()
    {
        // 爆弾の所持数が減る
        m_BombKeep -= 2;
        // 爆弾を生成
        GameObject bomb1 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        GameObject bomb2 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        // 爆弾の角度を調整
        bomb1.transform.Rotate(new Vector3(0, 1, 0), -15.0f);
        bomb2.transform.Rotate(new Vector3(0, 1, 0), +15.0f);
        // Rigidbodyに力を加えて投げる
        bomb1.GetComponent<Rigidbody>().AddForce(bomb1.transform.forward * m_CurrentForce, ForceMode.Impulse);
        bomb2.GetComponent<Rigidbody>().AddForce(bomb2.transform.forward * m_CurrentForce, ForceMode.Impulse);
    }

    // 爆弾を投擲（3個）
    void ThrowThree()
    {
        // 爆弾の所持数が減る
        m_BombKeep -= 3;
        // 爆弾を生成
        GameObject bomb1 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        GameObject bomb2 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        GameObject bomb3 = Instantiate(m_Bomb, m_BombThrow.transform.position, m_BombThrow.transform.rotation);
        // 爆弾の角度を調整
        bomb2.transform.Rotate(new Vector3(0, 1, 0), -15.0f);
        bomb3.transform.Rotate(new Vector3(0, 1, 0), +15.0f);
        // Rigidbodyに力を加えて投げる
        bomb1.GetComponent<Rigidbody>().AddForce(bomb1.transform.forward * m_CurrentForce, ForceMode.Impulse);
        bomb2.GetComponent<Rigidbody>().AddForce(bomb2.transform.forward * m_CurrentForce, ForceMode.Impulse);
        bomb3.GetComponent<Rigidbody>().AddForce(bomb3.transform.forward * m_CurrentForce, ForceMode.Impulse);
    }

    // 
    void Prone()
    {
        // 
        ProneMove();

        // Bボタンを押すと、通常状態に戻る
        if (Input.GetButtonDown("Crouch"))
        {
            m_State = PlayerStateAlpha.Normal;
        }
    }

    // 
    void ProneMove()
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
            m_AccelPowerProne
            * axisHorizontal
            * Time.deltaTime;
        // 上下キーで加速
        m_SpeedZ +=
            m_AccelPowerProne
            * axisVertical
            * Time.deltaTime;

        // 速度制限
        m_SpeedX = Mathf.Clamp(m_SpeedX, -m_MaxProneSpeed, m_MaxProneSpeed);
        m_SpeedZ = Mathf.Clamp(m_SpeedZ, -m_MaxProneSpeed, m_MaxProneSpeed);

        // 移動処理
        Vector3 velocity = forward * m_SpeedZ + Camera.main.transform.right * m_SpeedX;

        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // CharacterControllerに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);
        // Animatorに命令して、アニメーションを再生する
        // プレイヤー現在の移動量を取得
        float current_speed;
        current_speed = m_Controller.velocity.magnitude;
        m_Animator.SetFloat("ProneSpeed", current_speed);

        // 移動方向に向ける
        Vector3 direction = transform.position - m_PrevPosition;

        if (direction.sqrMagnitude > 0)
        {
            Vector3 orientiation = Vector3.Slerp(transform.forward,
                new Vector3(direction.x, 0.0f, direction.z),
                m_RotateSpeedProne * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            transform.LookAt(transform.position + orientiation);
            m_PrevPosition = transform.position;
        }
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
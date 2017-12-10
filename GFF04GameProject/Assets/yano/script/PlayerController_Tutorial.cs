using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤー制御（正式バージョン）
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

// プレイヤーの状態
enum T_PlayerState
{
    Normal,     // 通常
    Aiming,     // 照準中
    Creeping,   // 匍匐
    Dead        // 死亡
}

public class PlayerController_Tutorial : MonoBehaviour
{
    // 通常状態の移動関連
    [SerializeField]
    private float m_MaxSpeed = 6.0f;                // 最高速度（メートル/秒）
    [SerializeField]
    private float m_AccelPower = 6.0f;              // 加速度（メートル/秒/秒）
    [SerializeField]
    private float m_BrakePower = 12.0f;             // ブレーキ速度（メートル/秒/秒）
    [SerializeField]
    private float m_RotateSpeed = 360.0f;           // 回転速度（度/秒）
    [SerializeField]
    private float m_Gravity = 18.0f;                // 重力加速度（メートル/秒/秒）
    // ダッシュ中の移動関連
    [SerializeField]
    private float m_MaxSpeedDash = 12.0f;           // ダッシュ時の最高速度（メートル/秒）
    [SerializeField]
    private float m_AccelPowerDash = 12.0f;         // ダッシュ時の加速度（メートル/秒/秒）
    [SerializeField]
    private float m_BrakePowerDash = 18.0f;         // ダッシュ時のブレーキ速度（メートル/秒/秒）
    [SerializeField]
    private float m_RotateSpeedDash = 450.0f;       // ダッシュ時の回転速度（度/秒）
    // 照準中の移動関連
    [SerializeField]
    private float m_MaxSpeedAiming = 5.0f;          // 照準中の最高速度（メートル/秒）
    [SerializeField]
    private float m_MaxBackSpeed = 3.0f;            // 照準中の最高後退速度（メートル/秒）
    [SerializeField]
    private float m_AccelPowerAiming = 4.0f;        // 照準中の加速度（メートル/秒/秒）
    [SerializeField]
    private float m_RotateSpeedAiming = 20.0f;      // 照準中の回転速度（度/秒）
    // 匍匐時の移動関連
    [SerializeField]
    private float m_MaxCreepingSpeed = 1.0f;        // 匍匐時の最高速度（メートル/秒）
    [SerializeField]
    private float m_AccelPowerCreeping = 0.5f;      // 匍匐時の加速度（メートル/秒/秒）
    [SerializeField]
    private float m_RotateSpeedCreeping = 45.0f;    // 匍匐時の回転速度（度/秒）
    // 移動処理関連変数
    float m_VelocityY = 0.0f;                       // y軸方向の移動量
    float m_Speed = 0.0f;                           // 通常時の移動速度
    float m_SpeedX = 0.0f;                          // 照準中のX軸速度（右はプラス、左はマイナス）
    float m_SpeedZ = 0.0f;                          // 照準中のz軸速度（前進はプラス、後退はマイナス）
    float m_CurrentSpeedLimit;                      // 現在の速度制限
    float m_CurrentBrakePower;                      // 現在のブレーキ速度
    float m_CurrentRotateSpeed;                     // 現在の回転速度
    Vector3 m_PrevPosition;                         // 前回の位置（回転処理用）
    // コンポーネントと他の変数
    CharacterController m_Controller;               // キャラクターコントローラー
    Animator m_Animator;                            // アニメーター
    [SerializeField]
    T_PlayerState m_State;                            // プレイヤーの状態
    bool m_IsDash;                                  // ダッシュしているか
    bool m_IsAiming;                                // 照準中であるか
    bool m_IsCreeping;                              // 匍匐しているか
    bool m_IsDead;                                  // 死亡しているか

    float m_current_speed;

    //ボムスポーン(片岡実装)
    private GameObject m_BomSpawn;

    [SerializeField]
    private GameObject camera_pos_;

    // Use this for initialization
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();

        m_State = T_PlayerState.Normal;
        m_CurrentSpeedLimit = m_MaxSpeed;
        m_CurrentBrakePower = m_BrakePower;
        m_CurrentRotateSpeed = m_RotateSpeed;
        m_PrevPosition = transform.position;

        m_IsDash = false;
        m_IsAiming = false;
        m_IsCreeping = false;
        m_IsDead = false;

        m_current_speed = 0.0f;

        //片岡実装
        m_BomSpawn = GameObject.FindGameObjectWithTag("BomSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの状態に応じて処理を行う
        switch (m_State)
        {
            // 通常
            case T_PlayerState.Normal:
                Normal();
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = false;
                break;
            // 照準中
            case T_PlayerState.Aiming:
                Aiming();
                m_IsAiming = true;
                m_IsCreeping = false;
                m_IsDead = false;
                break;
            // 匍匐
            case T_PlayerState.Creeping:
                Creeping();
                m_IsAiming = false;
                m_IsCreeping = true;
                m_IsDead = false;
                break;
            // 死亡
            case T_PlayerState.Dead:
                Dead();
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = true;
                break;
            // デフォルト（バグ防止用）
            default:
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = true;
                break;
        }

        // プレイヤーが地面にいる場合、y軸加速度を0にする
        if (m_Controller.isGrounded) m_VelocityY = 0.0f;

        // プレイヤーと地面との距離を取得（着地アニメーション用）


        // アニメーターにプレイヤーの状態を知らせる
        m_Animator.SetBool("IsGrounded", m_Controller.isGrounded);
        m_Animator.SetBool("IsAiming", m_IsAiming);
        m_Animator.SetBool("IsCreeping", m_IsCreeping);
        m_Animator.SetBool("IsDead", m_IsDead);

        m_current_speed = m_Controller.velocity.magnitude;

        // 振動テスト
        /*if (Input.GetKeyDown("space"))
        {
            GameObject Camera = GameObject.FindGameObjectWithTag("MainCamera");
            Camera.GetComponent<CameraShake>().Shake(3.0f);
            // Debug.Log("外部からカメラに振動命令");
        }*/

        // 死亡テスト
        //if (Input.GetKeyDown("space"))
        //{
        //    if (m_State != T_PlayerState.Dead)
        //    {
        //        m_State = T_PlayerState.Dead;
        //    }
        //}
    }

    // 接触判定
    public void OnTriggerEnter(Collider other)
    {
        // 敵ロボットと接触したら死亡
        if (other.GetComponent<RobotDamage>() != null)
        {
            m_State = T_PlayerState.Dead;
        }

        // 倒壊中のビルと接触したら死亡
        if ((other.tag == "TowerCollision" && other.GetComponent<tower_collide>().Get_CollideFlag())
            || (other.tag == "DebrisCollision" && !other.GetComponent<DebrisGround>().Hit_Ground())
            || other.gameObject.name == "DeathCollide")
        {
            m_State = T_PlayerState.Dead;
        }
    }

    // 通常時の処理
    void Normal()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_CntActive())
        {
            // 通常時の移動処理
            NormalMove();

            // RTボタンを押すとダッシュ
            m_IsDash = (Input.GetAxis("Dash") > 0.5f) ? true : false;

            // RBボタンを押すと爆弾投げ状態に
            if (Input.GetButton("Aim"))
            {
                m_State = T_PlayerState.Aiming;
            }

            // Bボタンを押すとしゃがむ
            if (Input.GetButtonDown("Creeping"))
            {
                m_State = T_PlayerState.Creeping;
            }

        }
    }

    // 通常時の移動
    void NormalMove()
    {
        /*// カメラの正面向きのベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        // y成分を無視する
        forward.y = 0;
        // 正規化する
        forward.Normalize();

        // 方向入力を取得
        float axisHorizontal = Input.GetAxisRaw("Horizontal_L");    // x軸（左右）
        float axisVertical = Input.GetAxisRaw("Vertical_L");        // z軸（上下）

        // 減速する（入力が無い場合 or 進行方向と逆に入力がある場合）
        // X軸（左右キー）
        if ((axisHorizontal == 0) || (m_SpeedX * axisHorizontal < 0))
        {
            if (m_SpeedX > 0)
            {
                m_SpeedX = Mathf.Max(m_SpeedX - m_CurrentBrakePower * Time.deltaTime, 0);
            }
            else {
                m_SpeedX = Mathf.Min(m_SpeedX + m_CurrentBrakePower * Time.deltaTime, 0);
            }
        }
        // Z軸（上下キー）
        if ((axisVertical == 0) || (m_SpeedZ * axisVertical < 0))
        {
            if (m_SpeedZ > 0)
            {
                m_SpeedZ = Mathf.Max(m_SpeedZ - m_CurrentBrakePower * Time.deltaTime, 0);
            }
            else {
                m_SpeedZ = Mathf.Min(m_SpeedZ + m_CurrentBrakePower * Time.deltaTime, 0);
            }
        }

        // 接地状態であれば加速可能
        if (m_Controller.isGrounded)
        {
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

            // 速度制限、ブレーキ速度、および回転速度の変更
            if (m_IsDash)
            {
                m_CurrentSpeedLimit = m_MaxSpeedDash;
                m_CurrentBrakePower = m_BrakePowerDash;
                m_CurrentRotateSpeed = m_RotateSpeedDash;
            }
            else
            {
                if (m_CurrentSpeedLimit > m_MaxSpeed)
                {
                    m_CurrentSpeedLimit -= 0.1f;
                }
                m_CurrentBrakePower = m_BrakePower;
                if (m_CurrentRotateSpeed > m_RotateSpeed)
                {
                    m_CurrentRotateSpeed -= 0.1f;
                }
            }
            // 速度を制限する
            m_SpeedX = Mathf.Clamp(m_SpeedX, -m_CurrentSpeedLimit, m_CurrentSpeedLimit);
            m_SpeedZ = Mathf.Clamp(m_SpeedZ, -m_CurrentSpeedLimit, m_CurrentSpeedLimit);
        }

        // 移動処理
        Vector3 velocity = forward * m_SpeedZ + Camera.main.transform.right * m_SpeedX;
        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // キャラクターコントローラーに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // 移動方向に向ける
        Vector3 direction = transform.position - m_PrevPosition;
        if (direction.sqrMagnitude > 0)
        {
            Vector3 orientiation = Vector3.Slerp(transform.forward,
                new Vector3(direction.x, 0.0f, direction.z),
                m_CurrentRotateSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + orientiation);
            m_PrevPosition = transform.position;
        }

        // アニメーターに命令して、アニメーションを再生する
        // プレイヤー現在の移動量を取得
        float current_speed;
        current_speed = m_Controller.velocity.magnitude;
        if (current_speed > m_CurrentSpeedLimit) current_speed = m_CurrentSpeedLimit;
        m_Animator.SetFloat("NormalSpeed", current_speed);*/

        /*// カメラの正面向きのベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        // y成分を無視する
        forward.y = 0;
        // 正規化する
        forward.Normalize();

        // 方向入力を取得
        float axisHorizontal = Input.GetAxisRaw("Horizontal_L");    // x軸（左右）
        float axisVertical = Input.GetAxisRaw("Vertical_L");        // z軸（上下）

        // 移動速度、および回転速度の変更
        if (m_IsDash)
        {
            if (m_CurrentSpeedLimit < m_MaxSpeedDash)
                m_CurrentSpeedLimit += 0.4f;
            m_CurrentRotateSpeed = m_RotateSpeedDash;
        }
        else
        {
            if (m_CurrentSpeedLimit > m_MaxSpeed)
                m_CurrentSpeedLimit -= 0.2f;
            if (m_CurrentRotateSpeed > m_RotateSpeed)
                m_CurrentRotateSpeed -= 0.1f;
        }

        Vector3 velocity = Vector3.zero;
        velocity = forward * axisVertical * m_CurrentSpeedLimit + Camera.main.transform.right * axisHorizontal * m_CurrentSpeedLimit;

        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // キャラクターコントローラーに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // 移動方向に向ける
        Vector3 direction = transform.position - m_PrevPosition;
        if (direction.sqrMagnitude > 0)
        {
            Vector3 orientiation = Vector3.Slerp(transform.forward,
                new Vector3(direction.x, 0.0f, direction.z),
                m_CurrentRotateSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + orientiation);
            m_PrevPosition = transform.position;
        }

        // アニメーターに命令して、アニメーションを再生する
        // プレイヤー現在の移動量を取得
        float current_speed;
        current_speed = m_Controller.velocity.magnitude;
        if (current_speed > m_CurrentSpeedLimit) current_speed = m_CurrentSpeedLimit;
        m_Animator.SetFloat("NormalSpeed", current_speed);

        Debug.Log(m_CurrentSpeedLimit);*/

        // カメラの正面向きのベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        // y成分を無視する
        forward.y = 0;
        // 正規化する
        forward.Normalize();

        // 方向入力を取得

        float axisHorizontal = Input.GetAxisRaw("Horizontal_L");    // x軸（左右）
        float axisVertical = Input.GetAxisRaw("Vertical_L");        // z軸（上下）


        // 減速する（入力が無い場合）
        if (axisHorizontal == 0 && axisVertical == 0)
        {
            m_Speed = Mathf.Max(m_Speed - m_CurrentBrakePower * Time.deltaTime, 0);
        }


        // 接地状態であれば加速可能
        if (m_Controller.isGrounded)
        {
            // 加速する
            float accel;
            accel = (m_IsDash) ? accel = m_AccelPowerDash : accel = m_AccelPower;
            m_Speed += accel * Time.deltaTime;

            // 速度制限、ブレーキ速度、および回転速度の変更
            if (m_IsDash)
            {
                m_CurrentSpeedLimit = m_MaxSpeedDash;
                m_CurrentBrakePower = m_BrakePowerDash;
                m_CurrentRotateSpeed = m_RotateSpeedDash;
            }
            else
            {
                if (m_CurrentSpeedLimit > m_MaxSpeed)
                {
                    m_CurrentSpeedLimit -= 0.2f;
                }
                m_CurrentBrakePower = m_BrakePower;
                if (m_CurrentRotateSpeed > m_RotateSpeed)
                {
                    m_CurrentRotateSpeed -= 0.1f;
                }
            }
            // 速度を制限する
            m_Speed = Mathf.Clamp(m_Speed, 0.0f, m_CurrentSpeedLimit);
        }

        // 移動処理
        Vector3 velocity = forward * axisVertical * m_Speed + Camera.main.transform.right * axisHorizontal * m_Speed;
        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // キャラクターコントローラーに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // 移動方向に向ける
        Vector3 direction = transform.position - m_PrevPosition;
        if (direction.sqrMagnitude > 0)
        {
            Vector3 orientiation = Vector3.Slerp(
                transform.forward,
                new Vector3(direction.x, 0.0f, direction.z),
                m_CurrentRotateSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + orientiation);
            m_PrevPosition = transform.position;
        }

        // アニメーターに命令して、アニメーションを再生する
        // プレイヤー現在の移動量を取得
        /*float current_speed;
        current_speed = m_Controller.velocity.magnitude;
        if (current_speed > m_CurrentSpeedLimit) current_speed = m_CurrentSpeedLimit;
        m_Animator.SetFloat("NormalSpeed", current_speed);*/
        float animation_speed;
        animation_speed = m_current_speed;

        if (animation_speed > m_CurrentSpeedLimit) animation_speed = m_CurrentSpeedLimit;
        m_Animator.SetFloat("NormalSpeed", animation_speed);
    }

    // 照準中の処理
    void Aiming()
    {
        // 爆弾投げ時の移動処理
        AimingMove();

        var a = m_BomSpawn.GetComponent<BomSpawn>();
        //片岡の実装
        m_BomSpawn.GetComponent<BomSpawn>().Set(Camera.main.transform.forward, 150.0f);
        m_BomSpawn.GetComponent<BomSpawn>().SetDrawLine(true);

        if (Input.GetButtonDown("Bomb_Throw"))
        {
            m_BomSpawn.GetComponent<BomSpawn>().SpawnBom();
        }

        // RBボタンを放すと通常状態に戻る
        if (!Input.GetButton("Aim"))
        {
            m_BomSpawn.GetComponent<BomSpawn>().SetDrawLine(false);
            m_State = T_PlayerState.Normal;
        }
    }

    // 照準中の移動
    void AimingMove()
    {
        // カメラの正面向きのベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        // y成分を無視する
        forward.y = 0;
        // 正規化する
        forward.Normalize();

        // 方向入力を取得
        float axisHorizontal = Input.GetAxisRaw("Horizontal_L");    // x軸（左右）
        float axisVertical = Input.GetAxisRaw("Vertical_L");        // z軸（上下）

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

        // 接地状態であれば加速可能
        if (m_Controller.isGrounded)
        {
            // 加速する
            // 左右キーで加速
            m_SpeedX +=
                m_AccelPowerAiming
                * axisHorizontal
                * Time.deltaTime;
            // 上下キーで加速
            m_SpeedZ +=
                m_AccelPowerAiming
                * axisVertical
                * Time.deltaTime;

            // 速度を制限する
            m_SpeedX = Mathf.Clamp(m_SpeedX, -m_MaxSpeedAiming, m_MaxSpeedAiming);
            m_SpeedZ = Mathf.Clamp(m_SpeedZ, -m_MaxBackSpeed, m_MaxSpeedAiming);
        }

        // 移動処理
        Vector3 velocity = forward * m_SpeedZ + Camera.main.transform.right * m_SpeedX;
        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // キャラクターコントローラーに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // カメラと同じ方向に向く
        Vector3 camera_direction = Camera.main.transform.eulerAngles;
        Quaternion next_direction = Quaternion.Euler(0.0f, camera_direction.y, 0.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, next_direction, Time.deltaTime * m_RotateSpeedAiming);

        // アニメーターに命令して、アニメーションを再生する
        float current_speed_X = m_SpeedX;
        float current_speed_Z = m_SpeedZ;

        m_Animator.SetFloat("AimingSpeedX", current_speed_X);
        m_Animator.SetFloat("AimingSpeedZ", current_speed_Z);
    }

    // 匍匐時の処理
    void Creeping()
    {
        // 匍匐時の移動処理
        CreepingMove();

        // Bボタンを押すと立つ
        if (Input.GetButtonDown("Creeping"))
        {
            m_State = T_PlayerState.Normal;
        }
    }

    // 匍匐時の移動
    void CreepingMove()
    {
        // カメラの正面向きのベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        // y成分を無視する
        forward.y = 0;
        // 正規化する
        forward.Normalize();

        // 方向入力を取得
        float axisHorizontal = Input.GetAxisRaw("Horizontal_L");    // x軸（左右）
        float axisVertical = Input.GetAxisRaw("Vertical_L");        // z軸（上下）

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
            m_AccelPowerCreeping
            * axisHorizontal
            * Time.deltaTime;
        // 上下キーで加速
        m_SpeedZ +=
            m_AccelPowerCreeping
            * axisVertical
            * Time.deltaTime;

        // 速度制限
        m_SpeedX = Mathf.Clamp(m_SpeedX, -m_MaxCreepingSpeed, m_MaxCreepingSpeed);
        m_SpeedZ = Mathf.Clamp(m_SpeedZ, -m_MaxCreepingSpeed, m_MaxCreepingSpeed);

        // 移動処理
        Vector3 velocity = forward * m_SpeedZ + Camera.main.transform.right * m_SpeedX;

        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // キャラクターコントローラーに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // 移動方向に向ける
        Vector3 direction = transform.position - m_PrevPosition;
        if (direction.sqrMagnitude > 0)
        {
            Vector3 orientiation = Vector3.Slerp(transform.forward,
                new Vector3(direction.x, 0.0f, direction.z),
                m_RotateSpeedCreeping * Time.deltaTime / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + orientiation);
            m_PrevPosition = transform.position;
        }

        // アニメーターに命令して、アニメーションを再生する
        // プレイヤー現在の移動量を取得
        float current_speed;
        current_speed = m_Controller.velocity.magnitude;
        if (current_speed > m_CurrentSpeedLimit) current_speed = m_CurrentSpeedLimit;
        m_Animator.SetFloat("CreepingSpeed", current_speed);
    }

    // 死亡時の処理
    void Dead()
    {
        // 死亡モーションを再生
        m_Animator.Play("Dead");
    }

    public int GetPlayerState()
    {
        return (int)m_State;
    }

    // 死亡しているか
    public bool IsDead()
    {
        return m_IsDead;
    }

    // 照準状態であるか
    public bool IsAiming()
    {
        return m_IsAiming;
    }
}
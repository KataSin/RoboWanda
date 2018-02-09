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
    Dead,        // 死亡
    Passing,    // 乗り越え
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
    bool m_IsShot;                                  // 弾を発射したか

    [SerializeField]
    private float m_PassDistance;                   // 乗り越え距離

    float m_SpeedBeforePassing = 0.0f;              // 乗り越える前の速度

    float m_current_speed;                          // 現在の移動速度
    float m_passing_time = 0.0f;                    // 乗り越え処理の残り時間

    //ボムスポーン(片岡実装)
    private GameObject m_BomSpawn;

    [SerializeField]
    private GameObject camera_pos_;

    // グレネードランチャー
    [SerializeField]
    private GameObject m_Launcher;

    [SerializeField]
    private GameObject launcher_IK_;

    [SerializeField]
    private GameObject manager_;

    private AudioSource[] player_se_;
    private AudioClip playerSe_attack_;
    private AudioClip playerSe_walk_;
    private AudioClip playerSe_die_;

    private float test;

    private bool isDse;

    // Use this for initialization
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();

        player_se_ = GetComponents<AudioSource>();
        playerSe_attack_ = player_se_[0].clip;
        playerSe_walk_ = player_se_[1].clip;
        playerSe_die_ = player_se_[2].clip;

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
        test = 0f;

        //片岡実装
        m_BomSpawn = GameObject.FindGameObjectWithTag("BomSpawn");

        m_Launcher.SetActive(true);
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
            // 乗り越える
            case T_PlayerState.Passing:
                Passing_v2();
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = false;
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

        // RTボタンを放すと発射判定を解除（連射の防止）
        if (!(Input.GetAxis("Bomb_Throw") > 0.5f))
        {
            m_IsShot = false;
        }
    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // 乗り越え中、ガードレールとの接触判定を無視
        if (m_State == T_PlayerState.Passing)
        {
            Physics.IgnoreLayerCollision(13, 16, true);
        }
        // エイム中は乗り越えない
        else if (m_State == T_PlayerState.Aiming)
        {

        }
        else if (hit.gameObject.tag == "GuardRail")
        {
            // 接触前の速度を保存
            m_SpeedBeforePassing = m_current_speed;

            m_passing_time = 0.5f;
            m_State = T_PlayerState.Passing;
        }
    }

    // 接触判定
    public void OnTriggerEnter(Collider other)
    {
        //// 敵ロボットと接触したら死亡
        //if (other.GetComponent<RobotDamage>() != null
        //    ||
        //    other.gameObject.tag == "RobotArmAttack"
        //    ||
        //    other.gameObject.tag == "RobotBeam"
        //    ||
        //    other.gameObject.tag == "Missile"
        //    ||
        //    other.gameObject.tag == "ExplosionCollision"
        //    ||
        //    other.gameObject.tag == "BeamCollide")
        //{
        //    m_State = T_PlayerState.Dead;
        //    player_se_[1].Stop();
        //}

        //// 倒壊中のビルと接触したら死亡
        //if ((other.tag == "TowerCollision" && other.GetComponent<tower_collide>().Get_CollideFlag())
        //    || (other.tag == "DebrisCollision" && !other.GetComponent<DebrisGround>().Hit_Ground())
        //    || other.gameObject.name == "DeathCollide")
        //{
        //    m_State = T_PlayerState.Dead;
        //    player_se_[1].Stop();
        //}
    }

    public void OnAnimatorIK()
    {
        m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        m_Animator.SetIKPosition(AvatarIKGoal.LeftHand, launcher_IK_.transform.position);
        m_Animator.SetIKRotation(AvatarIKGoal.LeftHand, launcher_IK_.transform.rotation);
    }

    // 通常時の処理
    void Normal()
    {
        // 通常時の移動処理
        NormalMove();

        if (!manager_.GetComponent<TutorialManager>().GetPlayerCntrlFlag())
        {
            m_IsDash = false;
            return;
        }

        // RTボタンを押すとダッシュ
        m_IsDash = (Input.GetButton("Dash")) ? true : false;

        // LTボタンを押すとボム投げ状態に
        // if (Input.GetButton("Aim"))
        if (Input.GetAxis("Aim") > 0.5f)
        {
            m_State = T_PlayerState.Aiming;
        }
    }

    // 通常時の移動
    void NormalMove()
    {
        // カメラの正面向きのベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        // y成分を無視する
        forward.y = 0;
        // 正規化する
        forward.Normalize();

        // 方向入力を取得
        float axisHorizontal =
            Input.GetAxisRaw("Horizontal_L");    // x軸（左右）
        float axisVertical =
            Input.GetAxisRaw("Vertical_L");        // z軸（上下）


        // 減速する（入力が無い場合）
        if ((axisHorizontal == 0 && axisVertical == 0)
            || !manager_.GetComponent<TutorialManager>().GetPlayerCntrlFlag()
            || !camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_CntActive())
        {
            if (manager_.GetComponent<TutorialManager>().GetPlayerCntrlFlag())
                m_Speed = Mathf.Max(m_Speed - m_CurrentBrakePower * Time.deltaTime, 0);
            else
                m_Speed = Mathf.Max(m_Speed - m_CurrentBrakePower * Time.deltaTime * 4f, 0);

            player_se_[1].Stop();
        }

        else if (axisHorizontal != 0 || axisVertical != 0)
        {
            if (!player_se_[1].isPlaying)
            {
                player_se_[1].PlayOneShot(playerSe_walk_);
            }
        }


        // 接地状態であれば加速可能
        if (m_Controller.isGrounded)
        {
            // 加速する
            float accel;
            accel = (m_IsDash) ? accel = m_AccelPowerDash : accel = m_AccelPower;

            if (manager_.GetComponent<TutorialManager>().GetPlayerCntrlFlag())
                m_Speed += accel * Time.deltaTime;

            // 速度制限、ブレーキ速度、および回転速度の変更
            if (m_IsDash)
            {
                player_se_[1].pitch = 2.4f;
                m_CurrentSpeedLimit = m_MaxSpeedDash;
                m_CurrentBrakePower = m_BrakePowerDash;
                m_CurrentRotateSpeed = m_RotateSpeedDash;
            }
            else
            {
                player_se_[1].pitch = 1.2f;
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
        if (direction.sqrMagnitude > 0
            &&
            (manager_.GetComponent<TutorialManager>().GetPlayerCntrlFlag()
            && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_CntActive()))
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
        // ボム投げ時の移動処理
        AimingMove();

        var a = m_BomSpawn.GetComponent<BomSpawn>();
        //片岡の実装
        m_BomSpawn.GetComponent<BomSpawn>().Set(Camera.main.transform.forward, 150.0f);
        m_BomSpawn.GetComponent<BomSpawn>().SetDrawLine(true);

        // LTボタンが押されてる間、RTボタンを押すと、弾を発射
        // if (Input.GetButtonDown("Bomb_Throw"))
        if (Input.GetAxis("Bomb_Throw") > 0.5f && !m_IsShot)
        {
            m_IsShot = true;
            m_BomSpawn.GetComponent<BomSpawn>().SpawnBom();
            player_se_[0].PlayOneShot(playerSe_attack_);
        }

        // LTボタンを放すと通常状態に戻る
        // if (!Input.GetButton("Aim"))
        if (!(Input.GetAxis("Aim") > 0.5f))
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

    void Passing_v2()
    {
        // グレネードランチャーの表示を消す
        m_Launcher.SetActive(false);
        // 乗り越えモーションを再生
        m_Animator.Play("Passing");

        // 移動処理
        Vector3 velocity = transform.forward * m_SpeedBeforePassing;
        m_Controller.Move(velocity * Time.deltaTime);

        // 乗り越えアニメーションが終了すると、通常状態に戻る
        AnimatorStateInfo AniInfo;     // アニメーションの状態
        AniInfo = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        if (m_passing_time <= 0 && AniInfo.normalizedTime <= 0.9f)
        {
            // グレネードランチャーを表示
            m_Launcher.SetActive(true);
            // 通常状態に戻る
            m_State = T_PlayerState.Normal;
            Physics.IgnoreLayerCollision(13, 16, false);
        }

        m_passing_time -= Time.deltaTime;
    }

    // 死亡時の処理
    void Dead()
    {
        // グレネードランチャーの表示を消す
        m_Launcher.SetActive(false);
        // 死亡モーションを再生
        m_Animator.Play("Dead");

        //死亡SE
        if (test >= 0.8f && !isDse)
        {
            player_se_[2].PlayOneShot(playerSe_die_);
            isDse = true;
        }

        test += 1.0f * Time.deltaTime;
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
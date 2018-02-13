using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤー制御（正式バージョン）
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

// プレイヤーの状態
enum PlayerState
{
    StartFall,
    Normal,     // 通常
    Aiming,     // 照準中
    Creeping,   // 匍匐
    Dead,       // 死亡
    Passing,    // 乗り越え
    Setting,    // 爆発物設置
    BlewUp,     // 飛ばされる
    BlewUpDead  // 飛ばされて死亡
}

public class PlayerController : MonoBehaviour
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

    // 乗り越え処理関連変数
    [SerializeField]
    private float m_PassDistance;                   // 乗り越え距離

    [SerializeField]
    [Header("ジャンプ力")]
    private float m_JumpPower;                      // ジャンプ力
    float m_SpeedBeforePass = 0.0f;                 // 乗り越える前の速度
    bool m_IsPassed = false;                        // 障害物を乗り越えたか
    [SerializeField]
    [Header("距離探知レイキャスト座標")]
    private Transform m_RayPoint;      	            // 障害物探知用のレイキャスト座標（キャラクター下部、距離探知用）
    [SerializeField]
    [Header("探知有効距離（歩行）")]
    private float m_RayDistance_walk;               // 障害物探知の有効距離（歩行）
    [SerializeField]
    [Header("探知有効距離（ダッシュ）")]
    private float m_RayDistance_dash;               // 障害物探知の有効距離（ダッシュ）
    float m_CurrentRayDistance = 1.0f;              // 現在の探知距離

    // コンポーネントと他の変数
    CharacterController m_Controller;               // キャラクターコントローラー
    Animator m_Animator;                            // アニメーター
    [SerializeField]
    PlayerState m_State;                            // プレイヤーの状態
    bool m_IsDash;                                  // ダッシュしているか
    bool m_IsAiming;                                // 照準中であるか
    bool m_IsCreeping;                              // 匍匐しているか
    bool m_IsDead;                                  // 死亡しているか

    bool m_IsInvincible;                            // 無敵状態であるか（デバッグ、デモ用）
    bool m_IsShot;                                  // 弾を発射したか

    //矢野実装
    bool m_IsStartFall;                             //ヘリから降下中か
    bool m_isClear;                                 //Listの中身を消したかどうか

    private GameObject s_Heri;

    [SerializeField]
    private List<GameObject> rope_destibation_;

    float t;
    //

    float m_current_speed;                          // 現在の移動速度
    float m_passing_time = 0.0f;                    // 乗り越え処理の残り時間
    float m_setting_time = 0.0f;                    // 爆発物設置の残り時間

    GameObject m_BuildingNear;                      // 近くにある、倒壊したビル
    [SerializeField]
    private GameObject m_ExplosivePoint;            // 爆発物の設置ポイント
    [SerializeField]
    private GameObject m_Explosive;                 // 爆発物のプレハブ
    bool m_explosive_set;                           // 爆発物を設置したか

    //ボムスポーン(片岡実装)
    private GameObject m_BomSpawn;

    // グレネードランチャーのモデル
    [SerializeField]
    private GameObject m_Launcher;

    private AudioClip playerSe_attack_;
    private AudioClip playerSe_walk_;
    private AudioClip playerSe_die_;
    private AudioSource[] playerSe_;

    private float test;

    private bool isDse;

    private GameObject timer_;


    // Use this for initialization
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();

        m_Animator = GetComponent<Animator>();

        playerSe_ = GetComponents<AudioSource>();
        playerSe_attack_ = playerSe_[0].clip;
        playerSe_walk_ = playerSe_[1].clip;
        playerSe_die_ = playerSe_[2].clip;

        m_State = PlayerState.StartFall;
        m_CurrentSpeedLimit = m_MaxSpeed;
        m_CurrentBrakePower = m_BrakePower;
        m_CurrentRotateSpeed = m_RotateSpeed;
        m_PrevPosition = transform.position;

        m_IsDash = false;
        m_IsAiming = false;
        m_IsCreeping = false;
        m_IsDead = false;
        m_IsInvincible = false;
        m_IsShot = false;

        m_IsStartFall = true;
        m_isClear = false;

        m_current_speed = 0.0f;

        m_BuildingNear = null;
        m_explosive_set = false;

        //片岡実装
        m_BomSpawn = GameObject.FindGameObjectWithTag("BomSpawn");

        //矢野実装
        s_Heri = GameObject.FindGameObjectWithTag("StartHeri");

        m_Animator.speed = 0f;

        t = 0f;

        test = 0f;

        if (m_BomSpawn == null)
        {
            //Debug.Log("エラー発生したので終了します");
            //Debug.Log("場所：PlayerController.cs");
            //Debug.Log("Error Log：擲弾オブジェクトが存在しない");
            //Application.Quit();
        }

        if (m_Launcher == null)
        {
            //Debug.Log("エラー発生したので終了します");
            //Debug.Log("場所：PlayerController.cs");
            //Debug.Log("Error Log：ランチャーのモデルが存在しない");
            //Application.Quit();
        }

        if (m_Explosive == null)
        {
            //Debug.Log("エラー発生したので終了します");
            //Debug.Log("場所：PlayerController.cs");
            //Debug.Log("Error Log：爆発物のプレハブが存在しない");
            //Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Timer"))
            timer_ = GameObject.FindGameObjectWithTag("Timer");

        if (timer_ != null)
        {
            if (timer_.GetComponent<Timer>().GetTimer() <= 0f)
                return;
        }

        // プレイヤーの状態に応じて処理を行う
        switch (m_State)
        {
            //ヘリから降下
            case PlayerState.StartFall:
                Fall();
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = false;
                break;
            // 通常
            case PlayerState.Normal:
                Normal();
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = false;
                break;
            // 照準中
            case PlayerState.Aiming:
                Aiming();
                m_IsAiming = true;
                m_IsCreeping = false;
                m_IsDead = false;
                break;
            // 匍匐
            case PlayerState.Creeping:
                Creeping();
                m_IsAiming = false;
                m_IsCreeping = true;
                m_IsDead = false;
                break;
            // 乗り越える
            case PlayerState.Passing:
                Passing();
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = false;
                break;
            // 死亡
            case PlayerState.Dead:
                Dead();
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = true;
                break;
            // 爆発物設置
            case PlayerState.Setting:
                Setting();
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = false;
                break;
            // 飛ばされる
            case PlayerState.BlewUp:
                BlewUp();
                m_IsAiming = false;
                m_IsCreeping = false;
                m_IsDead = false;
                break;
            // 飛ばされて死亡
            case PlayerState.BlewUpDead:
                BlewUpDead();
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

        // アニメーターにプレイヤーの状態を知らせる
        if (m_State != PlayerState.StartFall)
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

        // 無敵状態にする（デバッグ、デモ用）
        if (Input.GetKeyDown("space"))
        {
            m_IsInvincible = !m_IsInvincible;

            Debug.Log("無敵状態：" + m_IsInvincible);
        }
        /*
        Debug.Log("前方に障害物があるか：" + IsObjectInDistance());
        Debug.Log("乗り越えられる障害物であるか：" + IsObjectCanPass());
        Debug.Log("障害物がある方向：" + ObjectAngle());
        Debug.Log("障害物に向く角度：" + CollisionAngle());
        Debug.Log("乗り越えるのか：" + CanPass());
        */
    }

    // Capsule Colliderの接触判定
    public void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.name);

        if (m_IsInvincible) return;

        // 敵ロボットと接触したら死亡
        if (other.GetComponent<RobotDamage>() != null
            ||
            other.gameObject.tag == "RobotArmAttack"
            ||
            other.gameObject.tag == "RobotBeam"
            ||
            other.gameObject.tag == "Missile"
            ||
            other.gameObject.tag == "ExplosionCollision"
            ||
            other.gameObject.tag == "BeamCollide")
        {
            m_State = PlayerState.Dead;
            playerSe_[1].Stop();
        }

        // 倒壊中のビルと接触したら死亡
        if ((other.tag == "TowerCollision" && other.GetComponent<tower_collide>().Get_CollideFlag())
            || (other.tag == "DebrisCollision" && !other.GetComponent<DebrisGround>().Hit_Ground())
            || other.gameObject.name == "DeathCollide")
        {
            m_State = PlayerState.Dead;
            playerSe_[1].Stop();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        // 倒壊しているビルと隣接している間、Aボタンを押すと、爆発物を設置
        // Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Break_Tower_Can_Break" && m_State == PlayerState.Normal && Input.GetButtonDown("BombSet"))
        {
            m_BuildingNear = other.gameObject;
            // Debug.Log("爆発物を設置する");

            m_setting_time = 1.0f;
            m_State = PlayerState.Setting;
        }
    }

    void Fall()
    {
        if (transform.position.y <= 0.7f
            || s_Heri == null)
        {
            m_Animator.speed = 1;
            m_IsStartFall = false;


            t += 2.0f * Time.deltaTime;
            if (t >= 1.6f)
            {
                // グレネードランチャーを表示し、通常状態に移行（Ho実装、2017/12/11）
                m_Launcher.SetActive(true);
                m_State = PlayerState.Normal;

                if (!m_isClear)
                {
                    if (rope_destibation_.Count != 0)
                    {
                        Destroy(rope_destibation_[3]);
                        rope_destibation_.RemoveAt(3);
                        Destroy(rope_destibation_[2]);
                        rope_destibation_.RemoveAt(2);
                        Destroy(rope_destibation_[1]);
                        rope_destibation_.RemoveAt(1);
                        Destroy(rope_destibation_[0]);
                        rope_destibation_.RemoveAt(0);
                    }

                    m_isClear = true;
                }
            }
            transform.parent = null;
        }

        if (s_Heri != null
            && s_Heri.GetComponent<PlayStartHeri>().Get_StopFlag() == true)
        {
            if (t <= 0.4f)
                for (int i = 0; i < rope_destibation_.Count; i++)
                    rope_destibation_[i].transform.position -= transform.up / 4f;

            if (m_IsStartFall)
                transform.position -= transform.up / 4f;
        }
    }

    void OnAnimatorIK()
    {
        if (m_State == PlayerState.StartFall
            &&
            (rope_destibation_[0] != null
            && rope_destibation_[1] != null))
        {
            m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            m_Animator.SetIKPosition(AvatarIKGoal.LeftHand, rope_destibation_[0].transform.position);

            m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
            m_Animator.SetIKPosition(AvatarIKGoal.RightHand, rope_destibation_[1].transform.position);
            m_Animator.SetIKRotation(AvatarIKGoal.RightHand, rope_destibation_[1].transform.rotation);

            m_Animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1f);
            m_Animator.SetIKHintPosition(AvatarIKHint.LeftElbow, rope_destibation_[2].transform.position);

            m_Animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);
            m_Animator.SetIKHintPosition(AvatarIKHint.RightElbow, rope_destibation_[3].transform.position);
        }
    }

    // 通常時の処理
    void Normal()
    {
        // 通常時の移動処理
        NormalMove();

        // RBボタンを押すとダッシュ
        // m_IsDash = (Input.GetAxis("Dash") > 0.5f) ? true : false;
        m_IsDash = (Input.GetButton("Dash")) ? true : false;

        // LTボタンを押すとボム投げ状態に
        // if (Input.GetButton("Aim"))
        if (Input.GetAxis("Aim") > 0.5f)
        {
            m_State = PlayerState.Aiming;
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
        float axisHorizontal = Input.GetAxisRaw("Horizontal_L");    // x軸（左右）
        float axisVertical = Input.GetAxisRaw("Vertical_L");        // z軸（上下）

        // 加速度、速度制限、ブレーキ速度、回転速度、および障害物探知距離の変更
        // 加速度
        float accel;
        accel = (m_IsDash) ? accel = m_AccelPowerDash : accel = m_AccelPower;
        // 速度制限、ブレーキ速度と回転速度
        if (m_IsDash)
        {
            playerSe_[1].pitch = 2.4f;
            m_CurrentSpeedLimit = m_MaxSpeedDash;
            m_CurrentBrakePower = m_BrakePowerDash;
            m_CurrentRotateSpeed = m_RotateSpeedDash;
            m_CurrentRayDistance = m_RayDistance_dash;
        }
        else
        {
            playerSe_[1].pitch = 1.2f;
            if (m_CurrentSpeedLimit > m_MaxSpeed)
            {
                m_CurrentSpeedLimit -= 0.2f;
            }
            m_CurrentBrakePower = m_BrakePower;
            if (m_CurrentRotateSpeed > m_RotateSpeed)
            {
                m_CurrentRotateSpeed -= 0.1f;
            }
            m_CurrentRayDistance = m_RayDistance_walk;
        }

        // 減速する（入力が無い場合）
        if (axisHorizontal == 0 && axisVertical == 0)
        {
            m_Speed = Mathf.Max(m_Speed - m_CurrentBrakePower * Time.deltaTime, 0);
            playerSe_[1].Stop();
        }
        else if (axisHorizontal != 0 || axisVertical != 0)
        {
            if (!playerSe_[1].isPlaying)
            {
                playerSe_[1].PlayOneShot(playerSe_walk_);
            }
        }

        // 接地状態であれば加速可能
        if (m_Controller.isGrounded)
        {
            // 加速する
            m_Speed += accel * Time.deltaTime;

            // 速度を制限する
            m_Speed = Mathf.Clamp(m_Speed, 0.0f, m_CurrentSpeedLimit);
        }

        // 入力がある場合
        Vector2 input = new Vector2(axisHorizontal, axisVertical);
        if (!(input.magnitude < 0.1))
        {
            // プレイヤーは入力した方向に向ける
            Vector3 new_direction = (forward * Input.GetAxis("Vertical_L") + Camera.main.transform.right * Input.GetAxis("Horizontal_L"));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new_direction), m_CurrentRotateSpeed * Time.deltaTime);
            
            // 乗り越えられる障害物がある場合、乗り越え処理を行う
            if (CanPass())
            {
                // 乗り越える前の速度を記憶
                m_SpeedBeforePass = m_Controller.velocity.magnitude;
                m_State = PlayerState.Passing;
            }
        }

        // 移動処理
        Vector3 velocity = forward * axisVertical * m_Speed + Camera.main.transform.right * axisHorizontal * m_Speed;
        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        // キャラクターコントローラーに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);

        // アニメーターに命令して、アニメーションを再生する
        // プレイヤー現在の移動量を取得
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

        // BomSpawnにバグ発生、一時コメントアウトする（2018-02-02）
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
            playerSe_[0].PlayOneShot(playerSe_attack_);
        }

        // LTボタンを放すと通常状態に戻る
        // if (!Input.GetButton("Aim"))
        if (!(Input.GetAxis("Aim") > 0.5f))
        {
            m_BomSpawn.GetComponent<BomSpawn>().SetDrawLine(false);
            m_State = PlayerState.Normal;
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
            else
            {
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
            else
            {
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
            m_State = PlayerState.Normal;
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
            else
            {
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
            else
            {
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

    // 乗り越え処理
    void Passing()
    {
        // グレネードランチャーの表示を消す
        m_Launcher.SetActive(false);
        // 乗り越えモーションを再生
        m_Animator.Play("Passing");

        // 1回ジャンプする
        if (!m_IsPassed)
        {
            m_VelocityY = m_JumpPower;
            m_IsPassed = true;
        }

        // 移動処理
        Vector3 velocity = transform.forward * m_SpeedBeforePass;
        // 重力加速度を加算
        m_VelocityY -= m_Gravity * Time.deltaTime;
        // y軸方向の移動量を加味する
        velocity.y = m_VelocityY;
        m_Controller.Move(velocity * Time.deltaTime);

        // 着地するか、アニメーションが終了すると、通常状態に戻る
        AnimatorStateInfo AniInfo;     // アニメーションの状態
        AniInfo = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        if (m_Controller.isGrounded || AniInfo.normalizedTime <= 0.9f)
        {
            // グレネードランチャーを表示
            m_Launcher.SetActive(true);
            m_State = PlayerState.Normal;
            m_IsPassed = false;
        }
    }

    // プレイヤー前方の障害物を探知
    bool IsObjectInDistance()
    {
        Ray ray = new Ray(m_RayPoint.position, transform.forward);
        RaycastHit hit;

        return (Physics.Raycast(ray, out hit, m_CurrentRayDistance));
    }

    // 乗り越えられる障害物であるか
    bool IsObjectCanPass()
    {
        Ray ray = new Ray(m_RayPoint.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_CurrentRayDistance))
        {
            if (hit.collider.gameObject.tag == "GuardRail") return true;
        }

        return false;
    }

    // 障害物からのプレイヤーの方向
    Vector3 ObjectNormal()
    {
        Ray ray1 = new Ray(m_RayPoint.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray1, out hit, m_CurrentRayDistance))
        {
            Debug.Log(Vector3.Angle(hit.transform.forward, hit.normal));
            return hit.normal;
        }

        return Vector3.zero;
    }

    float ObjectAngle()
    {
        Ray ray1 = new Ray(m_RayPoint.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray1, out hit, m_CurrentRayDistance))
        {
            return Vector3.Angle(hit.transform.forward, hit.normal);
        }

        return 90.0f;
    }

    // プレイヤーと障害物の角度を取得
    Vector3 CollisionAngle()
    {
        Ray ray = new Ray(m_RayPoint.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_CurrentRayDistance))
        {
            return Quaternion.FromToRotation(transform.forward, hit.normal).eulerAngles;
        }

        return Vector3.zero;
    }

    // 障害物を乗り越えるかどうを判定
    bool CanPass()
    {
        // 障害物は近くにあるか
        if (!IsObjectInDistance()) return false;
        // 障害物は乗り越えられるものなのか
        if (!IsObjectCanPass()) return false;
        // プレイヤーは障害物の正面か背面にいるか
        // if (ObjectNormal().z != 0.0f) return false;
        if (ObjectAngle() != 0.0f && ObjectAngle() != 180.0f) return false;
        // プレイヤーは障害物の真正面にいるか
        if (CollisionAngle().y > 200.0f || CollisionAngle().y < 160.0f) return false;

        return true;
    }

    // 乗り越え処理（改）
    void Passing_v2()
    {
        // グレネードランチャーの表示を消す
        m_Launcher.SetActive(false);
        // 乗り越えモーションを再生
        m_Animator.Play("Passing");

        // 移動処理
        Vector3 velocity = transform.forward * m_SpeedBeforePass;
        m_Controller.Move(velocity * Time.deltaTime);

        // 乗り越えアニメーションが終了すると、通常状態に戻る
        AnimatorStateInfo AniInfo;     // アニメーションの状態
        AniInfo = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        if (m_passing_time <= 0 && AniInfo.normalizedTime <= 0.9f)
        {
            // グレネードランチャーを表示
            m_Launcher.SetActive(true);
            // 通常状態に戻る
            m_State = PlayerState.Normal;
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
            playerSe_[2].PlayOneShot(playerSe_die_);
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

    // 爆発物設置の処理
    void Setting()
    {
        if (m_BuildingNear == null)
            return;

        // ビルに向かって爆発物を設置
        transform.LookAt(m_BuildingNear.transform);
        // グレネードランチャーの表示を消す
        m_Launcher.SetActive(false);
        // 設置モーションを再生
        m_Animator.Play("ExplosiveSet");

        // 爆発物を生成
        if (m_setting_time <= 0.5f && m_explosive_set == false)
        {
            Instantiate(m_Explosive, m_ExplosivePoint.transform.position, transform.rotation);
            m_explosive_set = true;
        }

        // アニメーションが終了すると、通常状態に戻る
        if (m_setting_time <= 0)
        {
            // グレネードランチャーを表示
            m_Launcher.SetActive(true);
            // 通常状態に戻る
            m_State = PlayerState.Normal;
            m_explosive_set = false;
        }

        m_setting_time -= Time.deltaTime;
    }

    // 飛ばされている間の処理
    void BlewUp()
    {

    }

    // 飛ばされて死亡の処理
    void BlewUpDead()
    {

    }
}
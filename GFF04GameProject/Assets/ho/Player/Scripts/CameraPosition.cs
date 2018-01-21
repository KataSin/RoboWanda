using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：カメラ座標制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

// カメラモード
enum PlayerCameraMode
{
    None,
    Landing,    // 着地
    Normal,     // 通常
    Aim,        // 照準
    Dead,       // 死亡
    Event,      // イベント
}

enum EventCameraState
{
    None,
    Jeep,
    LeadJeep,
    Bomber,
    Tank,
    Tank2,
    BossDead,
}

// カメラ距離
enum CameraDistance
{
    VeryClose,  // とても近い
    Close,      // 近い
    Normal,     // 普通
    Far         // 遠い
}

public class CameraPosition : MonoBehaviour
{
    // カメラ座標関連
    // とても近い時の位置座標
    [SerializeField]
    private float m_VeryClosePositionX = 1.0f;
    [SerializeField]
    private float m_VeryClosePositionY = 1.9f;
    [SerializeField]
    private float m_VeryClosePositionZ = -2.0f;

    // 近い時の位置座標
    [SerializeField]
    private float m_ClosePositionX = 0.0f;
    [SerializeField]
    private float m_ClosePositionY = 2.5f;
    [SerializeField]
    private float m_ClosePositionZ = -3.0f;

    // 通常時の位置座標
    [SerializeField]
    private float m_NormalPositionX = 0.0f;
    [SerializeField]
    private float m_NormalPositionY = 2.5f;
    [SerializeField]
    private float m_NormalPositionZ = -5.0f;

    // 遠い時の位置座標
    [SerializeField]
    private float m_FarPositionX = 0.0f;
    [SerializeField]
    private float m_FarPositionY = 6.0f;
    [SerializeField]
    private float m_FarPositionZ = -15.0f;

    // イベントカメラの位置座標
    private float m_EventPositionX;
    private float m_EventPositionY;
    private float m_EventPositionZ;

    // 元のカメラ座標（イベントモードから通常モードに戻るとき、カメラの位置を戻す用）
    float m_PrevPositionX;
    float m_PrevPositionY;
    float m_PrevPositionZ;

    [SerializeField]
    private float m_Speed = 10.0f;              // カメラの移動速度（通常モード、距離変更時）
    [SerializeField]
    private float m_EventSpeed = 10.0f;         // イベントカメラの移動速度

    private GameObject m_Player;                // プレイヤー
    private bool m_IsAiming = false;            // プレイヤーは照準状態であるか

    [SerializeField]
    private PlayerCameraMode m_Mode;            // カメラモード
    private CameraDistance m_Distance;          // カメラとプレイヤーの距離
    private int m_DistanceSelect;
    bool m_IsTriggered;                         // 十字キーの操作判定

    private float t, t1;
    private float m_intervalTimer;

    private Vector3 m_origin_pos;
    private Vector3 m_deadBefore_pos;
    private Quaternion m_origin_rotation;

    private Vector3 m_eventB_pos;
    private Quaternion m_eventB_rotation;

    private bool isDeadFinish;

    [SerializeField]
    private GameObject m_Prediction;             // カメラの予測座標

    [SerializeField]
    private EventCameraState m_EMode;           //イベントカメラモード

    private GameObject jeepMana_;

    private GameObject[] jeeps_;

    private GameObject bomber_;

    private GameObject tankHeri_;

    private GameObject tank_;

    private Vector3 m_tankHeri_pos;

    private Vector3 m_tank_pos;

    private float m_test;

    private float m_T2;

    private bool isM0;

    private bool isM2;

    private bool isMAllClear;

    private bool isBlack;

    private bool isEventEnd;

    private GameObject boss_;
    private GameObject bossPivot_;
    private float m_T3;

    private float m_black_t3;

    [SerializeField]
    private GameObject briefing_;

    private float m_T4;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Player != null)
        {
            m_IsAiming = m_Player.GetComponent<PlayerController>().IsAiming();
        }

        m_EventPositionX = 0.0f;
        m_EventPositionY = 0.0f;
        m_EventPositionZ = 0.0f;

        m_DistanceSelect = 2;
        m_IsTriggered = false;

        t = 0f;

        m_intervalTimer = 0f;

        m_origin_pos = transform.localPosition;
        m_origin_rotation = transform.localRotation;

        isDeadFinish = false;

        m_eventB_pos = transform.localPosition;
        m_eventB_rotation = transform.localRotation;

        if (!GameObject.FindGameObjectWithTag("JeepManager")
            ||
            !GameObject.FindGameObjectWithTag("StartHeri"))
        {
            m_Mode = PlayerCameraMode.Normal;
        }

        else if (GameObject.FindGameObjectWithTag("JeepManager"))
        {
            jeepMana_ = GameObject.FindGameObjectWithTag("JeepManager");
            m_Mode = PlayerCameraMode.Landing;
        }

        m_test = 0f;
        m_T2 = 0f;
        m_T3 = 0f;
        m_T4 = 0f;

        isM0 = false;
        isM2 = false;
        isMAllClear = false;
        isBlack = false;
        isEventEnd = true;

        if (m_Prediction != null)
            m_Prediction.transform.localPosition = transform.localPosition;

        if (GameObject.FindGameObjectWithTag("Robot"))
            boss_ = GameObject.FindGameObjectWithTag("Robot");
        if (GameObject.FindGameObjectWithTag("RobotCameraP"))
            bossPivot_ = GameObject.FindGameObjectWithTag("RobotCameraP");
    }

    // Update is called once per frame
    void Update()
    {
        if (boss_ != null)
        {
            if (boss_.GetComponent<RobotManager>().GetRobotHP() <= 0f)
            {
                m_Mode = PlayerCameraMode.Event;
                m_EMode = EventCameraState.BossDead;
            }
        }

        // カメラの状態に応じて処理を行う
        switch (m_Mode)
        {
            case PlayerCameraMode.None:
                break;
            case PlayerCameraMode.Landing:
                LandingMode();
                break;
            case PlayerCameraMode.Normal:
                NormalMode();
                break;
            case PlayerCameraMode.Aim:
                AimMode();
                break;
            case PlayerCameraMode.Dead:
                DeadMode();
                break;
            case PlayerCameraMode.Event:
                EventMode();
                break;
            default:
                NormalMode();
                break;
        }

        // 照準状態を更新
        m_IsAiming = m_Player.GetComponent<PlayerController>().IsAiming();
    }

    //プレイヤー着地時の挙動
    void LandingMode()
    {
        if (m_Player.GetComponent<PlayerController>().GetPlayerState() == 0)
        {
            transform.LookAt(m_Player.transform.position + new Vector3(0f, 0.6f, 0f));
            m_origin_rotation = transform.localRotation;
        }

        else if (m_Player.GetComponent<PlayerController>().GetPlayerState() != 0)
        {
            transform.localPosition = Vector3.Lerp(m_origin_pos, new Vector3(0f, 1.5f, -5f), t / 1f);
            transform.localRotation = Quaternion.Slerp(m_origin_rotation, Quaternion.identity, t / 1f);

            if (t >= 1f)
                m_Mode = PlayerCameraMode.Normal;

            t += 1.0f * Time.deltaTime;
        }
    }

    // 通常時の挙動
    void NormalMode()
    {
        // プレイヤーが死んだら、死亡モードに移行
        if (m_Player.GetComponent<PlayerController>().GetPlayerState() == 4)
        {
            m_Mode = PlayerCameraMode.Dead;
            t = 0f;
            m_deadBefore_pos = transform.position;
            return;
        }

        // 照準モードに移行
        if (m_IsAiming)
        {
            m_Mode = PlayerCameraMode.Aim;
        }

        // カメラの距離を変更
        switch (m_DistanceSelect)
        {
            case 0:
                m_Distance = CameraDistance.VeryClose;
                break;
            case 1:
                m_Distance = CameraDistance.Close;
                break;
            case 2:
                m_Distance = CameraDistance.Normal;
                break;
            case 3:
                m_Distance = CameraDistance.Far;
                break;
            default:
                m_Distance = CameraDistance.Normal;
                break;
        }

        // 十字キーの入力が無い場合、操作判定を解除
        if (Input.GetAxisRaw("Camera_Distance") == 0)
        {
            m_IsTriggered = false;
        }
        // 十字キーでカメラの距離を選択
        if (m_IsTriggered == false)
        {
            // 上ボタン
            if (Input.GetAxisRaw("Camera_Distance") < -0.9)
            {
                m_IsTriggered = true;
                m_DistanceSelect = m_DistanceSelect + 1;
            }
            // 下ボタン
            if (Input.GetAxisRaw("Camera_Distance") > 0.9)
            {
                m_IsTriggered = true;
                m_DistanceSelect = m_DistanceSelect - 1;
            }
        }
        m_DistanceSelect = Mathf.Clamp(m_DistanceSelect, 0, 3);

        // カメラの座標を変更
        Vector3 new_position;
        switch (m_Distance)
        {
            case CameraDistance.VeryClose:
                new_position.x = m_VeryClosePositionX;
                new_position.y = m_VeryClosePositionY;
                new_position.z = m_VeryClosePositionZ;
                break;
            case CameraDistance.Close:
                new_position.x = m_ClosePositionX;
                new_position.y = m_ClosePositionY;
                new_position.z = m_ClosePositionZ;
                break;
            case CameraDistance.Normal:
                new_position.x = m_NormalPositionX;
                new_position.y = m_NormalPositionY;
                new_position.z = m_NormalPositionZ;
                break;
            case CameraDistance.Far:
                new_position.x = m_FarPositionX;
                new_position.y = m_FarPositionY;
                new_position.z = m_FarPositionZ;
                break;
            default:
                new_position.x = m_NormalPositionX;
                new_position.y = m_NormalPositionY;
                new_position.z = m_NormalPositionZ;
                break;
        }

        // カメラの予測座標を更新（相対座標を使用）
        m_Prediction.transform.localPosition = new_position;

        // カメラがフィールドや障害物に透過しないようにする
        // カメラの最終座標を計算してから移動させる
        Ray ray = new Ray(m_Player.transform.position + Vector3.up, m_Prediction.transform.position - m_Player.transform.position - Vector3.up);
        float distance = Vector3.Distance(m_Player.transform.position, m_Prediction.transform.position);
        // Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, LayerMask.GetMask("Stage"))
            &&
            m_EMode != EventCameraState.BossDead)
        {
            // Debug.Log("壁に遮られた");
            Vector3 hit_position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.3f, hitInfo.point.z);
            transform.position = Vector3.Lerp(transform.position, hit_position, m_Speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, m_Prediction.transform.position, m_Speed * Time.deltaTime);
        }
    }

    // 照準時の挙動
    void AimMode()
    {
        // 通常モードに移行
        if (!m_IsAiming)
        {
            m_Mode = PlayerCameraMode.Normal;
        }

        // カメラの距離を変更
        switch (m_DistanceSelect)
        {
            case 0:
                m_Distance = CameraDistance.VeryClose;
                break;
            case 1:
                m_Distance = CameraDistance.Close;
                break;
            case 2:
                m_Distance = CameraDistance.Normal;
                break;
            case 3:
                m_Distance = CameraDistance.Far;
                break;
            default:
                m_Distance = CameraDistance.Normal;
                break;
        }

        // 十字キーの入力が無い場合、操作判定を解除
        if (Input.GetAxisRaw("Camera_Distance") == 0)
        {
            m_IsTriggered = false;
        }
        // 十字キーでカメラの距離を選択
        if (m_IsTriggered == false)
        {
            // 上ボタン
            if (Input.GetAxisRaw("Camera_Distance") < -0.9)
            {
                m_IsTriggered = true;
                m_DistanceSelect = m_DistanceSelect + 1;
            }
            // 下ボタン
            if (Input.GetAxisRaw("Camera_Distance") > 0.9)
            {
                m_IsTriggered = true;
                m_DistanceSelect = m_DistanceSelect - 1;
            }
        }
        m_DistanceSelect = Mathf.Clamp(m_DistanceSelect, 0, 3);

        // カメラが一番遠い座標にあったら
        if (m_DistanceSelect == 3)
        {
            // カメラの座標を変更
            Vector3 new_position;
            new_position.x = m_FarPositionX;
            new_position.y = m_FarPositionY;
            new_position.z = m_FarPositionZ;

            // カメラの予測座標を更新（相対座標を使用）
            m_Prediction.transform.localPosition = new_position;

            // カメラがフィールドや障害物に透過しないようにする
            // カメラの最終座標を計算してから移動させる
            Ray ray = new Ray(m_Player.transform.position + Vector3.up, m_Prediction.transform.position - m_Player.transform.position - Vector3.up);
            float distance = Vector3.Distance(m_Player.transform.position, m_Prediction.transform.position);
            // Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, distance, LayerMask.GetMask("Stage"))
                &&
                m_EMode != EventCameraState.BossDead)
            {
                // Debug.Log("壁に遮られた");
                Vector3 hit_position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.3f, hitInfo.point.z);

                float distance2 = Vector3.Distance(m_Player.transform.position, hit_position);
                if (distance2 < 12.0f)
                {
                    Vector3 final_position;
                    final_position.x = m_VeryClosePositionX;
                    final_position.y = m_VeryClosePositionY;
                    final_position.z = m_VeryClosePositionZ;

                    transform.localPosition = Vector3.Lerp(transform.localPosition, final_position, m_Speed / 4 * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, hit_position, m_Speed / 4 * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, m_Prediction.transform.position, m_Speed / 4 * Time.deltaTime);
            }
        }
        else
        {
            // カメラの座標を変更
            Vector3 new_position;
            new_position.x = m_VeryClosePositionX;
            new_position.y = m_VeryClosePositionY;
            new_position.z = m_VeryClosePositionZ;

            // カメラの予測座標を更新（相対座標を使用）
            m_Prediction.transform.localPosition = new_position;

            // カメラがフィールドや障害物に透過しないようにする
            // カメラの最終座標を計算してから移動させる
            Ray ray = new Ray(m_Player.transform.position + Vector3.up, m_Prediction.transform.position - m_Player.transform.position - Vector3.up);
            float distance = Vector3.Distance(m_Player.transform.position, m_Prediction.transform.position);
            // Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, distance, LayerMask.GetMask("Stage"))
                &&
                m_EMode != EventCameraState.BossDead)
            {
                // Debug.Log("壁に遮られた");
                Vector3 hit_position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.3f, hitInfo.point.z);
                transform.position = Vector3.Lerp(transform.position, hit_position, m_Speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, m_Prediction.transform.position, m_Speed * Time.deltaTime);
            }
        }
    }

    //死亡時の挙動
    void DeadMode()
    {
        if (t < 3f)
            transform.LookAt(m_Player.transform.position + m_Player.transform.forward);

        if (m_intervalTimer >= 2f)
        {
           transform.position =
                Vector3.Lerp(
                    m_deadBefore_pos,
                    m_Player.transform.position + m_Player.transform.forward + new Vector3(0f, 3f, 0f),
                    t / 3f
                    );

            if (t >= 3f)
                isDeadFinish = true;

            t += 1.0f * Time.deltaTime;
        }

        m_intervalTimer += 1.0f * Time.deltaTime;
    }

    // イベントカメラの挙動
    void EventMode()
    {
        switch (m_EMode)
        {
            case EventCameraState.None:
                EventStartMode();
                break;
            case EventCameraState.Jeep:
                JeepMode();
                break;
            case EventCameraState.LeadJeep:
                LJeepMode();
                break;
            case EventCameraState.Bomber:
                BomberMode();
                EventSkip();
                break;
            case EventCameraState.Tank:
                TankMode();
                EventSkip();
                break;
            case EventCameraState.Tank2:
                Tank2Mode();
                EventSkip();
                break;
            case EventCameraState.BossDead:
                BossDeadMode();
                break;
        }


        //Vector3 new_position = new Vector3(m_EventPositionX, m_EventPositionY, m_EventPositionZ);

        //// カメラの位置を更新
        //transform.position = Vector3.Lerp(transform.position, new_position, m_EventSpeed * Time.deltaTime);
    }

    private void EventStartMode()
    {
        if (jeepMana_ != null)
        {
            transform.position = jeepMana_.transform.position;
            jeeps_ = GameObject.FindGameObjectsWithTag("Jeep");
        }
        m_EMode = EventCameraState.Jeep;
        transform.rotation = Quaternion.Euler(-7.929f, 62.404f, 0.557f);
    }

    private void JeepMode()
    {
        if (jeeps_[jeeps_.Length - 1].transform.position.x <= transform.position.x + 3f)
        {
            isBlack = true;
        }

        if (jeeps_[jeeps_.Length - 1].transform.position.x <= transform.position.x - 3f
            &&
            m_black_t3 >= 1.0f)
        {
            m_EMode = EventCameraState.LeadJeep;
        }
    }

    private void LJeepMode()
    {
        if (briefing_.GetComponent<BriefingManager>().Get_TextState() == 11
            && briefing_ != null)
        {
            isEventEnd = true;

            if (m_T4 >= 2f)
            {
                m_Mode = PlayerCameraMode.Landing;
                m_EMode = EventCameraState.None;

                transform.localPosition = m_eventB_pos;
                transform.localRotation = m_eventB_rotation;
            }

            m_T4 += 1.0f * Time.deltaTime;
        }

        if (!isEventEnd)
            transform.position = jeeps_[0].transform.position + (-jeeps_[0].transform.forward * 6f) + (jeeps_[0].transform.up * 4f);

        transform.LookAt(jeeps_[0].transform.position + jeeps_[0].transform.forward + (jeeps_[0].transform.up * 2.2f));

        if ((m_test >= 2f || Input.GetKeyDown(KeyCode.U))
            && !isMAllClear)
        {
            isM0 = true;

            if (m_test >= 4f)
            {
                //m_EMode = EventCameraState.Bomber;
                //bomber_ = GameObject.FindGameObjectWithTag("Bomber");
                //m_test = 0f;
            }

            //EventSkip();
        }

        else if (isMAllClear && m_test >= 4f)
        {
            isEventEnd = true;
            if (m_test >= 12f)
            {
                m_Mode = PlayerCameraMode.Landing;
                m_EMode = EventCameraState.None;

                transform.localPosition = m_eventB_pos;
                transform.localRotation = m_eventB_rotation;
            }
        }

        m_test += 1.0f * Time.deltaTime;
    }

    private void BomberMode()
    {
        transform.LookAt(bomber_.transform.position + (-bomber_.transform.forward * 14f));
        transform.position = bomber_.transform.position
            + new Vector3(bomber_.transform.right.x * 34f, 7.6f, bomber_.transform.forward.z * 18f);

        if (m_test >= 6f)
        {
            m_EMode = EventCameraState.Tank;
            tankHeri_ = GameObject.FindGameObjectWithTag("TankHeri");
            m_tankHeri_pos = tankHeri_.transform.position;
            tank_ = GameObject.FindGameObjectWithTag("Tank");
            m_tank_pos = tank_.transform.position;
            m_test = 0f;
        }

        m_test += 1.0f * Time.deltaTime;
    }

    private void TankMode()
    {
        if (!isM2)
        {
            transform.rotation = Quaternion.Euler(-10f, -22.7f, 0f);
            transform.position = m_tankHeri_pos + new Vector3(tankHeri_.transform.right.x * 12f, -5f, -tankHeri_.transform.forward.z * 40f);
        }

        if (m_test >= 6f)
        {
            isM2 = true;
            if (isM2)
            {
                transform.position =
                    Vector3.Lerp(
                        m_tankHeri_pos + new Vector3(tankHeri_.transform.right.x * 12f, -5f, -tankHeri_.transform.forward.z * 40f),
                        m_tank_pos + new Vector3(-tank_.transform.right.x * 8f, 10f, tank_.transform.forward.z * 24f),
                        m_T2 / 2f
                        );

                transform.rotation =
                    Quaternion.Slerp(Quaternion.Euler(-10f, -22.7f, 0f), Quaternion.Euler(18f, -22.7f, 0f), m_T2 / 2f);

                m_T2 += 1.0f * Time.deltaTime;

                if (m_T2 >= 2f)
                {
                    m_test = 0f;
                    m_EMode = EventCameraState.Tank2;
                }
            }
        }

        m_test += 1.0f * Time.deltaTime;
    }

    private void Tank2Mode()
    {
        if (m_test >= 6f)
        {
            isMAllClear = true;
            m_test = 0f;
            m_EMode = EventCameraState.LeadJeep;
        }

        m_test += 1.0f * Time.deltaTime;
    }

    private void BossDeadMode()
    {
        transform.LookAt(bossPivot_.transform.position /*+ bossPivot_.transform.up * 10f*/);

        //float l_bossForward = boss_.transform.forward.z;
        //l_bossForward

        Vector3 l_bossCameraPos = bossPivot_.transform.position + (-boss_.transform.right * 20f) + (boss_.transform.forward * 20f);
        l_bossCameraPos.y = 7f;
        transform.position = l_bossCameraPos;

        //transform.position =
        //    new Vector3(
        //        bossPivot_.transform.position.x,
        //        10f,
        //        bossPivot_.transform.position.z + boss_.transform.forward.z * 30f
        //        );

        //transform.position = Vector3.Lerp(
        //    new Vector3(
        //        bossPivot_.transform.position.x,
        //        10f,
        //        bossPivot_.transform.position.z + boss_.transform.forward.z * 30f),
        //    new Vector3(
        //        bossPivot_.transform.position.x + -boss_.transform.right.x * 30f,
        //        10f,
        //        bossPivot_.transform.position.z + boss_.transform.forward.z * 20f),
        //    m_T3 / 20f
        //    );

        m_T3 += 1.0f * Time.deltaTime;
    }

    private void EventSkip()
    {
        if (Input.GetKeyDown(KeyCode.S)
            || Input.GetButtonDown("Submit"))
        {
            isM0 = true;
            isM2 = true;
            isMAllClear = true;

            m_test = 0f;

            m_EMode = EventCameraState.LeadJeep;
        }
    }

    // イベントカメラに移行
    public void ChangeEventMode(float position_x, float position_y, float position_z)
    {
        // 現在のカメラ座標を記録（相対座標を使用）
        m_PrevPositionX = transform.localPosition.x;
        m_PrevPositionY = transform.localPosition.y;
        m_PrevPositionZ = transform.localPosition.z;

        // 新しい座標を記録（実際の座標を使用）
        m_EventPositionX = position_x;
        m_EventPositionY = position_y;
        m_EventPositionZ = position_z;

        // イベントモードに移行
        m_Mode = PlayerCameraMode.Event;
    }

    // イベントカメラに移行（速度設定アリ）
    public void ChangeEventMode(float position_x, float position_y, float position_z, float camera_speed)
    {
        // 現在のカメラ座標を記録（相対座標を使用）
        m_PrevPositionX = transform.localPosition.x;
        m_PrevPositionY = transform.localPosition.y;
        m_PrevPositionZ = transform.localPosition.z;

        // 新しい座標を記録（実際の座標を使用）
        m_EventPositionX = position_x;
        m_EventPositionY = position_y;
        m_EventPositionZ = position_z;

        // 新しい速度を適用
        m_EventSpeed = camera_speed;

        // イベントモードに移行
        m_Mode = PlayerCameraMode.Event;
    }

    // イベントモードを解除し、通常モードに移行
    public void ChangeNormalMode()
    {
        if (m_Mode == PlayerCameraMode.Normal) return;

        Vector3 origin_position = new Vector3(m_PrevPositionX, m_PrevPositionY, m_PrevPositionZ);

        // カメラの位置を元に戻す（一瞬で戻る）
        transform.localPosition = origin_position;

        // 通常モードに移行
        m_Mode = PlayerCameraMode.Normal;
    }

    //現在のカメラモードの取得
    public int GetMode()
    {
        return (int)m_Mode;
    }

    public int GetEMode()
    {
        return (int)m_EMode;
    }

    //死亡カメラが終わったかどうかの取得
    public bool GetDeadFinish()
    {
        return isDeadFinish;
    }

    public float GetT()
    {
        return t;
    }

    public bool Get_M0Flag()
    {
        return isM0;
    }

    public bool Get_MAllFlag()
    {
        return isMAllClear;
    }

    public bool Get_BlackFlag()
    {
        return isBlack;
    }

    public bool Get_EventEnd()
    {
        return isEventEnd;
    }

    public void Set_BlackT3(float l_t3)
    {
        m_black_t3 = l_t3;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：カメラ位置制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

// カメラモード
enum PlayerCameraMode
{
    Landing,
    Normal,     // 通常
    Dead,
    Event,       // イベント
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
    /*// 相対位置を使用
    // 通常時の位置座標
    [SerializeField]
    private float m_NormalPositionX = 0.0f;     // 通常時のx軸位置
    [SerializeField]
    private float m_NormalPositionY = 2.5f;     // 通常時のy軸位置
    [SerializeField]
    private float m_NormalPositionZ = -5.0f;    // 通常時のz軸位置
    // 照準時の位置座標
    [SerializeField]
    private float m_AimingPositionX = 0.0f;     // 照準時のx軸位置
    [SerializeField]
    private float m_AimingPositionY = 2.5f;     // 照準時のy軸位置
    [SerializeField]
    private float m_AimingPositionZ = -5.0f;    // 照準時のz軸位置
    // 現在の位置座標
    float current_pos_X;                        // 現在のx軸座標
    float current_pos_Y;                        // 現在のy軸座標
    float current_pos_Z;                        // 現在のz軸座標

    [SerializeField]
    private float m_SpeedX = 0.0f;              // x軸座標の移動速度
    [SerializeField]
    private float m_SpeedY = 0.0f;              // y軸座標の移動速度
    [SerializeField]
    private float m_SpeedZ = 0.0f;              // z軸座標の移動速度

    [SerializeField]
    private float m_Speed = 0.0f;

    private GameObject m_Player;                // プレイヤー
    private bool m_IsAiming = false;            // プレイヤーは照準状態であるか*/

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

    private bool isDeadFinish;

    // Use this for initialization
    void Start()
    {
        /*m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Player != null)
        {
            m_IsAiming = m_Player.GetComponent<PlayerController>().IsAiming();
        }

        current_pos_X = m_NormalPositionX;
        current_pos_Y = m_NormalPositionY;
        current_pos_Z = m_NormalPositionZ;*/

        m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Player != null)
        {
            m_IsAiming = m_Player.GetComponent<PlayerController>().IsAiming();
        }

        m_EventPositionX = 0.0f;
        m_EventPositionY = 0.0f;
        m_EventPositionZ = 0.0f;

        m_Mode = PlayerCameraMode.Landing;
        m_DistanceSelect = 2;
        m_IsTriggered = false;

        t = 0f;

        m_intervalTimer = 0f;

        m_origin_pos = transform.localPosition;
        m_origin_rotation = transform.localRotation;

        isDeadFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*// プレイヤーの状態を取得
        if (m_Player != null)
        {
            m_IsAiming = m_Player.GetComponent<PlayerController>().IsAiming();
        }

        // 照準状態であれば
        if (m_IsAiming)
        {
            // 照準時の座標まで移動
            if (current_pos_X < m_AimingPositionX)
            {
                current_pos_X += m_SpeedX * Time.deltaTime;
            }
            if (current_pos_Y > m_AimingPositionY)
            {
                current_pos_Y -= m_SpeedY * Time.deltaTime;
            }
            if (current_pos_Z < m_AimingPositionZ)
            {
                current_pos_Z += m_SpeedZ * Time.deltaTime;
            }
        }
        else
        {
            // 通常時の座標まで移動
            if (current_pos_X > m_NormalPositionX)
            {
                current_pos_X -= m_SpeedX * Time.deltaTime;
            }
            if (current_pos_Y < m_NormalPositionY)
            {
                current_pos_Y += m_SpeedY * Time.deltaTime;
            }
            if (current_pos_Z > m_NormalPositionZ)
            {
                current_pos_Z -= m_SpeedZ * Time.deltaTime;
            }
        }

        // カメラの座標制限
        current_pos_X = Mathf.Clamp(current_pos_X, m_NormalPositionX, m_AimingPositionX);
        current_pos_Y = Mathf.Clamp(current_pos_Y, m_AimingPositionY, m_NormalPositionY);
        current_pos_Z = Mathf.Clamp(current_pos_Z, m_NormalPositionZ, m_AimingPositionZ);

        // カメラの位置を更新（相対位置を使用）
        transform.localPosition = new Vector3(current_pos_X, current_pos_Y, current_pos_Z);*/

        // カメラの距離を変更
        /*switch (m_DistanceSelect)
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
        // カメラの位置を更新（相対座標を使用）
        transform.localPosition = Vector3.Lerp(transform.localPosition, new_position, m_Speed * Time.deltaTime);

        // カメラがフィールドや障害物に透過しないようにする
        Ray ray = new Ray(m_Player.transform.position + Vector3.up, transform.position - m_Player.transform.position - Vector3.up);
        float distance = Vector3.Distance(m_Player.transform.position, transform.position);
        // Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo;

        if (transform.position.y <= 0.1f)
        {
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
            return;
        }

        if (Physics.Raycast(ray, out hitInfo, distance, LayerMask.GetMask("Stage")))
        {
            // Debug.Log("壁に遮られた");
            transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.1f, hitInfo.point.z);
        }*/

        // カメラの状態に応じて処理を行う
        switch (m_Mode)
        {
            case PlayerCameraMode.Landing:
                LandingMode();
                break;
            case PlayerCameraMode.Normal:
                NormalMode();
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
    }

    //プレイヤー着地時の挙動
    void LandingMode()
    {
        if (m_Player.GetComponent<PlayerController>().GetPlayerState() == 0)
        {
            transform.LookAt(m_Player.transform.position);
            m_origin_rotation = transform.localRotation;
        }

        else if (m_Player.GetComponent<PlayerController>().GetPlayerState() != 0)
        {
            t += 1.0f * Time.deltaTime;

            transform.localPosition = Vector3.Lerp(m_origin_pos, new Vector3(0f, 0.5f, -5f), t / 1f);
            transform.localRotation = Quaternion.Slerp(m_origin_rotation, Quaternion.identity, t / 1f);

            if (t >= 1f)
                m_Mode = PlayerCameraMode.Normal;
        }
    }

    // 通常時の挙動
    void NormalMode()
    {
        if (m_Player.GetComponent<PlayerController>().GetPlayerState() == 4)
        {
            m_Mode = PlayerCameraMode.Dead;
            t = 0f;
            m_deadBefore_pos = transform.position;
            return;
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
        // カメラの位置を更新（相対座標を使用）
        transform.localPosition = Vector3.Lerp(transform.localPosition, new_position, m_Speed * Time.deltaTime);

        // カメラがフィールドや障害物に透過しないようにする
        Ray ray = new Ray(m_Player.transform.position + Vector3.up, transform.position - m_Player.transform.position - Vector3.up);
        float distance = Vector3.Distance(m_Player.transform.position, transform.position);
        // Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo;

        if (transform.position.y <= 0.1f)
        {
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
            return;
        }

        if (Physics.Raycast(ray, out hitInfo, distance, LayerMask.GetMask("Stage")))
        {
            // Debug.Log("壁に遮られた");
            transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.1f, hitInfo.point.z);
        }
    }

    //死亡時の挙動
    void DeadMode()
    {
        if (t < 3f)
            transform.LookAt(m_Player.transform.position + m_Player.transform.forward);

        m_intervalTimer += 1.0f * Time.deltaTime;

        if (m_intervalTimer >= 2f)
        {
            t += 1.0f * Time.deltaTime;

            transform.position = 
                Vector3.Lerp(
                    m_deadBefore_pos,
                    m_Player.transform.position + m_Player.transform.forward + new Vector3(0f, 3f, 0f),
                    t / 3f
                    );

            if (t >= 3f)
                isDeadFinish = true;
        }
    }

    // イベントカメラの挙動
    void EventMode()
    {
        Vector3 new_position = new Vector3(m_EventPositionX, m_EventPositionY, m_EventPositionZ);

        // カメラの位置を更新
        transform.position = Vector3.Lerp(transform.position, new_position, m_EventSpeed * Time.deltaTime);
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

    //死亡カメラが終わったかどうかの取得
    public bool GetDeadFinish()
    {
        return isDeadFinish;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：カメラ位置制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

// カメラモード
enum T_PlayerCameraMode
{
    Tutorial_1,
    Tutorial_2,
    Tutorial_3,
    Normal,     // 通常
    Aim,
    Dead,
    Event,       // イベント
}

// カメラ距離
enum T_CameraDistance
{
    VeryClose,  // とても近い
    Close,      // 近い
    Normal,     // 普通
    Far         // 遠い
}

public class CameraPosition_Tutorial : MonoBehaviour
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
    private T_PlayerCameraMode m_Mode;            // カメラモード
    private T_CameraDistance m_Distance;          // カメラとプレイヤーの距離
    private int m_DistanceSelect;
    bool m_IsTriggered;                         // 十字キーの操作判定

    [SerializeField]
    private GameObject m_Prediction;             // カメラの予測座標

    [SerializeField]
    private float t;
    private float t1;
    private float m_intervalTimer;

    private Vector3 m_origin_pos;
    private Vector3 m_deadBefore_pos;
    private Quaternion m_origin_rotation;

    private bool isDeadFinish;

    private bool t_CameraFead1;
    private bool t_CameraFead2;
    private bool t_CameraFead3;

    [SerializeField]
    private List<GameObject> clear_point;

    [SerializeField]
    private GameObject tutorialMana_;

    private bool isCntActive;

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
            m_IsAiming = m_Player.GetComponent<PlayerController_Tutorial>().IsAiming();
        }

        m_EventPositionX = 0.0f;
        m_EventPositionY = 0.0f;
        m_EventPositionZ = 0.0f;

        m_Mode = T_PlayerCameraMode.Normal;
        m_DistanceSelect = 2;
        m_IsTriggered = false;

        t = 0f;

        m_intervalTimer = 0f;

        m_origin_pos = transform.position;
        m_origin_rotation = transform.rotation;

        isDeadFinish = false;

        t_CameraFead1 = false;
        t_CameraFead2 = false;
        t_CameraFead3 = false;

        isCntActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // カメラの状態に応じて処理を行う
        switch (m_Mode)
        {
            case T_PlayerCameraMode.Tutorial_1:
                isCntActive = false;
                Tutorial1Mode();
                break;
            case T_PlayerCameraMode.Tutorial_2:
                isCntActive = false;
                Tutorial2Mode();
                break;
            case T_PlayerCameraMode.Tutorial_3:
                isCntActive = false;
                Tutorial3Mode();
                break;
            case T_PlayerCameraMode.Normal:
                isCntActive = true;
                NormalMode();
                break;
            case T_PlayerCameraMode.Aim:
                isCntActive = true;
                AimMode();
                break;
            case T_PlayerCameraMode.Dead:
                DeadMode();
                break;
            case T_PlayerCameraMode.Event:
                EventMode();
                break;
            default:
                NormalMode();
                break;
        }

        // 照準状態を更新
        m_IsAiming = m_Player.GetComponent<PlayerController_Tutorial>().IsAiming();
    }

    void Tutorial1Mode()
    {
        if (!t_CameraFead1)
            t += 1.0f * Time.deltaTime;
        else
        {
            t -= 1.0f * Time.deltaTime;
            if (t < 0f)
            {
                m_intervalTimer = 0f;
                m_Mode = T_PlayerCameraMode.Normal;
            }
        }

        transform.LookAt(clear_point[0].transform.position);

        transform.position =
            Vector3.Lerp(m_origin_pos, new Vector3(3.91f, 4.1425f, 9.35f), t / 2f);
        transform.rotation =
            Quaternion.Slerp(m_origin_rotation, Quaternion.Euler(11.108f, -36.469f, 0f), t / 2f);

        if (t >= 2f)
        {
            m_intervalTimer += 1.0f * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space)
                || m_intervalTimer >= 3f)
                t_CameraFead1 = true;
        }

        t = Mathf.Clamp(t, 0f, 2f);
    }

    void Tutorial2Mode()
    {
        if (!t_CameraFead2)
            t += 1.0f * Time.deltaTime;
        else
        {
            t -= 1.0f * Time.deltaTime;
            if (t < 0f)
                m_Mode = T_PlayerCameraMode.Normal;
        }

        transform.LookAt(clear_point[1].transform.position);

        transform.position =
            Vector3.Lerp(m_origin_pos, new Vector3(3.91f, 4.1425f, 47.97f), t / 2f);
        transform.rotation =
            Quaternion.Slerp(m_origin_rotation, Quaternion.Euler(10.556f, -30.835f, 0f), t / 2f);

        if (t >= 2f)
        {
            m_intervalTimer += 1.0f * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space)
                || m_intervalTimer >= 3f)
                t_CameraFead2 = true;
        }

        t = Mathf.Clamp(t, 0f, 2f);
    }

    void Tutorial3Mode()
    {
        if (!t_CameraFead3)
            t += 1.0f * Time.deltaTime;
        else
        {
            t -= 1.0f * Time.deltaTime;
            if (t < 0f)
                m_Mode = T_PlayerCameraMode.Normal;
        }

        transform.LookAt(clear_point[2].transform.position);

        transform.position =
            Vector3.Lerp(m_origin_pos, new Vector3(3.36f, 6.76f, 63.96f), t / 2f);
        transform.rotation =
            Quaternion.Slerp(m_origin_rotation, Quaternion.Euler(-18.066f, -4.692f, 0f), t / 2f);

        if (t >= 2f)
        {
            m_intervalTimer += 1.0f * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space)
                || m_intervalTimer >= 3f)
                t_CameraFead3 = true;
        }

        t = Mathf.Clamp(t, 0f, 2f);
    }

    // 通常時の挙動
    void NormalMode()
    {
        m_origin_pos = transform.position;
        m_origin_rotation = transform.rotation;

        if (tutorialMana_.GetComponent<TutorialManager>().GetTutorialState() == 2
            && !t_CameraFead1)
        {
            m_Mode = T_PlayerCameraMode.Tutorial_1;
            m_intervalTimer = 0f;
            return;
        }

        if (tutorialMana_.GetComponent<TutorialManager>().GetTutorialState() == 3
            && !t_CameraFead2)
        {
            m_Mode = T_PlayerCameraMode.Tutorial_2;
            t = 0f;
            m_intervalTimer = 0f;
            return;
        }

        if (tutorialMana_.GetComponent<TutorialManager>().GetTutorialState() == 4
            && !t_CameraFead3)
        {
            m_Mode = T_PlayerCameraMode.Tutorial_3;
            t = 0f;
            m_intervalTimer = 0f;
            return;
        }

        if (m_Player.GetComponent<PlayerController_Tutorial>().GetPlayerState() == 3)
        {
            m_Mode = T_PlayerCameraMode.Dead;
            t = 0f;
            m_deadBefore_pos = transform.position;
            return;
        }

        // 照準モードに移行
        if (m_IsAiming)
        {
            m_Mode = T_PlayerCameraMode.Aim;
        }

        // カメラの距離を変更
        switch (m_DistanceSelect)
        {
            case 0:
                m_Distance = T_CameraDistance.VeryClose;
                break;
            case 1:
                m_Distance = T_CameraDistance.Close;
                break;
            case 2:
                m_Distance = T_CameraDistance.Normal;
                break;
            case 3:
                m_Distance = T_CameraDistance.Far;
                break;
            default:
                m_Distance = T_CameraDistance.Normal;
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
            case T_CameraDistance.VeryClose:
                new_position.x = m_VeryClosePositionX;
                new_position.y = m_VeryClosePositionY;
                new_position.z = m_VeryClosePositionZ;
                break;
            case T_CameraDistance.Close:
                new_position.x = m_ClosePositionX;
                new_position.y = m_ClosePositionY;
                new_position.z = m_ClosePositionZ;
                break;
            case T_CameraDistance.Normal:
                new_position.x = m_NormalPositionX;
                new_position.y = m_NormalPositionY;
                new_position.z = m_NormalPositionZ;
                break;
            case T_CameraDistance.Far:
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
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, LayerMask.GetMask("Stage")))
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

    void AimMode()
    {
        // 通常モードに移行
        if (!m_IsAiming)
        {
            m_Mode = T_PlayerCameraMode.Normal;
        }

        // カメラの距離を変更
        switch (m_DistanceSelect)
        {
            case 0:
                m_Distance = T_CameraDistance.VeryClose;
                break;
            case 1:
                m_Distance = T_CameraDistance.Close;
                break;
            case 2:
                m_Distance = T_CameraDistance.Normal;
                break;
            case 3:
                m_Distance = T_CameraDistance.Far;
                break;
            default:
                m_Distance = T_CameraDistance.Normal;
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

            if (Physics.Raycast(ray, out hitInfo, distance, LayerMask.GetMask("Stage")))
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

            if (Physics.Raycast(ray, out hitInfo, distance, LayerMask.GetMask("Stage")))
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
        m_Mode = T_PlayerCameraMode.Event;
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
        m_Mode = T_PlayerCameraMode.Event;
    }

    // イベントモードを解除し、通常モードに移行
    public void ChangeNormalMode()
    {
        if (m_Mode == T_PlayerCameraMode.Normal) return;

        Vector3 origin_position = new Vector3(m_PrevPositionX, m_PrevPositionY, m_PrevPositionZ);

        // カメラの位置を元に戻す（一瞬で戻る）
        transform.localPosition = origin_position;

        // 通常モードに移行
        m_Mode = T_PlayerCameraMode.Normal;
    }

    //現在のカメラモードの取得
    public int GetMode()
    {
        return (int)m_Mode;
    }

    public float Get_T()
    {
        return t;
    }

    public bool Get_TCF1()
    {
        return t_CameraFead1;
    }

    //死亡カメラが終わったかどうかの取得
    public bool GetDeadFinish()
    {
        return isDeadFinish;
    }

    public bool Get_CntActive()
    {
        return isCntActive;
    }
}
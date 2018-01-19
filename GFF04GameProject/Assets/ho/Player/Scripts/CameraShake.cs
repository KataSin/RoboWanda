using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：カメラの振動
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float m_ShakeTime;      // 振動時間
    float m_LifeTime = 0.0f;        // 振動時間の残り

    [SerializeField]
    private float m_Force = 0.5f;   // 振動力
    float m_CurrentForce;           // 現在の振動力

    Vector3 m_OriginalPosition;     // 本来の位置（ここは相対位置を使う）
    float m_LowRangeX = 0.0f;
    float m_MaxRangeX = 0.0f;
    float m_LowRangeY = 0.0f;
    float m_MaxRangeY = 0.0f;

    GameObject m_EnemyRobot;        // 敵ロボット
    float m_Distance = 1000.0f;     // プレイヤーとロボットの距離

    [SerializeField]
    private GameObject cameraPos_;

    // Use this for initialization
    void Start()
    {
        if (m_ShakeTime <= 0.0f) m_ShakeTime = 0.7f;
        m_CurrentForce = 0.0f;

        m_EnemyRobot = GameObject.FindGameObjectWithTag("Robot");
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraPos_.GetComponent<CameraPosition>().GetEMode() == 6)
        {
            transform.localPosition = Vector3.zero;
            m_LifeTime = 0.0f;
            return;
        }

        // 敵ロボットが存在する場合
        if (m_EnemyRobot != null)
        {
            // プレイヤーのロボットとの距離を取得
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 enemyPos = m_EnemyRobot.transform.position;

            m_Distance = Vector3.Distance(playerPos, enemyPos);
        }
        else m_Distance = 1000.0f;

        if (m_LifeTime <= 0.0f)
        {
            m_LowRangeX = 0.0f;
            m_MaxRangeX = 0.0f;
            m_LowRangeY = 0.0f;
            m_MaxRangeY = 0.0f;

            transform.localPosition = Vector3.zero;
            m_CurrentForce = 0.0f;
            m_LifeTime = 0.0f;
        }

        if (m_LifeTime > 0.0f)
        {
            m_LifeTime -= Time.deltaTime;

            // 振動力は時間につれ減少する
            m_LowRangeX *= 0.9f;
            m_MaxRangeX *= 0.9f;
            m_LowRangeY *= 0.9f;
            m_MaxRangeY *= 0.9f;

            float x_val = Random.Range(m_LowRangeX, m_MaxRangeX);
            float y_val = Random.Range(m_LowRangeY, m_MaxRangeY);
            transform.localPosition = new Vector3(x_val, y_val, transform.localPosition.z);
        }

        // ここに振動条件を入力


        // Spaceキーで振動（テスト用）
        // if (Input.GetKeyDown("space")) Shake();
    }

    // 振動処理
    void Shake()
    {
        m_OriginalPosition = Vector3.zero;
        m_CurrentForce = m_Force;

        m_LowRangeX = m_OriginalPosition.x - m_CurrentForce;
        m_MaxRangeX = m_OriginalPosition.x + m_CurrentForce;
        m_LowRangeY = m_OriginalPosition.y - m_CurrentForce;
        m_MaxRangeY = m_OriginalPosition.y + m_CurrentForce;
        m_LifeTime = m_ShakeTime;
    }

    // 振動処理（外部から力を設定）
    public void Shake(float force)
    {
        /*
        m_OriginalPosition = Vector3.zero;

        // 振動している（振動力が0ではない）なら、振動力と振動時間を加算
        if (m_LowRangeX != 0.0f && m_MaxRangeX != 0.0f && m_LowRangeY != 0.0f && m_MaxRangeY != 0.0f)
        {
            m_LowRangeX -= force;
            m_MaxRangeX += force;
            m_LowRangeY -= force;
            m_MaxRangeY += force;

            m_LifeTime += m_ShakeTime;
        }
        // 振動していない（振動力が0）なら、振動力と振動時間を設定
        else
        {
            m_LowRangeX = m_OriginalPosition.x - force;
            m_MaxRangeX = m_OriginalPosition.x + force;
            m_LowRangeY = m_OriginalPosition.y - force;
            m_MaxRangeY = m_OriginalPosition.y + force;

            m_LifeTime = m_ShakeTime;
        }
        */

        m_OriginalPosition = Vector3.zero;

        // 与えられた力が現在の振動力より高い場合、高い方の力とその振動時間を適用
        if (force > m_CurrentForce)
        {
            m_LowRangeX = m_OriginalPosition.x - force;
            m_MaxRangeX = m_OriginalPosition.x + force;
            m_LowRangeY = m_OriginalPosition.y - force;
            m_MaxRangeY = m_OriginalPosition.y + force;

            m_CurrentForce = force;
            m_LifeTime = m_ShakeTime;
        }
    }

    // 振動処理（外部から力と振動時間を設定）
    public void Shake(float force, float time)
    {
        m_OriginalPosition = Vector3.zero;

        // 与えられた力が現在の振動力より高い場合、高い方の力とその振動時間を適用
        if (force > m_CurrentForce)
        {
            m_LowRangeX = m_OriginalPosition.x - force;
            m_MaxRangeX = m_OriginalPosition.x + force;
            m_LowRangeY = m_OriginalPosition.y - force;
            m_MaxRangeY = m_OriginalPosition.y + force;

            m_CurrentForce = force;
            m_LifeTime = time;
        }
    }
}
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
    private int m_ShakeInterval = 60;   // 振動間隔設定（60フレーム＝1秒）
    int m_ShakeCount;                   // 振動間隔

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

    // Use this for initialization
    void Start()
    {
        if (m_ShakeTime <= 0.0f) m_ShakeTime = 0.7f;
        m_ShakeCount = m_ShakeInterval;
        m_CurrentForce = m_Force;
        
        m_EnemyRobot = GameObject.FindGameObjectWithTag("Robot");
    }

    // Update is called once per frame
    void Update()
    {
        // 敵ロボットが存在する場合
        if (m_EnemyRobot != null)
        {
            // プレイヤーのロボットとの距離を取得
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 enemyPos = m_EnemyRobot.transform.position;

            m_Distance = Vector3.Distance(playerPos, enemyPos);
        }
        else m_Distance = 1000.0f;

        if (m_LifeTime < 0.0f)
        {
            transform.localPosition = Vector3.zero;
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
        
        // 振動間隔が0になったら振動
        m_ShakeCount--;
        // 距離が100メートル以内なら振動
        if (m_ShakeCount <= 0 && m_Distance <= 100.0f)
        {
            // 距離が近いほど振動力が大きくなる
            m_CurrentForce = m_Force * (1 - (m_Distance / 100.0f));
            Shake();
            m_ShakeCount = m_ShakeInterval;
        }

        // Spaceキーで振動（テスト用）
        // if (Input.GetKeyDown("space")) Shake();
        // Debug.Log("ロボットとの距離：" + m_Distance);
    }

    // 振動処理
    void Shake()
    {
        m_OriginalPosition = Vector3.zero;
        m_LowRangeX = m_OriginalPosition.x - m_CurrentForce;
        m_MaxRangeX = m_OriginalPosition.x + m_CurrentForce;
        m_LowRangeY = m_OriginalPosition.y - m_CurrentForce;
        m_MaxRangeY = m_OriginalPosition.y + m_CurrentForce;
        m_LifeTime = m_ShakeTime;
    }
}
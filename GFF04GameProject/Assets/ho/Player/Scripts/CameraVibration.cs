using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：カメラ振動
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class CameraVibration : MonoBehaviour
{
    public float setShakeTime;

    private float lifeTime;
    Vector3 m_OriginalPosition;     // カメラの本来の位置
    private float lowRangeX;
    private float maxRangeX;
    private float lowRangeY;
    private float maxRangeY;

    // Use this for initialization
    void Start()
    {
        if (setShakeTime <= 0.0f) setShakeTime = 0.7f;
        lifeTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime < 0.0f)
        {
            transform.position = m_OriginalPosition;
            lifeTime = 0.0f;
        }

        if (lifeTime > 0.0f)
        {
            lifeTime -= Time.deltaTime;
            float x_val = Random.Range(lowRangeX, maxRangeX);
            float y_val = Random.Range(lowRangeY, maxRangeY);
            transform.position = new Vector3(x_val, y_val, transform.position.z);
        }

        if (Input.GetButtonDown("Submit"))
        {
            Shake();
        }
    }

    // 振動
    void Shake()
    {
        m_OriginalPosition = transform.position;
        lowRangeY = m_OriginalPosition.y - 1.0f;
        maxRangeY = m_OriginalPosition.y + 1.0f;
        lowRangeX = m_OriginalPosition.x - 1.0f;
        maxRangeX = m_OriginalPosition.x + 1.0f;
        lifeTime = setShakeTime;
    }
}

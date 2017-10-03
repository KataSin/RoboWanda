using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤー回転
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>
public class PlayerTurn : MonoBehaviour
{
    [SerializeField]
    private float m_RotateSpeed = 360.0f;       // 回転速度

    Vector3 m_PrevPosition;                     // 前回の位置

    // Use this for initialization
    void Start()
    {
        m_PrevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - m_PrevPosition;

        if (direction.sqrMagnitude > 0)
        {
            Vector3 forward = Vector3.Slerp(transform.forward,
                new Vector3(direction.x, 0.0f, direction.z),
                m_RotateSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            transform.LookAt(transform.position + forward);
            m_PrevPosition = transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤー移動
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 15.0f;      // 移動速度
    [SerializeField]
    private float m_Gravity = 20.0f;

    private Vector3 m_MoveDirection = Vector3.zero;
    CharacterController m_Controller;

    // Use this for initialization
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Controller.isGrounded)
        {
            m_MoveDirection = new Vector3(Input.GetAxis("Horizontal_L"), 0, Input.GetAxis("Vertical_L"));
            m_MoveDirection = transform.TransformDirection(m_MoveDirection);
            m_MoveDirection *= m_Speed;
        }

        m_MoveDirection.y -= m_Gravity * Time.deltaTime;
        m_Controller.Move(m_MoveDirection * Time.deltaTime);
    }
}

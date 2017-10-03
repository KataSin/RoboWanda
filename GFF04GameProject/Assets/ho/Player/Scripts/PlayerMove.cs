using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤー移動（慣性なし）
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 15.0f;      // 移動速度
    [SerializeField]
    private float m_Gravity = 20.0f;    // 重力

    //private Vector3 m_MoveDirection = Vector3.zero;
    Vector3 velocity = Vector3.zero;    // 移動量
    float vY = 0;                       // y軸速度
    CharacterController m_Controller;

    // Use this for initialization
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (m_Controller.isGrounded)
        //{
        //m_MoveDirection = new Vector3(Input.GetAxis("Horizontal_L"), 0, Input.GetAxis("Vertical_L"));
        //m_MoveDirection = transform.TransformDirection(m_MoveDirection);
        //m_MoveDirection *= m_Speed;
        //}

        //m_MoveDirection.y -= m_Gravity * Time.deltaTime;
        //m_Controller.Move(m_MoveDirection * Time.deltaTime);

        // カメラの正面向きのベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        // y成分を無視する
        forward.y = 0;
        // 正規化する
        forward.Normalize();

        // 方向入力を取得
        float axisHorizontal = Input.GetAxisRaw("Horizontal_L");    // x軸
        float axisVertical = Input.GetAxisRaw("Vertical_L");        // z軸

        // 移動量を計算
        if (m_Controller.isGrounded)
        {
            velocity = forward * axisVertical * m_Speed + Camera.main.transform.right * axisHorizontal * m_Speed;
            vY = 0;
        }

        vY -= m_Gravity * Time.deltaTime;
        velocity.y = vY;

        // CharacterControllerに命令して移動する
        m_Controller.Move(velocity * Time.deltaTime);
    }
}

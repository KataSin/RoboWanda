using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤー視点制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class PlayerLookAt : MonoBehaviour
{
    [SerializeField]
    private GameObject m_EyesDirection;      // 注視点

    [SerializeField]
    private float m_Weight = 1.0f;
    [SerializeField]
    private float m_EyesWeight = 1.0f;
    [SerializeField]
    private float m_HeadWeight = 1.0f;
    [SerializeField]
    private float m_BodyWeight = 1.0f;
    [SerializeField]
    private float m_ClampWeight = 1.0f;

    Animator m_Animator;

    // Use this for initialization
    void Start()
    {
        if (m_EyesDirection == null)
        {
            Debug.Log("エラー発生したので終了します");
            Debug.Log("場所：PlayerLookAt.cs");
            Debug.Log("Error Log：注視点が存在しない");
            //Application.Quit();
        }

        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAnimatorIK()
    {
        /*
        Vector3 camera_forward = Camera.main.transform.forward;     // カメラの前方座標を取得
        Vector3 direction = camera_forward - transform.forward;
        Vector3 i = transform.InverseTransformDirection(direction);
        i.Normalize();

        Debug.Log(i);
        */
        /*
        if (i.x < -0.5f || i.x > 0.5f)
        {
            m_Animator.SetLookAtWeight(m_Weight, m_BodyWeight, m_HeadWeight, m_EyesWeight, m_ClampWeight);

            Vector3 target_position = m_EyesDirection.transform.position;
            m_Animator.SetLookAtPosition(target_position);
        }
        */
        /*
        // カメラがプレイヤーの背後にある場合
        if (i.x < -0.5f || i.x > 0.5f)
        {
            m_Animator.SetLookAtWeight(m_Weight, m_BodyWeight, m_HeadWeight, m_EyesWeight, m_ClampWeight);

            Vector3 target_position = m_EyesDirection.transform.position;
            target_position.y = 1.65f;
            m_Animator.SetLookAtPosition(target_position);
        }
        // カメラがプレイヤーの正面にある場合
        else
        {
            m_Animator.SetLookAtWeight(m_Weight, m_BodyWeight, 0.5f, m_EyesWeight, m_ClampWeight);

            Vector3 target_position = m_EyesDirection.transform.position;
            target_position.y = 1.65f;
            m_Animator.SetLookAtPosition(target_position);
        }
        */

        m_Animator.SetLookAtWeight(m_Weight, m_BodyWeight, m_HeadWeight, m_EyesWeight, m_ClampWeight);

        Vector3 target_position = m_EyesDirection.transform.position;
        target_position.y = 1.65f;
        m_Animator.SetLookAtPosition(target_position);
    }
}

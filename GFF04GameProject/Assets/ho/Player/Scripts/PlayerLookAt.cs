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
        m_Animator.SetLookAtWeight(m_Weight, m_BodyWeight, m_HeadWeight, m_EyesWeight, m_ClampWeight);
        m_Animator.SetLookAtPosition(m_EyesDirection.transform.position);
    }
}

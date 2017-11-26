using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤーの透明化
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class PlayerFade : MonoBehaviour
{
    private Camera m_Camera;    // カメラ

    [SerializeField]
    private GameObject m_PlayerBody;
    [SerializeField]
    private GameObject m_PlayerHead;

    // Use this for initialization
    void Start()
    {
        m_Camera = Camera.main;

        // カメラが存在しない場合、強制終了
        if (m_Camera == null)
        {
            Debug.Log("エラー発生したので終了します");
            Debug.Log("Error Log：カメラが存在しない");
            Application.Quit();
        }

        if (m_PlayerBody == null || m_PlayerHead == null)
        {
            Debug.Log("エラー発生したので終了します");
            Debug.Log("Error Log：プレイヤーのモデルを見つからない");
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーとカメラの距離を取得
        float distance;
        distance = Vector3.Distance(transform.position, m_Camera.transform.position);
        Debug.Log("カメラとプレイヤーの距離：" + distance);

        // 距離が2以下である場合、プレイヤーを透明化させる
        if (distance < 2.0f)
        {
            Debug.Log("カメラとプレイヤーが近い！");

            SkinnedMeshRenderer body_renderer = m_PlayerBody.GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer head_renderer = m_PlayerHead.GetComponent<SkinnedMeshRenderer>();

            body_renderer.material.SetFloat("__Mode", 2);
            head_renderer.material.SetFloat("__Mode", 2);

            body_renderer.material.SetColor("_Color", new Color(1, 1, 1, 0));
            head_renderer.material.SetColor("_Color", new Color(1, 1, 1, 0));
        }
        else
        {
            SkinnedMeshRenderer body_renderer = m_PlayerBody.GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer head_renderer = m_PlayerHead.GetComponent<SkinnedMeshRenderer>();

            body_renderer.material.SetFloat("__Mode", 0);
            head_renderer.material.SetFloat("__Mode", 0);

            body_renderer.material.SetColor("_Color", new Color(1, 1, 1, 1));
            head_renderer.material.SetColor("_Color", new Color(1, 1, 1, 1));
        }
    }
}

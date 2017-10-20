using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：爆弾投擲
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

// 投擲モード
enum ThrowMode
{
    One,        // 1個
    Two,        // 2個
    Three       // 3個
}
public class BombThrow : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Bomb;      // 爆弾のプレハブ

    int m_BombKeep = 3;             // 爆弾の保持数
    int m_ThrowAmount = 1;          // 爆弾の投擲数

    bool m_IsTriggered;             // 十字キーの操作判定
    ThrowMode m_Mode;               // 投擲モード

    // Use this for initialization
    void Start()
    {
        m_Mode = ThrowMode.One;
    }

    // Update is called once per frame
    void Update()
    {
        // 投擲モードを変更
        switch (m_ThrowAmount)
        {
            case 1:
                m_Mode = ThrowMode.One;
                break;
            case 2:
                m_Mode = ThrowMode.Two;
                break;
            case 3:
                m_Mode = ThrowMode.Three;
                break;
            default:
                m_Mode = ThrowMode.One;
                break;
        }

        // 十字キーの入力が無い場合、操作判定を解除
        if (Input.GetAxisRaw("Throw_Amount") == 0)
        {
            m_IsTriggered = false;
        }

        // 十字ボタンの上下で投擲数を選択
        if (m_IsTriggered == false)
        {
            // 上ボタン
            if (Input.GetAxisRaw("Throw_Amount") < -0.9)
            {
                m_IsTriggered = true;
                m_ThrowAmount = m_ThrowAmount + 1;
            }
            // 下ボタン
            if (Input.GetAxisRaw("Throw_Amount") > 0.9)
            {
                m_IsTriggered = true;
                m_ThrowAmount = m_ThrowAmount - 1;
            }
        }

        // 爆弾の数を１～３の範囲内に限定
        m_ThrowAmount = Mathf.Clamp(m_ThrowAmount, 1, 3);

        // RBボタンを押している間、着弾点を表示
        if (Input.GetButton("Bomb_Hold"))
        {
            ShowThrowDirection();
        }

        // RBボタンを押している間、LBボタンを押すと、爆弾を投擲
        if (Input.GetButton("Bomb_Hold") && Input.GetButtonDown("Bomb_Throw"))
        {
            Throw();
        }

        // 爆弾が全て起爆した場合、爆弾の保持数を3に戻す

        if (GameObject.FindGameObjectsWithTag("Bomb").Length == 0)
        {
            m_BombKeep = 3;
        }
        Debug.Log(m_ThrowAmount);
    }

    // 着弾点を表示
    void ShowThrowDirection()
    {
        switch (m_Mode)
        {
            case ThrowMode.One:
                ShowThrowDirectionOne();
                break;
            case ThrowMode.Two:
                ShowThrowDirectionTwo();
                break;
            case ThrowMode.Three:
                ShowThrowDirectionThree();
                break;
            default:
                ShowThrowDirectionOne();
                break;
        }
    }

    // 着弾点を表示（1個投擲時）
    void ShowThrowDirectionOne()
    {

    }

    // 着弾点を表示（2個投擲時）
    void ShowThrowDirectionTwo()
    {

    }

    // 着弾点を表示（3個投擲時）
    void ShowThrowDirectionThree()
    {

    }

    // 爆弾を投擲
    void Throw()
    {
        if (m_BombKeep <= 0) return;

        switch (m_Mode)
        {
            case ThrowMode.Three:
                if (m_BombKeep == 1)
                {
                    ThrowOne();
                    break;
                }
                if (m_BombKeep == 2)
                {
                    ThrowTwo();
                    break;
                }
                ThrowThree();
                break;
            case ThrowMode.Two:
                if (m_BombKeep == 1)
                {
                    ThrowOne();
                    break;
                }
                ThrowTwo();
                break;
            case ThrowMode.One:
                ThrowOne();
                break;
            default:
                break;
        }
    }

    // 爆弾を投擲（1個）
    void ThrowOne()
    {
        m_BombKeep -= 1;
        GameObject bomb = Instantiate(m_Bomb, transform.position, transform.rotation);
    }

    // 爆弾を投擲（2個）
    void ThrowTwo()
    {
        m_BombKeep -= 2;
        GameObject bomb1 = Instantiate(m_Bomb, transform.position, transform.rotation);
        GameObject bomb2 = Instantiate(m_Bomb, transform.position, transform.rotation);

        bomb1.transform.Rotate(new Vector3(0, 1, 0), -15.0f);
        bomb2.transform.Rotate(new Vector3(0, 1, 0), +15.0f);
    }

    // 爆弾を投擲（3個）
    void ThrowThree()
    {
        m_BombKeep -= 3;
        GameObject bomb1 = Instantiate(m_Bomb, transform.position, transform.rotation);
        GameObject bomb2 = Instantiate(m_Bomb, transform.position, transform.rotation);
        GameObject bomb3 = Instantiate(m_Bomb, transform.position, transform.rotation);

        bomb2.transform.Rotate(new Vector3(0, 1, 0), -15.0f);
        bomb3.transform.Rotate(new Vector3(0, 1, 0), +15.0f);
    }
}

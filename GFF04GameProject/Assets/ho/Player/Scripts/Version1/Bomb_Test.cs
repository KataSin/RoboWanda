using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：ボム
/// 製作者：Ho Siu Ki（何　兆祺）
/// </summary>

// ボムの状態
enum BombState
{
    Holding,    // 保持中
    Thrown      // 投擲された
}

public class Bomb_Test : MonoBehaviour
{
    [SerializeField]
    private float m_Power = 10.0f;

    GameObject m_BombHolder;        // ボム保持点
    float m_Angle;                  // 投擲角度

    Rigidbody m_RigidBody;
    BombState m_State;              // ボムの状態

    // Use this for initialization
    void Start()
    {
        m_BombHolder = GameObject.FindGameObjectWithTag("BombHolder");
        m_RigidBody = GetComponent<Rigidbody>();
        m_State = BombState.Holding;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case BombState.Holding:
                Holding();
                break;
            case BombState.Thrown:
                Thrown();
                break;
            default:
                break;
        }
    }

    // 保持中
    void Holding()
    {
        transform.position = m_BombHolder.transform.position;

        // RBボタンが押されてなかったら消滅
        if (!Input.GetButton("Bomb_Hold"))
        {
            Destroy(gameObject);
        }

        // 着弾点を表示

        // LBボタンを押して投擲
        if (Input.GetButtonDown("Bomb_Throw"))
        {
            Throw();
            m_State = BombState.Thrown;
        }
    }

    // 投擲された
    void Thrown()
    {
        // RBボタンが押されてない間に、LBボタンを押すと起爆
        if (!Input.GetButton("Bomb_Hold") && Input.GetButtonDown("Bomb_Throw"))
        {
            Destroy(gameObject);
        }
    }

    // 投擲処理
    void Throw()
    {
        var forward = m_BombHolder.transform.forward;
        var up = m_BombHolder.transform.up;
        m_RigidBody.AddForce(forward * m_Power + up * m_Power, ForceMode.Impulse);
    }

    // 接触判定
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.isKinematic = true;
        }
    }
}
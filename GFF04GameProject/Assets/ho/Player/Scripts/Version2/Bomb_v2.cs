using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：爆弾制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>
public class Bomb_v2 : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Explosion;     // 爆発の当たり判定
    Rigidbody m_RigidBody;

    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        var forward = transform.forward;
        var up = transform.up;
        m_RigidBody.AddForce(forward * 8.0f + up * 8.0f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // RBボタンが押されてない間に、LBボタンを押すと起爆
        if (!Input.GetButton("Bomb_Hold") && Input.GetButtonDown("Bomb_Throw"))
        {
            Destroy(gameObject);
            // 爆発の当たり判定を発生
            Instantiate(m_Explosion, transform.position, Quaternion.identity);
        }
    }

    // 接触判定
    public void OnTriggerEnter(Collider other)
    {
        // 他の爆弾とプレイヤーとの接触判定は発生しない
        if (other.tag == "Bomb" || other.tag == "Player") return;

        m_RigidBody.velocity = Vector3.zero;
        m_RigidBody.isKinematic = true;
    }
}

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
    private float m_Force = 6.0f;       // 与える力
    [SerializeField]
    private GameObject m_Explosion;     // 爆発の当たり判定
    Rigidbody m_RigidBody;

    private Quaternion m_currentRotation;

    private bool isLanding;

    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        isLanding = false;
    }
    // Update is called once per frame
    void Update()
    {

        // RBボタンが押されてない間に、LBボタンを押すと起爆
        if ((!Input.GetButton("Aim") && !Input.GetKey(KeyCode.P)) 
            && (Input.GetButtonDown("Bomb_Throw")||Input.GetKeyDown(KeyCode.O)))
        {
            Destroy(gameObject);
            // 爆発の当たり判定を発生
            Instantiate(m_Explosion, transform.position, Quaternion.identity);
        }

        if (isLanding)
        {
            transform.rotation = m_currentRotation;
            return;
        }

        Vector3 l_bomForward = GetComponent<Rigidbody>().velocity;
        transform.rotation = Quaternion.LookRotation(l_bomForward) * Quaternion.Euler(90, 0, 0);


    }

    // 接触判定
    public void OnTriggerEnter(Collider other)
    {
        // 他の爆弾とプレイヤーとの接触判定は発生しない
        if (other.tag == "Bomb" || other.tag == "Player" || other.tag == "SandSmoke") return;


        //矢野追加10270231
        if (!isLanding)
        {
            m_currentRotation = transform.rotation;
            transform.parent = other.transform;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.isKinematic = true;

            isLanding = true;
        }
    }
}

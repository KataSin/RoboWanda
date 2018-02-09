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

    public GameObject m_SmokeExplosion;
    Rigidbody m_RigidBody;

    private Quaternion m_currentRotation;

    private bool isLanding;

    public BomSpawn.Bom m_Bullet;

    private Vector3 m_scale;


    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        isLanding = false;
        m_scale = new Vector3(0.4f, 0.4f, 0.4f);
        transform.localScale = m_scale;
    }
    // Update is called once per frame
    void Update()
    {

        // RBボタンが押されてない間に、LBボタンを押すと起爆
        /*if ((!Input.GetButton("Aim") && !Input.GetKey(KeyCode.P))
            && (Input.GetButtonDown("Bomb_Throw") || Input.GetKeyDown(KeyCode.O)))*/
        if (!(Input.GetAxis("Aim") > 0.5f) && Input.GetAxis("Bomb_Throw") > 0.5f)
        {
            Destroy(gameObject);
            // 爆発の当たり判定を発生
            if (m_Bullet == BomSpawn.Bom.BOM)
                Instantiate(m_Explosion, transform.position, Quaternion.identity);
            else
                Instantiate(m_SmokeExplosion, transform.position, Quaternion.identity);

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
        if (other.tag == "Bomb"
            || other.tag == "Player"
            || other.tag == "SandSmoke"
            || other.tag == "LightCollision"
            || other.tag == "ExplosionCollision"
            || other.tag == "ClearPoint"
            || other.tag == "DoorCheck") return;


        //矢野追加10270231
        if (!isLanding)
        {
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.isKinematic = true;
            m_currentRotation = transform.rotation;
            transform.parent = other.transform.parent;


            isLanding = true;
        }
    }
}

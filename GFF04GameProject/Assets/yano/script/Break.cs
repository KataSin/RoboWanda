using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    //破片
    [SerializeField]
    [Header("破片")]
    private GameObject debris_;

    //砂煙
    [SerializeField]
    [Header("砂煙horizontal")]
    private List<ParticleSystem> deserts_;

    //落下スピード
    [SerializeField]
    [Header("落下スピード")]
    private float m_down_speed;

    //倒壊しているかどうか
    private bool isBreak;

    //倒壊後かどうか
    private bool isAfter;

    //倒壊経過時間
    private float m_break_time;

    //ビルの回転
    private Quaternion m_rotation;


    // Use this for initialization
    void Start()
    {
        //砂煙を停止
        for (int i = 0; i < deserts_.Count; ++i)
            deserts_[i].Stop();

        //初期化
        m_rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isBreak = true;

        if (isBreak)
        {
            //倒壊時間経過
            m_break_time += 1f * Time.deltaTime;

            //落下スピード上昇
            m_down_speed += 0.002f * Time.deltaTime;

            //8sまでは砂煙発生
            if (m_break_time < 8f)
            {
                for (int i = 0; i < deserts_.Count; ++i)
                    deserts_[i].Play();
            }

            //5s経過後
            if (m_break_time >= 5f)
            {
                //倒壊済みでなければ
                if (!isAfter)
                {
                    //倒壊後のビルを生成
                    Instantiate(debris_);
                    isAfter = true;
                }

                //8s経過後
                if (m_break_time >= 8f)
                {
                    //砂煙停止
                    for (int i = 0; i < deserts_.Count; ++i)
                        deserts_[i].Stop();

                    //自分を消去
                    Destroy(gameObject);
                }
            }
        }

        //落下
        transform.position += new Vector3(0f, -m_down_speed, 0f);

        //回転
        transform.rotation =
            Quaternion.Lerp(Quaternion.Euler(0f, 0f, 0f), m_rotation, m_break_time / 12f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bom"||other.gameObject.tag=="RobotArmAttack")
        {
            isBreak = true;

            //左下
            if (other.gameObject.transform.position.x <= transform.position.x
                &&
                other.gameObject.transform.position.z <= transform.position.z)
            {
                if (other.gameObject.transform.position.x < other.gameObject.transform.position.z)
                {
                    m_rotation = Quaternion.Euler(0f, 0f, 90f);
                }
                else if (other.gameObject.transform.position.x > other.gameObject.transform.position.z)
                {
                    m_rotation = Quaternion.Euler(-90f, 0f, 0f);
                }
            }

            //左上
            if (other.gameObject.transform.position.x <= transform.position.x
                &&
                other.gameObject.transform.position.z >= transform.position.z)
            {
                if (other.gameObject.transform.position.x < other.gameObject.transform.position.z)
                {
                    float l_inv_pos_x = other.gameObject.transform.position.x * (-1f);
                    if (l_inv_pos_x <= other.gameObject.transform.position.z)
                    {
                        m_rotation = Quaternion.Euler(90f, 0f, 0f);
                    }
                    else if (l_inv_pos_x >= other.gameObject.transform.position.z)
                    {
                        m_rotation = Quaternion.Euler(0f, 0f, 90f);
                    }
                }
            }

            //右上
            if (other.gameObject.transform.position.x >= transform.position.x
                &&
                other.gameObject.transform.position.z >= transform.position.z)
            {
                if (other.gameObject.transform.position.x < other.gameObject.transform.position.z)
                {
                    m_rotation = Quaternion.Euler(90f, 0f, 0f);
                }
                else if (other.gameObject.transform.position.x > other.gameObject.transform.position.z)
                {
                    m_rotation = Quaternion.Euler(0f, 0f, -90f);
                }
            }

            //右下
            if (other.gameObject.transform.position.x >= transform.position.x
                &&
                other.gameObject.transform.position.z <= transform.position.z)
            {
                if (other.gameObject.transform.position.x > other.gameObject.transform.position.z)
                {
                    float l_inv_pos_z = other.gameObject.transform.position.z * (-1f);
                    if (l_inv_pos_z >= other.gameObject.transform.position.x)
                    {
                        m_rotation = Quaternion.Euler(-90f, 0f, 0f);
                    }
                    else if (l_inv_pos_z <= other.gameObject.transform.position.x)
                    {
                        m_rotation = Quaternion.Euler(0f, 0f, -90f);
                    }
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RobotArmAttack")
        {
            isBreak = true;

            //左下
            if (other.gameObject.transform.position.x <= transform.position.x
                &&
                other.gameObject.transform.position.z <= transform.position.z)
            {
                if (other.gameObject.transform.position.x < other.gameObject.transform.position.z)
                {
                    m_rotation = Quaternion.Euler(0f, 0f, 90f);
                }
                else if (other.gameObject.transform.position.x > other.gameObject.transform.position.z)
                {
                    m_rotation = Quaternion.Euler(-90f, 0f, 0f);
                }
            }

            //左上
            if (other.gameObject.transform.position.x <= transform.position.x
                &&
                other.gameObject.transform.position.z >= transform.position.z)
            {
                if (other.gameObject.transform.position.x < other.gameObject.transform.position.z)
                {
                    float l_inv_pos_x = other.gameObject.transform.position.x * (-1f);
                    if (l_inv_pos_x <= other.gameObject.transform.position.z)
                    {
                        m_rotation = Quaternion.Euler(90f, 0f, 0f);
                    }
                    else if (l_inv_pos_x >= other.gameObject.transform.position.z)
                    {
                        m_rotation = Quaternion.Euler(0f, 0f, 90f);
                    }
                }
            }

            //右上
            if (other.gameObject.transform.position.x >= transform.position.x
                &&
                other.gameObject.transform.position.z >= transform.position.z)
            {
                if (other.gameObject.transform.position.x < other.gameObject.transform.position.z)
                {
                    m_rotation = Quaternion.Euler(90f, 0f, 0f);
                }
                else if (other.gameObject.transform.position.x > other.gameObject.transform.position.z)
                {
                    m_rotation = Quaternion.Euler(0f, 0f, -90f);
                }
            }

            //右下
            if (other.gameObject.transform.position.x >= transform.position.x
                &&
                other.gameObject.transform.position.z <= transform.position.z)
            {
                if (other.gameObject.transform.position.x > other.gameObject.transform.position.z)
                {
                    float l_inv_pos_z = other.gameObject.transform.position.z * (-1f);
                    if (l_inv_pos_z >= other.gameObject.transform.position.x)
                    {
                        m_rotation = Quaternion.Euler(-90f, 0f, 0f);
                    }
                    else if (l_inv_pos_z <= other.gameObject.transform.position.x)
                    {
                        m_rotation = Quaternion.Euler(0f, 0f, -90f);
                    }
                }
            }
        }
    }


}

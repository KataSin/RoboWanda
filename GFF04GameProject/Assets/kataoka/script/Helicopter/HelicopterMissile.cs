using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMissile : MonoBehaviour
{
    //壊れたかどうか
    public bool m_IsBreak;
    //プロペラ
    public GameObject m_Propeller;
    //火
    private GameObject m_FireEffect;
    //壊れるヘリ
    public GameObject m_HeliBreakPrefab;
    //帰っているかどうか
    public bool m_ReturnFlag;
    //回転すべき角度
    private float m_ToPointAngle;


    //回転した量
    private float m_RotateAmount;

    private GameObject m_Robot;

    public GameObject m_Light;

    private AudioClip heri_se_;

    // Use this for initialization
    void Start()
    {
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
        m_FireEffect = transform.Find("FireEffect").gameObject;


        m_FireEffect.SetActive(false);

        m_ReturnFlag = false;
        m_IsBreak = false;

        m_ToPointAngle = Quaternion.LookRotation(m_Robot.transform.position - transform.position).eulerAngles.y;

        heri_se_ = GetComponent<AudioSource>().clip;
        GetComponent<AudioSource>().PlayOneShot(heri_se_);
        transform.LookAt(new Vector3(m_Robot.transform.position.x, transform.position.y, m_Robot.transform.position.z));
        m_RotateAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);

        if (m_IsBreak)
        {
            m_FireEffect.SetActive(true);
            transform.Rotate(new Vector3(0, 1, 0.4f), 10.0f);

            transform.position += Vector3.down * 5.0f * Time.deltaTime;
            return;
        }



        if (m_ReturnFlag)
        {
            if (m_RotateAmount <= 180.0f)
            {
                transform.Rotate(Vector3.up, 20.0f * Time.deltaTime);
                m_RotateAmount += 20.0f * Time.deltaTime;
            }

        }
        Vector3 front = new Vector3(transform.forward.x, 0.0f, transform.forward.z).normalized;

        m_Propeller.transform.localEulerAngles += new Vector3(0, 0, 1000) * Time.deltaTime;
        transform.position += front * 10.0f * Time.deltaTime;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (m_IsBreak && other.tag != "ExplosionCollision")
        {
            Instantiate(m_HeliBreakPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Beam")
        {
            m_IsBreak = true;
        }
    }
}

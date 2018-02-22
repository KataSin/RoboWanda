using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombing1 : MonoBehaviour
{
    public enum BrieBomberType
    {
        None,
        Break,
        Normal,
    }

    private BrieBomberType m_type;

    //爆弾
    public GameObject m_BomPrefab;
    //移動するベクトル
    private Vector3 m_Vec;
    //時間
    private float m_Time;
    //ゴール座標
    private Vector3 m_GoalPos;

    private Quaternion m_BBefore_rotation;

    [SerializeField]
    private bool isMove;

    [SerializeField]
    private bool isBreak;

    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
        isMove = false;
        isBreak = false;
        m_BBefore_rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //回転
        if (!isBreak)
            transform.rotation = Quaternion.LookRotation(m_Vec);

        switch (m_type)
        {
            case BrieBomberType.Break:
                if (!isBreak && isMove)
                {
                    transform.position += 20f * transform.forward * Time.deltaTime;
                    m_BBefore_rotation = transform.rotation;
                }
                else if (isBreak && isMove)
                {
                    //移動
                    transform.position += 40.0f * transform.forward * Time.deltaTime;
                    transform.rotation = Quaternion.Slerp(
                        m_BBefore_rotation, Quaternion.Euler(20f, 70f, 15f), m_Time / 1f);
                    m_Time += 1.0f * Time.deltaTime;
                }
                break;

            default:
                if (isMove)
                {
                    //移動
                    transform.position += 40.0f * m_Vec * Time.deltaTime;
                    //削除処理
                    if (Vector3.Distance(transform.position, m_GoalPos) <= 2.0f)
                    {
                        Destroy(gameObject);
                    }

                    if (m_BomPrefab == null) return;

                    //爆撃
                    if (m_Time >= 0.5f)
                    {
                        Instantiate(m_BomPrefab, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                        m_Time = 0.0f;
                    }

                    m_Time += 2.0f * Time.deltaTime;
                }
                break;
        }
    }

    public void SetStartEnd(Vector3 start, Vector3 end)
    {
        m_GoalPos = end;
        m_Vec = (end - start).normalized;
    }

    public void SetType(BrieBomberType l_type)
    {
        m_type = l_type;
    }

    public void SetMoveFlag(bool l_flag)
    {
        isMove = l_flag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ExplosionCollision")
        {
            isBreak = true;
            GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.35f, 0f, 0f));
        }

        if (other.tag == "UnderBrie")
        {
            Destroy(gameObject);
        }
    }
}

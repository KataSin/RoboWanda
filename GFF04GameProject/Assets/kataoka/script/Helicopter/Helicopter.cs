using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    //壊れたかどうか
    public bool m_IsBreak;
    //ヘリコプターの速度
    private Vector3 m_Velo;
    //ヘリコプターのベクトル
    private Vector3 m_Vec;
    //ヘリコプターセーブ
    private Vector3 m_SevePos;
    //プロペラ
    public GameObject m_Propeller;
    //火
    private GameObject m_FireEffect;
    //壊れるヘリ
    public GameObject m_HeliBreakPrefab;
    private GameObject m_Robot;
    //撤退しているか
    public bool m_ReturnFlag;

    private Vector3 m_RobotToHeliVec;

    private Vector3 m_ResPos;
    private Vector3 m_Pos;
    private Vector3 m_SpringVelo;

    // Use this for initialization
    void Start()
    {
        m_Velo = Vector3.zero;
        m_SevePos = transform.position;
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
        m_FireEffect = transform.Find("FireEffect").gameObject;
        m_FireEffect.SetActive(false);

        m_ResPos = transform.position;
        m_Pos = transform.position;
        m_SpringVelo = Vector3.zero;

        m_RobotToHeliVec = Vector3.zero;

        m_ReturnFlag = false;
        m_IsBreak = false;

        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsBreak)
        {
            m_FireEffect.SetActive(true);
            transform.Rotate(new Vector3(0, 1, 0.4f), 10.0f);
            m_Velo.y = -4.0f;
            transform.position += m_Velo * 3.0f * Time.deltaTime;
            return;
        }

        //ヘリコプターの回転処理
        m_Velo.y = 0.0f;
        Vector3 rotateVec = Vector3.Cross(Vector3.up, m_Velo);

        m_Propeller.transform.localEulerAngles += new Vector3(0, 0, 1000) * Time.deltaTime;
        //帰還する時の動き
        if (m_ReturnFlag)
        {
            //帰還する瞬間の前方向に移動させる
            Vector3 returnVec = 5.0f * transform.forward;
            returnVec.y = 0.0f;
            m_ResPos += returnVec * Time.deltaTime;
            transform.rotation =
                    Quaternion.AngleAxis(m_Velo.magnitude * 130.0f, rotateVec) *
                    Quaternion.LookRotation(m_RobotToHeliVec);
        }
        //照らしているときの動き
        else
        {
            m_RobotToHeliVec = (m_Robot.transform.position - transform.position);
            m_RobotToHeliVec.y = 0;
            transform.rotation =
                    Quaternion.AngleAxis(m_Velo.magnitude * 130.0f, rotateVec) *
                    Quaternion.LookRotation(m_RobotToHeliVec);
        }


        Spring(m_ResPos, ref m_Pos, ref m_Velo, 0.1f, 0.2f, 7.0f);
        transform.position = m_Pos;

    }

    public void LateUpdate()
    {
        m_Velo = transform.position - m_SevePos;
        m_SevePos = transform.position;
    }

    /// <summary>
    /// ヘリコプたーの位置を設定する
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position)
    {
        //帰還中は外側からは呼び出せない
        if (m_ReturnFlag) return;
        m_ResPos = position;
    }

    public float Vector2Cross(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.z - rhs.x * lhs.z;
    }


    /// <summary>
    /// バネ補間をする
    /// </summary>
    /// <param name="resPos">行きたい</param>
    /// <param name="pos">現在</param>
    /// <param name="velo">速度</param>
    /// <param name="stiffness">なんか</param>
    /// <param name="friction">なんか</param>
    /// <param name="mass">重さ</param>
    private void Spring(Vector3 resPos, ref Vector3 pos, ref Vector3 velo, float stiffness, float friction, float mass)
    {
        // バネの伸び具合を計算
        Vector3 stretch = (pos - resPos);
        // バネの力を計算
        Vector3 force = -stiffness * stretch;
        // 加速度を追加
        Vector3 acceleration = force / mass;
        // 移動速度を計算
        velo = friction * (velo + acceleration);
        // 座標の更新
        pos += velo;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (m_IsBreak && other.tag != "ExplosionCollision")
        {
            Instantiate(m_HeliBreakPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "ExplosionCollision")
        {
            m_IsBreak = true;
        }
    }



}

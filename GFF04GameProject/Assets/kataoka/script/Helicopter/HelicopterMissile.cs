using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMissile : MonoBehaviour
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
    //帰っているかどうか
    public bool m_ReturnFlag;

    private GameObject m_Robot;

    private float m_ResAngle;
    private float m_Angle;
    private float m_AngleVelo;

    private Vector3 m_ResPos;
    private Vector3 m_Pos;
    private Vector3 m_SpringVelo;

    private Vector3 m_SpawnPos;

    private Quaternion m_LerpSeveRotate;
    private Vector3 m_LerpSevePos;
    private float m_LerpReturnTime;
    private float m_LerpAttackTime;

    private HelicopterMissileManager m_MissileManager;

    public GameObject m_Light;

    private AudioClip heri_se_;

    // Use this for initialization
    void Start()
    {
        m_Velo = Vector3.zero;
        m_SevePos = transform.position;
        m_SpawnPos = transform.position;
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
        m_FireEffect = transform.Find("FireEffect").gameObject;


        m_FireEffect.SetActive(false);

        m_ResPos = transform.position;
        m_Pos = transform.position;
        m_SpringVelo = Vector3.zero;

        m_ResAngle = transform.rotation.eulerAngles.y;
        m_Angle = transform.rotation.eulerAngles.y;
        m_AngleVelo = 0.0f;

        m_ReturnFlag = false;
        m_IsBreak = false;

        m_LerpReturnTime = 0.0f;
        m_LerpAttackTime = 0.0f;

        m_LerpSeveRotate = Quaternion.identity;
        m_LerpSevePos = Vector3.zero;

        heri_se_ = GetComponent<AudioSource>().clip;
        GetComponent<AudioSource>().PlayOneShot(heri_se_);
    }

    // Update is called once per frame
    void Update()
    {

        m_Light.transform.rotation = Quaternion.LookRotation((m_Robot.transform.position + new Vector3(0, 4, 0)) - transform.position);
        //もし戻ってきたら消す
        if (m_ReturnFlag && Vector3.Distance(m_SpawnPos, transform.position) <= 10.0f)
        {
            Destroy(gameObject);
        }

        if (m_IsBreak)
        {
            m_FireEffect.SetActive(true);
            transform.Rotate(new Vector3(0, 1, 0.4f), 10.0f);
            transform.position += (new Vector3(0.0f, -4.0f, 0.0f)) * 3.0f * Time.deltaTime;
            return;
        }

        //ヘリコプターの回転処理
        m_Velo.y = 0.0f;
        Vector3 rotateVec = Vector3.Cross(Vector3.up, m_Velo);
        Vector3 vec = (m_Robot.transform.position - transform.position);
        vec.y = 0;

        if (m_ReturnFlag)
        {
            if (m_LerpReturnTime == 0.0f)
            {
                m_LerpSevePos = transform.position;
                m_LerpSeveRotate = transform.rotation;
            }
            //帰っていたら
            Vector3 returnVec = (m_SpawnPos - transform.position).normalized;
            returnVec.y = 0.0f;
            Quaternion rotate = Quaternion.LookRotation(returnVec);
            m_LerpReturnTime += 0.1f * Time.deltaTime;
            m_LerpReturnTime = Mathf.Clamp(m_LerpReturnTime, 0.0f, 1.0f);
            transform.rotation = Quaternion.Lerp(m_LerpSeveRotate, rotate, m_LerpReturnTime);
        }
        else
        {
            if (m_LerpAttackTime == 0.0f)
            {
                m_LerpSevePos = transform.position;
                m_LerpSeveRotate = transform.rotation;
            }
            //攻撃するか
            Vector3 attackVec = (m_Robot.transform.position - transform.position);
            attackVec.y = 0.0f;
            Quaternion rotate = Quaternion.LookRotation(attackVec);
            m_LerpAttackTime += 0.1f * Time.deltaTime;
            m_LerpAttackTime = Mathf.Clamp(m_LerpAttackTime, 0.0f, 1.0f);
            transform.rotation = Quaternion.Lerp(m_LerpSeveRotate, rotate, m_LerpAttackTime);
        }

        transform.rotation *=
            Quaternion.AngleAxis((int)(m_Velo.magnitude), -rotateVec);




        m_Propeller.transform.localEulerAngles += new Vector3(0, 0, 1000) * Time.deltaTime;


        Vector3 forwerd = transform.forward.normalized;
        forwerd.y = 0.0f;
        transform.position += forwerd * 8.0f * Time.deltaTime;

    }

    public void LateUpdate()
    {
        m_Velo = (transform.position - m_SevePos) / Time.deltaTime;
        m_SevePos = transform.position;
    }

    /// <summary>
    /// ヘリコプたーの位置を設定する
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position)
    {
        m_ResPos = position;
    }

    public float Vector2Cross(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.z - rhs.x * lhs.z;
    }
    /// <summary>
    /// バネ補間をする(float)
    /// </summary>
    /// <param name="resNum">行きたい</param>
    /// <param name="num">現在</param>
    /// <param name="velo">速度</param>
    /// <param name="stiffness">なんか</param>
    /// <param name="friction">なんか</param>
    /// <param name="mass">重さ</param>
    private void Spring(float resNum, ref float num, ref float velo, float stiffness, float friction, float mass)
    {
        // バネの伸び具合を計算
        float stretch = (num - resNum);
        // バネの力を計算
        float force = -stiffness * stretch;
        // 加速度を追加
        float acceleration = force / mass;
        // 移動速度を計算
        velo = friction * (velo + acceleration);
        // 座標の更新
        num += velo;
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
        if (other.gameObject.tag == "Beam")
        {
            m_IsBreak = true;
        }
    }
}

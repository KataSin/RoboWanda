using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Break_v2 : MonoBehaviour
{
    private tower_Type towerType_;
    private float m_tower_revision;

    //砂煙
    [SerializeField]
    [Header("砂煙")]
    private GameObject sand_smoke_manager_;

    [SerializeField]
    [Header("砂煙スケール倍率")]
    private float m_sand_smoke_scalar;

    //ビルの回転
    private Quaternion m_origin_rotation;

    //倒壊しているかどうか
    [SerializeField]
    private bool isBreak;

    //発生したかどうか
    private bool isOutBreak;

    [SerializeField]
    private GameObject originBill_obj_;
    private Vector3 m_origin_Lpos;
    private Vector3 m_origin_Lscale;

    [SerializeField]
    private float m_origin_rotationY;

    private GameObject scoreMana_;

    [SerializeField]
    private GameObject after_bill_;

    [SerializeField]
    private GameObject gareki_;

    private AudioClip break_se_;


    // Use this for initialization
    void Start()
    {
        towerType_ = GetComponent<tower_Type>();

        //ビルの大きさによる補正
        TypeAdaptation();

        //初期化
        m_origin_rotation = transform.rotation;
        //m_Bill_rotation = Quaternion.identity;
        //m_Break_rotation = Quaternion.identity;

        m_origin_Lpos = originBill_obj_.transform.localPosition;
        m_origin_Lscale = originBill_obj_.transform.localScale;

        gareki_.SetActive(false);

        if (GetComponent<AudioSource>() != null)
            break_se_ = GetComponent<AudioSource>().clip;
    }

    //ビルの大きさによる補正
    private void TypeAdaptation()
    {
        switch (towerType_.Get_TowerType())
        {
            //Low
            case 0:
                m_tower_revision = 1f;
                break;

            case 1:
                m_tower_revision = 1f;
                break;


            //High
            case 2:
                m_tower_revision = 1.4f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //倒壊挙動制御
        Collapse();
    }

    //倒壊挙動制御
    private void Collapse()
    {
        //倒壊していたら
        if (isBreak)
        {
            OutBreak_Smoke();
        }

    }

    //砂煙発生
    public void OutBreak_Smoke()
    {
        if (!isOutBreak)
        {
            Destroy(originBill_obj_);

            Vector3 ob_pos = transform.position;
            ob_pos.y = 0f;
            GameObject smoke = Instantiate(sand_smoke_manager_, ob_pos, Quaternion.identity);
            smoke.transform.Find("desert_Horizontal").localScale = transform.localScale * m_sand_smoke_scalar;
            smoke.transform.Find("desert_Vertical").localScale = transform.localScale * m_sand_smoke_scalar;

            Instantiate(after_bill_, transform);
            after_bill_.transform.localPosition = m_origin_Lpos;
            after_bill_.transform.localScale = m_origin_Lscale;

            if (GameObject.FindGameObjectWithTag("ScoreManager")
                && !GameObject.FindGameObjectWithTag("BriefingManager"))
            {
                scoreMana_ = GameObject.FindGameObjectWithTag("ScoreManager");
                scoreMana_.GetComponent<ScoreManager>().SetBreakCount();
            }

            gareki_.SetActive(true);

            if (GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().PlayOneShot(break_se_);

            isOutBreak = true;
        }
    }

    //倒壊フラグの設定
    public void Set_BreakFlag(bool breakflag)
    {
        isBreak = breakflag;
    }

    //倒壊フラグの取得
    public bool Get_BreakFlag()
    {
        return isBreak;
    }

    //タワーの崩壊状態の取得
    public uint Get_BreakType()
    {
        return towerType_.Get_BreakType();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RobotDamage>() != null
            ||
            other.gameObject.tag == "bom"
            ||
            other.gameObject.tag == "RobotArmAttack"
            ||
            other.gameObject.tag == "RobotBeam"
            ||
            other.gameObject.tag == "Missile"
            ||
            other.gameObject.tag == "ExplosionCollision"
            ||
            other.gameObject.tag == "BeamCollide")
            isBreak = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Break : MonoBehaviour
{
    private tower_Type towerType_;
    private float m_tower_revision;

    //破片
    [SerializeField]
    [Header("破片低")]
    private GameObject debris_low;

    //破片
    [SerializeField]
    [Header("破片高")]
    private GameObject debris_high;

    //砂煙
    [SerializeField]
    [Header("砂煙")]
    private GameObject sand_smoke_manager_;

    [SerializeField]
    [Header("砂煙スケール倍率")]
    private float m_sand_smoke_scalar;

    //回転スピード
    [SerializeField]
    [Header("回転にかかる時間(s)")]
    private float m_rotated_time;

    [SerializeField]
    private GameObject navMOb_obj_;
    private NavMeshObstacle navMOb_;

    //落下移動量
    private float m_down_pos_Y;

    //倒壊経過時間
    private float m_break_time;

    //ビルの回転
    [SerializeField]
    private Quaternion m_origin_rotation;

    //ビルの回転
    private Quaternion m_Bill_rotation;

    //破壊後の回転
    private Quaternion m_Break_rotation;

    //倒壊しているかどうか
    [SerializeField]
    private bool isBreak;

    //発生したかどうか
    private bool isOutBreak;

    //倒壊後かどうか
    private bool isAfter;

    //当たり判定
    [SerializeField]
    [Header("ビルの当たり判定")]
    private GameObject collide_manager_obj_;
    private tower_collide_manager collide_manager_;

    [SerializeField]
    private GameObject originBill_obj_;
    private Vector3 m_origin_Lpos;

    [SerializeField]
    private float m_origin_rotationY;

    private GameObject scoreMana_;

    [SerializeField]
    private GameObject after_bill_;

    [SerializeField]
    private GameObject gareki_bill_;

    [SerializeField]
    private GameObject bill_Lpoint_;

    private AudioClip break_se_;


    // Use this for initialization
    void Start()
    {
        towerType_ = GetComponent<tower_Type>();

        collide_manager_ = collide_manager_obj_.GetComponent<tower_collide_manager>();

        //ビルの大きさによる補正
        TypeAdaptation();

        //初期化
        m_origin_rotation = transform.rotation;
        //m_Bill_rotation = Quaternion.identity;
        //m_Break_rotation = Quaternion.identity;

        navMOb_ = navMOb_obj_.GetComponent<NavMeshObstacle>();

        m_origin_Lpos = originBill_obj_.transform.localPosition;

        gareki_bill_.SetActive(false);

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
        //倒壊していたら
        if (isBreak)
        {
            //倒壊時間経過
            m_break_time += 1f * Time.deltaTime;

            //落下スピード上昇
            m_down_pos_Y += 0.001f * (transform.localScale.y * m_sand_smoke_scalar) * Time.deltaTime;

            //倒壊中、後の処理
            //BreakAfter();
        }

        //倒壊挙動制御
        Collapse();

        //崩壊状態の反映
        BreakType_Reflect();

        //NavMeshObstracleのアクティブ
        Active_NavMesh();
    }

    //倒壊挙動制御
    private void Collapse()
    {
        if (collide_manager_.Get_Bill_CollideFlag())
        {
            isBreak = false;
            isOutBreak = false;
            collide_manager_.Set_Direction(0);
            collide_manager_.Set_Bill_CollideFlag(false);
        }

        //倒壊方向判定
        Collapse_Direction();

        //落下
        if (isBreak)
        {


            //transform.position += new Vector3(0f, -m_down_pos_Y, 0f);
        }

        //回転
        //if (!collide_manager_.Get_BreakAfterFlag())
        //    transform.rotation =
        //        Quaternion.Lerp(m_origin_rotation, m_Bill_rotation, m_break_time / m_rotated_time);
    }

    //倒壊方向判定
    private void Collapse_Direction()
    {
        if (!isBreak)
        {
            switch (collide_manager_.Get_Direction())
            {
                //前
                case 1:
                    OutBreak_Smoke();
                    m_Bill_rotation = Quaternion.Euler(90f, m_origin_rotationY, 0f);
                    m_Break_rotation = Quaternion.Euler(90f, m_origin_rotationY, 0f);

                    break;

                //左
                case 2:
                    OutBreak_Smoke();
                    m_Bill_rotation = Quaternion.Euler(0f, m_origin_rotationY, 90f);
                    m_Break_rotation = Quaternion.Euler(0f, m_origin_rotationY, 90f);

                    break;

                //後ろ
                case 3:
                    OutBreak_Smoke();
                    m_Bill_rotation = Quaternion.Euler(-90f, m_origin_rotationY, 0f);
                    m_Break_rotation = Quaternion.Euler(-90f, m_origin_rotationY, 0f);

                    break;

                //右
                case 4:
                    OutBreak_Smoke();
                    m_Bill_rotation = Quaternion.Euler(0f, m_origin_rotationY, -90f);
                    m_Break_rotation = Quaternion.Euler(0f, m_origin_rotationY, -90f);

                    break;

                default:
                    break;
            }
        }
    }

    //砂煙発生
    public void OutBreak_Smoke()
    {
        if (!isOutBreak)
        {
            Destroy(originBill_obj_);
            Destroy(bill_Lpoint_);
            Destroy(collide_manager_obj_);

            Vector3 ob_pos = transform.position;
            ob_pos.y = 0f;
            GameObject smoke = Instantiate(sand_smoke_manager_, ob_pos, Quaternion.identity);
            smoke.transform.Find("desert_Horizontal").localScale = transform.localScale * m_sand_smoke_scalar;
            smoke.transform.Find("desert_Vertical").localScale = transform.localScale * m_sand_smoke_scalar;

            Instantiate(after_bill_, transform);
            after_bill_.transform.localPosition = m_origin_Lpos;

            gareki_bill_.SetActive(true);
            //GareakiAdjustment();

            if (GameObject.FindGameObjectWithTag("ScoreManager")
                && !GameObject.FindGameObjectWithTag("BriefingManager"))
            {
                scoreMana_ = GameObject.FindGameObjectWithTag("ScoreManager");
                scoreMana_.GetComponent<ScoreManager>().SetBreakCount();
            }

            GetComponent<AudioSource>().PlayOneShot(break_se_);

            isBreak = true;
            isOutBreak = true;
        }
    }

    private void GareakiAdjustment()
    {
        //switch (test_)
        //{
        //    case 0:
        //        gareki_bill_.transform.localPosition = new Vector3(0f, -13.3f, 0f);
        //        gareki_bill_.transform.localRotation = Quaternion.Euler(0f, 10f, 0f);
        //        gareki_bill_.transform.localScale = new Vector3(15f, 15f, 15f);
        //        break;

        //    case 1:
        //        gareki_bill_.transform.localPosition = new Vector3(0f, -162f, 0f);
        //        gareki_bill_.transform.localRotation = Quaternion.Euler(0f, 10f + (m_origin_rotationY + 180f), 0f);
        //        gareki_bill_.transform.localScale = new Vector3(328f, 243f, 243f);
        //        break;
        //}
    }

    //倒壊中、後の処理
    private void BreakAfter()
    {
        //5s経過後
        if (m_break_time >= (6.9f * m_tower_revision))
        {
            //倒壊済みでなければ
            if (!isAfter && !collide_manager_.Get_BreakAfterFlag())
            {
                Vector3 ba_pos = new Vector3(
                    transform.position.x, 0f, transform.position.z);

                if (originBill_obj_ != null)
                {
                    //倒壊後のビルを生成
                    switch (towerType_.Get_TowerType())
                    {
                        //Low
                        case 0:
                            GameObject ba_obj = Instantiate(debris_low, ba_pos, m_Break_rotation);
                            ba_obj.transform.localScale = transform.localScale;
                            break;

                        //High
                        case 1:
                            ba_obj = Instantiate(debris_high, ba_pos, m_Break_rotation);
                            ba_obj.transform.localScale = transform.localScale;
                            break;
                    }
                }

                isAfter = true;
            }

            //8s経過後
            if (m_break_time >= (9f * m_tower_revision))
            {


                //自分を消去
                Destroy(gameObject);
            }
        }
    }

    //崩壊状態の反映
    private void BreakType_Reflect()
    {
        if (!collide_manager_.Get_BreakAfterFlag() || transform.rotation == Quaternion.identity)
            towerType_.Set_BreakType(0);

        else if (collide_manager_.Get_BreakAfterFlag() && transform.rotation != Quaternion.identity)
            towerType_.Set_BreakType(1);
    }

    //NavMeshObstracleのアクティブ
    private void Active_NavMesh()
    {
        if (originBill_obj_ != null)
        {
            if (isBreak)
                navMOb_.enabled = false;

            else if (!isBreak && collide_manager_.Get_BreakAfterFlag())
                navMOb_.enabled = true;
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

    //回転の設定
    public void Set_Rotation(Quaternion rotation)
    {
        m_Bill_rotation = rotation;
    }

    //崩壊後のタワーの回転の設定
    public void Set_BreakRotation(Quaternion rotation)
    {
        m_Break_rotation = rotation;
    }

    //タワーの崩壊状態の取得
    public uint Get_BreakType()
    {
        return towerType_.Get_BreakType();
    }
}
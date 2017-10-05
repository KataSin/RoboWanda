using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    private tower_Type towerType_;
    private float m_tower_revision;

    //破片
    [SerializeField]
    [Header("破片")]
    private GameObject debris_;

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

    //落下移動量
    private float m_down_pos_Y;

    //倒壊経過時間
    private float m_break_time;

    //ビルの回転
    private Quaternion m_Bill_rotation;

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

    // Use this for initialization
    void Start()
    {
        towerType_ = this.gameObject.GetComponent<tower_Type>();

        collide_manager_ = collide_manager_obj_.GetComponent<tower_collide_manager>();

        //ビルの大きさによる補正
        TypeAdaptation();

        //初期化
        m_Bill_rotation = Quaternion.identity;
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

            //High
            case 1:
                m_tower_revision = 1.5f;
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
            BreakAfter();
        }

        //倒壊挙動制御
        Collapse();
    }

    //倒壊挙動制御
    private void Collapse()
    {
        if (collide_manager_.Get_Bill_CollideFlag())
            isBreak = false;

        //倒壊方向判定
        Collapse_Direction();

        //落下
        if (isBreak)
            transform.position += new Vector3(0f, -m_down_pos_Y, 0f);

        //回転
        transform.rotation =
            Quaternion.Lerp(Quaternion.Euler(0f, 0f, 0f), m_Bill_rotation, m_break_time / m_rotated_time);
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
                    m_Bill_rotation = Quaternion.Euler(90f, 0f, 0f);

                    break;

                //左
                case 2:
                    OutBreak_Smoke();
                    m_Bill_rotation = Quaternion.Euler(0f, 0f, 90f);

                    break;

                //後ろ
                case 3:
                    OutBreak_Smoke();
                    m_Bill_rotation = Quaternion.Euler(-90f, 0f, 0f);

                    break;

                //右
                case 4:
                    OutBreak_Smoke();
                    m_Bill_rotation = Quaternion.Euler(0f, 0f, -90f);
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
            Vector3 ob_pos = transform.position;
            ob_pos.y = 0f;
            GameObject smoke = Instantiate(sand_smoke_manager_, ob_pos, Quaternion.identity);
            smoke.transform.Find("desert_Horizontal").localScale = transform.localScale * m_sand_smoke_scalar;
            smoke.transform.Find("desert_Vertical").localScale = transform.localScale * m_sand_smoke_scalar;

            isBreak = true;
            isOutBreak = true;
        }
    }

    //倒壊中、後の処理
    private void BreakAfter()
    {
        //5s経過後
        if (m_break_time >= (6f * m_tower_revision))
        {
            //倒壊済みでなければ
            if (!isAfter)
            {
                Vector3 ba_scale =
                    new Vector3(
                    transform.localScale.x,
                    transform.localScale.y / 3,
                    transform.localScale.z
                    );
                debris_.transform.localScale = ba_scale * m_sand_smoke_scalar;

                Vector3 ba_pos = new Vector3(
                    transform.position.x, debris_.transform.localScale.y / 3, transform.position.z);

                //倒壊後のビルを生成
                GameObject ba_obj = Instantiate(debris_, ba_pos, Quaternion.identity);
                isAfter = true;
            }

            //8s経過後
            if (m_break_time >= (8f * m_tower_revision))
            {
                //自分を消去
                Destroy(gameObject);
            }
        }
    }

    public void Set_BreakFlag(bool breakflag)
    {
        isBreak = breakflag;
    }

    public bool Get_BreakFlag()
    {
        return isBreak;
    }

    public void Set_Rotation(Quaternion rotation)
    {
        m_Bill_rotation = rotation;
    }
}
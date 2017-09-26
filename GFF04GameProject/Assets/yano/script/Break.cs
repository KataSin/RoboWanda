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
    [Header("砂煙")]
    private GameObject sand_smoke_manager_;

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
    private bool isBreak;

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
        //初期化
        m_Bill_rotation = Quaternion.identity;

        collide_manager_ = collide_manager_obj_.GetComponent<tower_collide_manager>();
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
            m_down_pos_Y += 0.001f * transform.localScale.y * Time.deltaTime;

            //倒壊中、後の処理
            BreakAfter();
        }

        //倒壊挙動制御
        Collapse();
    }

    //倒壊挙動制御
    private void Collapse()
    {
        //倒壊方向判定
        Collapse_Direction();

        //落下
        transform.position += new Vector3(0f, -m_down_pos_Y, 0f);

        //回転
        transform.rotation =
            Quaternion.Lerp(Quaternion.Euler(0f, 0f, 0f), m_Bill_rotation, m_break_time / m_rotated_time);
    }

    //倒壊方向判定
    private void Collapse_Direction()
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
        }
    }

    //砂煙発生
    private void OutBreak_Smoke()
    {
        if (!isBreak)
        {
            Vector3 ob_pos = transform.position;
            ob_pos.y = 0f;
            GameObject smoke = Instantiate(sand_smoke_manager_, ob_pos, Quaternion.identity);
            smoke.transform.Find("desert_Horizontal").localScale = transform.localScale;
            smoke.transform.Find("desert_Vertical").localScale = transform.localScale;
        }
        isBreak = true;
    }

    //倒壊中、後の処理
    private void BreakAfter()
    {
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
                //自分を消去
                Destroy(gameObject);
            }
        }
    }
}
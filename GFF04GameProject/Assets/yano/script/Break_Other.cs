using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Other : MonoBehaviour
{
    //落下移動量
    private float m_down_pos_Y;

    //倒壊経過時間
    private float m_break_time;

    //発生したかどうか
    private bool isOutBreak;

    //砂煙
    [SerializeField]
    [Header("砂煙")]
    private GameObject sand_smoke_manager_;

    [SerializeField]
    [Header("砂煙スケール倍率")]
    private float m_sand_smoke_scalar;

    [SerializeField]
    [Header("建物の当たり判定")]
    private GameObject obuild_collide_manager_obj_;
    private obuild_collide_manager obuild_collide_manager_;

    // Use this for initialization
    void Start()
    {
        obuild_collide_manager_ =
            obuild_collide_manager_obj_.GetComponent<obuild_collide_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obuild_collide_manager_.Get_HitFlag())
        {
            m_break_time += 1f * Time.deltaTime;

            m_down_pos_Y += 0.01f * transform.localScale.y * Time.deltaTime;

            Collapse();

            OutBreakSmoke();    
        }
    }

    private void Collapse()
    {
        transform.position += new Vector3(0f, -m_down_pos_Y, 0f);

        if (m_break_time >= 5f)
            Destroy(this.gameObject);
    }

    private void OutBreakSmoke()
    {
        if (!isOutBreak)
        {
            Vector3 ob_pos = transform.position;
            ob_pos.y = 0f;
            GameObject smoke = Instantiate(sand_smoke_manager_, ob_pos, Quaternion.identity);
            smoke.transform.Find("desert_Horizontal").localScale = transform.localScale * m_sand_smoke_scalar;
            smoke.transform.Find("desert_Vertical").localScale = transform.localScale * m_sand_smoke_scalar;

            isOutBreak = true;
        }
    }
}
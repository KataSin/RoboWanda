using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_collide_manager : MonoBehaviour
{
    //前の当たり判定
    [SerializeField]
    private GameObject forward_obj_;
    private tower_collide forward_;

    //左の当たり判定
    [SerializeField]
    private GameObject left_obj_;
    private tower_collide left_;

    //後ろの当たり判定
    [SerializeField]
    private GameObject back_obj_;
    private tower_collide back_;

    //右の当たり判定
    [SerializeField]
    private GameObject right_obj_;
    private tower_collide right_;

    //方向(1:前、2:左、3:後ろ、4:右)
    [SerializeField]
    [Header("方向(1:前、2:左、3:後ろ、4:右)")]
    private int m_direction;

    //ビルと当たったか
    [SerializeField]
    [Header("ビルと当たったか")]
    private bool isBillCollide;

    private bool isBreakAfter;

    // Use this for initialization
    void Start()
    {
        forward_ = forward_obj_.GetComponent<tower_collide>();
        left_ = left_obj_.GetComponent<tower_collide>();
        back_ = back_obj_.GetComponent<tower_collide>();
        right_ = right_obj_.GetComponent<tower_collide>();

        m_direction = 0;
        isBillCollide = false;

        isBreakAfter = false;

    }

    // Update is called once per frame
    void Update()
    {
        //前(forward)
        if (forward_.Get_CollideFlag())
            m_direction = 1;

        //左(left)
        else if (left_.Get_CollideFlag())
            m_direction = 2;

        //後ろ(back)
        else if (back_.Get_CollideFlag())
            m_direction = 3;

        //右(right)
        else if (right_.Get_CollideFlag())
            m_direction = 4;

        //ビル
        if ((forward_.Get_Bill_CollideFlag() || left_.Get_Bill_CollideFlag()
            || back_.Get_Bill_CollideFlag() || right_.Get_Bill_CollideFlag()))
        {
            isBillCollide = true;

            Flag_Reset();
        }

        if ((forward_.Get_BreakAfterFlag() || left_.Get_BreakAfterFlag()
            || back_.Get_BreakAfterFlag() || right_.Get_BreakAfterFlag()))
        {
            isBreakAfter = true;
        }
    }

    public void Flag_Reset()
    {
        forward_.Set_CollideFlag(false);
        left_.Set_CollideFlag(false);
        back_.Set_CollideFlag(false);
        right_.Set_CollideFlag(false);

        forward_.Set_Bill_CollideFlag(false);
        left_.Set_Bill_CollideFlag(false);
        back_.Set_Bill_CollideFlag(false);
        right_.Set_Bill_CollideFlag(false);
    }

    //方向の取得
    public int Get_Direction()
    {
        return m_direction;
    }

    public void Set_Direction(int direction)
    {
        m_direction = direction;
    }

    //ビルと当たったかどうかの取得
    public bool Get_Bill_CollideFlag()
    {
        return isBillCollide;
    }

    public void Set_Bill_CollideFlag(bool flag)
    {
        isBillCollide = flag;
    }

    public bool Get_BreakAfterFlag()
    {
        return isBreakAfter;
    }
}

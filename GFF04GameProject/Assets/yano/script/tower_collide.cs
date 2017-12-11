using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_collide : MonoBehaviour
{
    private enum HitOther
    {
        Neutral,
        Bomb,
        Robot,
    }
    private HitOther hitOther_;

    //当たったかどうか
    [SerializeField]
    [Header("当たったかどうか")]
    private bool isCollide;

    [SerializeField]
    [Header("ビルと当たったか")]
    private bool isBillCollide;

    [SerializeField]
    [Header("当たり判定を纏める親")]
    private GameObject parent_tower_obj_;
    private Transform parent_Tower_;

    [SerializeField]
    private bool isBreakAfter;

    private bool isRobotHit;

    private bool isOriginChange;

    // Use this for initialization
    void Start()
    {
        hitOther_ = HitOther.Neutral;

        isCollide = false;
        isBillCollide = false;

        isBreakAfter = false;
        isRobotHit = false;

        isOriginChange = false;

        parent_Tower_ = parent_tower_obj_.GetComponent<Transform>();
    }

    //当たったかどうかの取得
    public bool Get_CollideFlag()
    {
        return isCollide;
    }

    public void Set_CollideFlag(bool flag)
    {
        isCollide = flag;
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

    //当たった相手取得
    public uint Get_HitOther()
    {
        return (uint)hitOther_;
    }

    //ロボットと当たったかどうかの取得
    public bool Get_RobotHit()
    {
        return isRobotHit;
    }

    public bool Get_OriginChange()
    {
        return isOriginChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponent<RobotDamage>() != null
        //    &&
        //    parent_Tower_.transform.rotation != Quaternion.identity)
        //{
        //    isOriginChange = true;
        //}

        //ここに対象オブジェクトを指名
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
            other.gameObject.tag == "RobotEye")
        {
            isCollide = true;

            CheckOther(other);
        }

        if (!isBreakAfter
            &&
            !other.transform.IsChildOf(parent_Tower_)
            &&
            other.gameObject.tag == "TowerCollision")
        {
            isBillCollide = true;
            isBreakAfter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isCollide
            &&
            parent_Tower_.transform.rotation == Quaternion.identity
            &&
            !other.transform.IsChildOf(parent_Tower_)
            &&
            other.gameObject.tag == "TowerCollision")
        {
            isBreakAfter = false;
        }
    }

    //当たった相手チェック
    private void CheckOther(Collider other)
    {
        if (other.gameObject.tag == "bom")
            hitOther_ = HitOther.Bomb;

        else if (other.gameObject.tag == "RobotArmAttack")
        {
            hitOther_ = HitOther.Robot;
            isRobotHit = true;
        }
    }
}

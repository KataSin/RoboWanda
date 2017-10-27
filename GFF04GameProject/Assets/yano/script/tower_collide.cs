using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_collide : MonoBehaviour
{
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

    // Use this for initialization
    void Start()
    {
        isCollide = false;
        isBillCollide = false;

        isBreakAfter = false;

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

    private void OnTriggerEnter(Collider other)
    {
        //ここに対象オブジェクトを指名
        if ((other.gameObject.tag == "bom"
            ||
            other.gameObject.tag == "RobotArmAttack"))
        {
            isCollide = true;
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
}

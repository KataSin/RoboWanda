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

    // Use this for initialization
    void Start()
    {
        isCollide = false;
        isBillCollide = false;

        parent_Tower_ = parent_tower_obj_.GetComponent<Transform>();
    }

    //当たったかどうかの取得
    public bool Get_CollideFlag()
    {
        return isCollide;
    }

    //ビルと当たったかどうかの取得
    public bool Get_Bill_CollideFlag()
    {
        return isBillCollide;
    }

    void OnTriggerEnter(Collider other)
    {
        //ここに対象オブジェクトを指名
        if (other.gameObject.tag == "bom"||other.tag=="RobotArmAttack")
        {
            isCollide = true;
        }

        if (!other.transform.IsChildOf(parent_Tower_) && other.gameObject.tag == "Tower")
        {
            isBillCollide = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_collide : MonoBehaviour
{
    //当たったかどうか
    [SerializeField]
    private bool isCollide;

    [SerializeField]
    private bool isBillCollide;

    [SerializeField]
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

    public bool Get_Bill_CollideFlag()
    {
        return isBillCollide;
    }

    //void OnCollisionEnter(Collision other)
    //{
    //    //ここに対象オブジェクトを指名
    //    if (other.gameObject.tag == "bom")
    //    {
    //        isCollide = true;
    //    }

    //    if (/*!other.transform.IsChildOf(parent_Tower_) && */other.gameObject.tag == "Tower")
    //    {
    //        Debug.Log("当たった");
    //        isBillCollide = true;
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        //ここに対象オブジェクトを指名
        if (other.gameObject.tag == "bom")
        {
            isCollide = true;
        }

        if (!other.transform.IsChildOf(parent_Tower_) && other.gameObject.tag == "Tower")
        {
            Debug.Log("当たった");
            isBillCollide = true;
        }
    }
}

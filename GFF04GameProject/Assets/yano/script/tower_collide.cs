using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_collide : MonoBehaviour
{
    //当たったかどうか
    [SerializeField]
    private bool isCollide;

    // Use this for initialization
    void Start()
    {
        isCollide = false;
    }

    //当たったかどうかの取得
    public bool Get_CollideFlag()
    {
        return isCollide;
    }

    void OnCollisionEnter(Collision other)
    {
        //ここに対象オブジェクトを指名
        if (other.gameObject.tag == "bom")
        {
            isCollide = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obuild_collide_manager : MonoBehaviour
{
    [SerializeField]
    [Header("当たり判定1")]
    private GameObject collide_1_obj_;
    private obuild_collide collide_1_;

    [SerializeField]
    [Header("当たり判定2")]
    private GameObject collide_2_obj_;
    private obuild_collide collide_2_;

    private bool isHit;

    // Use this for initialization
    void Start()
    {
        collide_1_ = collide_1_obj_.GetComponent<obuild_collide>();
        collide_2_ = collide_2_obj_.GetComponent<obuild_collide>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collide_1_.Get_CollideFlag() || collide_2_.Get_CollideFlag())
        {
            isHit = true;
        }
    }

    //当たったかの取得
    public bool Get_HitFlag()
    {
        return isHit;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obuild_collide : MonoBehaviour
{
    [SerializeField]
    [Header("当たったかどうか")]
    private bool isCollide;

    // Use this for initialization
    void Start()
    {
        isCollide = false;
    }

    public bool Get_CollideFlag()
    {
        return isCollide;
    }

    private void OnTriggerEnter(Collider other)
    {
        //ここに対象オブジェクトを指名
        if (other.gameObject.tag == "bom" || other.gameObject.tag == "RobotArmAttack")
        {
            isCollide = true;
        }
    }
}

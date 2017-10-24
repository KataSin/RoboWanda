﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBillCollision : MonoBehaviour
{
    private bool m_IsCollision;
    // Use this for initialization
    void Start()
    {
        m_IsCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        //m_IsCollision = false;
    }
    /// <summary>
    /// 当たっているかどうかを取得する
    /// </summary>
    /// <returns>当たっているかどうか</returns>
    public bool GetCollisionFlag()
    {
        return m_IsCollision;
    }
    /// <summary>
    /// あたり判定フラグを設定する
    /// </summary>
    /// <param name="flag"></param>
    public void SetCollisionFlag(bool flag)
    {
        m_IsCollision = flag;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TowerCollision")
        {
            m_IsCollision = true;
        }
    }

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "TowerCollision")
    //    {
    //        m_IsCollision = false;
    //    }
    //}

}

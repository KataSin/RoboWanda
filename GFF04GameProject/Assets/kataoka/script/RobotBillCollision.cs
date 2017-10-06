using System.Collections;
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
        m_IsCollision = false;
    }
    /// <summary>
    /// 当たっているかどうかを取得する
    /// </summary>
    /// <returns>当たっているかどうか</returns>
    public bool GetCollisionFlag()
    {
        return m_IsCollision;
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "BreakTower")
        {
            if (other.GetComponent<tower_Type>().Get_TowerType() == 0)
                m_IsCollision = true;
        }
    }


}

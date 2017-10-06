using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_Type : MonoBehaviour
{
    private enum TowerType
    {
        Low,
        High,
        Other,
    }

    [SerializeField]
    [Header("取得する場合 0:Low 1:High 2:Other")]
    private TowerType towerType;


    public uint Get_TowerType()
    {
        return (uint)towerType;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="RobotArmAttack")
        {
            Destroy(gameObject);
        }
    }


}

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

    private enum BreakType
    {
        No,
        H_Collapse,
    }

    [SerializeField]
    [Header("取得する場合 0:Low 1:High 2:Other")]
    private TowerType towerType;

    [SerializeField]
    [Header("取得する場合 0:健在 1:半壊")]
    private BreakType breakType;

    //タワーのタイプ取得
    public uint Get_TowerType()
    {
        return (uint)towerType;
    }

    //タワーの崩壊状態の取得
    public uint Get_BreakType()
    {
        return (uint)breakType;
    }

    //タワーの崩壊状態の設定
    public void Set_BreakType(uint type)
    {
        breakType = (BreakType)type;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="RobotArmAttack")
        {
            Destroy(gameObject);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_point : MonoBehaviour
{
    [SerializeField]
    [Header("親")]
    private GameObject tower_parent_obj_;
    private Break tower_break_;
    private Break_v2 tower_breakV2_;

    // Use this for initialization
    void Start()
    {
        if (tower_parent_obj_.GetComponent<Break>() != null)
            tower_break_ = tower_parent_obj_.GetComponent<Break>();

        else if (tower_parent_obj_.GetComponent<Break_v2>() != null)
            tower_breakV2_ = tower_parent_obj_.GetComponent<Break_v2>();
    }

    //倒壊フラグの取得
    public bool Get_BreakFlag()
    {
        if (tower_parent_obj_.GetComponent<Break>() != null)
            return tower_break_.Get_BreakFlag();

        else
            return tower_breakV2_.Get_BreakFlag();
    }

    //タワーの崩壊状態の取得(0:健在,1:半壊)
    public uint Get_BreakType()
    {
        return tower_break_.Get_BreakType();
    }
}

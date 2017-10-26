using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_point : MonoBehaviour
{
    [SerializeField]
    private GameObject tower_parent_obj_;
    private Break tower_break_;

    // Use this for initialization
    void Start()
    {
        tower_break_ = tower_parent_obj_.GetComponent<Break>();
    }

    public bool Get_BreakFlag()
    {
        return tower_break_.Get_BreakFlag();
    }
}

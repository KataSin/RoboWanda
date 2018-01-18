﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBillBreak : MonoBehaviour
{
    private Break_ST break_;

    [SerializeField]
    [Header("ここにアタッチしたビルをテスト倒壊")]
    private List<GameObject> bills_;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < bills_.Count; ++i)
            break_ = bills_[i].GetComponent<Break_ST>();

        isClear = false;
    }

    void Update()
    {
        if (!isClear && !break_.Get_BreakFlag())
            TestCollapse();
    }

    private void TestCollapse()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            break_.Set_BreakFlag(true);
            break_.OutBreak_Smoke();

            isClear = true;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            break_.Set_BreakFlag(true);
            break_.OutBreak_Smoke();

            isClear = true;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            break_.Set_BreakFlag(true);
  
            break_.OutBreak_Smoke();

            isClear = true;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            break_.Set_BreakFlag(true);
        
            break_.OutBreak_Smoke();

            isClear = true;
        }
    }
}

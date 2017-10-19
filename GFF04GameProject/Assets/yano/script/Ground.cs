using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private GameObject break_ground_obj_;
    private Break_Ground break_ground_;

    [SerializeField]
    [Header("沈下後のパーツ")]
    private GameObject break_ground_parts_;

    [SerializeField]
    [Header("穴の判定")]
    private GameObject hole_collide_obj_;

    private bool isClear;


    // Use this for initialization
    void Start()
    {
        break_ground_ = break_ground_obj_.GetComponent<Break_Ground>();

        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (break_ground_.Get_BreakFlag() && !isClear)
        {
            Instantiate(break_ground_parts_, transform);
            Instantiate(hole_collide_obj_, transform);

            isClear = true;
        }
    }
}

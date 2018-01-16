using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingCnt : MonoBehaviour
{
    [SerializeField]
    private Camera main_Cam_;

    [SerializeField]
    private Camera cnt_Cam_;


    // Use this for initialization
    void Start()
    {
        main_Cam_.enabled = true;
        cnt_Cam_.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            cnt_Cam_.enabled = !cnt_Cam_.enabled;
        }
    }
}

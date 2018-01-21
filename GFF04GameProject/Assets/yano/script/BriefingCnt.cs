using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingCnt : MonoBehaviour
{
    [SerializeField]
    private Camera main_Cam_;

    [SerializeField]
    private Camera cnt_Cam_;

    [SerializeField]
    private Camera debug_Cam_;

    [SerializeField]
    private bool isDebug;


    // Use this for initialization
    void Start()
    {
        main_Cam_.enabled = true;
        cnt_Cam_.enabled = false;
        debug_Cam_.enabled = false;
        isDebug = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)
            && debug_Cam_.enabled == false)
        {
            cnt_Cam_.enabled = !cnt_Cam_.enabled;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            debug_Cam_.enabled = !debug_Cam_.enabled;
            isDebug = !isDebug;
        }
    }

    public bool Get_Debug()
    {
        return isDebug;
    }
}

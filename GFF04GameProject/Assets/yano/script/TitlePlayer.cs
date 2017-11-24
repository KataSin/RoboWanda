using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    enum TitlePlayerState
    {
        SitDown,
        StandUp,
    }

    [SerializeField]
    private TitlePlayerState state_;

    [SerializeField]
    private bool standFlag;

    [SerializeField]
    private bool smokeFlag;

    private Animator animator_;

    // Use this for initialization
    void Start()
    {
        state_ = TitlePlayerState.SitDown;
        standFlag = false;
        smokeFlag = false;

        animator_ = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            state_ = TitlePlayerState.StandUp;
            //standFlag = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            smokeFlag = true;

        animator_.SetBool("Standflag", standFlag);
        animator_.SetBool("Smokeflag", smokeFlag);

        smokeFlag = false;
    }

    public void Set_StandFlag(bool l_standFlag)
    {
        standFlag = l_standFlag;
    }
}

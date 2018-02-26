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

    [SerializeField]
    private GameObject titleMana_;

    [SerializeField]
    private GameObject look_point_;

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

        if (Input.GetButtonDown("Cancel"))
            smokeFlag = true;

        animator_.SetBool("Standflag", standFlag);
        animator_.SetBool("Smokeflag", smokeFlag);
        animator_.SetBool("Skip", titleMana_.GetComponent<TitleManager>().GetStandClear());

        smokeFlag = false;

        if (titleMana_.GetComponent<TitleManager>().GetStandClear())
            titleMana_.GetComponent<TitleManager>().SetStandClear(false);
    }


    void OnAnimatorIK()
    {
        if (titleMana_.GetComponent<TitleManager>().GetState() == TitleManager.TitleState.Sally)
        {
            animator_.SetLookAtWeight(1.0f, 0.0f, 1.0f, 0.0f, 0.0f);
            animator_.SetLookAtPosition(look_point_.transform.position);
        }
    }

    public void Set_StandFlag(bool l_standFlag)
    {
        standFlag = l_standFlag;
    }
}

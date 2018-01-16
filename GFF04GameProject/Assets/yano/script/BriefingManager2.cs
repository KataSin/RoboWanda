using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingManager2 : MonoBehaviour
{
    private enum State
    {
        None,
        One,
        Two,
        Three,
        Four,
        Five,
        Finish,
    }

    [SerializeField]
    private State state_;

    [SerializeField]
    private GameObject briefing_cam_;

    [SerializeField]
    private GameObject tower_mana_;

    [SerializeField]
    private GameObject robot_;

    [SerializeField]
    private GameObject bomber_;

    private bool isTrigger;

    private float t1;

    [SerializeField]
    private GameObject ch47_;

    private bool isCh47;

    [SerializeField]
    private GameObject lightSpawn_;

    [SerializeField]
    private GameObject ui_;

    // Use this for initialization
    void Start()
    {
        state_ = State.None;

        isTrigger = false;

        bomber_.SetActive(false);
        ch47_.GetComponent<StrategyDropTank>().enabled = false;

        t1 = 0f;

        isCh47 = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state_)
        {
            case State.None:
                NoneUpdate();
                break;
            case State.One:
                OneUpdate();
                break;
            case State.Two:
                TwoUpdate();
                break;
            case State.Three:
                ThreeUpdate();
                break;
            case State.Four:
                FourUpdate();
                break;
            case State.Five:
                FiveUpdate();
                break;
            case State.Finish:
                ui_.GetComponent<BlackOut_UI>().BlackOut();
                break;
        }
    }

    private void NoneUpdate()
    {
        ui_.GetComponent<BlackOut_UI>().FeadIn();

        if (Input.GetKeyDown(KeyCode.Z)
            && ui_.GetComponent<BlackOut_UI>().Get_Clear())
        {
            state_ = State.One;
            tower_mana_.GetComponent<TowerManager>().InitBill();
            ui_.GetComponent<BlackOut_UI>().ResetT();
        }
    }

    private void OneUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().TargetCam_Move();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            state_ = State.Two;
        }
    }

    private void TwoUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().TargetCam_Back();
        if (briefing_cam_.GetComponent<BriefingCamera>().Get_Clear())
            tower_mana_.GetComponent<TowerManager>().TowerUp();
        if (tower_mana_.GetComponent<TowerManager>().Get_Clear())
        {
            robot_.GetComponent<BriefingRobot>().Beam();
        }

        if (Input.GetKeyDown(KeyCode.Z)
            &&
            robot_.GetComponent<BriefingRobot>().Get_MissileFinishFlag())
        {
            state_ = State.Three;
            bomber_.SetActive(true);
            tower_mana_.GetComponent<TowerManager>().TowerCheck();
        }
    }

    private void ThreeUpdate()
    {
        tower_mana_.GetComponent<TowerManager>().Tower2Up();
        briefing_cam_.GetComponent<BriefingCamera>().TopViewCam();

        if (t1 >= 25f)
        {
            state_ = State.Four;
            tower_mana_.GetComponent<TowerManager>().TowerCheck2();
        }

        t1 += 1.0f * Time.deltaTime;
    }

    private void FourUpdate()
    {
        tower_mana_.GetComponent<TowerManager>().Tower3Up();
        briefing_cam_.GetComponent<BriefingCamera>().SideViewCam();
        if (!isCh47)
        {
            ch47_.GetComponent<StrategyDropTank>().enabled = true;
            isCh47 = true;
        }
        tower_mana_.GetComponent<TowerManager>().TowerBreak();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            state_ = State.Five;
        }
    }

    private void FiveUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().NormalViewCam();
        lightSpawn_.GetComponent<LightBomSpawn>().Spawn();

        if (robot_.GetComponent<BriefingRobot>().GetState() == 3)
            state_ = State.Finish;
    }
}

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

    private float t0, t1, t2;

    [SerializeField]
    private GameObject ch47_;

    private bool isCh47;

    [SerializeField]
    private GameObject lightSpawn_;

    [SerializeField]
    private GameObject ui_;

    private GameObject sceneCnt_;

    private bool isLScene;

    [SerializeField]
    private GameObject text_;

    [SerializeField]
    private int m_textState;

    [SerializeField]
    private GameObject target_ui_;

    // Use this for initialization
    void Start()
    {
        state_ = State.None;
        m_textState = 1;
        isTrigger = false;

        bomber_.SetActive(false);
        ch47_.GetComponent<StrategyDropTank>().enabled = false;
        target_ui_.SetActive(false);

        t0 = 0f;
        t1 = 0f;
        t2 = 0f;

        isCh47 = false;

        isLScene = false;

        if (GameObject.FindGameObjectWithTag("SceneController"))
            sceneCnt_ = GameObject.FindGameObjectWithTag("SceneController");
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
                if (!isLScene && sceneCnt_ != null
                    && ui_.GetComponent<BlackOut_UI>().Get_Clear())
                {
                    StartCoroutine(sceneCnt_.GetComponent<SceneController>().SceneLoad("lightTest 5"));
                    isLScene = true;
                }
                break;
        }

        text_.GetComponent<TextBriefing>().Set_State(m_textState);
    }

    private void NoneUpdate()
    {
        ui_.GetComponent<BlackOut_UI>().FeadIn();

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Submit"))
            && ui_.GetComponent<BlackOut_UI>().Get_Clear())
        {
            state_ = State.One;
            tower_mana_.GetComponent<TowerManager>().InitBill();
            ui_.GetComponent<BlackOut_UI>().ResetT();

            text_.GetComponent<TextBriefing>().TextReset();
            m_textState++;
        }
    }

    private void OneUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().TargetCam_Move();
        if (briefing_cam_.GetComponent<BriefingCamera>().Get_Pick())
            target_ui_.SetActive(true);
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Submit")))
        {
            text_.GetComponent<TextBriefing>().TextReset();
            m_textState++;
            if (m_textState == 3)
            {
                state_ = State.Two;
                target_ui_.SetActive(false);
            }
        }
    }

    private void TwoUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().TargetCam_Back();

        if (briefing_cam_.GetComponent<BriefingCamera>().Get_Clear()
            && !tower_mana_.GetComponent<TowerManager>().Get_Clear())
            tower_mana_.GetComponent<TowerManager>().TowerUp();

        if (tower_mana_.GetComponent<TowerManager>().Get_Clear())
        {
            tower_mana_.GetComponent<TowerManager>().BeforeBreakColor1();

            if (t0 >= 2f)
            {
                robot_.GetComponent<BriefingRobot>().Beam();
                if (t0 >= 7f)
                    tower_mana_.GetComponent<TowerManager>().BeforeBreakColor2();
            }
            t0 += 1.0f * Time.deltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Submit"))
            &&
            robot_.GetComponent<BriefingRobot>().Get_MissileFinishFlag())
        {
            text_.GetComponent<TextBriefing>().TextReset();
            m_textState++;
            state_ = State.Three;
            bomber_.SetActive(true);
            tower_mana_.GetComponent<TowerManager>().TowerCheck();
        }
    }

    private void ThreeUpdate()
    {
        tower_mana_.GetComponent<TowerManager>().Tower2Up();
        briefing_cam_.GetComponent<BriefingCamera>().TopViewCam();

        if (t1 >= 3f)
        {
            tower_mana_.GetComponent<TowerManager>().BeforeBreakColor3();

            if (t1 >= 18f)
            {
                text_.GetComponent<TextBriefing>().TextReset();
                m_textState += 2;
                state_ = State.Four;
                tower_mana_.GetComponent<TowerManager>().TowerCheck2();
            }
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

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Submit")))
        {
            text_.GetComponent<TextBriefing>().TextReset();
            m_textState += 2;
            state_ = State.Five;
        }
    }

    private void FiveUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().NormalViewCam();
        lightSpawn_.GetComponent<LightBomSpawn>().Spawn();

        if (robot_.GetComponent<BriefingRobot>().GetState() == 3)
        {
            if (t2 >= 10f)
            {
                text_.GetComponent<TextBriefing>().TextReset();
                m_textState += 2;
                state_ = State.Finish;
            }
            t2 += 1.0f * Time.deltaTime;
        }
    }
}

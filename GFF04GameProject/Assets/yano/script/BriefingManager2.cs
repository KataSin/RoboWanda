using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject tank_;

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

    [SerializeField]
    private GameObject targetUnder_ui_;

    [SerializeField]
    private GameObject skip_gauge;

    [SerializeField]
    private GameObject skip_button_ui;

    [SerializeField]
    private float m_skip_timer;

    // Use this for initialization
    void Start()
    {
        state_ = State.None;
        m_textState = 1;
        isTrigger = false;

        bomber_.SetActive(false);
        target_ui_.SetActive(false);
        targetUnder_ui_.SetActive(false);

        t0 = 0f;
        t1 = 0f;
        t2 = 0f;

        m_skip_timer = 0f;
        skip_button_ui.SetActive(false);

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
                SkipBriefing();
                break;
            case State.Two:
                TwoUpdate();
                SkipBriefing();
                break;
            case State.Three:
                ThreeUpdate();
                SkipBriefing();
                break;
            case State.Four:
                FourUpdate();
                SkipBriefing();
                break;
            case State.Five:
                FiveUpdate();
                SkipBriefing();
                break;
            case State.Finish:
                FinishUpdate();
                break;
        }

        m_skip_timer = Mathf.Clamp(m_skip_timer, 0f, 4f);
        text_.GetComponent<TextBriefing>().Set_State(m_textState);
    }

    private void NoneUpdate()
    {
        ui_.GetComponent<BlackOut_UI>().FeadIn();

        if (t0 >= 3f
            && ui_.GetComponent<BlackOut_UI>().Get_Clear())
        {
            t0 = 0f;
            state_ = State.One;
            tower_mana_.GetComponent<TowerManager>().InitBill();
            ui_.GetComponent<BlackOut_UI>().ResetT();

            text_.GetComponent<TextBriefing>().TextReset();
            m_textState++;
        }

        t0 += 1.0f * Time.deltaTime;
    }

    private void OneUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().TargetCam_Move();
        if (briefing_cam_.GetComponent<BriefingCamera>().Get_Pick())
        {
            targetUnder_ui_.SetActive(true);

            if (targetUnder_ui_.GetComponent<ui_text_underLine>().Get_Clear())
                target_ui_.SetActive(true);
        }
        if (t0 >= 3f)
        {
            text_.GetComponent<TextBriefing>().TextReset();
            m_textState++;
            if (m_textState == 3)
            {
                t0 = 0f;
                state_ = State.Two;
                target_ui_.SetActive(false);
                targetUnder_ui_.SetActive(false);
            }
        }

        t0 += 1.0f * Time.deltaTime;
    }

    private void TwoUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().TargetCam_Back();

        if (briefing_cam_.GetComponent<BriefingCamera>().Get_Clear()
            && !tower_mana_.GetComponent<TowerManager>().Get_Clear())
            tower_mana_.GetComponent<TowerManager>().TowerUp();

        if (tower_mana_.GetComponent<TowerManager>().Get_Clear())
        {
            if (t0 >= 2f)
            {
                robot_.GetComponent<BriefingRobot>().Beam();
            }
            t0 += 1.0f * Time.deltaTime;
        }

        if (robot_.GetComponent<BriefingRobot>().Get_MissileFinishFlag())
        {
            if (t1 >= 1.5f)
            {
                text_.GetComponent<TextBriefing>().TextReset();
                m_textState++;
                state_ = State.Three;
                t0 = 0f;
                t1 = 0f;
                bomber_.SetActive(true);
                tower_mana_.GetComponent<TowerManager>().TowerCheck();
            }

            t1 += 1.0f * Time.deltaTime;
        }
    }

    private void ThreeUpdate()
    {
        tower_mana_.GetComponent<TowerManager>().Tower2Up();
        briefing_cam_.GetComponent<BriefingCamera>().TopViewCam();

        if (t0 >= 3f)
        {
            if (t0 >= 12f)
            {
                text_.GetComponent<TextBriefing>().TextReset();
                m_textState += 2;
                state_ = State.Four;
                t0 = 0f;
                tower_mana_.GetComponent<TowerManager>().TowerCheck2();
            }
        }

        t0 += 1.0f * Time.deltaTime;
    }

    private void FourUpdate()
    {
        tower_mana_.GetComponent<TowerManager>().Tower3Up();
        tower_mana_.GetComponent<TowerManager>().Tower4Up();
        briefing_cam_.GetComponent<BriefingCamera>().SideViewCam();
        if (!isCh47)
        {
            tank_.GetComponent<BriefingStTank>().Set_SpawnFlag(true);
            isCh47 = true;
        }
        tower_mana_.GetComponent<TowerManager>().TowerBreak();

        if (GameObject.FindGameObjectWithTag("GameTank"))
        {
            if (tank_.GetComponent<BriefingStTank>().Get_Clear())
            {
                if (t0 >= 1.5f)
                {
                    text_.GetComponent<TextBriefing>().TextReset();
                    m_textState += 2;
                    state_ = State.Five;
                }
                t0 += 1.0f * Time.deltaTime;
            }
        }
    }

    private void FiveUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().NormalViewCam();
        lightSpawn_.GetComponent<LightBomSpawn>().Spawn();

        if (robot_.GetComponent<BriefingRobot>().GetState() == 3)
        {
            if (t2 >= 8f)
            {
                text_.GetComponent<TextBriefing>().TextReset();
                m_textState += 2;
                state_ = State.Finish;
            }
            t2 += 1.0f * Time.deltaTime;
        }
    }

    private void FinishUpdate()
    {
        ui_.GetComponent<BlackOut_UI>().BlackOut();
        if (!isLScene && sceneCnt_ != null
            && ui_.GetComponent<BlackOut_UI>().Get_Clear())
        {
            StartCoroutine(sceneCnt_.GetComponent<SceneController>().SceneLoad("lightTest 5"));
            isLScene = true;
        }
    }

    private void SkipBriefing()
    {
        if (m_skip_timer <= 0f)
        {
            skip_gauge.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            skip_button_ui.SetActive(false);
        }
        else if (m_skip_timer > 0f)
        {
            skip_gauge.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            skip_button_ui.SetActive(true);
        }

        skip_gauge.GetComponent<Image>().fillAmount = m_skip_timer / 3f;

        if (m_skip_timer >= 3f)
        {
            ui_.GetComponent<BlackOut_UI>().ResetT();
            skip_gauge.SetActive(false);
            skip_button_ui.SetActive(false);
            state_ = State.Finish;
        }

        if (Input.GetKey(KeyCode.P)
            || Input.GetButton("Submit"))
        {
            m_skip_timer += 1.0f * Time.deltaTime;
            return;
        }

        m_skip_timer -= 0.4f * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BriefingManager2 : MonoBehaviour
{
    private GameObject sceneCnt_;

    private bool isLScene;

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

    [SerializeField]
    private GameObject text_;

    [SerializeField]
    private int m_textState;

    [SerializeField]
    private GameObject target_ui_;

    [SerializeField]
    private GameObject targetUnder_ui_;

    [SerializeField]
    private GameObject bomber_ui_;

    [SerializeField]
    private GameObject bomberUnder_ui_;

    [SerializeField]
    private GameObject Sbomb_ui_;

    [SerializeField]
    private GameObject SbombUnder_ui_;

    [SerializeField]
    private GameObject lightBomb_ui_;

    [SerializeField]
    private GameObject lightBombUnder_ui_;

    [SerializeField]
    private GameObject skip_gauge;

    [SerializeField]
    private GameObject skip_button_ui;

    private float m_skip_timer;

    [SerializeField]
    private GameObject cb_base_;

    [SerializeField]
    private GameObject main_disp_;

    [SerializeField]
    private GameObject scan_;

    private bool isText2;
    private bool isText3;
    private bool isText5;
    private bool isText6;
    private bool isText8;
    private bool isText9;
    private bool isText11;
    private bool isText12;

    private bool isSpawn;

    [SerializeField]
    private GameObject white_disp_;

    [SerializeField]
    private GameObject black_disp_;

    // Use this for initialization
    void Start()
    {
        state_ = State.None;
        m_textState = 1;
        isTrigger = false;

        bomber_.SetActive(false);

        target_ui_.SetActive(false);
        targetUnder_ui_.SetActive(false);

        bomber_ui_.SetActive(false);
        bomberUnder_ui_.SetActive(false);

        Sbomb_ui_.SetActive(false);
        SbombUnder_ui_.SetActive(false);

        lightBomb_ui_.SetActive(false);
        lightBombUnder_ui_.SetActive(false);

        scan_.SetActive(false);

        white_disp_.SetActive(false);

        t0 = 0f;
        t1 = 0f;
        t2 = 0f;

        m_skip_timer = 0f;
        skip_button_ui.SetActive(false);

        isCh47 = false;

        isLScene = false;

        isText2 = false;
        isText3 = false;
        isText5 = false;
        isText6 = false;
        isText8 = false;
        isText9 = false;
        isText11 = false;
        isText12 = false;

        isSpawn = false;

        if (GameObject.FindGameObjectWithTag("SceneController"))
            sceneCnt_ = GameObject.FindGameObjectWithTag("SceneController");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && text_ != null)
        {
            text_.GetComponent<TextBriefing>().TextReset();
            m_textState++;
        }

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
        if (text_.activeInHierarchy && !main_disp_.activeInHierarchy)
            text_.GetComponent<TextBriefing>().Set_State(m_textState);
    }

    private void NoneUpdate()
    {
        ui_.GetComponent<BlackOut_UI>().FeadIn();

        if (ui_.GetComponent<BlackOut_UI>().Get_Clear())
        {
            cb_base_.GetComponent<Briefing_CBBase>().FeadOutBase();
            cb_base_.GetComponent<Briefing_CBBase>().FlashingBase();
        }

        if (!cb_base_.activeInHierarchy)
        {
            main_disp_.GetComponent<BriefingDiapMain>().DisplayOn();
            if (!main_disp_.activeInHierarchy)
            {
                scan_.SetActive(true);
                if (t0 >= 3f)
                {
                    NextText();
                    t0 = 0f;
                    state_ = State.One;
                    tower_mana_.GetComponent<TowerManager>().InitBill();
                    ui_.GetComponent<BlackOut_UI>().ResetT();
                }
                t0 += 1.0f * Time.deltaTime;
            }
        }
    }

    private void OneUpdate()
    {
        briefing_cam_.GetComponent<BriefingCamera>().TargetCam_Move();
        if (briefing_cam_.GetComponent<BriefingCamera>().Get_Pick())
        {
            targetUnder_ui_.SetActive(true);

            if (targetUnder_ui_.GetComponent<ui_text_underLine>().Get_Clear())
                target_ui_.SetActive(true);

            if (t0 >= 4f)
            {
                t0 = 0f;
                state_ = State.Two;
                target_ui_.SetActive(false);
                targetUnder_ui_.SetActive(false);
            }

            t0 += 1.0f * Time.deltaTime;
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
            if (!isText2)
            {
                NextText();
                isText2 = true;
            }
            if (t0 >= 2f)
            {
                robot_.GetComponent<BriefingRobot>().Beam();
                if (t0 >= 7f)
                {
                    if (!isText3)
                    {
                        NextText();
                        isText3 = true;
                    }
                }
            }

            t0 += 1.0f * Time.deltaTime;
        }

        if (robot_.GetComponent<BriefingRobot>().Get_MissileFinishFlag())
        {
            if (t1 >= 1.5f)
            {
                NextText();
                state_ = State.Three;
                t0 = 0f;
                t1 = 0f;
                bomber_.SetActive(true);
                bomber_.GetComponent<StrategyBombing1>().SpawnBomber();
                tower_mana_.GetComponent<TowerManager>().TowerCheck();
            }

            t1 += 1.0f * Time.deltaTime;
        }
    }

    private void ThreeUpdate()
    {
        tower_mana_.GetComponent<TowerManager>().Tower2Up();

        if (!briefing_cam_.GetComponent<BriefingCamera>().Get_Bombing())
            briefing_cam_.GetComponent<BriefingCamera>().BombingViewCam();

        else
        {
            if (t0 < 4f)
            {
                bomber_ui_.SetActive(true);
                bomberUnder_ui_.SetActive(true);
            }

            else if (t0 >= 4f)
            {
                if (!briefing_cam_.GetComponent<BriefingCamera>().Get_Top())
                {
                    bomber_ui_.SetActive(false);
                    bomberUnder_ui_.SetActive(false);
                    briefing_cam_.GetComponent<BriefingCamera>().TopViewCam();
                }
                else
                {
                    robot_.GetComponent<BriefingRobot>().BeamBombing();
                }

                if (t0 >= 10f)
                {
                    if (!isText5)
                    {
                        NextText();
                        isText5 = true;
                    }

                    if (!briefing_cam_.GetComponent<BriefingCamera>().Get_Bombing2())
                        briefing_cam_.GetComponent<BriefingCamera>().BombingViewCam2();

                    if (briefing_cam_.GetComponent<BriefingCamera>().Get_Bombing2())
                    {
                        if (!isSpawn)
                        {
                            bomber_.GetComponent<StrategyBombing1>().SpawnBomber();
                            isSpawn = true;
                        }

                        if (!isText6)
                        {
                            NextText();
                            isText6 = true;
                        }

                        if (GameObject.FindGameObjectWithTag("Bomber"))
                            GameObject.FindGameObjectWithTag("Bomber").GetComponent<Bombing1>().SetMoveFlag(true);
                    }

                    if (t0 >= 22f)
                    {
                        state_ = State.Four;
                        t0 = 0f;
                        tower_mana_.GetComponent<TowerManager>().TowerCheck2();
                    }
                }
            }

            t0 += 1.0f * Time.deltaTime;
        }

        //if (t0 >= 4f)
        //{
        //    if (!isText5)
        //    {
        //        NextText();
        //        isText5 = true;
        //    }
        //    if (t0 >= 8f)
        //    {
        //        if (!isText6)
        //        {
        //            NextText();
        //            isText6 = true;
        //        }
        //        if (t0 >= 12f)
        //        {
        //            state_ = State.Four;
        //            t0 = 0f;
        //            tower_mana_.GetComponent<TowerManager>().TowerCheck2();
        //        }
        //    }
        //}


    }

    private void FourUpdate()
    {
        tower_mana_.GetComponent<TowerManager>().Tower3Up();
        tower_mana_.GetComponent<TowerManager>().Tower4Up();

        if (!briefing_cam_.GetComponent<BriefingCamera>().Get_Side())
            briefing_cam_.GetComponent<BriefingCamera>().SideViewCam();

        if (!isCh47)
        {
            NextText();
            tank_.GetComponent<BriefingStTank>().Set_SpawnFlag(true);
            isCh47 = true;
        }

        if (t0 >= 3.5f)
        {
            if (!briefing_cam_.GetComponent<BriefingCamera>().Get_TankBomb())
                briefing_cam_.GetComponent<BriefingCamera>().TankBombViewCam();

            else if (briefing_cam_.GetComponent<BriefingCamera>().Get_TankBomb())
            {
                if (t0 < 8f)
                {
                    Sbomb_ui_.SetActive(true);
                    SbombUnder_ui_.SetActive(true);
                }

                if (!briefing_cam_.GetComponent<BriefingCamera>().Get_TankBomb2()
                    && t0 >= 8f)
                {
                    briefing_cam_.GetComponent<BriefingCamera>().TankBombViewCam2();
                    Sbomb_ui_.SetActive(false);
                    SbombUnder_ui_.SetActive(false);
                }

            }

            if (briefing_cam_.GetComponent<BriefingCamera>().Get_TankBomb2()
                && t0 >= 10f)
                tower_mana_.GetComponent<TowerManager>().TowerBreak();

            if (!isText8)
            {
                NextText();
                isText8 = true;
            }

            if (t0 >= 12f)
            {
                if (!isText9)
                {
                    NextText();
                    isText9 = true;
                }
            }
        }



        if (GameObject.FindGameObjectWithTag("GameTank"))
        {
            if (tank_.GetComponent<BriefingStTank>().Get_Clear())
            {
                if (t1 >= 1.5f)
                {
                    NextText();
                    t0 = 0f;
                    t1 = 0f;
                    state_ = State.Five;
                }
                t1 += 1.0f * Time.deltaTime;
            }
        }

        t0 += 1.0f * Time.deltaTime;
    }

    private void FiveUpdate()
    {
        if (!briefing_cam_.GetComponent<BriefingCamera>().Get_LightBomb())
            briefing_cam_.GetComponent<BriefingCamera>().LightBombViewCam();

        else
        {
            lightSpawn_.GetComponent<LightBomSpawn>().Spawn();
        }

        if (t0 >= 4f)
        {
            //if (!briefing_cam_.GetComponent<BriefingCamera>().Get_LightBomb2())
            //    briefing_cam_.GetComponent<BriefingCamera>().LightBombViewCam2();

            //if (briefing_cam_.GetComponent<BriefingCamera>().Get_LightBomb2()
            //    && !briefing_cam_.GetComponent<BriefingCamera>().Get_LightBomb3())
            if (!briefing_cam_.GetComponent<BriefingCamera>().Get_LightBomb2())
            {
                if (t0 < 9f)
                {
                    lightBomb_ui_.SetActive(true);
                    lightBombUnder_ui_.SetActive(true);
                }

                else if (t0 >= 9f)
                {
                    lightBomb_ui_.SetActive(false);
                    lightBombUnder_ui_.SetActive(false);
                    //briefing_cam_.GetComponent<BriefingCamera>().LightBombViewCam3();
                    briefing_cam_.GetComponent<BriefingCamera>().LightBombViewCam2();
                }
            }

            if (!isText11)
            {
                NextText();
                isText11 = true;
            }
        }

        if (robot_.GetComponent<BriefingRobot>().GetState() == 3)
        {
            if (t2 >= 4f)
            {
                if (!isText12)
                {
                    NextText();
                    isText12 = true;
                }
                if (t2 >= 9f)
                {
                    NextText();
                    t0 = 0f;
                    state_ = State.Finish;
                }
            }
            t2 += 1.0f * Time.deltaTime;
        }

        t0 += 1.0f * Time.deltaTime;
    }

    private void FinishUpdate()
    {
        if (t0 >= 4f)
        {
            white_disp_.SetActive(true);
            black_disp_.SetActive(true);
            white_disp_.GetComponent<BriefingDisp_white>().DisplayOff();

            if (!white_disp_.activeInHierarchy)
            {
                ui_.GetComponent<BlackOut_UI>().BlackOut();

                if (!isLScene && sceneCnt_ != null
                    && ui_.GetComponent<BlackOut_UI>().Get_Clear())
                {
                    StartCoroutine(sceneCnt_.GetComponent<SceneController>().SceneLoad("Master"));
                    isLScene = true;
                }
            }
        }

        t0 += 1.0f * Time.deltaTime;
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

    private void NextText()
    {
        if (text_.activeInHierarchy)
        {
            text_.GetComponent<TextBriefing>().TextReset();
            m_textState++;
        }
    }
}

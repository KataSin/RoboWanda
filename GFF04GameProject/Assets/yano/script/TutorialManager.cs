using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private enum TutorialState
    {
        Fead,
        Start,
        Mission1,
        Mission2,
        Mission3,
        Mission4,
        Mission5,
        End,
    }

    private enum Tutorial_OtherState
    {
        None,
        Part1,
        Part2,
        Part3,
        Part4,
        Part5,
    }

    private TutorialState m_state;
    private Tutorial_OtherState m_otherState;

    private GameObject sceneCnt_;

    [SerializeField]
    private List<GameObject> clear_pod_;

    [SerializeField]
    private GameObject bill1_;

    [SerializeField]
    private GameObject bill2_;

    [SerializeField]
    private GameObject mission_bar_;

    [SerializeField]
    private GameObject tutorial_canvas_;

    [SerializeField]
    private Image missionC_ui_;

    [SerializeField]
    private GameObject disp_black_;

    [SerializeField]
    private GameObject disp_white_;

    [SerializeField]
    private GameObject disp_main_;

    [SerializeField]
    private GameObject mission_text_;

    [SerializeField]
    private GameObject camera_pos_;

    [SerializeField]
    private GameObject door_;

    [SerializeField]
    private GameObject door2_;

    [SerializeField]
    private GameObject door3_;

    [SerializeField]
    private GameObject door3Check_;

    [SerializeField]
    private GameObject door4_;

    [SerializeField]
    private GameObject door4Check_;

    [SerializeField]
    private List<GameObject> turrets_;

    [SerializeField]
    private GameObject cameraM4_;

    [SerializeField]
    private GameObject cameraM5_;

    [SerializeField]
    private GameObject cntrlUI_ico_1;
    [SerializeField]
    private GameObject cntrlUI_ico_2;
    [SerializeField]
    private GameObject cntrlUI_ico_3;
    [SerializeField]
    private GameObject cntrlUI_ico_4;
    [SerializeField]
    private GameObject cntrlUI_ico_5;

    [SerializeField]
    private GameObject player_ui_;

    [SerializeField]
    private GameObject cb_base_;

    private AudioSource bgm_;


    private float m_intervalTime;

    private float m_startInterval;

    private float m_textInterval;

    private bool isLScene;

    private bool isClear;

    private int m_text_state;

    private bool p_cntrlFlag;
    private bool isText1;
    private bool isText2;
    private bool isText3;
    private bool isText4;
    private bool isText5;
    private bool isText6;
    private bool isText7;
    private bool isText8;
    private bool isText9;
    private bool isText10;
    private bool isText11;
    private bool isText12;
    private bool isText13;
    private bool isText14;
    private bool isText15;
    private bool isText16;
    private bool isText17;
    private bool isText18;
    private bool isTextOther2;

    // Use this for initialization
    void Start()
    {
        bgm_ = GetComponent<AudioSource>();
        bgm_.volume = 0f;
        bgm_.Play();

        //UI
        missionC_ui_.enabled = false;
        disp_white_.GetComponent<Image>().enabled = false;
        cntrlUI_ico_1.SetActive(false);
        cntrlUI_ico_2.SetActive(false);
        cntrlUI_ico_3.SetActive(false);
        cntrlUI_ico_4.SetActive(false);
        cntrlUI_ico_5.SetActive(false);
        player_ui_.SetActive(false);

        //イベントカメラ
        cameraM4_.SetActive(false);
        cameraM5_.SetActive(false);

        //値
        m_intervalTime = 0f;
        m_startInterval = 0f;
        m_textInterval = 0f;
        m_text_state = 0;

        //状態
        m_state = TutorialState.Fead;
        m_otherState = Tutorial_OtherState.None;

        //フラグ
        isLScene = false;
        isClear = false;
        p_cntrlFlag = false;
        isText1 = false;
        isText2 = false;
        isText3 = false;
        isText4 = false;
        isText5 = false;
        isText6 = false;
        isText7 = false;
        isText8 = false;
        isText9 = false;
        isText10 = false;
        isText11 = false;
        isText12 = false;
        isText13 = false;
        isText14 = false;
        isText15 = false;
        isText16 = false;
        isText17 = false;
        isText18 = false;
        isTextOther2 = false;

        if (GameObject.FindGameObjectWithTag("SceneController"))
            sceneCnt_ = GameObject.FindGameObjectWithTag("SceneController");
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case TutorialState.Fead:
                Fead_Update();
                p_cntrlFlag = false;
                break;

            case TutorialState.Start:
                Start_Update();
                p_cntrlFlag = false;
                break;

            case TutorialState.Mission1:
                Mission1_Update();
                break;

            case TutorialState.Mission2:
                Mission2_Update();
                break;

            case TutorialState.Mission3:
                Mission3_Update();
                break;

            case TutorialState.Mission4:
                Mission4_Update();
                break;

            case TutorialState.Mission5:
                Mission5_Update();
                break;

            case TutorialState.End:
                End_Update();
                break;
        }

        mission_text_.GetComponent<TextTutorial>().Set_State(m_text_state);
        Debug.Log(p_cntrlFlag);
    }

    private void Fead_Update()
    {
        if (!tutorial_canvas_.GetComponent<BlackOut_UI>().Get_Clear())
        {
            tutorial_canvas_.GetComponent<BlackOut_UI>().FeadIn();
            bgm_.volume =
                Mathf.Lerp(0f, 0.1f,
                tutorial_canvas_.GetComponent<BlackOut_UI>().GetT() / tutorial_canvas_.GetComponent<BlackOut_UI>().GetBFeadTime());
        }

        else
        {
            cb_base_.GetComponent<Briefing_CBBase>().FeadOutBase();
            cb_base_.GetComponent<Briefing_CBBase>().FlashingBase();

            if (!cb_base_.activeInHierarchy)
            {
                disp_main_.GetComponent<BriefingDiapMain>().DisplayOn();

                if (!disp_main_.activeInHierarchy)
                {
                    if (m_startInterval >= 1f)
                    {
                        TextNext();
                        m_state = TutorialState.Start;
                        tutorial_canvas_.GetComponent<BlackOut_UI>().ResetT();
                        m_startInterval = 0f;
                    }

                    m_startInterval += 1.0f * Time.deltaTime;
                }
            }
        }
    }

    private void Start_Update()
    {
        if (m_startInterval >= 3f || Input.GetButtonDown("Submit"))
        {
            if (!isText1)
            {
                TextNext();
                isText1 = true;
            }
        }

        if (m_startInterval >= 6f || (Input.GetButtonDown("Submit") && isText1))
            m_state = TutorialState.Mission1;

        m_startInterval += 1.0f * Time.deltaTime;
    }

    private void Mission1_Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 0
            && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f
            && !clear_pod_[0].GetComponent<ClearPoint>().GetClear())
        {
            if (!isText2)
            {
                TextNext();
                isText2 = true;
                p_cntrlFlag = true;
            }
        }

        if (p_cntrlFlag
            && camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 5
            && !clear_pod_[0].GetComponent<ClearPoint>().GetClear())
        {
            cntrlUI_ico_1.SetActive(true);
        }

        if (clear_pod_[0].GetComponent<ClearPoint>().GetClear()
            && !door_.GetComponent<DoorOpen>().GetClear())
        {
            p_cntrlFlag = false;
            cntrlUI_ico_1.SetActive(false);
            missionC_ui_.enabled = true;
            mission_text_.SetActive(false);

            if (m_intervalTime >= 2f)
                door_.GetComponent<DoorOpen>().Open();

            m_intervalTime += 1.0f * Time.deltaTime;
        }

        if (door_.GetComponent<DoorOpen>().GetClear())
        {
            clear_pod_[0].SetActive(false);
            missionC_ui_.enabled = false;
            m_state = TutorialState.Mission2;
            m_intervalTime = 0f;
        }
    }

    private void Mission2_Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 1
            && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f
            && !clear_pod_[1].GetComponent<ClearPoint>().GetClear())
        {
            if (!isText3)
            {
                mission_text_.SetActive(true);
                TextNext();
                isText3 = true;
                p_cntrlFlag = true;
            }
        }

        if (p_cntrlFlag
            && camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 5
            && !clear_pod_[1].GetComponent<ClearPoint>().GetClear())
        {
            cntrlUI_ico_2.SetActive(true);
        }

        if (clear_pod_[1].GetComponent<ClearPoint>().GetClear())
        {
            p_cntrlFlag = false;
            cntrlUI_ico_2.SetActive(false);

            if (m_intervalTime < 2f)
            {
                mission_text_.SetActive(false);

                if (!door2_.GetComponent<DoorOpen>().GetClear())
                    missionC_ui_.enabled = true;
            }

            else if (m_intervalTime >= 2f)
            {
                door2_.GetComponent<DoorOpen>().Open();

                if (!isText4)
                {
                    mission_text_.SetActive(true);
                    clear_pod_[1].SetActive(false);
                    missionC_ui_.enabled = false;
                    TextNext();
                    isText4 = true;
                }

                if (door2_.GetComponent<DoorOpen>().GetClear())
                {
                    if (m_textInterval >= 2f)
                    {
                        if (!isText5)
                        {
                            mission_text_.SetActive(true);
                            TextNext();
                            isText5 = true;
                        }

                        if ((Input.GetButtonDown("Submit")
                            || m_textInterval >= 7f)
                            && isText5 && !isText6)
                        {
                            if (!isText6)
                            {
                                TextNext();
                                isText6 = true;
                                m_textInterval = 7f;
                            }
                        }
                        else if ((Input.GetButtonDown("Submit")
                            || m_textInterval >= 12f)
                            && isText6 && !isText7)
                        {
                            if (!isText7)
                            {
                                TextNext();
                                isText7 = true;
                                m_textInterval = 12f;
                            }
                        }
                        else if ((Input.GetButtonDown("Submit")
                            || m_textInterval >= 18f)
                            && isText7 && !isText8)
                        {
                            if (!isText8)
                            {
                                TextNext();
                                isText8 = true;
                                m_textInterval = 18f;
                            }
                        }

                        else if ((Input.GetButtonDown("Submit")
                            || m_textInterval >= 24f)
                            && isText8 && !isText9)
                        {
                            mission_text_.SetActive(false);
                            m_state = TutorialState.Mission3;
                            m_intervalTime = 0f;
                            m_textInterval = 0f;
                        }
                    }
                    m_textInterval += 1.0f * Time.deltaTime;
                }
            }
            m_intervalTime += 1.0f * Time.deltaTime;
        }
    }

    void Mission3_Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 2
           && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f
           && !bill1_.GetComponent<Break>().Get_BreakFlag())
        {
            if (!isText9)
            {
                mission_text_.SetActive(true);
                TextNext();
                isText9 = true;
                p_cntrlFlag = true;
            }
        }

        if (p_cntrlFlag
            && !bill1_.GetComponent<Break>().Get_BreakFlag())
        {
            if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 5
                && !isClear)
            {
                cntrlUI_ico_3.SetActive(true);
                player_ui_.SetActive(true);
                isClear = true;
            }

            cntrlUI_ico_3.GetComponent<TutorialCntrlico>().ICO_Change();
        }

        if (bill1_.GetComponent<Break>().Get_BreakFlag())
        {
            p_cntrlFlag = false;

            cntrlUI_ico_3.SetActive(false);

            if (m_intervalTime < 2f)
                mission_text_.SetActive(false);
            if (m_intervalTime < 5f)
                missionC_ui_.enabled = true;

            if (m_intervalTime >= 5f)
            {
                door3_.GetComponent<DoorOpen>().Open();

                if (door3_.GetComponent<DoorOpen>().GetClear())
                {
                    m_state = TutorialState.Mission4;
                    missionC_ui_.enabled = false;
                    m_intervalTime = 0f;
                    cameraM4_.SetActive(true);
                    isClear = false;
                }
            }
            m_intervalTime += 1.0f * Time.deltaTime;
        }

    }

    void Mission4_Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 3
           && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f)
        {
            if (!isTextOther2)
            {
                mission_text_.SetActive(true);
                mission_text_.GetComponent<TextTutorial>().TextReset();
                m_text_state = 21;
                isTextOther2 = true;
                p_cntrlFlag = true;
            }
        }

        if (!clear_pod_[2].GetComponent<ClearPoint>().GetClear())
        {
            if (door3Check_.GetComponent<DoorCheck>().Get_CheckFlag())
            {
                if (!isText10)
                {
                    mission_text_.GetComponent<TextTutorial>().TextReset();
                    m_text_state = 11;
                    isText10 = true;
                }
                if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetOtherMode() == 1
                    && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f)
                {
                    if (!isText11)
                    {
                        TextNext();
                        isText11 = true;
                    }
                }
                else if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetOtherMode() == 2
                    && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f
                    && !isText12)
                {
                    if (!isText12)
                    {
                        TextNext();
                        isText12 = true;
                    }
                }
                else if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetOtherMode() == 0
                    && isText12)
                {
                    if (!isText13)
                    {
                        TextNext();
                        isText13 = true;
                    }
                }

                if (p_cntrlFlag
                    && isText13
                    && camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 5)
                {
                    cntrlUI_ico_4.SetActive(true);
                }

                OtherMission4Update();
            }

            if (turrets_[0].GetComponent<TutorialTurret>().Get_State() == 2
                && turrets_[1].GetComponent<TutorialTurret>().Get_State() == 2)
            {
                clear_pod_[2].SetActive(true);
            }
            else if (turrets_[0].GetComponent<TutorialTurret>().Get_State() == 1
               || turrets_[1].GetComponent<TutorialTurret>().Get_State() == 1)
            {
                clear_pod_[2].SetActive(false);
            }
        }

        else if (clear_pod_[2].GetComponent<ClearPoint>().GetClear())
        {
            p_cntrlFlag = false;
            cntrlUI_ico_4.SetActive(false);
            cntrlUI_ico_5.SetActive(false);
            missionC_ui_.enabled = true;
            mission_text_.SetActive(false);
            cameraM4_.SetActive(false);

            turrets_[0].GetComponent<TutorialTurret>().Off();
            turrets_[1].GetComponent<TutorialTurret>().Off();

            if (m_intervalTime > 2f)
                door4_.GetComponent<DoorOpen>().Open();

            if (door4_.GetComponent<DoorOpen>().GetClear())
            {
                clear_pod_[2].SetActive(false);
                missionC_ui_.enabled = false;
                m_state = TutorialState.Mission5;
                m_otherState = Tutorial_OtherState.None;
                m_intervalTime = 0f;
                cameraM5_.SetActive(true);
                isTextOther2 = false;
            }

            m_intervalTime += 1.0f * Time.deltaTime;
        }
    }

    private void OtherMission4Update()
    {
        if (!isClear)
        {
            turrets_[0].GetComponent<TutorialTurret>().Boot();
            turrets_[1].GetComponent<TutorialTurret>().Boot();
            isClear = true;
        }

        switch (m_otherState)
        {
            case Tutorial_OtherState.None:
                if (m_intervalTime >= 4f || Input.GetButtonDown("Submit"))
                {
                    m_otherState = Tutorial_OtherState.Part1;
                    m_intervalTime = 0f;
                }
                p_cntrlFlag = false;
                m_intervalTime += 1.0f * Time.deltaTime;
                break;
            case Tutorial_OtherState.Part1:
                p_cntrlFlag = true;
                m_otherState = Tutorial_OtherState.Part2;
                break;
            case Tutorial_OtherState.Part2:
                break;
            case Tutorial_OtherState.Part3:
                break;
        }
    }

    void Mission5_Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 4
           && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f)
        {
            if (!isTextOther2)
            {
                mission_text_.SetActive(true);
                mission_text_.GetComponent<TextTutorial>().TextReset();
                m_text_state = 21;
                isTextOther2 = true;
                p_cntrlFlag = true;
            }
        }
        if (!bill2_.GetComponent<Break_v2Tutorial>().Get_BreakFlag())
        {
            if (door4Check_.GetComponent<DoorCheck>().Get_CheckFlag())
            {
                if (!isText14)
                {
                    mission_text_.GetComponent<TextTutorial>().TextReset();
                    m_text_state = 15;
                    isText14 = true;
                }
                if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetOtherMode() == 1
                    && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f)
                {
                    if (!isText15)
                    {
                        TextNext();
                        isText15 = true;
                    }
                }
                else if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetOtherMode() == 2
                    && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f)
                {
                    if (!isText16)
                    {
                        TextNext();
                        isText16 = true;
                    }
                }

                if (p_cntrlFlag
                    && isText16
                    && camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 5)
                {
                    cntrlUI_ico_4.SetActive(true);
                }

                OtherMission4Update();
            }
        }

        else if (bill2_.GetComponent<Break_v2Tutorial>().Get_BreakFlag())
        {
            cntrlUI_ico_4.SetActive(false);
            cntrlUI_ico_5.SetActive(false);

            if (m_intervalTime < 2f)
                mission_text_.SetActive(false);
            if (m_intervalTime < 5f)
                missionC_ui_.enabled = true;
            cameraM5_.SetActive(false);
            //controller_ico_.SetActive(false);

            if (m_intervalTime >= 2f)
            {
                p_cntrlFlag = false;
                if (!isText17)
                {
                    mission_text_.SetActive(true);
                    TextNext();
                    isText17 = true;
                }

                if (m_intervalTime >= 5f)
                {
                    m_state = TutorialState.End;
                    missionC_ui_.enabled = false;
                    m_intervalTime = 0f;
                }
            }
            m_intervalTime += 1.0f * Time.deltaTime;
        }
    }

    private void OtherMission5Update()
    {
        switch (m_otherState)
        {
            case Tutorial_OtherState.None:
                if (m_intervalTime >= 4f || Input.GetButtonDown("Submit"))
                {
                    m_otherState = Tutorial_OtherState.Part1;
                    m_intervalTime = 0f;
                }
                p_cntrlFlag = false;
                m_intervalTime += 1.0f * Time.deltaTime;
                break;
            case Tutorial_OtherState.Part1:
                p_cntrlFlag = true;
                m_otherState = Tutorial_OtherState.Part2;
                break;
            case Tutorial_OtherState.Part2:
                break;
            case Tutorial_OtherState.Part3:
                break;
        }
    }

    private void End_Update()
    {
        if (m_intervalTime >= 4f)
        {
            if (!isText18)
            {
                TextNext();
                isText18 = true;
            }

            if (m_intervalTime >= 8f)
            {
                disp_white_.GetComponent<Image>().enabled = true;
                disp_black_.SetActive(true);

                disp_white_.GetComponent<BriefingDisp_white>().DisplayOff();

                if (!disp_white_.activeInHierarchy)
                {
                    if (!tutorial_canvas_.GetComponent<BlackOut_UI>().Get_Clear())
                    {
                        tutorial_canvas_.GetComponent<BlackOut_UI>().BlackOut();
                        bgm_.volume =
                            Mathf.Lerp(0.1f, 0f,
                            tutorial_canvas_.GetComponent<BlackOut_UI>().GetT() / tutorial_canvas_.GetComponent<BlackOut_UI>().GetBFeadTime());
                    }

                    else if (tutorial_canvas_.GetComponent<BlackOut_UI>().Get_Clear()
                        && sceneCnt_ != null
                        && !isLScene)
                    {
                        StartCoroutine(sceneCnt_.GetComponent<SceneController>().SceneLoad("Briefing"));
                        isLScene = true;
                    }
                }
            }
        }

        m_intervalTime += 1.0f * Time.deltaTime;
    }

    private void TextNext()
    {
        mission_text_.GetComponent<TextTutorial>().TextReset();
        m_text_state++;
    }

    public int GetTutorialState()
    {
        return (int)m_state;
    }

    public int GetOtherTutorialState()
    {
        return (int)m_otherState;
    }

    public bool GetPlayerCntrlFlag()
    {
        return p_cntrlFlag;
    }
}

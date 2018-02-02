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
    private GameObject bill_;

    [SerializeField]
    private GameObject mission_bar_;

    [SerializeField]
    private GameObject tutorial_canvas_;

    [SerializeField]
    private Image missionC_ui_;

    [SerializeField]
    private GameObject mission1_ui_;

    [SerializeField]
    private GameObject mission2_ui_;

    [SerializeField]
    private GameObject mission3_ui_;

    [SerializeField]
    private GameObject mission4_ui_1_;

    [SerializeField]
    private GameObject mission5_ui_1_;

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
    private List<GameObject> turrets_;

    //[SerializeField]
    //private GameObject controller_ico_;

    private AudioSource bgm_;


    private float m_intervalTime;

    private float m_startInterval;

    private bool isLScene;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        bgm_ = GetComponent<AudioSource>();
        bgm_.volume = 0f;
        bgm_.Play();

        missionC_ui_.enabled = false;
        mission1_ui_.SetActive(false);
        mission2_ui_.SetActive(false);
        mission3_ui_.SetActive(false);
        mission4_ui_1_.SetActive(false);
        mission5_ui_1_.SetActive(false);

        //controller_ico_.SetActive(false);

        m_intervalTime = 0f;
        m_startInterval = 0f;

        m_state = TutorialState.Fead;
        m_otherState = Tutorial_OtherState.None;

        if (GameObject.FindGameObjectWithTag("SceneController"))
            sceneCnt_ = GameObject.FindGameObjectWithTag("SceneController");

        isLScene = false;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case TutorialState.Fead:
                Fead_Update();
                break;

            case TutorialState.Start:
                Start_Update();
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

        Debug.Log(m_state);
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
            //m_state = TutorialState.Start;
            //tutorial_canvas_.GetComponent<BlackOut_UI>().ResetT();
        }
    }

    private void Start_Update()
    {
        m_startInterval += 1.0f * Time.deltaTime;

        if (m_startInterval >= 1.5f)
            m_state = TutorialState.Mission1;
    }

    private void Mission1_Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 0
            && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f
            && !clear_pod_[0].GetComponent<ClearPoint>().GetClear())
        {
            //mission_bar_.GetComponent<ui_imageScale>().ScaleChange();
            mission1_ui_.SetActive(true);
            //if (mission_bar_.GetComponent<ui_imageScale>().GetClear())
            //{
                
            //    //controller_ico_.SetActive(true);
            //}
        }


        if (clear_pod_[0].GetComponent<ClearPoint>().GetClear()
            && !door_.GetComponent<DoorOpen>().GetClear())
        {
            missionC_ui_.enabled = true;
            mission1_ui_.SetActive(false);
            //controller_ico_.SetActive(false);
            //mission_bar_.GetComponent<ui_imageScale>().ScaleBack();

            m_intervalTime += 1.0f * Time.deltaTime;

            if (m_intervalTime > 2f)
                door_.GetComponent<DoorOpen>().Open();
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
            //mission_bar_.GetComponent<ui_imageScale>().ScaleChange();
            mission2_ui_.SetActive(true);
            if (mission_bar_.GetComponent<ui_imageScale>().GetClear())
            {
                
                //controller_ico_.SetActive(true);
            }
        }

        if (clear_pod_[1].GetComponent<ClearPoint>().GetClear())
        {
            missionC_ui_.enabled = true;
            mission2_ui_.SetActive(false);
            //controller_ico_.SetActive(false);
            //mission_bar_.GetComponent<ui_imageScale>().ScaleBack();

            m_intervalTime += 1.0f * Time.deltaTime;

            if (m_intervalTime > 2f)
                door2_.GetComponent<DoorOpen>().Open();
        }

        if (door2_.GetComponent<DoorOpen>().GetClear())
        {
            clear_pod_[1].SetActive(false);
            missionC_ui_.enabled = false;
            m_state = TutorialState.Mission3;
            m_intervalTime = 0f;
        }
    }

    void Mission3_Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 2
           && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f
           && !bill_.GetComponent<Break>().Get_BreakFlag())
        {
            //mission_bar_.GetComponent<ui_imageScale>().ScaleChange();
            mission3_ui_.SetActive(true);
            if (mission_bar_.GetComponent<ui_imageScale>().GetClear())
            {
                
                //controller_ico_.SetActive(true);
            }
        }

        if (bill_.GetComponent<Break>().Get_BreakFlag())
        {

            if (m_intervalTime < 5f)
                missionC_ui_.enabled = true;
            mission3_ui_.SetActive(false);
            //controller_ico_.SetActive(false);
            //mission_bar_.GetComponent<ui_imageScale>().ScaleBack();

            m_intervalTime += 1.0f * Time.deltaTime;

            if (m_intervalTime >= 5f)
                door3_.GetComponent<DoorOpen>().Open();

            if (door3_.GetComponent<DoorOpen>().GetClear())
            {
                m_state = TutorialState.Mission4;
                missionC_ui_.enabled = false;
                m_intervalTime = 0f;
            }
        }

    }

    void Mission4_Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 3
           && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f)
        {
            //mission_bar_.GetComponent<ui_imageScale>().ScaleChange();
            mission4_ui_1_.SetActive(true);
            if (mission_bar_.GetComponent<ui_imageScale>().GetClear())
            {
                
                //controller_ico_.SetActive(true);
            }
        }

        if (!clear_pod_[2].GetComponent<ClearPoint>().GetClear())
        {
            if (door3Check_.GetComponent<DoorCheck>().Get_CheckFlag())
            {
                OtherMission4Update();
            }

            if (turrets_[0].GetComponent<TutorialTurret>().Get_State() == 2
                && turrets_[1].GetComponent<TutorialTurret>().Get_State() == 2)
            {
                clear_pod_[2].SetActive(true);
            }
            else if (turrets_[0].GetComponent<TutorialTurret>().Get_State() == 1
               && turrets_[1].GetComponent<TutorialTurret>().Get_State() == 1)
            {
                clear_pod_[2].SetActive(false);
            }
        }

        else if (clear_pod_[2].GetComponent<ClearPoint>().GetClear())
        {
            turrets_[0].GetComponent<TutorialTurret>().Off();
            turrets_[1].GetComponent<TutorialTurret>().Off();
            missionC_ui_.enabled = true;
            mission4_ui_1_.SetActive(false);
            //controller_ico_.SetActive(false);
            //mission_bar_.GetComponent<ui_imageScale>().ScaleBack();

            m_intervalTime += 1.0f * Time.deltaTime;

            if (m_intervalTime > 2f)
                door4_.GetComponent<DoorOpen>().Open();

            if (door4_.GetComponent<DoorOpen>().GetClear())
            {
                clear_pod_[2].SetActive(false);
                missionC_ui_.enabled = false;
                m_state = TutorialState.Mission5;
                m_intervalTime = 0f;
            }
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
                m_otherState = Tutorial_OtherState.Part1;
                break;
            case Tutorial_OtherState.Part1:
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
            //mission_bar_.GetComponent<ui_imageScale>().ScaleChange();
            mission5_ui_1_.SetActive(true);
            if (mission_bar_.GetComponent<ui_imageScale>().GetClear())
            {
                
                //controller_ico_.SetActive(true);
            }
        }

        //if (clear_pod_[2].GetComponent<ClearPoint>().GetClear())
        //{
        //    missionC_ui_.enabled = true;
        //    mission3_ui_.SetActive(false);
        //    //controller_ico_.SetActive(false);
        //    mission_bar_.GetComponent<ui_imageScale>().ScaleBack();

        //    m_intervalTime += 1.0f * Time.deltaTime;

        //    if (m_intervalTime > 2f)
        //        door4_.GetComponent<DoorOpen>().Open();
        //}

        //if (door4_.GetComponent<DoorOpen>().GetClear())
        //{
        //    clear_pod_[2].SetActive(false);
        //    missionC_ui_.enabled = false;
        //    m_state = TutorialState.Mission5;
        //    m_intervalTime = 0f;
        //}
    }

    private void End_Update()
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

    public int GetTutorialState()
    {
        return (int)m_state;
    }

    public int GetOtherTutorialState()
    {
        return (int)m_otherState;
    }
}

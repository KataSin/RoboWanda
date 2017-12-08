using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private enum TutorialState
    {
        Start,
        Mission1,
        Mission2,
        Mission3,
    }

    private TutorialState m_state;

    [SerializeField]
    private List<GameObject> clear_pod_;

    [SerializeField]
    private GameObject tower_;

    [SerializeField]
    private GameObject mission_bar_;

    [SerializeField]
    private GameObject missionC_ui_;

    [SerializeField]
    private GameObject mission1_ui_;

    [SerializeField]
    private GameObject mission2_ui_;

    [SerializeField]
    private GameObject mission3_ui_;

    [SerializeField]
    private GameObject camera_pos_;

    [SerializeField]
    private GameObject door_;

    [SerializeField]
    private GameObject door2_;

    [SerializeField]
    private GameObject controller_ico_;

    private float m_intervalTime;

    private float m_startInterval;

    // Use this for initialization
    void Start()
    {
        missionC_ui_.SetActive(false);
        mission1_ui_.SetActive(false);
        mission2_ui_.SetActive(false);
        mission3_ui_.SetActive(false);

        controller_ico_.SetActive(false);

        m_intervalTime = 0f;
        m_startInterval = 0f;

        m_state = TutorialState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
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
        }

        Debug.Log(m_state);
    }

    private void Start_Update()
    {
        m_startInterval += 1.0f * Time.deltaTime;

        if (m_startInterval >= 2f)
            m_state = TutorialState.Mission1;
    }

    private void Mission1_Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 0
            && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f
            && !clear_pod_[0].GetComponent<ClearPoint>().GetClear())
        {
            mission_bar_.GetComponent<ui_imageScale>().ScaleChange();

            if (mission_bar_.GetComponent<ui_imageScale>().GetClear())
            {
                mission1_ui_.SetActive(true);
                controller_ico_.SetActive(true);
            }
        }


        if (clear_pod_[0].GetComponent<ClearPoint>().GetClear()
            && !door_.GetComponent<DoorOpen>().GetClear())
        {
            missionC_ui_.SetActive(true);
            mission1_ui_.SetActive(false);
            controller_ico_.SetActive(false);
            mission_bar_.GetComponent<ui_imageScale>().ScaleBack();

            m_intervalTime += 1.0f * Time.deltaTime;

            if (m_intervalTime > 2f)
                door_.GetComponent<DoorOpen>().Open();
        }

        if (door_.GetComponent<DoorOpen>().GetClear())
        {
            missionC_ui_.SetActive(false);
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
            mission_bar_.GetComponent<ui_imageScale>().ScaleChange();

            if (mission_bar_.GetComponent<ui_imageScale>().GetClear())
            {
                mission2_ui_.SetActive(true);
                controller_ico_.SetActive(true);
            }
        }

        if (clear_pod_[1].GetComponent<ClearPoint>().GetClear())
        {
            missionC_ui_.SetActive(true);
            mission2_ui_.SetActive(false);
            controller_ico_.SetActive(false);
            mission_bar_.GetComponent<ui_imageScale>().ScaleBack();

            m_intervalTime += 1.0f * Time.deltaTime;

            if (m_intervalTime > 2f)
                door2_.GetComponent<DoorOpen>().Open();
        }

        if (door2_.GetComponent<DoorOpen>().GetClear())
        {
            missionC_ui_.SetActive(false);
            m_state = TutorialState.Mission3;
            m_intervalTime = 0f;
        }
    }

    void Mission3_Update()
    {
        if (tower_ != null)
        {
            if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 2
               && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 2f
               && !tower_.GetComponent<Break>().Get_BreakFlag())
            {
                mission_bar_.GetComponent<ui_imageScale>().ScaleChange();

                if (mission_bar_.GetComponent<ui_imageScale>().GetClear())
                {
                    mission3_ui_.SetActive(true);
                    //controller_ico_.SetActive(true);
                }
            }

            if (tower_.GetComponent<Break>().Get_BreakFlag())
            {
                missionC_ui_.SetActive(true);
                mission3_ui_.SetActive(false);
                //controller_ico_.SetActive(false);
                mission_bar_.GetComponent<ui_imageScale>().ScaleBack();

                m_intervalTime += 1.0f * Time.deltaTime;
            }
        }

        else if (tower_ == null)
        {
            missionC_ui_.SetActive(false);
            //m_state = TutorialState.Mission3;
            //m_intervalTime = 0f;
        }
    }

    public int GetTutorialState()
    {
        return (int)m_state;
    }
}

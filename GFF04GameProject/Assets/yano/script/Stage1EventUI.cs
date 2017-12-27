using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1EventUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> mission_text_;

    [SerializeField]
    private GameObject camera_pos_;

    private bool isClear;

    private float m_finishTimer;

    private bool EtoNFlag;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < mission_text_.Count; i++)
            mission_text_[i].SetActive(false);

        isClear = false;
        m_finishTimer = 0f;
        EtoNFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (camera_pos_.GetComponent<CameraPosition>().Get_BlackFlag()
            && !isClear)
        {
            GetComponent<BlackOut_UI>().JeepOut(0.2f);
        }

        if (GetComponent<BlackOut_UI>().Get_ClearJ()
            && !isClear)
        {
            isClear = true;
            GetComponent<BlackOut_UI>().ResetJC();
        }

        else if (isClear
            && camera_pos_.GetComponent<CameraPosition>().GetEMode() == 2
            && !camera_pos_.GetComponent<CameraPosition>().Get_MAllFlag())
        {
            GetComponent<BlackOut_UI>().JeepIn(0.8f);
        }


        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 2
        && camera_pos_.GetComponent<CameraPosition>().Get_M0Flag()
        && !camera_pos_.GetComponent<CameraPosition>().Get_MAllFlag()
        && GetComponent<BlackOut_UI>().Get_ClearJ()
        && isClear)
        {
            mission_text_[0].SetActive(true);
        }

        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 3)
        {
            mission_text_[0].SetActive(false);
            mission_text_[1].SetActive(true);
        }

        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 4)
        {
            mission_text_[1].SetActive(false);
            mission_text_[2].SetActive(true);
        }

        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 5)
        {
            mission_text_[2].SetActive(false);
            mission_text_[3].SetActive(true);

            GetComponent<BlackOut_UI>().ResetT();
        }

        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 2
            && camera_pos_.GetComponent<CameraPosition>().Get_MAllFlag())
        {
            mission_text_[3].SetActive(false);
            mission_text_[4].SetActive(true);

            if (camera_pos_.GetComponent<CameraPosition>().Get_EventEnd()
                && m_finishTimer >= 6f)
            {
                GetComponent<BlackOut_UI>().BlackOut();
            }

            m_finishTimer += 1.0f * Time.deltaTime;
        }

        if (camera_pos_.GetComponent<CameraPosition>().Get_EventEnd()
            &&
            camera_pos_.GetComponent<CameraPosition>().GetEMode() == 0)
        {
            mission_text_[4].SetActive(false);
            if (!EtoNFlag)
            {
                GetComponent<BlackOut_UI>().ResetT();
                EtoNFlag = true;
            }

            else if (EtoNFlag)
                GetComponent<BlackOut_UI>().FeadIn();
        }
    }
}

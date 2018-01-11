using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject camera_pos_;

    [SerializeField]
    private GameObject c_briefing_;

    [SerializeField]
    private GameObject map_briefing_;

    [SerializeField]
    private GameObject mapScan_briefing_;

    [SerializeField]
    private GameObject text_briefing_;

    [SerializeField]
    private GameObject target_briefing_;

    [SerializeField]
    private GameObject bomber_briefing_;

    [SerializeField]
    private GameObject ch47P_briefing_;

    [SerializeField]
    private GameObject ch47_briefing_1;
    [SerializeField]
    private GameObject ch47_briefing_2;
    [SerializeField]
    private GameObject ch47_briefing_4;
    [SerializeField]
    private GameObject ch47_briefing_5;
    [SerializeField]
    private GameObject ch47_briefing_6;

    private float t1;

    [SerializeField]
    private int m_textState;

    // Use this for initialization
    void Start()
    {
        t1 = 0f;

        m_textState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 2
            && !camera_pos_.GetComponent<CameraPosition>().Get_MAllFlag())
        {
            if (!c_briefing_.GetComponent<Commnd_Briefing>().Get_Clear())
                c_briefing_.GetComponent<Commnd_Briefing>().LeftFead();
            else
            {
                if (t1 >= 1f)
                {
                    c_briefing_.GetComponent<Commnd_Briefing>().PivotChange();
                    c_briefing_.GetComponent<Commnd_Briefing>().RightFead();
                    if (t1 >= 2f)
                    {
                        if (!map_briefing_.GetComponent<MapBriefing>().Get_Clear())
                            map_briefing_.GetComponent<MapBriefing>().Open();
                        else if (m_textState >= 11)
                            map_briefing_.GetComponent<MapBriefing>().Close();

                        if (map_briefing_.GetComponent<MapBriefing>().Get_Clear())
                        {
                            mapScan_briefing_.GetComponent<MapScan>().ScanFead();

                            if (mapScan_briefing_.transform.localPosition.x >= -150f)
                                target_briefing_.GetComponent<TargetBriefing>().ActiveMark();

                            if (m_textState == 2)
                                target_briefing_.GetComponent<TargetBriefing>().ActiveTarget();
                            else if (m_textState == 3)
                                target_briefing_.GetComponent<TargetBriefing>().CloseTarget();

                            if (m_textState == 4)
                            {
                                if (mapScan_briefing_.transform.localPosition.x >= -465f)
                                    bomber_briefing_.GetComponent<BomberBriefing>().ActiveMark();

                                if (bomber_briefing_.GetComponent<BomberBriefing>().Get_T1() >= 1f)
                                    bomber_briefing_.GetComponent<BomberBriefing>().ActiveBomber();
                            }

                            else if (m_textState == 6)
                            {
                                bomber_briefing_.GetComponent<BomberBriefing>().CloseBomber();

                                if (mapScan_briefing_.transform.localPosition.x >= -437f)
                                {
                                    ch47_briefing_1.GetComponent<CH47Briefing>().ActiveMark();
                                    ch47_briefing_4.GetComponent<CH47Briefing>().ActiveMark();
                                }

                                if (mapScan_briefing_.transform.localPosition.x >= -360f)
                                {
                                    ch47_briefing_2.GetComponent<CH47Briefing>().ActiveMark();
                                    ch47_briefing_5.GetComponent<CH47Briefing>().ActiveMark();
                                }

                                if (mapScan_briefing_.transform.localPosition.x >= -290f)
                                {
                                    ch47P_briefing_.GetComponent<CH47BriefingPick>().ActiveMark();
                                    ch47_briefing_6.GetComponent<CH47Briefing>().ActiveMark();
                                }

                                if (ch47P_briefing_.GetComponent<CH47BriefingPick>().Get_T1() >= 1f)
                                    ch47P_briefing_.GetComponent<CH47BriefingPick>().ActiveCH47();
                            }
                        }

                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            text_briefing_.GetComponent<TextBriefing>().TextReset();
                            m_textState++;
                        }

                        text_briefing_.GetComponent<TextBriefing>().Set_State(m_textState);
                    }
                }

                t1 += 1.0f * Time.deltaTime;
            }
        }
    }
}

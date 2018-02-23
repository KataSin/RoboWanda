using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public enum TitleState
    {
        Opening,
        Ready,
        Start,
        Select,
        Sally,
    }

    private enum ModeState
    {
        Yes,
        No,
    }

    public TitleState titleState_ = TitleState.Opening;

    private ModeState modeState_ = ModeState.Yes;

    [SerializeField]
    private float nextViewTimer;

    [SerializeField]
    private GameObject titleCamera_;

    [SerializeField]
    private GameObject title_uis_;

    [SerializeField]
    private GameObject mode_uis_;

    [SerializeField]
    private GameObject title_player_;

    [SerializeField]
    private GameObject question_text_;

    [SerializeField]
    private GameObject Heri;

    private float m_sallyTimer;
    private float m_feadSTimer;

    private GameObject sceneCnt_;

    private bool isLScene;

    private bool isStandClear;
    private bool isToIdel;

    private bool isClear;

    [SerializeField]
    private GameObject vc_;

    [SerializeField]
    private GameObject se_;

    // Use this for initialization
    void Start()
    {
        nextViewTimer = 0f;
        m_sallyTimer = 0f;
        m_feadSTimer = 0f;
        isLScene = false;
        isStandClear = false;
        isToIdel = false;
        isClear = false;

        title_uis_.SetActive(false);
        mode_uis_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (titleState_ == TitleState.Opening)
        {
            titleCamera_.GetComponent<TitleCamera>().Set_Timer(1.0f * Time.deltaTime);
            titleCamera_.GetComponent<TitleCamera>().titleReadyCamera();

            if (Input.anyKeyDown)
            {
                titleCamera_.GetComponent<TitleCamera>().
                    Set_Timer(titleCamera_.GetComponent<TitleCamera>().Get_ReadyTime());
            }

            else if (titleCamera_.GetComponent<TitleCamera>().Get_Timer()
                >= titleCamera_.GetComponent<TitleCamera>().Get_ReadyTime())
            {
                title_uis_.SetActive(true);
                titleCamera_.GetComponent<TitleCamera>().Reset_Timer();
                titleState_ = TitleState.Ready;
            }
        }

        else if (Input.anyKeyDown && titleState_ == TitleState.Ready)
        {
            se_.GetComponents<AudioSource>()[0].PlayOneShot(se_.GetComponents<AudioSource>()[0].clip);
            titleState_ = TitleState.Start;
            sceneCnt_ = GameObject.FindGameObjectWithTag("SceneController");
        }

        else if (titleState_ == TitleState.Start)
        {
            if (nextViewTimer >= 2f)
            {
                if (!isClear)
                {
                    title_player_.GetComponent<TitlePlayer>().Set_StandFlag(true);
                    se_.GetComponents<AudioSource>()[1].PlayOneShot(se_.GetComponents<AudioSource>()[1].clip);
                    title_uis_.SetActive(false);
                    isClear = true;
                }

                titleCamera_.GetComponent<TitleCamera>().Set_Timer(1.0f * Time.deltaTime);
                titleCamera_.GetComponent<TitleCamera>().titleReadyToStart();

                if (Input.anyKeyDown && mode_uis_.activeSelf == false
                    && nextViewTimer >= 2.2f)
                {
                    titleCamera_.GetComponent<TitleCamera>().
                        Set_Timer(titleCamera_.GetComponent<TitleCamera>().Get_FeadTime());
                    isStandClear = true;
                }
                else if ((titleCamera_.GetComponent<TitleCamera>().Get_Timer()
                    >= titleCamera_.GetComponent<TitleCamera>().Get_FeadTime()) && mode_uis_.activeSelf == false)
                {
                    mode_uis_.SetActive(true);
                    vc_.GetComponent<AudioSource>().PlayOneShot(vc_.GetComponent<AudioSource>().clip);
                }
            }
            nextViewTimer += 1.0f * Time.deltaTime;

            if (mode_uis_.activeSelf == true)
            {
                mode_uis_.GetComponent<titleSelectUI>().SelectMode();

                if (mode_uis_.GetComponent<titleSelectUI>().GetModeYN() == titleSelectUI.ModeYN.Tutorial)
                    sceneCnt_.GetComponent<SceneController>().SetNextScene(1);

                else if (mode_uis_.GetComponent<titleSelectUI>().GetModeYN() == titleSelectUI.ModeYN.GamePlay)
                    sceneCnt_.GetComponent<SceneController>().SetNextScene(0);

                if (Input.GetButtonDown("Submit"))
                {
                    se_.GetComponents<AudioSource>()[0].PlayOneShot(se_.GetComponents<AudioSource>()[0].clip);
                    question_text_.GetComponent<questionText>().SetState(1);
                    titleCamera_.GetComponent<TitleCamera>().Reset_Timer();
                    titleState_ = TitleState.Sally;
                }
            }
        }

        else if (titleState_ == TitleState.Sally)
        {
            m_sallyTimer += 1.0f * Time.deltaTime;

            if (Input.GetButtonDown("Submit"))
                mode_uis_.SetActive(false);

            if (m_sallyTimer >= 4f || mode_uis_.activeSelf == false)
            {
                mode_uis_.SetActive(false);
                titleCamera_.GetComponent<TitleCamera>().Set_Timer(1.0f * Time.deltaTime);
                Heri.GetComponent<TitleHeri>().TitleHeriMove();
                titleCamera_.GetComponent<TitleCamera>().titleHeriSally();

                m_feadSTimer += 1.0f * Time.deltaTime;

                if (m_feadSTimer >= 4f)
                {
                    GetComponent<BlackOut_UI>().BlackOut();

                    if (GetComponent<BlackOut_UI>().Get_Clear() == true
                        && !isLScene)
                    {
                        StartCoroutine(sceneCnt_.GetComponent<SceneController>().SceneLoad("Loading"));
                        isLScene = true;
                    }
                }
            }


        }
    }

    public bool GetStandClear()
    {
        return isStandClear;
    }

    public void SetStandClear(bool l_isStandClear)
    {
        isStandClear = l_isStandClear;
    }

    public bool GetToIdelFlag()
    {
        return isToIdel;
    }
}

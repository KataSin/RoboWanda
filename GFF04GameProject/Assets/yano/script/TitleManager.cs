﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public enum TitleState
    {
        Opening,
        Ready,
        Start,
    }

    private enum ModeState
    {
        Yes,
        No,
    }

    public TitleState titleState_ = TitleState.Opening;

    private ModeState modeState_ = ModeState.Yes;

    private SceneController scene_;

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
    private List<GameObject> quetion_y_n_back_;

    // Use this for initialization
    void Start()
    {
        //scene_ = GameObject.Find("SceneController").GetComponent<SceneController>();
        nextViewTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (titleState_ == TitleState.Opening)
        {
            titleCamera_.GetComponent<TitleCamera>().Set_Timer(1.0f * Time.deltaTime);
            titleCamera_.GetComponent<TitleCamera>().titleReadyCamera();

            if (titleCamera_.GetComponent<TitleCamera>().Get_Timer()
                >= titleCamera_.GetComponent<TitleCamera>().Get_ReadyTime())
            {
                title_uis_.SetActive(true);
                titleCamera_.GetComponent<TitleCamera>().Reset_Timer();
                titleState_ = TitleState.Ready;
            }
        }

        if (Input.anyKeyDown && titleState_ == TitleState.Ready)
        {
            titleState_ = TitleState.Start;
            //scene_.SceneChange("StageSelect");
        }

        if (titleState_ == TitleState.Start)
            nextViewTimer += 1.0f * Time.deltaTime;

        if (nextViewTimer >= 2f)
        {
            title_player_.GetComponent<TitlePlayer>().Set_StandFlag(true);
            title_uis_.SetActive(false);
            titleCamera_.GetComponent<TitleCamera>().Set_Timer(1.0f * Time.deltaTime);
            titleCamera_.GetComponent<TitleCamera>().titleReadyToStart();

            if (titleCamera_.GetComponent<TitleCamera>().Get_Timer()
                >= titleCamera_.GetComponent<TitleCamera>().Get_FeadTime())
            {
                mode_uis_.SetActive(true);
            }
        }

        if (mode_uis_ == true)
        {
            if (modeState_ == ModeState.Yes)
            {
                quetion_y_n_back_[0].SetActive(true);
                quetion_y_n_back_[1].SetActive(false);

                if (Input.GetKeyDown(KeyCode.S))
                    modeState_ = ModeState.No;
            }
            else if (modeState_ == ModeState.No)
            {
                quetion_y_n_back_[0].SetActive(false);
                quetion_y_n_back_[1].SetActive(true);

                if (Input.GetKeyDown(KeyCode.W))
                    modeState_ = ModeState.Yes;
            }
        }
    }
}

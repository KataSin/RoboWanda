﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public enum ResultState
    {
        Fead,
        Result,
        Finish,
    }

    public ResultState state_;

    [SerializeField]
    private GameObject scene_;

    [SerializeField]
    private GameObject camera_;

    [SerializeField]
    private GameObject result_uis_;

    [SerializeField]
    private GameObject heri_;

    private float m_finishTimer;

    private bool isLScene;

    // Use this for initialization
    void Start()
    {
        state_ = ResultState.Fead;

        result_uis_.SetActive(false);

        m_finishTimer = 0f;
        isLScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state_ == ResultState.Fead)
        {
            GetComponent<BlackOut_UI>().FeadIn();
            camera_.GetComponent<ResultCamera>().ResultMove();

            if (camera_.GetComponent<ResultCamera>().Get_Clear())
                state_ = ResultState.Result;

            result_uis_.SetActive(false);
        }
        else if (state_ == ResultState.Result)
        {
            result_uis_.SetActive(true);

            if (Input.anyKeyDown)
            {
                GetComponent<BlackOut_UI>().ResetT();
                state_ = ResultState.Finish;
            }
        }
        else if (state_ == ResultState.Finish)
        {
            heri_.GetComponent<ResultHeri>().FinishHeriMove();
            result_uis_.SetActive(false);

            m_finishTimer += 1.0f * Time.deltaTime;

            if (m_finishTimer >= 4f)
            {
                GetComponent<BlackOut_UI>().BlackOut();

                if (GetComponent<BlackOut_UI>().Get_Clear()
                    && !isLScene)
                {
                    StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Title"));
                    isLScene = true;
                }
            }
        }

    }
}

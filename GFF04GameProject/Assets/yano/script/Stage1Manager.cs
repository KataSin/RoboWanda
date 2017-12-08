﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Manager : MonoBehaviour
{
    private SceneController scene_;

    private enum OverState
    {
        Retry,
        TitleTo,
        TurtorialTo,
    }

    private OverState state_ = OverState.Retry;

    [SerializeField]
    private List<GameObject> arows_;

    [SerializeField]
    private GameObject robot_;

    [SerializeField]
    private GameObject camera_pos_;

    [SerializeField]
    private GameObject boss_ui_;

    [SerializeField]
    private GameObject go_uis_;

    [SerializeField]
    private GameObject timer_ui_;

    private float test;

    private bool isLScene;

    // Use this for initialization
    void Start()
    {
        scene_ = GameObject.Find("SceneController").GetComponent<SceneController>();
        test = 0f;
        isLScene = false;
        go_uis_.SetActive(false);
        timer_ui_.SetActive(false);
        boss_ui_.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<BlackOut_UI>().Get_Clear())
            GetComponent<BlackOut_UI>().FeadIn();

        if (camera_pos_.GetComponent<CameraPosition>().GetMode() == 1)
        {
            boss_ui_.GetComponent<Image>().enabled = true;
            timer_ui_.SetActive(true);

            if (robot_.GetComponent<RobotManager>().GetRobotHP() > 0f)
                timer_ui_.GetComponent<Timer>().TimerUpdate();
        }

        //クリアかオーバーかのチェック
        ClearOverCheck();

        //ステージ上の全キャラクターのHPチェック
        Check_CharaHp();
    }

    //クリアかオーバーかのチェック
    private void ClearOverCheck()
    {
        if (GetComponent<BlackOut_UI>().Get_ClearGO())
        {
            if (state_ == OverState.Retry)
            {
                arows_[0].SetActive(true);
                arows_[1].SetActive(false);
                arows_[2].SetActive(false);

                if (Input.GetKeyDown(KeyCode.Return))
                    scene_.SceneChange("newnewNightTest 1");

                if (Input.GetKeyDown(KeyCode.DownArrow))
                    state_ = OverState.TitleTo;
            }
            else if (state_ == OverState.TitleTo)
            {
                arows_[0].SetActive(false);
                arows_[1].SetActive(true);
                arows_[2].SetActive(false);

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    scene_.SceneChange("Title");                   
                }



                if (Input.GetKeyDown(KeyCode.UpArrow))
                    state_ = OverState.Retry;

                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    state_ = OverState.TurtorialTo;
            }
            else if (state_ == OverState.TurtorialTo)
            {
                arows_[0].SetActive(false);
                arows_[1].SetActive(false);
                arows_[2].SetActive(true);

                if (Input.GetKeyDown(KeyCode.UpArrow))
                    state_ = OverState.TitleTo;
            }
        }

        if (GetComponent<BlackOut_UI>().Get_ClearGC()
            && !isLScene)
        {
            StartCoroutine(scene_.SceneLoad("Result"));
            isLScene = true;
        }
    }

    //ステージ上の全キャラクターのHPチェック
    private void Check_CharaHp()
    {
        //ボスのチェック
        if (robot_.GetComponent<RobotManager>().GetRobotHP() <= 0f)
        {
            test += 1.0f * Time.deltaTime;
            if (test >= 35.0f)
            {
                GetComponent<BlackOut_UI>().GameClearFead();
                boss_ui_.GetComponent<Image>().enabled = false;
            }
        }

        //プレイヤーのチェック
        if (camera_pos_.GetComponent<CameraPosition>().GetDeadFinish())
        {
            timer_ui_.SetActive(false);
            boss_ui_.GetComponent<Image>().enabled = false;
            GetComponent<BlackOut_UI>().GameOverFead();
            if (GetComponent<BlackOut_UI>().Get_ClearGO())
            {
                go_uis_.SetActive(true);
            }
        }
    }

}

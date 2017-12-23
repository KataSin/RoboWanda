using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Manager : MonoBehaviour
{
    private GameObject scene_;

    private enum OverState
    {
        Retry,
        TitleTo,
    }

    private OverState state_ = OverState.Retry;

    [SerializeField]
    private List<GameObject> arows_;

    [SerializeField]
    private GameObject robot_;

    [SerializeField]
    private GameObject camera_pos_;

    //[SerializeField]
    //private List<GameObject> boss_ui_;

    [SerializeField]
    private GameObject go_uis_;

    [SerializeField]
    private GameObject timer_ui_;

    private float test;

    private bool isLScene;

    [SerializeField]
    private GameObject bezeru_ui_;

    // Use this for initialization
    void Start()
    {
        test = 0f;
        isLScene = false;
        go_uis_.SetActive(false);
        timer_ui_.SetActive(false);
        //boss_ui_[0].SetActive(false);
        //boss_ui_[1].GetComponent<Image>().enabled = false;

        arows_[0].SetActive(false);
        arows_[1].SetActive(false);

        if (GameObject.FindGameObjectWithTag("SceneController"))
            scene_ = GameObject.FindGameObjectWithTag("SceneController");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<BlackOut_UI>().Get_Clear())
            GetComponent<BlackOut_UI>().FeadIn();

        if (camera_pos_.GetComponent<CameraPosition>().GetMode() == 0)
        {
            bezeru_ui_.GetComponent<ui_Bezeru>().SetT(camera_pos_.GetComponent<CameraPosition>().GetT());
        }

        if (camera_pos_.GetComponent<CameraPosition>().GetMode() == 1)
        {
            //boss_ui_[0].SetActive(true);
            //boss_ui_[1].GetComponent<Image>().enabled = true;
            timer_ui_.SetActive(false);

            //if (robot_.GetComponent<RobotManager>().GetRobotHP() > 0f)
            //    timer_ui_.GetComponent<Timer>().TimerUpdate();
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

                if (Input.GetButtonDown("Submit") && scene_ != null)
                    StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("lightTest 1"));

                if (Input.GetAxis("Vertical_L") <= -1f)
                    state_ = OverState.TitleTo;
            }
            else if (state_ == OverState.TitleTo)
            {
                arows_[0].SetActive(false);
                arows_[1].SetActive(true);

                if (Input.GetButtonDown("Submit") && scene_ != null)
                {
                    StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Title"));
                }

                if (Input.GetAxis("Vertical_L") >= 1f)
                    state_ = OverState.Retry;
            }
        }

        if (GetComponent<BlackOut_UI>().Get_ClearGC()
            && !isLScene
            && scene_ != null)
        {
            StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Result"));
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
                //boss_ui_[0].SetActive(false);
                //boss_ui_[1].GetComponent<Image>().enabled = false;
            }
        }

        //プレイヤーのチェック
        if (camera_pos_.GetComponent<CameraPosition>().GetDeadFinish()
            || timer_ui_.GetComponent<Timer>().GetTimer() <= 0f)
        {
            timer_ui_.SetActive(false);
            //boss_ui_[0].SetActive(false);
            //boss_ui_[1].GetComponent<Image>().enabled = false;
            GetComponent<BlackOut_UI>().GameOverFead();
            if (GetComponent<BlackOut_UI>().Get_ClearGO())
            {
                go_uis_.SetActive(true);
            }
        }
    }

}

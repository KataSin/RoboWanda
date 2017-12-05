using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    private SceneController scene_;

    private enum OverState
    {
        Retry,
        TitleTo,
        TurtorialTo,
    }

    [SerializeField]
    private GameObject black_curtain_;

    [SerializeField]
    private List<GameObject> arows_;

    [SerializeField]
    private GameObject robot_;

    private OverState state_ = OverState.Retry;

    float test;

    private bool isLScene;

    // Use this for initialization
    void Start()
    {
        scene_ = GameObject.Find("SceneController").GetComponent<SceneController>();
        test = 0f;
        isLScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (black_curtain_.GetComponent<BlackOut_UI>().Get_ClearGO())
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
                    scene_.SceneChange("Title");



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

        if (black_curtain_.GetComponent<BlackOut_UI>().Get_ClearGC()
            && !isLScene)
        {
            StartCoroutine(scene_.SceneLoad("Result"));
            isLScene = true;
        }

        //ステージ上の全キャラクターのHPチェック
        Check_CharaHp();
    }

    //ステージ上の全キャラクターのHPチェック
    private void Check_CharaHp()
    {
        if (robot_.GetComponent<RobotManager>().GetRobotHP() <= 0f)
        {
            test += 1.0f * Time.deltaTime;
            if (test >= 35.0f)
                black_curtain_.GetComponent<BlackOut_UI>().GameClearFead();
        }

        //else if (player_.IsDead())
        //    scene_.SceneChange("GameOver");
    }

}

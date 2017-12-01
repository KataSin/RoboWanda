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

    private OverState state_ = OverState.Retry;

    // Use this for initialization
    void Start()
    {
        scene_ = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(black_curtain_.GetComponent<BlackOut_UI>().Get_ClearGO())
        {
            if(state_==OverState.Retry)
            {
                arows_[0].SetActive(true);
                arows_[1].SetActive(false);
                arows_[2].SetActive(false);

                if(Input.GetKeyDown(KeyCode.Return))
                    scene_.SceneChange("NightTest 1");

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

        //ステージ上の全キャラクターのHPチェック
        Check_CharaHp();
    }

    //ステージ上の全キャラクターのHPチェック
    private void Check_CharaHp()
    {
        //if (robotMana_.GetRobotHP() <= 0)
        //    scene_.SceneChange("Result");

        //else if (player_.IsDead())
        //    scene_.SceneChange("GameOver");
    }

}

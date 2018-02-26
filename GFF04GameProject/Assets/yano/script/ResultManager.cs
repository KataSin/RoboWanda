using System.Collections;
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

    private GameObject scene_;

    [SerializeField]
    private GameObject camera_;

    [SerializeField]
    private GameObject result_uis_;

    [SerializeField]
    private GameObject heri_;

    private float m_finishTimer;

    private bool isLScene;

    [SerializeField]
    private GameObject se_;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        state_ = ResultState.Fead;

        result_uis_.SetActive(false);

        m_finishTimer = 0f;
        isLScene = false;
        isClear = false;

        if (GameObject.FindGameObjectWithTag("SceneController"))
            scene_ = GameObject.FindGameObjectWithTag("SceneController");
    }

    // Update is called once per frame
    void Update()
    {
        if (state_ == ResultState.Fead)
        {
            GetComponent<BlackOut_UI>().FeadIn();
            camera_.GetComponent<ResultCamera>().ResultMove();

            if (Input.anyKeyDown)
            {
                GetComponent<BlackOut_UI>().SetT(GetComponent<BlackOut_UI>().GetBFeadTime());
                camera_.GetComponent<ResultCamera>().SetT(camera_.GetComponent<ResultCamera>().GetMoveTime());
            }

            if (camera_.GetComponent<ResultCamera>().Get_Clear())
                state_ = ResultState.Result;

            result_uis_.SetActive(false);
        }
        else if (state_ == ResultState.Result)
        {
            result_uis_.SetActive(true);
            result_uis_.GetComponent<ResultScore>().ScoreUpdate();

            if (!isClear)
            {
                se_.GetComponents<AudioSource>()[0].PlayOneShot(se_.GetComponents<AudioSource>()[0].clip);
                isClear = true;
            }

            if (Input.anyKeyDown)
            {
                se_.GetComponents<AudioSource>()[1].PlayOneShot(se_.GetComponents<AudioSource>()[1].clip);
                GetComponent<BlackOut_UI>().ResetT();
                state_ = ResultState.Finish;
            }
        }
        else if (state_ == ResultState.Finish)
        {
            heri_.GetComponent<ResultHeri>().FinishHeriMove();
            camera_.GetComponent<ResultCamera>().FinishMove();

            if (heri_.GetComponent<ResultHeri>().Get_Speed() >= 1f)
            {
                camera_.GetComponent<ResultCamera>().FinishRotate();
            }
            result_uis_.SetActive(false);

            m_finishTimer += 1.0f * Time.deltaTime;

            if (m_finishTimer >= 4f)
            {
                GetComponent<BlackOut_UI>().BlackOut();

                if (GetComponent<BlackOut_UI>().Get_Clear()
                    && !isLScene
                    && scene_ != null)
                {
                    StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Title"));
                    isLScene = true;
                }
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    private GameObject scoreMana_;

    [SerializeField]
    private GameObject timer_ui_;
    private Text timer_text;

    [SerializeField]
    private GameObject attackScore_ui_;
    private Text attackScore_text;

    [SerializeField]
    private GameObject billRatio_ui_;
    private Text billRatio_text;

    private int minute;
    private int second;
    private string second_string;

    private int m_attackScore;

    private float m_billPersent;


    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("ScoreManager"))
            scoreMana_ = GameObject.FindGameObjectWithTag("ScoreManager");

        minute = 0;
        second = 0;
        m_attackScore = 0;
        m_billPersent = 0f;

        TimerApplication();
        AttackScoreApplication();
        BillRatioApplication();
    }

    public void ScoreUpdate()
    {
        TimerUpdate();

        AttackScoreUpdate();

        BillRatioUpdate();
    }

    private void TimerApplication()
    {
        if (scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() >= 300f)
            minute = 5;
        else if (scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() < 300f
            && scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() >= 240f)
            minute = 4;
        else if (scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() < 240f
            && scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() >= 180f)
            minute = 3;
        else if (scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() < 180f
            && scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() >= 120f)
            minute = 2;
        else if (scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() < 120f
            && scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() >= 60f)
            minute = 1;
        else if (scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() < 60f
            && scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() >= 0f)
            minute = 0;

        second = (int)(scoreMana_.GetComponent<ScoreManager>().Get_ScoreTimer() - minute * 60);

        if (second >= 10)
            second_string = second.ToString();
        else if (second < 10)
            second_string = "0" + second;
    }

    private void TimerUpdate()
    {
        timer_text = timer_ui_.GetComponent<Text>();
        timer_text.text = "0" + minute.ToString() + ":" + second_string;
    }

    private void AttackScoreApplication()
    {
        m_attackScore = (int)scoreMana_.GetComponent<ScoreManager>().Get_AttackScore();
    }

    private void AttackScoreUpdate()
    {
        attackScore_text = attackScore_ui_.GetComponent<Text>();
        attackScore_text.text = m_attackScore.ToString();
    }

    private void BillRatioApplication()
    {
        m_billPersent = scoreMana_.GetComponent<ScoreManager>().Get_BillRatio() * 100;
    }

    private void BillRatioUpdate()
    {
        billRatio_text = billRatio_ui_.GetComponent<Text>();
        billRatio_text.text = m_billPersent.ToString("F0") + "%";
    }
}

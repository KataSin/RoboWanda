using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private GameObject timer_;

    [SerializeField]
    private float m_score_timer;

    [SerializeField]
    private int m_attack_score;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Timer"))
        {
            timer_ = GameObject.FindGameObjectWithTag("Timer");
        }

        if (timer_ != null)
            m_score_timer = timer_.GetComponent<Timer>().Get_ElapsedTimer();
    }

    public float Get_ScoreTimer()
    {
        return m_score_timer;
    }

    public float Get_AttackScore()
    {
        return m_attack_score;
    }

    public void SetAtackScore(int attackScore)
    {
        m_attack_score += attackScore;
    }
}

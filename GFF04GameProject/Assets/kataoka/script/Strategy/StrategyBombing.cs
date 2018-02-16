using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StrategyBombing : MonoBehaviour
{


    //作戦の時間
    public float m_StrategyTime;
    //時間
    private float m_Time;
    //作戦音声
    public AudioClip m_WirelessClip;
    [SerializeField, MultilineAttribute(2)]
    public string m_Text;
    private Text m_TextUi;
    //爆撃機プレハブ
    public GameObject m_BomberPrefab;

    public GameObject m_FailureObj;

    private bool m_FirstFlag;
    private AudioSource m_AudioSource;

    private float m_VibrationTime;
    private int m_VibrationCount;
    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
        if (GameObject.FindGameObjectWithTag("StrategyText"))
            m_TextUi = GameObject.FindGameObjectWithTag("StrategyText").GetComponent<Text>();
        m_AudioSource = GameObject.FindGameObjectWithTag("StrategySound").GetComponent<AudioSource>();
        m_FirstFlag = true;
        m_VibrationCount = 0;
        m_VibrationTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;

        //振動系
        if (m_Time >= m_StrategyTime - 1.5f && m_VibrationCount <= 2 && m_WirelessClip != null)
        {
            m_VibrationTime += Time.deltaTime;
            if (m_VibrationTime <= 0.2f)
            {
                XInputDotNetPure.GamePad.SetVibration(0, 0.0f, 20.0f);
            }
            else if (m_VibrationTime <= 0.4f)
            {
                XInputDotNetPure.GamePad.SetVibration(0, 0.0f, 0.0f);
            }
            else
            {
                m_VibrationTime = 0.0f;
                m_VibrationCount++;
            }
        }



        //ロボットは早めに行動を変える
        if (m_Time >= m_StrategyTime - 5.0f)
            GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>().SetBehavior(RobotManager.RobotBehavior.ROBOT_TWO);

        if (m_Time >= m_StrategyTime)
        {
            if (m_FirstFlag)
            {
                Vector3 spawnPos = transform.Find("BomBingPointStart").position;
                Vector3 goalPos = transform.Find("BomBingPointEnd").position;
                GameObject bomber = Instantiate(m_BomberPrefab, spawnPos, Quaternion.identity);
                bomber.GetComponent<Bombing>().SetStartEnd(spawnPos, goalPos);
                bomber.GetComponent<Bombing>().m_FailureObj = m_FailureObj;
                //音声を流す
                m_AudioSource.clip = m_WirelessClip;
                if (m_WirelessClip != null)
                    m_AudioSource.PlayOneShot(m_AudioSource.clip);
                m_FirstFlag = false;
            }
            //テキストUI
            if (GameObject.FindGameObjectWithTag("StrategyText"))
                m_TextUi.text = m_Text;

            if (!m_AudioSource.isPlaying)
            {
                if (GameObject.FindGameObjectWithTag("StrategyText"))
                    m_TextUi.text = "";
                transform.Find("BombingBeamPoint").parent = null;
                Destroy(gameObject);
            }

        }
    }
}

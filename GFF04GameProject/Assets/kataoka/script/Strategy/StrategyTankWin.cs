using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class StrategyTankWin : MonoBehaviour
{
    //無線の時間
    public AudioClip m_WirelessClip;
    [SerializeField, MultilineAttribute(2)]
    public string m_Text;
    private Text m_TextUi;

    private bool m_FirstFlag;
    //時間
    private float m_Time;
    //オーディオソース
    private AudioSource m_AudioSource;


    private float m_VibrationTime;
    private int m_VibrationCount;
    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
        m_AudioSource = GameObject.FindGameObjectWithTag("StrategySound").GetComponent<AudioSource>();
        if (GameObject.FindGameObjectWithTag("StrategyText"))
            m_TextUi = GameObject.FindGameObjectWithTag("StrategyText").GetComponent<Text>();
        m_FirstFlag = true;

        m_VibrationCount = 0;
        m_VibrationTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        var breakBill = GameObject.FindGameObjectsWithTag("BreakTower");
        foreach (var i in breakBill)
        {
            if (!i.GetComponent<Break_ST>().Get_BreakFlag())
            {
                return;
            }
        }
        m_Time += Time.deltaTime;
        //振動系
        if (m_VibrationCount <= 2 && m_WirelessClip != null)
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
        if (m_Time >= 1.5f)
        {
            if (m_FirstFlag)
            {
                GameObject.FindGameObjectWithTag("CameraPosition").GetComponent<CameraPosition>().SetEventState(PlayerCameraMode.Event, EventCameraState.BossLook);
                //無線再生
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
                Destroy(gameObject);
            }

        }
    }
}

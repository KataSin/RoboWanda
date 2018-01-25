using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class StrategyWireless : MonoBehaviour
{
    //無線の時間
    public float m_WirelessTime;
    public AudioClip m_WirelessClip;
    [SerializeField, MultilineAttribute(2)]
    public string m_Text;
    private Text m_TextUi;

    private bool m_FirstFlag;
    //ヘリを帰らせるかどうか
    public bool m_HeliReturn;
    //ライトをつけるかどうか
    public bool m_IsLight;
    //時間
    private float m_Time;
    //オーディオソース
    private AudioSource m_AudioSource;

    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
        m_AudioSource = GameObject.FindGameObjectWithTag("StrategySound").GetComponent<AudioSource>();
        m_TextUi = GameObject.FindGameObjectWithTag("StrategyText").GetComponent<Text>();
        m_FirstFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= m_WirelessTime)
        {
            if (m_FirstFlag)
            {
                //無線再生
                m_AudioSource.clip = m_WirelessClip;
                if (m_WirelessClip != null)
                    m_AudioSource.PlayOneShot(m_AudioSource.clip);
                //ヘリを帰らせるかどうか
                GameObject.FindGameObjectWithTag("HeliManager").GetComponent<HelocopterManager>().m_ReturnHeli = m_HeliReturn;
                //ライトを付けるかどうか
                GameObject.FindGameObjectWithTag("RobotLightManager").GetComponent<StrategyRobotLightManager>().m_IsLight = m_IsLight;
                m_FirstFlag = false;
            }
            //テキストUI
            m_TextUi.text = m_Text;

            if (!m_AudioSource.isPlaying)
            {
                m_TextUi.text = "";
                Destroy(gameObject);
            }

        }
    }
}

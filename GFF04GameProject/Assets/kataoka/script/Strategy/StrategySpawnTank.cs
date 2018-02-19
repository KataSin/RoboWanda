using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategySpawnTank : MonoBehaviour
{
    //作戦の時間
    public float m_StrategyTime;
    public GameObject m_TankPrefab;
    [SerializeField, MultilineAttribute(2)]
    public string m_Text;
    private Text m_TextUi;

    public AudioClip m_WirelessClip;

    private float m_Time;

    private List<GameObject> m_SpawnPoints;

    private AudioSource m_AudioSource;

    private bool m_FirstFlag;

    private float m_VibrationTime;
    private int m_VibrationCount;

    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
        m_FirstFlag = true;
        m_SpawnPoints = new List<GameObject>();

        var trans = GetComponentsInChildren<Transform>();
        foreach (var i in trans)
        {
            if (i.name =="TankSpawn")
                m_SpawnPoints.Add(i.gameObject);
        }
        if (GameObject.FindGameObjectWithTag("StrategyText"))
            m_TextUi = GameObject.FindGameObjectWithTag("StrategyText").GetComponent<Text>();
        m_AudioSource = GameObject.FindGameObjectWithTag("StrategySound").GetComponent<AudioSource>();

        m_VibrationTime = 0.0f;
        m_VibrationCount = 0;
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


        if (m_Time >= m_StrategyTime)
        {
            if (m_FirstFlag)
            {
                GameObject.FindGameObjectWithTag("CameraPosition").GetComponent<CameraPosition>().SetEventState(PlayerCameraMode.Event, EventCameraState.LightHeliLook);
                GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>().SetBehavior(RobotManager.RobotBehavior.ROBOT_THREE);
                foreach (var i in m_SpawnPoints)
                {
                    Instantiate(m_TankPrefab, i.transform.position, Quaternion.identity);
                }
                m_AudioSource.clip = m_WirelessClip;
                if (m_WirelessClip != null)
                    m_AudioSource.PlayOneShot(m_AudioSource.clip);
                m_FirstFlag = false;
            }
            //テキストUI
            if (GameObject.FindGameObjectWithTag("StrategyText"))
                m_TextUi.text = m_Text;

            if (!m_AudioSource.isPlaying||m_WirelessClip==null)
            {
                if (GameObject.FindGameObjectWithTag("StrategyText"))
                    m_TextUi.text = "";
                Destroy(gameObject);
            }

        }
    }
}

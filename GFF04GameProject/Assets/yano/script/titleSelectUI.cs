using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleSelectUI : MonoBehaviour
{
    public enum ModeYN
    {
        Tutorial,
        GamePlay,
    }

    private ModeYN m_mode;

    [SerializeField]
    private GameObject yesUI_;

    [SerializeField]
    private GameObject noUI_;

    [SerializeField]
    private Sprite originImage_Y_;

    [SerializeField]
    private Sprite originImage_N_;

    [SerializeField]
    private Sprite otherModeImage_Y_;

    [SerializeField]
    private Sprite otherModeImage_N_;

    [SerializeField]
    private GameObject wave_;

    [SerializeField]
    private GameObject wave_parent_;

    private float m_intervalTime;

    // Use this for initialization
    void Start()
    {
        m_mode = ModeYN.Tutorial;
        m_intervalTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectMode()
    {
        switch (m_mode)
        {
            case ModeYN.Tutorial:
                yesUI_.GetComponent<Image>().sprite = originImage_Y_;
                noUI_.GetComponent<Image>().sprite = originImage_N_;

                if (m_intervalTime <= 0f)
                {
                    Instantiate(wave_, yesUI_.transform.position, Quaternion.identity, wave_parent_.transform);
                    m_intervalTime = 1f;
                }

                //if (Input.GetAxis("Vertical_L") <= -0.9f)
                //{
                //    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                //    m_mode = ModeYN.GamePlay;
                //}

                if (Input.GetAxis("Horizontal_L") >= 0.9f)
                {
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                    m_mode = ModeYN.GamePlay;
                    m_intervalTime = 1f;
                }

                m_intervalTime -= 2.0f * Time.deltaTime;

                break;

            case ModeYN.GamePlay:
                yesUI_.GetComponent<Image>().sprite = otherModeImage_Y_;
                noUI_.GetComponent<Image>().sprite = otherModeImage_N_;

                //if (Input.GetAxis("Vertical_L") >= 0.9f)
                //{
                //    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                //    m_mode = ModeYN.Tutorial;
                //}

                if (m_intervalTime <= 0f)
                {
                    Instantiate(wave_, noUI_.transform.position, Quaternion.identity, wave_parent_.transform);
                    m_intervalTime = 1f;
                }

                if (Input.GetAxis("Horizontal_L") <= -0.9f)
                {
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                    m_mode = ModeYN.Tutorial;
                    m_intervalTime = 1f;
                }

                m_intervalTime -= 2.0f * Time.deltaTime;

                break;
        }
    }

    public ModeYN GetModeYN()
    {
        return m_mode;
    }
}

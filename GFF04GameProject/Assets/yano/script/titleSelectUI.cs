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

    private Sprite originImage_Y_;
    private Sprite originImage_N_;

    [SerializeField]
    private Sprite otherModeImage_Y_;

    [SerializeField]
    private Sprite otherModeImage_N_;

    // Use this for initialization
    void Start()
    {
        m_mode = ModeYN.Tutorial;
        originImage_Y_ = yesUI_.GetComponent<Image>().sprite;
        originImage_N_ = noUI_.GetComponent<Image>().sprite;
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
                yesUI_.GetComponent<Image>().sprite = otherModeImage_Y_;
                noUI_.GetComponent<Image>().sprite = originImage_N_;

                if (Input.GetAxis("Vertical_L") <= -0.9f)
                {
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                    m_mode = ModeYN.GamePlay;
                }

                break;

            case ModeYN.GamePlay:
                yesUI_.GetComponent<Image>().sprite = originImage_Y_;
                noUI_.GetComponent<Image>().sprite = otherModeImage_N_;

                if (Input.GetAxis("Vertical_L") >= 0.9f)
                {
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                    m_mode = ModeYN.Tutorial;
                }

                break;
        }
    }

    public ModeYN GetModeYN()
    {
        return m_mode;
    }
}

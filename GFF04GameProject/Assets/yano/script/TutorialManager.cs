using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject clear_pod_;

    [SerializeField]
    private GameObject missionC_ui_;

    [SerializeField]
    private GameObject mission1_ui_;

    [SerializeField]
    private GameObject camera_pos_;

    // Use this for initialization
    void Start()
    {
        missionC_ui_.SetActive(false);
        mission1_ui_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (camera_pos_.GetComponent<CameraPosition_Tutorial>().GetMode() == 0
            && camera_pos_.GetComponent<CameraPosition_Tutorial>().Get_T() >= 3f
            && !clear_pod_.GetComponent<ClearPoint>().GetClear())
        {
            mission1_ui_.SetActive(true);
        }


        if (clear_pod_.GetComponent<ClearPoint>().GetClear())
        {
            missionC_ui_.SetActive(true);
            mission1_ui_.SetActive(false);
        }
    }
}

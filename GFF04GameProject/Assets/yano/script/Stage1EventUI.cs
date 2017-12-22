using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1EventUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> mission_text_;

    [SerializeField]
    private GameObject camera_pos_;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < mission_text_.Count; i++)
            mission_text_[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 2
            && camera_pos_.GetComponent<CameraPosition>().Get_M0Flag()
            && !camera_pos_.GetComponent<CameraPosition>().Get_MAllFlag())
        {
            mission_text_[0].SetActive(true);
        }

        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 3)
        {
            mission_text_[0].SetActive(false);
            mission_text_[1].SetActive(true);
        }

        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 4)
        {
            mission_text_[1].SetActive(false);
            mission_text_[2].SetActive(true);
        }

        if(camera_pos_.GetComponent<CameraPosition>().GetEMode() == 2
            && camera_pos_.GetComponent<CameraPosition>().Get_MAllFlag())
        {
            mission_text_[2].SetActive(false);
            mission_text_[3].SetActive(true);
        }
    }
}

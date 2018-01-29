using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    private float m_correction_value;

    [SerializeField]
    private float m_correction_valueDelta;

    private GameObject camera_pos_;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("CameraPosition"))
        {
            camera_pos_ = GameObject.Find("CameraPosition");
        }

        m_correction_valueDelta = m_correction_value * 1.0f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        m_correction_valueDelta = m_correction_value * 0.7f * Time.deltaTime;

        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() != 0)
            transform.position += (transform.forward / m_correction_valueDelta) * Time.deltaTime;

        if (camera_pos_.GetComponent<CameraPosition>().GetMode() == 2
            &&
            camera_pos_.GetComponent<CameraPosition>().Get_EventEnd())
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEvent : MonoBehaviour
{
    private GameObject camera_pos_;

    private Vector3 m_origin_pos;

    private float t;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("CameraPosition"))
        {
            camera_pos_ = GameObject.Find("CameraPosition");
        }

        m_origin_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 4)
        {
            transform.position = Vector3.Lerp(m_origin_pos, m_origin_pos + (transform.forward * 8f), t / 2f);
            t += 1.0f * Time.deltaTime;
        }
    }
}

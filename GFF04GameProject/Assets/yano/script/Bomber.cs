using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    private GameObject camera_pos_;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("CameraPosition"))
        {
            camera_pos_ = GameObject.Find("CameraPosition");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (camera_pos_.GetComponent<CameraPosition>().GetEMode() == 3)
            transform.position += transform.forward;
    }
}

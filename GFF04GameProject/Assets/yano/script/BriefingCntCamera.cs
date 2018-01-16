using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingCntCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject camera_pos_;

    private Vector3 m_origin_Lpos;

    private float t;

    // Use this for initialization
    void Start()
    {
        m_origin_Lpos = camera_pos_.transform.localPosition;
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-transform.up, Input.GetAxis("Horizontal_L"));

        t += Input.GetAxis("Vertical_L") * Time.deltaTime;

        t = Mathf.Clamp(t, 0f, 2f);

        camera_pos_.transform.localPosition =
            Vector3.Lerp(m_origin_Lpos, new Vector3(m_origin_Lpos.x, 1.9f, -0.4f), t / 2f);
    }
}

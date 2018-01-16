using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingCamera : MonoBehaviour
{
    private Vector3 m_origin_pos;
    private Vector3 m_targetCam_pos;
    private Vector3 m_topViewCam_pos;
    private Vector3 m_sideViewCam_pos;

    private Quaternion m_origin_rotation;

    private float t, t1, t2, t3;

    private bool isClear;
    private bool isTopView;
    private bool isSideView;

    // Use this for initialization
    void Start()
    {
        m_origin_pos = new Vector3(-62.6f, 52.8f, -74.4f);
        m_targetCam_pos = new Vector3(-62.6f, 42f, 36.4f);
        m_topViewCam_pos = new Vector3(-62.6f, 98.7f, 88.3f);
        m_sideViewCam_pos = new Vector3(-149f, 11.6f, 91.6f);

        m_origin_rotation = transform.rotation;

        t = 0f;
        t1 = 0f;
        t2 = 0f;
        t3 = 0f;

        isClear = false;
        isTopView = false;
        isSideView = false;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp(t, 0f, 2f);

        if (!isClear)
            transform.position = Vector3.Lerp(m_origin_pos, m_targetCam_pos, t / 2f);

        if (isClear
            && !isTopView)
        {
            transform.position = Vector3.Lerp(m_origin_pos, m_topViewCam_pos, t1 / 2f);
            transform.rotation =
                Quaternion.Slerp(m_origin_rotation, Quaternion.Euler(90f, m_origin_rotation.y, m_origin_rotation.z), t1 / 2f);
        }

        if (isTopView
            && !isSideView)
        {
            transform.position = Vector3.Lerp(m_topViewCam_pos, m_sideViewCam_pos, t2 / 2f);
            transform.rotation =
                Quaternion.Slerp(Quaternion.Euler(90f, m_origin_rotation.y, m_origin_rotation.z),
                Quaternion.Euler(m_origin_rotation.x, 90f, m_origin_rotation.z), t2 / 2f);
        }

        if (isSideView)
        {
            transform.position = Vector3.Lerp(m_sideViewCam_pos, m_origin_pos, t3 / 2f);
            transform.rotation =
                Quaternion.Slerp(Quaternion.Euler(m_origin_rotation.x, 90f, m_origin_rotation.z),
                m_origin_rotation, t3 / 2f);
        }
    }

    public void TargetCam_Move()
    {
        t += 2.0f * Time.deltaTime;
    }

    public void TargetCam_Back()
    {
        t -= 2.0f * Time.deltaTime;

        if (t <= 0f)
            isClear = true;
    }

    public void TopViewCam()
    {
        t1 += 2.0f * Time.deltaTime;

        if (t1 >= 2f)
            isTopView = true;
    }

    public void SideViewCam()
    {
        t2 += 1.0f * Time.deltaTime;

        if (t2 >= 2f)
            isSideView = true;
    }

    public void NormalViewCam()
    {
        t3 += 1.0f * Time.deltaTime;
    }

    public bool Get_Clear()
    {
        return isClear;
    }
}

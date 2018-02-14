using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingCamera : MonoBehaviour
{
    private Vector3 m_origin_pos;
    private Vector3 m_targetCam_pos;
    private Vector3 m_topViewCam_pos;
    private Vector3 m_sideViewCam_pos;
    private Vector3 m_bombingViewCam_pos;
    private Vector3 m_bombingViewCam_pos2;
    private Vector3 m_tankBombViewCam_pos;

    private Quaternion m_origin_rotation;

    private float t;

    private bool isClear;
    private bool isPick;
    private bool isTopView;
    private bool isSideView;

    private bool isBombingView;
    private bool isBombingView2;

    private bool isTankBombView;

    // Use this for initialization
    void Start()
    {
        m_origin_pos = new Vector3(-62.6f, 52.8f, -74.4f);
        m_targetCam_pos = new Vector3(-62.6f, 42f, 36.4f);
        m_topViewCam_pos = new Vector3(-62.6f, 98.7f, 88.3f);
        m_sideViewCam_pos = new Vector3(-62.6f, 145.6f, 88.3f);
        //m_sideViewCam_pos = new Vector3(-149f, 11.6f, 91.6f);
        m_bombingViewCam_pos = new Vector3(-154f, 60f, 65f);
        m_bombingViewCam_pos2 = new Vector3(-58f, 17f, 48f);
        m_tankBombViewCam_pos = new Vector3(-10f, 2.9f, 134f);

        m_origin_rotation = transform.rotation;

        t = 0f;

        isClear = false;
        isPick = false;
        isTopView = false;
        isSideView = false;

        isBombingView = false;
        isBombingView2 = false;

        isTankBombView = false;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp(t, 0f, 2f);

        if (!isClear)
            transform.position = Vector3.Lerp(m_origin_pos, m_targetCam_pos, t / 2f);

        if (isClear
            && !isBombingView)
        {
            transform.position = Vector3.Lerp(m_origin_pos, m_bombingViewCam_pos, t / 2f);
            transform.rotation =
                Quaternion.Slerp(m_origin_rotation, Quaternion.Euler(15.8f, -45.2f, 0f), t / 2f);
        }

        if (isBombingView
            && !isTopView)
        {
            transform.position = Vector3.Lerp(m_bombingViewCam_pos, m_topViewCam_pos, t / 2f);
            transform.rotation =
                Quaternion.Slerp(Quaternion.Euler(15.8f, -45.2f, 0f), Quaternion.Euler(90f, m_origin_rotation.y, m_origin_rotation.z), t / 2f);
        }

        //if (isBombingView
        //    && !isTopView)
        //{
        //    transform.position = Vector3.Lerp(m_bombingViewCam_pos, m_bombingViewCam_pos2, t / 2f);
        //    transform.rotation =
        //        Quaternion.Slerp(Quaternion.Euler(15.8f, -45.2f, 0f), Quaternion.Euler(-5f, -30f, 0f), t / 2f);
        //}

        if (isTopView
            && !isBombingView2)
        {
            transform.position = Vector3.Lerp(m_topViewCam_pos, m_bombingViewCam_pos2, t / 2f);
            transform.rotation =
                Quaternion.Slerp(Quaternion.Euler(90f, m_origin_rotation.y, m_origin_rotation.z), Quaternion.Euler(-5f, -30f, 0f), t / 2f);
        }

        if (isBombingView2
            && !isSideView)
        {
            transform.position = Vector3.Lerp(m_bombingViewCam_pos2, m_sideViewCam_pos, t / 2f);
            transform.rotation =
                Quaternion.Slerp(Quaternion.Euler(-5f, -30f, 0f),
                Quaternion.Euler(90f, m_origin_rotation.y, m_origin_rotation.z), t / 2f);
        }

        //if (isSideView)
        //{
        //    transform.position = Vector3.Lerp(m_sideViewCam_pos, m_origin_pos, t / 2f);
        //    transform.rotation =
        //        Quaternion.Slerp(Quaternion.Euler(90f, m_origin_rotation.y, m_origin_rotation.z),
        //        m_origin_rotation, t / 2f);
        //}

        if (isSideView
            && !isTankBombView)
        {
            transform.position = Vector3.Lerp(m_sideViewCam_pos, m_tankBombViewCam_pos, t / 2f);
            transform.rotation =
                Quaternion.Slerp(Quaternion.Euler(90f, m_origin_rotation.y, m_origin_rotation.z),
                Quaternion.Euler(0f, -28f, 0f), t / 2f);
        }
    }

    public void TargetCam_Move()
    {
        t += 2.0f * Time.deltaTime;

        if (t >= 2f)
            isPick = true;
    }

    public void TargetCam_Back()
    {
        t -= 2.0f * Time.deltaTime;

        if (t <= 0f)
        {
            isClear = true;
            t = 0f;
        }
    }

    public void TopViewCam()
    {
        t += 1.0f * Time.deltaTime;

        if (t >= 2f)
        {
            isTopView = true;
            t = 0f;
        }
    }

    public void SideViewCam()
    {
        t += 1.0f * Time.deltaTime;

        if (t >= 2f)
        {
            isSideView = true;
            t = 0f;
        }
    }

    public void NormalViewCam()
    {
        t += 1.0f * Time.deltaTime;
    }

    public void BombingViewCam()
    {
        t += 1.0f * Time.deltaTime;

        if (t >= 2f)
        {
            isBombingView = true;
            t = 0f;
        }
    }

    public void BombingViewCam2()
    {
        t += 1.0f * Time.deltaTime;

        if (t >= 2f)
        {
            isBombingView2 = true;
            t = 0f;
        }
    }

    public void TankBombViewCam()
    {
        t += 1.0f * Time.deltaTime;

        if (t >= 2f)
        {
            isTankBombView = true;
            t = 0f;
        }
    }

    public bool Get_Clear()
    {
        return isClear;
    }

    public bool Get_Pick()
    {
        return isPick;
    }

    public bool Get_Bombing()
    {
        return isBombingView;
    }

    public bool Get_Bombing2()
    {
        return isBombingView2;
    }

    public bool Get_Top()
    {
        return isTopView;
    }

    public bool Get_Side()
    {
        return isSideView;
    }

    public bool Get_TankBomb()
    {
        return isTankBombView;
    }
}

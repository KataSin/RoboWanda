using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    private Vector3 m_originPos;

    private float m_readyTime;

    private float m_feadTime;

    private float t;

    [SerializeField]
    private GameObject player_;

    // Use this for initialization
    void Start()
    {
        m_originPos = transform.position;
        m_readyTime = 6f;
        m_feadTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void titleReadyCamera()
    {
        transform.position = Vector3.Slerp(m_originPos, new Vector3(0f, 0.8f, -10.5f), t / m_readyTime);
    }

    public void titleReadyToStart()
    {
        transform.position = Vector3.Slerp(new Vector3(0f, 0.8f, -10.5f), new Vector3(2.5f, 0.9f, -10.0f), t / m_feadTime);
        transform.LookAt(player_.transform.position + new Vector3(-0.61f, 0.9f, -1.0f));
    }

    public float Get_ReadyTime()
    {
        return m_readyTime;
    }

    public float Get_FeadTime()
    {
        return m_feadTime;
    }

    public float Get_Timer()
    {
        return t;
    }

    public void Set_Timer(float timer)
    {
        t += timer;
    }

    public void Reset_Timer()
    {
        t = 0f;
    }

}

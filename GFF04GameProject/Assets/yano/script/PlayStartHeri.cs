using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStartHeri : MonoBehaviour
{
    private float t, t1, t2, t3;

    [SerializeField]
    private float m_LOverTime;

    private Vector3 m_originPos;

    private Vector3 m_LOverPos;

    private Vector3 m_FallPos;

    private Vector3 m_originL_Ropepos;

    [SerializeField]
    private GameObject landingCamera_;

    [SerializeField]
    private GameObject player_;

    [SerializeField]
    private GameObject rope_obj_;

    private float m_speed;

    private float m_intervalTimer1, m_intervalTimer2;

    private bool isClear;
    private bool isStop;

    // Use this for initialization
    void Start()
    {
        t = 0f;
        t1 = 0f;
        t2 = 0f;
        t3 = 0f;
        m_intervalTimer1 = 0f;
        m_intervalTimer2 = 0f;

        m_speed = 1.0f;

        m_originPos = transform.position;

        m_LOverPos = new Vector3(m_originPos.x, m_originPos.y, landingCamera_.transform.position.z);

        m_originL_Ropepos = rope_obj_.transform.localPosition;

        isClear = false;
        isStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        // m_speed -= (0.8f / (m_LOverTime)) * Time.deltaTime;

        if (player_.GetComponent<PlayerController>().GetPlayerState() == 0)
        {
            m_speed -= 0.12f * Time.deltaTime;
            if (m_speed <= 0.1f)
                m_speed = 0.1f;
            t += m_speed * Time.deltaTime;

            if (t >= m_LOverTime)
            {
                m_intervalTimer1 += 1.0f * Time.deltaTime;
                if (m_intervalTimer1 >= 1f)
                {
                    m_speed = 0f;

                    if (!isClear)
                    {
                        m_FallPos = new Vector3(m_LOverPos.x, m_LOverPos.y - 50f, m_LOverPos.z);
                        isClear = true;
                    }

                    t1 += 1.0f * Time.deltaTime;

                    if (t1 >= 4f)
                    {
                        t1 = 4f;


                        t2 += 1.0f * Time.deltaTime;
                        //rope_obj_.transform.localPosition = Vector3.Lerp(new Vector3(m_originL_Ropepos.x, 14f, m_originL_Ropepos.z), new Vector3(m_originL_Ropepos.x, -13f, m_originL_Ropepos.z), t2 / 1.5f);

                        if (t2 >= 1f)
                            isStop = true;
                    }

                    MoveFallPoint();
                }
            }

            else if (t < m_LOverTime)
                MoveLandingOverPoint();
        }
        MoveLeave();
    }

    private void MoveLandingOverPoint()
    {
        transform.position = Vector3.Lerp(m_originPos, m_LOverPos, t / m_LOverTime);
    }

    private void MoveFallPoint()
    {
        transform.position = Vector3.Lerp(m_LOverPos, m_FallPos, t1 / 4f);
    }

    private void MoveLeave()
    {
        if (player_.GetComponent<PlayerController>().GetPlayerState() != 0)
        {
            m_intervalTimer2 += 1.0f * Time.deltaTime;

            if (m_intervalTimer2 >= 4f)
            {
                t3 += 1.0f * Time.deltaTime;
                if (t3 < 4f)
                {
                    transform.position = Vector3.Lerp(m_FallPos, m_LOverPos, t3 / 4f);
                    rope_obj_.transform.localPosition =
                        Vector3.Lerp(new Vector3(m_originL_Ropepos.x, -13f, m_originL_Ropepos.z), new Vector3(m_originL_Ropepos.x, 14f, m_originL_Ropepos.z), t3 / 1.5f);
                }
                else if (t3 >= 4f)
                    transform.position -= transform.forward;
            }
        }
    }

    public bool Get_StopFlag()
    {
        return isStop;
    }
}

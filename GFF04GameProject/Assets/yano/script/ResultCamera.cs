using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCamera : MonoBehaviour
{
    private Vector3 m_origin_pos;

    private Quaternion m_origin_rotation;

    [SerializeField]
    private float moveTime;

    [SerializeField]
    private GameObject heri_;

    private float t, t2, t3;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        m_origin_pos = transform.position;
        m_origin_rotation = transform.rotation;
        t = 0f;
        t2 = 0f;
        t3 = 0f;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResultMove()
    {
        t += 1.0f * Time.deltaTime;

        transform.position = Vector3.Lerp(m_origin_pos, new Vector3(-1.3f, -0.18f, -13.3f), t / moveTime);

        if (t >= moveTime)
            isClear = true;
    }

    public void FinishMove()
    {
        transform.position = Vector3.Lerp(new Vector3(-1.3f, -0.18f, -13.3f), new Vector3(-8.86f, -0.18f, -18.8f), t2 / 3f);

        t2 += 1.0f * Time.deltaTime;
    }

    public void FinishRotate()
    {
        transform.rotation = Quaternion.Slerp(m_origin_rotation, Quaternion.Euler(transform.rotation.x, 65f, 0f), t3 / 2f);

        t3 += 1.0f * Time.deltaTime;
    }

    public bool Get_Clear()
    {
        return isClear;
    }

    public void SetT(float l_t)
    {
        t = l_t;
    }

    public float GetMoveTime()
    {
        return moveTime;
    }
}

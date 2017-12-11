using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCamera : MonoBehaviour
{
    private Vector3 m_origin_pos;

    [SerializeField]
    private float moveTime;

    private float t;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        m_origin_pos = transform.position;
        t = 0f;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResultMove()
    {
        t += 1.0f * Time.deltaTime;

        transform.position = Vector3.Lerp(m_origin_pos, new Vector3(5.74f, -2.49f, -21.68f), t / moveTime);

        if (t >= moveTime)
            isClear = true;
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

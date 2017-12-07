using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Vector3 m_origin_pos;

    private float t;

    private bool isClear;

    [SerializeField]
    private float m_afterLposY;

    // Use this for initialization
    void Start()
    {
        m_origin_pos = transform.localPosition;

        t = 0f;

        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp(t, 0f, 2f);

        isClear = (t >= 2f) ? true : false;

        transform.localPosition =
            Vector3.Lerp(m_origin_pos, new Vector3(m_origin_pos.x, m_afterLposY, m_origin_pos.z), t / 2f);
    }

    public void Open()
    {
        t += 1.0f * Time.deltaTime;
    }

    public void Close()
    {
        t -= 1.0f * Time.deltaTime;
    }

    public bool GetClear()
    {
        return isClear;
    }
}

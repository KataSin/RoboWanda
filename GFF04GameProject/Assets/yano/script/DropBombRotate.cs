using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBombRotate : MonoBehaviour
{
    private Quaternion m_origin_rotation;
    private float t;

    // Use this for initialization
    void Start()
    {
        m_origin_rotation = transform.rotation;
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation =
            Quaternion.Slerp(m_origin_rotation, Quaternion.Euler(m_origin_rotation.x, m_origin_rotation.y, -90f), t / 2f);

        t += 1.0f * Time.deltaTime;
    }
}

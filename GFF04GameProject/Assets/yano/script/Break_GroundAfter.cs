using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_GroundAfter : MonoBehaviour
{
    //経過時間
    private float m_time;

    // Use this for initialization
    void Start()
    {
        m_time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (m_time >= 3f)
        //    Destroy(this.gameObject);

        m_time += Time.deltaTime;
    }
}

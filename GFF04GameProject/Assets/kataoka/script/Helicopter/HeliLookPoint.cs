using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliLookPoint : MonoBehaviour
{
    private GameObject m_Player;
    private Vector3 m_Position;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = m_Player.transform.position;
        Vector3 vec = playerPos - transform.position;
        vec.y = 0.0f;
        vec = vec.normalized;

        transform.rotation = Quaternion.LookRotation(-vec);
    }
}

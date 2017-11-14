using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawn : MonoBehaviour
{
    //ミサイル
    public GameObject m_Missile;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            for (int i = 0; i <= 10.0f; i++)
            {
                Instantiate(m_Missile, transform.position, Quaternion.identity);
            }
        }
    }
}

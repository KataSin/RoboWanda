using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationLeg : MonoBehaviour
{
    private GameObject m_Player;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        string name = other.name.Substring(0,4);
        if (name == "Rigi")
        {
            float maxDis = 100.0f;

            float dis = Vector3.Distance(m_Player.transform.position, transform.position);
            if (dis < maxDis)
            {
                Camera.main.GetComponent<CameraShake>().Shake((maxDis - dis) / 10.0f);
            }

        }
    }
}

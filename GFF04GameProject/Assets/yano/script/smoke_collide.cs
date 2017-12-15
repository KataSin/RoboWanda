using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke_collide : MonoBehaviour
{
    private GameObject camera_;

    private float m_distance;

    // Use this for initialization
    void Start()
    {
        camera_ = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        m_distance = Vector3.Distance(transform.position, camera_.transform.position);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            camera_.GetComponent<CameraShake>().Shake((m_distance - 25f) / 25f);
        }
    }
}

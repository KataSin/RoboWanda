using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    Rigidbody rigids_;

    // Use this for initialization
    void Start()
    {
        rigids_ = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 l_clampVelocity = rigids_.velocity;
        l_clampVelocity.x = Mathf.Clamp(l_clampVelocity.x, -0.1f, 0.1f);
        l_clampVelocity.z = Mathf.Clamp(l_clampVelocity.z, -0.1f, 0.1f);
        l_clampVelocity.y = Mathf.Clamp(l_clampVelocity.y, -9.8f, 5f);
        rigids_.velocity = l_clampVelocity;
    }

}

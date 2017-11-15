using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> debris_;

    private Rigidbody rigids_;
    private Vector3 clampVelocities;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < debris_.Count; i++)
        {
            rigids_ = debris_[i].GetComponent<Rigidbody>();
            clampVelocities = rigids_.velocity;
            clampVelocities.x = Mathf.Clamp(rigids_.velocity.x, -10f, 10f);
            clampVelocities.z = Mathf.Clamp(rigids_.velocity.z, -10f, 10f);
            clampVelocities.y = Mathf.Clamp(rigids_.velocity.y, rigids_.velocity.y, 50f);
            rigids_.velocity = clampVelocities;
        }
    }

}

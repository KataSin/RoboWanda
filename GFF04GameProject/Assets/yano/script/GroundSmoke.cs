using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSmoke : MonoBehaviour
{
    private ParticleSystem smoke_;

    // Use this for initialization
    void Start()
    {
        smoke_ = GetComponent<ParticleSystem>();
        smoke_.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!smoke_.isPlaying)
            Destroy(this.gameObject);
    }
}

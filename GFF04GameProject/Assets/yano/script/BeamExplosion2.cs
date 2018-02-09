using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamExplosion2 : MonoBehaviour
{
    private AudioClip explosion_se_;

    // Use this for initialization
    void Start()
    {
        explosion_se_ = GetComponent<AudioSource>().clip;
        //GetComponent<AudioSource>().PlayOneShot(explosion_se_);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<ParticleSystem>().IsAlive(true))
        {
            Destroy(this.gameObject);
        }
    }
}

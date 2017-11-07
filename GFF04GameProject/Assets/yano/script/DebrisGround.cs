using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisGround : MonoBehaviour
{
    [SerializeField]
    private bool isGround;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Hit_Ground()
    {
        return isGround;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
            isGround = true;
    }
}

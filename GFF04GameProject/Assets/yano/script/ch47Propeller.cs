using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch47Propeller : MonoBehaviour
{
    [SerializeField]
    private GameObject f_Pro_;

    [SerializeField]
    private GameObject b_Pro_;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        f_Pro_.transform.Rotate(Vector3.up, 40f);
        b_Pro_.transform.Rotate(-Vector3.up, 40f);
    }
}

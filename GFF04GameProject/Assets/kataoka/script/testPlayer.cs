using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{

    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 vec = Camera.main.transform.forward * 30.0f;
            vec.y = 0.0f;
            rb.AddForce(vec);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 vec = -Camera.main.transform.forward * 30.0f;
            vec.y = 0.0f;
            rb.AddForce(vec);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Camera.main.transform.right * 30.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-Camera.main.transform.right * 30.0f);
        }
    }
}

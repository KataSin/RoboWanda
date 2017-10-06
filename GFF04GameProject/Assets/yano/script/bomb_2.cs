using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_2 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * 2;
        else if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * 2;
        else if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * 2;
        else if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * 2;
    }
}

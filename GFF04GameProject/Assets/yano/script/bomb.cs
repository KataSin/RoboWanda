using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += transform.forward * 2;
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.position -= transform.forward * 2;
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.position += transform.right * 2;
        else if (Input.GetKey(KeyCode.LeftArrow))
            transform.position -= transform.right * 2;
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}

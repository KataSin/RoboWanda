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
        if (Input.GetKeyDown(KeyCode.UpArrow))
            transform.position += transform.forward / 10f;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            transform.position -= transform.forward / 10f;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.position += transform.right / 10f;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            transform.position -= transform.right / 10f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCamera : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            transform.position += transform.forward / 2f;
        else if (Input.GetKey(KeyCode.Alpha4))
            transform.position -= transform.forward / 2f;

        if (Input.GetKey(KeyCode.Alpha2))
            transform.position -= transform.right / 2f;
        else if (Input.GetKey(KeyCode.Alpha3))
            transform.position += transform.right / 2f;

        if (Input.GetKey(KeyCode.Alpha5))
            transform.position += transform.up / 2f;
        else if (Input.GetKey(KeyCode.Alpha6))
            transform.position -= transform.up / 2f;

        if (Input.GetKey(KeyCode.Alpha7))
            transform.Rotate(-transform.up * 2f);
        else if (Input.GetKey(KeyCode.Alpha8))
            transform.Rotate(transform.up * 2f);

        if (Input.GetKey(KeyCode.Alpha9))
            transform.Rotate(transform.right * 2f);
        else if (Input.GetKey(KeyCode.Alpha0))
            transform.Rotate(-transform.right * 2f);
    }
}

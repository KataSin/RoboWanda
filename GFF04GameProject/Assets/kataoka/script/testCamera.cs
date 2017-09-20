using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamera : MonoBehaviour
{
    public GameObject rotateCamera;

    public Vector3 angle;
    // Use this for initialization
    void Start()
    {
        angle = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle.y -= 100.0f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle.y += 100.0f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            angle.x -= 100.0f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            angle.x += 100.0f * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(angle);
        Camera.main.transform.LookAt(transform.position + new Vector3(0, 4, 0));
    }

}

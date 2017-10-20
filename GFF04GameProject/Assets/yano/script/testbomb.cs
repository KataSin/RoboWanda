using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbomb : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            new Vector3(0f, -Input.GetAxis("Vertical_R") * Time.deltaTime, 0f)
            );
    }
}

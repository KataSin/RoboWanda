using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    private bool isCheck;

    // Use this for initialization
    void Start()
    {
        isCheck = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isCheck = true;
    }

    public bool Get_CheckFlag()
    {
        return isCheck;
    }
}

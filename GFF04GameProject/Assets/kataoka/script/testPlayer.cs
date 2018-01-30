using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    private Vector3 sevePos;

    private Vector3 velo;
    // Use this for initialization
    void Start()
    {
        sevePos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        velo = sevePos - transform.localPosition;
        sevePos = transform.localPosition;
    }

    public Vector3 GetVelo()
    {
        return velo;
    }


}

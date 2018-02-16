using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class testScript : MonoBehaviour
{
    public testPlayer test;
    private float mTime;

    private bool mFlag;
    // Use this for initialization
    void Start()
    {
        mTime = 0.0f;
        mFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)&&!mFlag)
        {
            mFlag = true;
        }
        if (mFlag)
        {
            mTime += Time.deltaTime;
            GameObject.FindGameObjectWithTag("CameraPosition").GetComponent<CameraPosition>().SetEventState(PlayerCameraMode.Event,EventCameraState.BossLook);
            if (mTime >= 3.0f)
            {
                GameObject.FindGameObjectWithTag("CameraPosition").GetComponent<CameraPosition>().SetEventState(PlayerCameraMode.Event, EventCameraState.PlayerLook);
                mFlag = false;
                mTime = 0.0f;
            }
        }
    }

    public float Vector2Cross(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.z - rhs.x * lhs.z;
    }
}
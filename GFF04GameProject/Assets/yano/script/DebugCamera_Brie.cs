using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCamera_Brie : MonoBehaviour
{
    private float m_rotateRight;

    private float m_mousePosX, m_mousePosY;

    [SerializeField]
    private bool isMouseFlag;

    [SerializeField]
    private GameObject cameraDebuger_;

    // Use this for initialization
    void Start()
    {
        m_rotateRight = 0f;

        m_mousePosX = 0f;
        m_mousePosY = 0f;

        isMouseFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveRotate();
    }

    private void MoveRotate()
    {
        if (cameraDebuger_.GetComponent<BriefingCnt>().Get_Debug())
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

            if (Input.GetKeyDown(KeyCode.M))
                isMouseFlag = !isMouseFlag;

            switch (isMouseFlag)
            {
                case false:

                    if (Input.GetKey(KeyCode.Alpha7))
                        transform.Rotate(-Vector3.up * 2f, Space.World);
                    else if (Input.GetKey(KeyCode.Alpha8))
                        transform.Rotate(Vector3.up * 2f, Space.World);

                    if (Input.GetKey(KeyCode.Alpha9))
                        transform.Rotate(Vector3.right * 2f);
                    else if (Input.GetKey(KeyCode.Alpha0))
                        transform.Rotate(-Vector3.right * 2f);

                    break;

                case true:

                    transform.Rotate(-Vector3.right * Input.GetAxis("MouseY") / 8f);
                    transform.Rotate(Vector3.up * Input.GetAxis("MouseX") / 8f, Space.World);

                    break;
            }
        }
    }
}

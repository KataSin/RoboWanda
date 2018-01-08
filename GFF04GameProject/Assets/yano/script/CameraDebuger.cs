using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDebuger : MonoBehaviour
{
    private enum CameraMode
    {
        Main,
        Debug,
    }

    [SerializeField]
    private CameraMode m_Mode;

    [SerializeField]
    private Camera mainCamera_;

    [SerializeField]
    private Camera debugCamera_;

    // Use this for initialization
    void Start()
    {
        m_Mode = CameraMode.Main;

        mainCamera_.enabled = true;
        debugCamera_.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraModeChange();
    }

    private void CameraModeChange()
    {
        if (Input.GetKeyDown(KeyCode.B))
            debugCamera_.enabled = !debugCamera_.enabled;

        if (debugCamera_.enabled)
        {
            m_Mode = CameraMode.Debug;
            Cursor.visible = false;
        }
        else
        {
            m_Mode = CameraMode.Main;
            Cursor.visible = true;
        }
    }

    public int GetMode()
    {
        return (int)m_Mode;
    }
}

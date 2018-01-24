using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugSelect : MonoBehaviour
{
    private GameObject scene_;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("SceneController"))
            scene_ = GameObject.FindGameObjectWithTag("SceneController");

        if (!isClear)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)
                && scene_ != null)
            {
                StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Title"));
                isClear = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)
                && scene_ != null)
            {
                StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Master 2"));
                isClear = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)
                && scene_ != null)
            {
                StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Tutorial"));
                isClear = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)
                && scene_ != null)
            {
                StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Briefing"));
                isClear = true;
            }
        }
    }
}

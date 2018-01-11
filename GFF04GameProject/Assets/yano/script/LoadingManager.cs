using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    private GameObject sceneCnt_;

    private bool isLScene;

    // Use this for initialization
    void Start()
    {
        isLScene = false;

        if (GameObject.FindGameObjectWithTag("SceneController"))
            sceneCnt_ = GameObject.FindGameObjectWithTag("SceneController");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLScene && sceneCnt_ != null)
        {
            if (sceneCnt_.GetComponent<SceneController>().GetNextScene() == 0)
                StartCoroutine(sceneCnt_.GetComponent<SceneController>().SceneLoad("lightTest 5"));

            else if (sceneCnt_.GetComponent<SceneController>().GetNextScene() == 1)
                StartCoroutine(sceneCnt_.GetComponent<SceneController>().SceneLoad("Tutorial"));

            isLScene = true;
        }
    }
}

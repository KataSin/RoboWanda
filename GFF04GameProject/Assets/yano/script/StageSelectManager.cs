using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    private SceneController scene_;

    // Use this for initialization
    void Start()
    {
        scene_ = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            scene_.SceneChange("stage_a");
    }
}

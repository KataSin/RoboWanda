using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugSelect : MonoBehaviour
{
    private GameObject scene_;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("SceneController"))
            scene_ = GameObject.FindGameObjectWithTag("SceneController");

        if (Input.GetKeyDown(KeyCode.Alpha1)
            && scene_ != null)
            StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Title"));
        else if (Input.GetKeyDown(KeyCode.Alpha2)
            && scene_ != null)
            StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("lightTest 5"));
        else if (Input.GetKeyDown(KeyCode.Alpha3)
            && scene_ != null)
            StartCoroutine(scene_.GetComponent<SceneController>().SceneLoad("Tutorial"));
    }
}

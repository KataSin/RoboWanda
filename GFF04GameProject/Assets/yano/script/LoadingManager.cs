using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject sceneCnt_;

    [SerializeField]
    private GameObject neko_;

    private bool isLScene;

    // Use this for initialization
    void Start()
    {
        isLScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLScene)
        {
            StartCoroutine(sceneCnt_.GetComponent<SceneController>().SceneLoad("newnewNightTest 1"));
            isLScene = true;
        }
    }
}

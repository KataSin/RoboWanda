using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject sceneCnt_;

    [SerializeField]
    private GameObject scoreMana_;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("SceneController") == null)
        {
            Instantiate(sceneCnt_);
        }

        if (GameObject.FindGameObjectWithTag("ScoreManager") == null)
        {
            Instantiate(scoreMana_);
        }
    }
}

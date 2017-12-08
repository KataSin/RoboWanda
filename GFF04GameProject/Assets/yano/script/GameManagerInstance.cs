using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInstance : MonoBehaviour
{
    [SerializeField]
    private GameObject gameMana_;

    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameManager") == null)
        {
            Instantiate(gameMana_);
        }
    }
}

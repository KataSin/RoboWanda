using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeepManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> jeeps_;

    // Use this for initialization
    void Start()
    {
        Instantiate(jeeps_[0], new Vector3(440.1f, 0.42f, 96.6f), Quaternion.Euler(0f, -90f, 0f));
        Instantiate(jeeps_[1], new Vector3(450.1f, 0.42f, 96.6f), Quaternion.Euler(0f, -90f, 0f));
        Instantiate(jeeps_[2], new Vector3(460.1f, 0.42f, 96.6f), Quaternion.Euler(0f, -90f, 0f));
        Instantiate(jeeps_[3], new Vector3(470.1f, 0.42f, 96.6f), Quaternion.Euler(0f, -90f, 0f));
    }

    // Update is called once per frame
    void Update()
    {

    }
}

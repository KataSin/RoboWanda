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
        Instantiate(jeeps_[0], new Vector3(transform.position.x + 41.1f, 0.42f, 96.6f), Quaternion.Euler(0f, -90f, 0f));
        Instantiate(jeeps_[1], new Vector3(transform.position.x + 51.1f, 0.42f, 96.6f), Quaternion.Euler(0f, -90f, 0f));
        Instantiate(jeeps_[2], new Vector3(transform.position.x + 61.1f, 0.42f, 96.6f), Quaternion.Euler(0f, -90f, 0f));
        Instantiate(jeeps_[3], new Vector3(transform.position.x + 71.1f, 0.42f, 96.6f), Quaternion.Euler(0f, -90f, 0f));

    }

    // Update is called once per frame
    void Update()
    {
    }
}

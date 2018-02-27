using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadLight : MonoBehaviour
{
    [SerializeField]
    private GameObject angry_text_;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LightCheckCollision")
        {
            angry_text_.GetComponent<AngryText>().AngryCall();
        }
    }
}

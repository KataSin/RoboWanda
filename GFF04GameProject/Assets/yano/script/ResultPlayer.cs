using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayer : MonoBehaviour
{
    private Animator animator_;

    [SerializeField]
    private GameObject look_pos_;

    // Use this for initialization
    void Start()
    {
        animator_ = GetComponent<Animator>();

        animator_.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAnimatorIK()
    {
        animator_.SetLookAtWeight(1.0f, 0.0f, 1.0f, 0.0f, 0.0f);
        animator_.SetLookAtPosition(look_pos_.transform.position);
    }
}

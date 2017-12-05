using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayer : MonoBehaviour
{
    private Animator animator_;

    [SerializeField]
    private GameObject destinationL_;

    [SerializeField]
    private GameObject destinationR_;

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
        animator_.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator_.SetIKPosition(AvatarIKGoal.LeftHand, destinationL_.transform.position);

        animator_.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator_.SetIKPosition(AvatarIKGoal.RightHand, destinationR_.transform.position);
    }
}

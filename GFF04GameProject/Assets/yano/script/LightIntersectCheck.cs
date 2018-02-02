using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntersectCheck : MonoBehaviour
{
    [SerializeField]
    private bool isAttackFlag;

    // Use this for initialization
    void Start()
    {
        isAttackFlag = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LightCollision")
            isAttackFlag = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LightCollision")
            isAttackFlag = false;
    }

    public bool Get_AttackFlag()
    {
        return isAttackFlag;
    }
}

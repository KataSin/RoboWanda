using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisPart_collide : MonoBehaviour
{
    [SerializeField]
    private GameObject dp_collide_manager_;

    [SerializeField]
    private bool isBreak;

    // Use this for initialization
    void Start()
    {
        isBreak = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (!dp_collide_manager_.GetComponent<DebrisPart_collide_manager>().Get_Clear()
            &&
            (other.GetComponent<RobotDamage>() != null
            ||
            other.gameObject.tag == "bom"))
        {
            isBreak = true;
        }

        //if (other.gameObject.tag == "bom"
        //   ||
        //   other.gameObject.tag == "RobotArmAttack"
        //   ||
        //   other.gameObject.tag == "RobotBeam"
        //   ||
        //   other.gameObject.tag == "Missile")
        //{
        //    isBreak = true;
        //}
    }

    public bool Get_BreakFlag()
    {
        return isBreak;
    }
}

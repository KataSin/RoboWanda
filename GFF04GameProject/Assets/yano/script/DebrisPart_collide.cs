using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisPart_collide : MonoBehaviour
{
    [SerializeField]
    [Header("破片パーツ")]
    private GameObject debrisParts_obj_;

    [SerializeField]
    private GameObject parentBill_obj_;

    [SerializeField]
    private GameObject originBill_obj_;

    [SerializeField]
    private GameObject afterBill_obj_;

    [SerializeField]
    private bool isBreak;

    [SerializeField]
    private bool isClear;

    [SerializeField]
    private GameObject dp_1_obj_;

    // Use this for initialization
    void Start()
    {
        isBreak = false;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBreak && !isClear)
        {
            Instantiate(debrisParts_obj_, transform.position, transform.rotation);
            if (debrisParts_obj_.ToString() == "DP_2")
            {

            }
            Destroy(this.gameObject);
            Destroy(originBill_obj_);
            Instantiate(
                afterBill_obj_,
                originBill_obj_.transform.position,
                originBill_obj_.transform.rotation,
                parentBill_obj_.transform
                );

            isClear = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RobotDamage>() != null)
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
}

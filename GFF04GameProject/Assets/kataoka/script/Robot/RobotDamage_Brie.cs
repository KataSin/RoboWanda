using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDamage_Brie : MonoBehaviour
{
    //ロボットマネージャー
    private BriefingRobot robot_;

    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Robot").GetComponent<BriefingRobot>() != null)
            robot_ = GameObject.FindGameObjectWithTag("Robot").GetComponent<BriefingRobot>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (robot_ == null) return;

        if (other.GetComponent<TankGun1>() != null)
        {
            robot_.Damage();
        }
    }

}

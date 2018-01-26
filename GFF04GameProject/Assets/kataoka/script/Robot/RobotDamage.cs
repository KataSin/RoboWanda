using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDamage : MonoBehaviour
{
    //ロボットマネージャー
    private RobotManager m_Manager;

    //スコアマネージャー
    private GameObject scoreMana_;

    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>() != null)
            m_Manager = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>();

        if (GameObject.FindGameObjectWithTag("ScoreManager"))
            scoreMana_ = GameObject.FindGameObjectWithTag("ScoreManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (m_Manager == null) return;
        if (other.tag == "bom")
        {
            if (GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>() != null)
                m_Manager.Damage(10);

            if (scoreMana_ != null)
                scoreMana_.GetComponent<ScoreManager>().SetAtackScore(10);
        }
        //if (other.tag == "TowerCollision")
        //{
        //    if (other.transform.parent.GetComponent<tower_collide_manager>().Get_HitOther() == 1 &&
        //         !other.transform.parent.GetComponent<tower_collide_manager>().Get_RobotHit())
        //    {
        //        other.transform.parent.GetComponent<tower_collide_manager>().Set_RobotHit(true);
        //        m_Manager.Damage(40);
        //    }
        //}
        if (other.tag == "ExplosionCollision")
        {
            m_Manager.Damage(1);
        }
    }

}

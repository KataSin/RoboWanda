using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotAI : MonoBehaviour
{
    private RobotAction action;
    private RobotManager manager;
    private GameObject player;
    private NavMeshAgent agent;
    private GameObject robotEye;


    private float attackTime;
    // Use this for initialization
    void Start()
    {
        action = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotAction>();
        manager = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>();

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GameObject.FindGameObjectWithTag("Robot").GetComponent<NavMeshAgent>();


        robotEye = GameObject.FindGameObjectWithTag("RobotEye");
        attackTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.remainingDistance);
        //attackTime += Time.deltaTime;
        //if (attackTime >= 10.0f&&agent.velocity.magnitude<=1.0f&&agent.remainingDistance<=1000.0f)
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_ARM_ATTACK,false);
        //    attackTime = 0.0f;
        //}
        //else if (agent.remainingDistance >= 1000.0f)
        //{
        //    manager.SetAction(RobotAction.RobotState.ROBOT_MOVE, false);
        //}


        Vector3 vec = (player.transform.position - robotEye.transform.position).normalized * 2000.0f;
        Vector3 start = robotEye.transform.position;
        RaycastHit hit;
        int mask = ~(1 << 8);
        if (Physics.Raycast(start, vec, out hit, 20000.0f,mask))
        {
            if (hit.collider.tag == "Player")
            {
                manager.SetAction(RobotAction.RobotState.ROBOT_MOVE, false);
            }
            else
            {
                manager.SetAction(RobotAction.RobotState.ROBOT_IDLE, false);
            }
        }


    }
}

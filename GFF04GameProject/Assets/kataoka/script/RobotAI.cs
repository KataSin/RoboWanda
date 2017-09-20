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



    private float attackTime;
    // Use this for initialization
    void Start()
    {
        action = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotAction>();
        manager = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>();

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GameObject.FindGameObjectWithTag("Robot").GetComponent<NavMeshAgent>();


        attackTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        attackTime += Time.deltaTime;
        if (attackTime >= 10.0f&&agent.velocity.magnitude<=1.0f&&agent.remainingDistance<=500.0f)
        {
            manager.SetAction(RobotAction.RobotState.ROBOT_ARM_ATTACK,false);
            attackTime = 0.0f;
        }
        else if (agent.remainingDistance >= 500.0f)
        {
            manager.SetAction(RobotAction.RobotState.ROBOT_MOVE, false);
        }

        Debug.Log(agent.remainingDistance);
    }
}

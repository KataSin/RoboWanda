using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：爆弾の爆発
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>
public class BombExplosion : MonoBehaviour
{
    [SerializeField]
    [Header("爆発エフェクト")]
    private GameObject exlosion_obj_;


    // Use this for initialization
    void Start()
    {
        Instantiate(exlosion_obj_, transform.position, Quaternion.identity);

        // 出現後0.5秒、消滅
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject robot = GameObject.FindGameObjectWithTag("Robot");

        if (other.transform.IsChildOf(robot.transform))
        {
            RobotManager robot_mana_ = robot.GetComponent<RobotManager>();
            robot_mana_.Damage(10);
        }
    }
}

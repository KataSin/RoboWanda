using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    private SceneController scene_;

    [SerializeField]
    private GameObject robot_obj_;
    private RobotManager robotMana_;

    [SerializeField]
    private GameObject player_obj_;
    private PlayerControllerAlpha player_;

    // Use this for initialization
    void Start()
    {
        scene_ = GameObject.Find("SceneController").GetComponent<SceneController>();

        robotMana_ = robot_obj_.GetComponent<RobotManager>();

        player_ = player_obj_.GetComponent<PlayerControllerAlpha>();

    }

    // Update is called once per frame
    void Update()
    {
        //ステージ上の全キャラクターのHPチェック
        Check_CharaHp();
    }

    //ステージ上の全キャラクターのHPチェック
    private void Check_CharaHp()
    {
        if (robotMana_.GetRobotHP() <= 0)
            scene_.SceneChange("Result");

        else if (player_.IsDead())
            scene_.SceneChange("GameOver");
    }

}

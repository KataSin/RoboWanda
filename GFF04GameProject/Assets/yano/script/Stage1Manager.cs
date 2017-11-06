using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    private SceneController scene_;

    private RobotManager robotMana_;

    // Use this for initialization
    void Start()
    {
        if (scene_ != null)
            scene_ = GameObject.Find("SceneController").GetComponent<SceneController>();

        robotMana_ = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>();
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
        if (robotMana_.GetRobotHP() <= 0&& scene_ != null)
            scene_.SceneChange("Result");
    }

}

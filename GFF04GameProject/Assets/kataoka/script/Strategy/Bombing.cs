using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombing : MonoBehaviour
{
    //爆弾
    public GameObject m_BomPrefab;
    //移動するベクトル
    private Vector3 m_Vec;
    //時間
    private float m_Time;
    //ゴール座標
    private Vector3 m_GoalPos;
    //壊れたかどうか
    public bool m_BreakFlag;

    private Quaternion m_FirstRotate;
    private Quaternion m_EndRotate;
    //炎のエフェクト
    private GameObject m_FireEffect;
    public float m_BomSpanwIntervalTime = 0.1f;

    private int m_SpawnCount;
    public GameObject m_Exprosion;
    private bool m_IsStop;
    //壊れた時の線形保管
    private float m_BreakLerpTime;

    private float m_StopTime;
    // Use this for initialization
    void Start()
    {
        m_SpawnCount = 0;
        m_StopTime = 0.0f;
        m_Time = 0.0f;
        m_BreakLerpTime = 0.0f;
        m_BreakFlag = false;
        m_IsStop = false;
        m_FireEffect = transform.Find("Fire").gameObject;
        GameObject.FindGameObjectWithTag("RobotLightManager").GetComponent<StrategyRobotLightManager>().m_IsLight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsStop)
        {
            m_StopTime += Time.deltaTime;
        }
        if (m_StopTime >= 0.5f) return;
        if (m_BreakFlag)
        {
            m_FireEffect.SetActive(true);
            m_BreakLerpTime += 0.5f * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(m_FirstRotate, m_EndRotate, m_BreakLerpTime);
            transform.position += transform.forward.normalized * 30.0f * Time.deltaTime;
            return;
        }

        //移動
        transform.position += 40.0f * m_Vec * Time.deltaTime;
        //削除処理
        if (Vector3.Distance(transform.position, m_GoalPos) <= 5.0f)
        {
            GameObject.FindGameObjectWithTag("RobotLightManager").GetComponent<StrategyRobotLightManager>().m_IsLight = false;
            //ロボットの行動変更
            GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>().SetBehavior(RobotManager.RobotBehavior.ROBOT_ONE);
            Destroy(gameObject);
        }

        if (m_BomPrefab == null) return;

        //爆撃
        m_Time += 2.0f * Time.deltaTime;
        if (m_Time >= m_BomSpanwIntervalTime)
        {
            Instantiate(m_BomPrefab, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
            m_Time = 0.0f;
        }
    }

    public void SetStartEnd(Vector3 start, Vector3 end)
    {
        //ゴールに行くためのベクトルを求める
        m_GoalPos = end;
        m_Vec = (end - start).normalized;
        //回転を求める
        transform.rotation = Quaternion.LookRotation(m_Vec);
        //壊れた時の回転を求める
        m_FirstRotate = transform.rotation;
        m_EndRotate = Quaternion.AngleAxis(50.0f, transform.forward) *
            Quaternion.AngleAxis(30.0f, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_IsStop) return;

        if(other.tag== "ExplosionCollision")
        {
            m_BreakFlag = true;
            //壊れたら行動を変更する
            GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotManager>().SetBehavior(RobotManager.RobotBehavior.ROBOT_ONE);
        }

        m_SpawnCount++;
        if (m_SpawnCount % 2 == 0)
        {
            Instantiate(m_Exprosion, transform.position, Quaternion.identity);
        }
        if (other.tag == "GroundCollisionRigid")
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            m_IsStop = true;
        }
    }

}

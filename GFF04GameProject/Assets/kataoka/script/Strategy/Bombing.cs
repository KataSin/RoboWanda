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


    public float m_BomSpanwIntervalTime = 0.1f;
    // Use this for initialization
    void Start()
    {
        m_Time = 0.0f;
        GameObject.FindGameObjectWithTag("RobotLightManager").GetComponent<StrategyRobotLightManager>().m_IsLight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        transform.position += 40.0f * m_Vec * Time.deltaTime;
        //回転
        transform.rotation = Quaternion.LookRotation(m_Vec);
        //削除処理
        if (Vector3.Distance(transform.position, m_GoalPos) <= 5.0f)
        {
            GameObject.FindGameObjectWithTag("RobotLightManager").GetComponent<StrategyRobotLightManager>().m_IsLight = false;
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
        m_GoalPos = end;
        m_Vec = (end - start).normalized;
    }
}

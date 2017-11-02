using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLeg : MonoBehaviour
{
    //足がはまっているかどうか
    private bool m_IsLeg;
    //穴のIKポイント
    private Vector3 m_IkPoint;
    // Use this for initialization
    void Start()
    {
        m_IsLeg = false;
        m_IkPoint = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hole")
        {
            m_IsLeg = true;
            m_IkPoint = other.transform.parent.Find("IKPoint").transform.position;
            Destroy(other.gameObject);
        }
    }
    /// <summary>
    /// 足はまっているかどうか
    /// </summary>
    /// <returns>true:はまってるfalse:はまっていない</returns>
    public bool GetLegFlag()
    {
        return m_IsLeg;
    }
    /// <summary>
    /// 埋まった穴のIKポイントを取得
    /// </summary>
    /// <returns>IKポイント</returns>
    public Vector3 GetIKPoint()
    {
        return m_IkPoint;
    }
    /// <summary>
    /// フラグを設定する
    /// </summary>
    /// <param name="flag">フラグ</param>
    public void SetLegFlag(bool flag)
    {
        m_IsLeg = flag;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMissileManager : MonoBehaviour
{
    private List<GameObject> m_RightMissiles;
    private List<GameObject> m_LeftMissiles;

    private bool m_FiringFlag;

    private int m_FiringIndex;

    private float m_Time;

    private bool m_IsEnd;

    // Use this for initialization
    void Start()
    {
        m_RightMissiles = new List<GameObject>();
        m_LeftMissiles = new List<GameObject>();

        var trans = transform.GetComponentsInChildren<Transform>();
        foreach (var i in trans)
        {
            if (i.name == "MissileRight")
            {
                m_RightMissiles.Add(i.gameObject);
            }
            else if (i.name == "MissileLeft")
            {
                m_LeftMissiles.Add(i.gameObject);
            }
        }
        m_FiringFlag = false;
        m_IsEnd = false;
        m_FiringIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_FiringIndex >= m_LeftMissiles.Count)
        {
            m_IsEnd = true;
            return;
        }
        if (m_FiringFlag)
        {
            if (m_Time >= 0.2f)
            {
                if (m_LeftMissiles[m_FiringIndex] != null)
                    m_LeftMissiles[m_FiringIndex].GetComponent<HeliMissileObj>().FiringMissile();
                if (m_RightMissiles[m_FiringIndex] != null)
                    m_RightMissiles[m_FiringIndex].GetComponent<HeliMissileObj>().FiringMissile();
                m_Time = 0.0f;
                m_FiringIndex++;
            }
        }
    }
    /// <summary>
    /// 発射する
    /// </summary>
    public void Firing()
    {
        m_FiringFlag = true;
    }
    /// <summary>
    /// 発射終わったかどうか
    /// </summary>
    /// <returns>true:終わったfalse;おわってない</returns>
    public bool GetIsEnd()
    {
        return m_IsEnd;
    }
}

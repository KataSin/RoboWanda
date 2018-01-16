using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawn : MonoBehaviour
{
    //ミサイル
    public GameObject m_Missile;
    public int m_MissileNum = 10;
    private int m_MissileSpawnNum;
    //ミサイル時間
    private float m_MissileTime;
    //ミサイル発射
    private bool m_MissileSpawnFlag;
    // Use this for initialization
    void Start()
    {
        m_MissileTime = 0.0f;
        m_MissileSpawnFlag = false;
        m_MissileSpawnNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_MissileSpawnFlag)
        {
            for (int i = 0; i <= m_MissileNum;i++)
            {
                Instantiate(m_Missile, transform.position, Quaternion.identity);
            }
            m_MissileSpawnFlag = false;
        }
        else
        {
            m_MissileTime = 0.0f;
            m_MissileSpawnNum = 0;
        }
    }
    /// <summary>
    /// スポーンするかどうか
    /// </summary>
    /// <param name="flag">true:発射</param>
    public void SpawnFlag(bool flag)
    {
        m_MissileSpawnFlag = flag;
    }

    public bool GetSpawnFlag()
    {
        return m_MissileSpawnFlag;
    }
}

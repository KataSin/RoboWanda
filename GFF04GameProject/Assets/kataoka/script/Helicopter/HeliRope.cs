using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliRope : MonoBehaviour
{
    public GameObject m_RopePrefab;

    public GameObject m_TankPrefab;

    private LineRenderer m_Line;

    private List<GameObject> m_Ropes;

    private GameObject m_Tank;
    // Use this for initialization
    void Start()
    {
        m_Ropes = new List<GameObject>();
        m_Line = gameObject.GetComponent<LineRenderer>();
        for(int i = 0; i <= 5; i++)
        {
            m_Ropes.Add(Instantiate(m_RopePrefab, transform.position + new Vector3(0.0f, -i, 0.0f),Quaternion.identity));
        }

        for(int i = 0; i <= 5; i++)
        {
            var hinge = m_Ropes[i].GetComponent<HingeJoint>();
            if ((i - 1) < 0)
            {
                hinge.connectedBody = gameObject.GetComponent<Rigidbody>();
                continue;
            }
            hinge.connectedBody = m_Ropes[i - 1].GetComponent<Rigidbody>();
        }
        //タンク生成
        m_Tank=Instantiate(m_TankPrefab, m_Ropes[m_Ropes.Count - 1].transform.position, Quaternion.identity);
        m_Tank.GetComponent<HingeJoint>().connectedBody = m_Ropes[m_Ropes.Count - 1].GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Tank == null) return;
        m_Line.positionCount = m_Ropes.Count;
        //Linerenderer設定
        for (int i = 0; i <= m_Ropes.Count - 1; i++)
        {
            m_Line.SetPosition(i, m_Ropes[i].transform.position);
        }
    }
    /// <summary>
    /// 戦車を離す
    /// </summary>
    public void JointFree()
    {
        Destroy(m_Tank.GetComponent<HingeJoint>());
        m_Tank.GetComponent<Tank>().Free();
    }

    public void TankDestroy()
    {
        if (m_Tank == null) return;
        m_Line.enabled = false;
        Destroy(m_Tank);
        foreach(var i in m_Ropes)
        {
            Destroy(i);
        }
    }
}

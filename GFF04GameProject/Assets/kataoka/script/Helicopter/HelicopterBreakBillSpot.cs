using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBreakBillSpot : MonoBehaviour
{

    private Vector3 m_Velo;
    private Vector3 m_SeveVelo;

    private Quaternion m_SeveQuaternion;

    private float m_RotateLerpTime;

    public GameObject m_Light;
    public GameObject m_Propeller;
    public GameObject m_ToPoint;

    public GameObject m_LightLookPoint;

    private Vector3 m_ToPointPos;
    private Vector3 m_StartToPos;
    private Vector3 m_EndToPos;
    private Vector3 m_SevePos;
    private float m_LerpTime;
    // Use this for initialization
    void Start()
    {
        m_SeveQuaternion = transform.rotation;

        m_RotateLerpTime = 0.0f;
        m_LerpTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Propeller.transform.localEulerAngles += new Vector3(0, 0, 1000) * Time.deltaTime;

        if (m_ToPoint == null) return;
        float dis = Vector3.Distance(new Vector3(transform.position.x, 0.0f, transform.position.z), new Vector3(m_ToPoint.transform.position.x, 0.0f, m_ToPoint.transform.position.z));
        if (dis <= 2.0f) return;

        transform.position += transform.forward * 15.0f * Time.deltaTime;

        m_Velo = (m_SeveVelo - transform.position);
        m_SeveVelo = transform.position;

        Quaternion toQuaternion = Quaternion.LookRotation(m_ToPoint.transform.position - transform.position);

        if (m_SeveQuaternion != toQuaternion)
        {
            m_SeveQuaternion = toQuaternion;
            m_RotateLerpTime = 0.0f;
        }
        m_RotateLerpTime += 0.2f * Time.deltaTime;

        Quaternion start = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
        Quaternion end = Quaternion.Euler(0.0f, toQuaternion.eulerAngles.y, 0.0f);
        transform.rotation =
            Quaternion.Lerp(start, end, m_RotateLerpTime);

        if (m_LightLookPoint.transform.position != m_SevePos)
        {
            m_StartToPos = m_ToPointPos;
            m_SevePos = m_LightLookPoint.transform.position;
            m_EndToPos = m_LightLookPoint.transform.position;

            m_LerpTime = 0.0f;
        }
        m_LerpTime += 0.2f * Time.deltaTime;
        m_LerpTime = Mathf.Lerp(0.0f, 1.0f, m_LerpTime);
        m_ToPointPos = Vector3.Lerp(m_StartToPos, m_EndToPos, m_LerpTime);


        m_Light.transform.LookAt(m_ToPointPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(m_ToPointPos,Vector3.one*10.0f);
    }


}

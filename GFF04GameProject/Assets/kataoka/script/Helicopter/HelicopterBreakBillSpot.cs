using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBreakBillSpot : MonoBehaviour
{
    private GameObject m_HeliModel;

    private Vector3 m_Velo;
    private Vector3 m_SeveVelo;

    private Quaternion m_SeveQuaternion;

    private float m_RotateLerpTime;


    public GameObject m_ToPoint;
    // Use this for initialization
    void Start()
    {
        m_HeliModel = transform.Find("Heri").gameObject;
        m_SeveQuaternion = transform.rotation;
        m_ToPoint.transform.rotation= transform.rotation;

        m_RotateLerpTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 5.0f * Time.deltaTime;
        m_Velo = m_SeveVelo - transform.position;
        m_SeveVelo = transform.position;

        Quaternion toQuaternion = Quaternion.LookRotation(m_ToPoint.transform.position - transform.position);
        
        if (m_SeveQuaternion != toQuaternion)
        {
            m_SeveQuaternion = toQuaternion;
            m_RotateLerpTime = 0.0f;
        }
        m_RotateLerpTime += Time.deltaTime;

        Quaternion start = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
        Quaternion end = Quaternion.Euler(0.0f, toQuaternion.eulerAngles.y, 0.0f);
        transform.rotation =
            Quaternion.Lerp(start, end, m_RotateLerpTime)*
            Quaternion.AngleAxis(m_Velo.magnitude*10.0f,transform.right);




        Debug.Log(m_Velo.magnitude);
    }
}

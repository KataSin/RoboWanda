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
    // Use this for initialization
    void Start()
    {
        m_SeveQuaternion = transform.rotation;

        m_RotateLerpTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Propeller.transform.localEulerAngles += new Vector3(0, 0, 1000) * Time.deltaTime;

        if (m_ToPoint == null) return;

        float dis = Vector3.Distance(new Vector3(transform.position.x, 0.0f, transform.position.z), new Vector3(m_ToPoint.transform.position.x, 0.0f, m_ToPoint.transform.position.z));
        if (dis <= 40.0f) return;

        transform.position += transform.forward * 5.0f * Time.deltaTime;

        m_Velo = (m_SeveVelo - transform.position);
        m_SeveVelo = transform.position;

        Quaternion toQuaternion = Quaternion.LookRotation(m_ToPoint.transform.position - transform.position);
        
        if (m_SeveQuaternion != toQuaternion)
        {
            m_SeveQuaternion = toQuaternion;
            m_RotateLerpTime = 0.0f;
        }
        m_RotateLerpTime +=0.2f* Time.deltaTime;

        Quaternion start = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
        Quaternion end = Quaternion.Euler(0.0f, toQuaternion.eulerAngles.y, 0.0f);
        transform.rotation =
            Quaternion.Lerp(start, end, m_RotateLerpTime);


        m_Light.transform.LookAt(m_ToPoint.transform);



        Debug.Log(m_Velo.magnitude);
    }
}

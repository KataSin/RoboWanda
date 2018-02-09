using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBullet : MonoBehaviour
{
    public GameObject m_Parasol;

    public GameObject m_Light;

    //爆発する直前の横ベクトル
    private Vector3 m_RightVec;

    public bool m_IsExprosion;

    private float m_Time;

    private Rigidbody mRigid;

    private Vector3 m_VertexPoint;

    public GameObject m_CollisionObj;
    // Use this for initialization
    void Start()
    {
        //m_IsExprosion = false;
        mRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CollisionObj != null)
            if (m_CollisionObj.GetComponent<CollisionCheck>().GetFlag())
            {
                m_IsExprosion = true;
                Destroy(m_CollisionObj);
            }



        if (m_IsExprosion)
        {
            if (m_Time == 0.0f)
            {
                mRigid.velocity = Vector3.zero;
                mRigid.isKinematic = true;
                m_Parasol.SetActive(true);
                m_Light.SetActive(true);
                transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);
                m_RightVec = transform.right;
                //親子関係を変更
                m_Parasol.transform.parent = null;
                m_Light.transform.parent = null;
                m_Light.transform.parent = m_Parasol.transform;
                transform.parent = m_Parasol.transform;
            }
            float numSin = (Mathf.Sin(m_Time * Mathf.Deg2Rad) + 1.0f) / 2.0f;

            m_Time += 50.0f * Time.deltaTime;
            m_Parasol.transform.position -= new Vector3(0, 0.4f, 0) * Time.deltaTime;
            m_Parasol.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(-40, 40, numSin), m_RightVec);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(mRigid.velocity);
        }
    }

    public void SetVertex(Vector3 vertexPoint)
    {
        m_VertexPoint = vertexPoint;
        //頂点が決められなかった場合は即座に爆破
        if (m_VertexPoint == Vector3.zero)
            m_IsExprosion = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LightCollision" || other.name == "Collision") return;
        if (m_IsExprosion)
        {
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
        }
    }



}

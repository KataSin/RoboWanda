using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LightSerch : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    public GameObject m_LightRotateY;
    public GameObject m_LightRotateZ;

    public GameObject m_Exprosion;
    public GameObject m_FireEffect;
    
    public bool m_IsBreak;
    public bool m_FirstBreak;


    private GameObject m_Robot;

    private List<GameObject> m_SerchLights;
    // Use this for initialization
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Robot = GameObject.FindGameObjectWithTag("Robot");

        m_SerchLights = new List<GameObject>();
        GameObject serchLight = transform.Find("SerchLight").gameObject;
        m_SerchLights.Add(serchLight);
        serchLight = serchLight.transform.Find("SearchlightSupport").gameObject;
        m_SerchLights.Add(serchLight);
        serchLight = serchLight.transform.Find("SearchlightHead").gameObject;
        m_SerchLights.Add(serchLight);

        m_IsBreak = false;
        m_FirstBreak = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsBreak)
        {
            if (!m_FirstBreak)
            {
                Dead();
            }
            return;
        }

        Quaternion lookRobot = Quaternion.LookRotation(transform.position - m_Robot.transform.position);

        m_LightRotateY.transform.eulerAngles =
            new Vector3(m_LightRotateY.transform.eulerAngles.x,
            lookRobot.eulerAngles.y,
            m_LightRotateY.transform.eulerAngles.z);

        m_LightRotateZ.transform.eulerAngles =
            new Vector3(lookRobot.eulerAngles.x,
                m_LightRotateZ.transform.eulerAngles.y,
                m_LightRotateZ.transform.eulerAngles.z
                );
    }

    private void Dead()
    {
        Instantiate(m_Exprosion, transform.position, Quaternion.identity);
        Destroy(gameObject.GetComponent<NavMeshAgent>());
        Destroy(gameObject.GetComponent<BoxCollider>());
        Destroy(transform.Find("LightPushHuman").GetComponent<Animator>());
        Destroy(transform.Find("LightPushHuman").GetComponent<PushLightHuman>());

        m_FireEffect.SetActive(true);
        foreach (var i in m_SerchLights)
        {
            if (i.name == "SearchlightHead")
                Destroy(i.transform.Find("SerchLight").gameObject);
            i.transform.parent = transform;
            i.GetComponent<Rigidbody>().isKinematic = false;
        }
        m_FirstBreak = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBreak : MonoBehaviour
{
    private float m_Time;
    public GameObject m_HelicopterExprosion;
    // Use this for initialization
    void Start()
    {
        Instantiate(m_HelicopterExprosion,transform.position,Quaternion.identity);
        transform.rotation = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
        Transform[] objs = GetComponentsInChildren<Transform>();
        foreach (var i in objs)
        {
            if (i.name != name && i.gameObject.GetComponent<Rigidbody>()!=null)
            i.gameObject.GetComponent<Rigidbody>().AddExplosionForce(500.0f, transform.position,10.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= 5.0f)
        {
            Destroy(gameObject);
        }
    }
}

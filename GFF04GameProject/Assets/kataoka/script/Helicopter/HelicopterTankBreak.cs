using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterTankBreak : MonoBehaviour
{
    private List<GameObject> m_HeliAddFoce;

    public GameObject m_Exprosion;
    // Use this for initialization
    void Start()
    {
        m_HeliAddFoce = new List<GameObject>();
        foreach (var i in GetComponentsInChildren<Transform>())
        {
            if (i.name == "ExprosionHeli")
                m_HeliAddFoce.Add(i.gameObject);
        }
        foreach (var i in m_HeliAddFoce)
        {
            i.GetComponent<Rigidbody>().AddExplosionForce(600.0f, transform.position, 100, .0f);
        }
        Instantiate(m_Exprosion, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyTankPoint : MonoBehaviour
{
    private GameObject m_Gareki;
    // Use this for initialization
    void Start()
    {
        m_Gareki = transform.parent.Find("Gareki_Bill").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetTankDownFlag()
    {
        return m_Gareki.activeInHierarchy;
    }
}

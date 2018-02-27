using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPoint : MonoBehaviour
{
    public GameObject m_Bill;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Bill.GetComponent<tower_Type>().GetIsBom())
            Destroy(gameObject);
    }
}

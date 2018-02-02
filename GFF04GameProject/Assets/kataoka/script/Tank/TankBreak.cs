using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBreak : MonoBehaviour
{
    private List<Rigidbody> m_Rigids;
    // Use this for initialization
    void Start()
    {
        m_Rigids = new List<Rigidbody>();
        foreach(var i in gameObject.GetComponentsInChildren<Transform>())
        {
            Rigidbody rb = i.gameObject.GetComponent<Rigidbody>();
            if ( rb!= null)
            {
                rb.AddExplosionForce(5000, transform.position, 10);
            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

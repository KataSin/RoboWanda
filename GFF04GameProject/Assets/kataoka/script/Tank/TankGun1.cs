using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGun1 : MonoBehaviour
{
    private float m_Time;
    public GameObject m_Exprosion;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= 30.0f)
        {
            Destroy(gameObject);
        }
        transform.position += 50.0f * transform.forward * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name.Substring(0, 3) == "Exp"
            || other.tag == "ExplosionCollision"
            || other.tag == "Robot") return;
        Instantiate(m_Exprosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

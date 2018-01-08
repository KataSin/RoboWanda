using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBom : MonoBehaviour
{
    public GameObject m_Exprosion;

    private float m_VeloY;
    // Use this for initialization
    void Start()
    {
        m_VeloY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_VeloY += 20.0f * Time.deltaTime;

        m_VeloY = Mathf.Clamp(m_VeloY, 0.0f, 40.0f);

        transform.position -= new Vector3(0.0f, m_VeloY, 0.0f) * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        Instantiate(m_Exprosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

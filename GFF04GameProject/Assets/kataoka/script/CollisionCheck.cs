using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private bool m_Collision;
    // Use this for initialization
    void Start()
    {
        m_Collision = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TowerCollision" || other.tag == "Tower" || other.tag == "GroundCollisionRigid")
        {
            m_Collision = true;
        }
    }
    public bool GetFlag()
    {
        return m_Collision;
    }
}

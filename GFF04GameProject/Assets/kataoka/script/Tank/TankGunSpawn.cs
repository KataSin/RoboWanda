using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGunSpawn : MonoBehaviour
{
    public GameObject m_Bullet;
    public GameObject m_Exprosion;
    // Use this for initialization
    public void SpawnBullet()
    {
        //弾
        Instantiate(m_Bullet, transform.position, transform.rotation);
        //爆発
        Instantiate(m_Exprosion, transform.position, transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGunSpawn1 : MonoBehaviour
{
    public GameObject m_Bullet;

    // Use this for initialization
    public void SpawnBullet()
    {
        //弾
        Instantiate(m_Bullet, transform.position, transform.rotation);
    }
}

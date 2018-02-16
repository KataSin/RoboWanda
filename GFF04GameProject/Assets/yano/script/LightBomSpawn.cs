using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject lightBom_;

    private float t;
    private bool isClear;

    // Use this for initialization
    void Start()
    {
        t = 0f;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        if (!isClear)
        {
            GameObject lBom = Instantiate(lightBom_, new Vector3(-75.3f, 45.14f, 103f), Quaternion.identity);
            lBom.GetComponent<LightBullet>().m_IsExprosion = true;
            isClear = true;
        }

        t += 1.0f * Time.deltaTime;
    }
}

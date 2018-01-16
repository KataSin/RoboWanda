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
        if (t >= 4f)
        {
            if (!isClear)
            {
                GameObject lBom = Instantiate(lightBom_, new Vector3(-70.3f, 40.14f, 103f), Quaternion.identity);
                lBom.GetComponent<LightBullet>().m_IsExprosion = true;
                isClear = true;
            }
        }

        t += 1.0f * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamCollide : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // 出現後0.5秒、消滅
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBreakWall : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GroundCollisionRigid"
            || other.name == "DestroyCollision"
            || other.tag == "DebrisCollision"
            || other.tag == "SandSmoke") return;

        Destroy(other.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class testScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform trans = transform;
        Debug.Log(trans.position);
    }

    public float Vector2Cross(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.z - rhs.x * lhs.z;
    }
}

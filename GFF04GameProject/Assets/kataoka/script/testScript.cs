using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class testScript : MonoBehaviour
{
    public GameObject right;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec=transform.position-right.transform.position;
        float dot = Vector2Cross(right.transform.right.normalized, vec.normalized);
        Debug.Log(dot);
    }

    public float Vector2Cross(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.z - rhs.x * lhs.z;
    }
}

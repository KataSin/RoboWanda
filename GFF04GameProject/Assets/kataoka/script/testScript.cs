using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class testScript : MonoBehaviour
{
    public testPlayer test;
    private float mTime;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
    }

    public float Vector2Cross(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.z - rhs.x * lhs.z;
    }
}
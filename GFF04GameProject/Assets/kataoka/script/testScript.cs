using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class testScript : MonoBehaviour
{

    private float mTime;
    // Use this for initialization
    void Start()
    {
        mTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(10, 0, 0) * Time.deltaTime;

        mTime += Time.deltaTime;
        Debug.Log(mTime);
    }

    public float Vector2Cross(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.z - rhs.x * lhs.z;
    }
}
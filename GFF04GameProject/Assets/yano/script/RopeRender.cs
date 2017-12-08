using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRender : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ropes_parts_;

    private LineRenderer rope_;

    // Use this for initialization
    void Start()
    {
        rope_ = GetComponent<LineRenderer>();
        rope_.positionCount = ropes_parts_.Count;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ropes_parts_.Count; i++)
            rope_.SetPosition(i, ropes_parts_[i].transform.position);
        //rope_.SetPosition(1, ropes_parts_[1].transform.position);
        //rope_.SetPosition(2, ropes_parts_[2].transform.position);
        //rope_.SetPosition(3, ropes_parts_[3].transform.position);
        //rope_.SetPosition(0, ropes_parts_[0].transform.position);
        //rope_.SetPosition(1, ropes_parts_[1].transform.position);
    }
}

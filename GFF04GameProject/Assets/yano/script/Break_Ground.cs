using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Ground : MonoBehaviour
{
    [SerializeField]
    [Header("地面の当たり判定")]
    private GameObject ground_collide_;

    private bool isBreak;

    // Use this for initialization
    void Start()
    {
        isBreak = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isBreak = true;
            Destroy(ground_collide_);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bom")
        {
            isBreak = true;
            Destroy(ground_collide_);
            Destroy(this.gameObject);
        }
    }

    public bool Get_BreakFlag()
    {
        return isBreak;
    }
}

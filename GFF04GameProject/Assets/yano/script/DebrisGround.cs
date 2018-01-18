using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisGround : MonoBehaviour
{
    [SerializeField]
    private bool isGround;

    [SerializeField]
    private float destroyTime;

    [SerializeField]
    private float intervalTime = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (destroyTime < 0f)
            return;

        if (isGround)
        {
            destroyTime -= 1f * Time.deltaTime;

            intervalTime -= 1f * Time.deltaTime;
        }
    }

    //地面についたかどうか
    public bool Hit_Ground()
    {
        return isGround;
    }

    //消滅時間取得
    public float Get_Destroyime()
    {
        return destroyTime;
    }

    //点滅のインターバル取得
    public float Get_Interval()
    {
        return intervalTime;
    }

    //点滅のインターバル設定
    public void Set_Interval(float interval)
    {
        intervalTime = interval;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
            isGround = true;

        GetComponent<MeshCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UnderBrie")
            GetComponent<MeshRenderer>().enabled = false;
    }
}

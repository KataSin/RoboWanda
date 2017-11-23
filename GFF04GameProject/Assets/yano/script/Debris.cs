using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField]
    [Header("崩壊後残す破片の個数")]
    private int m_remainDebris;

    [SerializeField]
    [Header("破片のオブジェクト")]
    private List<GameObject> debris_;

    private Rigidbody rigids_;
    private Vector3 clampVelocities;


    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        //破片の破壊制御(点滅も)
        destroyDebris(m_remainDebris);
    }

    //破片の破壊制御(点滅も)
    private void destroyDebris(int remainDebrisCnt)
    {
        //破片がremainDebrisCnt個以上の時
        if (debris_.Count >= remainDebrisCnt)
        {
            for (int i = 0; i < debris_.Count; i++)
            {
                if (debris_[i].GetComponent<DebrisGround>().Hit_Ground())
                {
                    if (debris_[i].GetComponent<DebrisGround>().Get_Interval() < 0f)
                    {
                        //点滅(アクティブ制御)
                        debris_[i].GetComponent<Renderer>().enabled = !debris_[i].GetComponent<Renderer>().enabled;
                        
                        //点滅インターバル再設定
                        debris_[i].GetComponent<DebrisGround>().Set_Interval(0.05f);
                    }

                    //消滅時間が0以下になったら
                    else if (debris_[i].GetComponent<DebrisGround>().Get_Destroyime() <= 0f)
                    {
                        //破壊
                        Destroy(debris_[i]);

                        //i番目の要素削除
                        debris_.RemoveAt(i);
                    }
                }
            }
        }

        //破片がremainDebrisCntより下回って
        else if (debris_.Count < remainDebrisCnt)
        {
            for (int i = 0; i < debris_.Count; i++)
            {
                //Rendererが非アクティブになっていたら
                if (debris_[i].GetComponent<Renderer>().enabled == false)
                {
                    //破壊
                    Destroy(debris_[i]);

                    //i番目の要素削除
                    debris_.RemoveAt(i);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < debris_.Count; i++)
        {
            rigids_ = debris_[i].GetComponent<Rigidbody>();
            clampVelocities = rigids_.velocity;
            clampVelocities.x = Mathf.Clamp(rigids_.velocity.x, -10f, 10f);
            clampVelocities.z = Mathf.Clamp(rigids_.velocity.z, -10f, 10f);
            clampVelocities.y = Mathf.Clamp(rigids_.velocity.y, rigids_.velocity.y, 10f);
            rigids_.velocity = clampVelocities;
        }
    }

}

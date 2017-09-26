using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSmoke : MonoBehaviour
{
    //砂煙
    [SerializeField]
    private List<ParticleSystem> sand_smokes_;

    //倒壊経過時間
    private float m_break_time;

    // Use this for initialization
    void Start()
    {
        //砂煙を停止
        for (int i = 0; i < sand_smokes_.Count; ++i)
            sand_smokes_[i].Stop();       
    }

    // Update is called once per frame
    void Update()
    {
        m_break_time += 1.0f * Time.deltaTime;

        if (m_break_time < 8f)
        {
            for (int i = 0; i < sand_smokes_.Count; ++i)
                sand_smokes_[i].Play();
        }

        else if (m_break_time >= 8f)
        {
            for (int i = 0; i < sand_smokes_.Count; ++i)
                sand_smokes_[i].Stop();
        }
    }
}

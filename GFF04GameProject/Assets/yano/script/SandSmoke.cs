using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSmoke : MonoBehaviour
{
    //砂煙
    [SerializeField]
    private List<ParticleSystem> sand_smokes_;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        //砂煙を停止
        for (int i = 0; i < sand_smokes_.Count; ++i)
            sand_smokes_[i].Stop();

        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClear)
        {
            for (int i = 0; i < sand_smokes_.Count; ++i)
                sand_smokes_[i].Play();

            isClear = true;
        }

        else if (isClear
            && (!sand_smokes_[0].isPlaying && !sand_smokes_[1].isPlaying))
        {
            Destroy(this.gameObject);
        }
    }
}

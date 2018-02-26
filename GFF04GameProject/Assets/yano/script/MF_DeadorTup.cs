using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MF_DeadorTup : MonoBehaviour
{
    [SerializeField]
    private GameObject base_;

    [SerializeField]
    private Sprite dead_sprite_;

    [SerializeField]
    private Sprite timeUp_sprite_;

    public void YouAreDead()
    {
        base_.GetComponent<Image>().sprite = dead_sprite_;
    }

    public void TimeUp()
    {
        base_.GetComponent<Image>().sprite = timeUp_sprite_;
    }
}

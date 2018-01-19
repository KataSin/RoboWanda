using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMKCnt : MonoBehaviour
{
    [SerializeField]
    private GameObject main_model_;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Instantiate(float l_t)
    {
        main_model_.GetComponent<mkSBill>().Instantiate(l_t);
    }

    public void BeforeBreakColor()
    {
        main_model_.GetComponent<mkSBill>().BeforeBreakColor();
    }
}

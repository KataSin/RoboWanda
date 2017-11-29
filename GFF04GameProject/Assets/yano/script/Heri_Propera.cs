using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heri_Propera : MonoBehaviour
{
    [SerializeField]
    [Header("メインプロペラ")]
    private GameObject mainPropera_;

    [SerializeField]
    [Header("サブプロペラ")]
    private GameObject subPropera_;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mainPropera_.transform.Rotate(Vector3.forward * 15f);

        subPropera_.transform.Rotate(Vector3.right * 20f);
    }
}

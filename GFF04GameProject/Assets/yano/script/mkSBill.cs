using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mkSBill : MonoBehaviour
{
    private float t;

    [SerializeField]
    private int m_materialSize;

    // Use this for initialization
    void Start()
    {
        //for (int i = 0; i < m_materialSize; i++)
        //{
        //    GetComponent<Renderer>().materials[i].SetColor("_EmissionColor", new Color(0f, 0f, 0f));
        //    GetComponent<Renderer>().materials[i].SetFloat("_XRayInside", 0f);
        //    GetComponent<Renderer>().materials[i].SetFloat("_XRayRimSize", 0f);
        //}
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (t < 1f)
        //{
        //    for (int i = 0; i < m_materialSize; i++)
        //    {
        //        GetComponent<Renderer>().materials[i].SetColor(
        //        "_EmissionColor", Color.Lerp(new Color(0f, 0f, 0f), new Color(0.12f, 0.35f, 0f), t / 1f)
        //        );
        //        GetComponent<Renderer>().materials[i].SetFloat(
        //           "_XRayInside", Mathf.Lerp(0f, 1f, t / 1f)
        //           );
        //        GetComponent<Renderer>().materials[i].SetFloat(
        //          "_XRayRimSize", Mathf.Lerp(0f, 0.5f, t / 1f)
        //          );
        //    }
        //}
    }

    public void Instantiate(float l_t)
    {
        t = l_t - 1f;
    }

    public void BeforeBreakColor()
    {
        for (int i = 0; i < m_materialSize; i++)
            GetComponent<Renderer>().materials[i].SetColor("_EmissionColor", new Color(0.35f, 0f, 0f));
    }
}

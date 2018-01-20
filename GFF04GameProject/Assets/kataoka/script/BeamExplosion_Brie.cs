using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamExplosion_Brie : MonoBehaviour
{
    //爆発あたり判定の時間
    private float t;

    private Vector3 m_origin_scale;

    // Use this for initialization
    void Start()
    {
        m_origin_scale = Vector3.zero;

        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(m_origin_scale, new Vector3(30f, 30f, 30f), t / 2f);

        if (t <= 1f)
        {
            GetComponent<Renderer>().materials[0].SetColor(
                "_Color", Color.Lerp(new Color(1f, 0f, 0f, 0f), new Color(1f, 0f, 0f, 1f), t / 1f));

            GetComponent<Renderer>().materials[1].SetColor(
                "_EmissionColor", Color.Lerp(new Color(0f, 0f, 0f), new Color(0.6f, 0f, 0f), t / 1f));

            GetComponent<Renderer>().materials[1].SetFloat(
                "_XRayInside", Mathf.Lerp(0f, 0.15f, t / 1f));

            GetComponent<Renderer>().materials[1].SetFloat(
                "_XRayRimSize", Mathf.Lerp(0f, 0.2f, t / 1f));
        }

        if (t >= 1f)
        {
            GetComponent<Renderer>().materials[0].SetColor(
                "_Color", Color.Lerp(new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 0f), t - 1f / 1f));

            GetComponent<Renderer>().materials[1].SetColor(
                "_EmissionColor", Color.Lerp(new Color(0.6f, 0f, 0f), new Color(0f, 0f, 0f), t - 1f / 1f));

            GetComponent<Renderer>().materials[1].SetFloat(
               "_XRayInside", Mathf.Lerp(0.15f, 0f, t - 1f / 1f));

            GetComponent<Renderer>().materials[1].SetFloat(
                "_XRayRimSize", Mathf.Lerp(0.2f, 0f, t - 1f / 1f));
        }

        if (t >= 2f)
            Destroy(gameObject);

        t += 2.0f * Time.deltaTime;
    }
}

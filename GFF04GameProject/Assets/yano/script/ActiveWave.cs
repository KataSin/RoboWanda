using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveWave : MonoBehaviour
{
    private RectTransform m_rect;

    private float t;

    // Use this for initialization
    void Start()
    {
        m_rect = GetComponent<RectTransform>();
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_rect.localScale =
            Vector3.Lerp(Vector3.zero, new Vector3(0.6f, 0.6f, 1f), t / 1f);

        GetComponent<Image>().color = Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0f), t / 1f);

        if (t >= 1f)
            Destroy(this.gameObject);

        t += 1.0f * Time.deltaTime;
    }
}

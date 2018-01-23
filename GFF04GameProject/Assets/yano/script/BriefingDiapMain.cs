using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingDiapMain : MonoBehaviour
{
    [SerializeField]
    private GameObject disp_black_;

    private RectTransform rect_;

    private float t0, t1;

    // Use this for initialization
    void Start()
    {
        rect_ = GetComponent<RectTransform>();
        disp_black_.SetActive(true);
        t0 = 0f;
        t1 = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayOn()
    {
        if (t0 <= 1f)
            rect_.localScale =
                Vector3.Lerp(new Vector3(0.05f, 0f, 1f), new Vector3(0.05f, 1f, 1f), t0 / 1f);

        if (t0 >= 1f)
        {
            rect_.localScale =
                Vector3.Lerp(new Vector3(0.05f, 1f, 1f), new Vector3(1f, 1f, 1f), t1 / 1f);

            if (t1 >= 1f)
            {
                disp_black_.SetActive(false);
                gameObject.SetActive(false);
            }

            t1 += 10.0f * Time.deltaTime;
        }

        t0 += 8.0f * Time.deltaTime;
    }
}

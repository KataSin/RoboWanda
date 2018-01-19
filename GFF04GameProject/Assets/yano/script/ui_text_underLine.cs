using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_text_underLine : MonoBehaviour
{
    private RectTransform rect_;

    private float t;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        rect_ = GetComponent<RectTransform>();
        rect_.localScale = new Vector3(0f, 0.2f, 1f);
        t = 0f;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        rect_.localScale = Vector3.Lerp(new Vector3(0f, 0.2f, 1f), new Vector3(0.45f, 0.2f, 1f), t / 1f);
        LineUpdate();
    }

    public void LineUpdate()
    {
        t += 8.0f * Time.deltaTime;

        if (t >= 1f) isClear = true;
    }

    public bool Get_Clear()
    {
        return isClear;
    }
}

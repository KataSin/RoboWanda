using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAreaBriefing : MonoBehaviour
{
    private float t;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        t = 0f;
        isClear = false;

        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp01(t);

        transform.localScale =
            Vector3.Lerp(Vector3.zero, new Vector3(2.4f, 1.5f, 1f), t / 1f);
    }

    public void OpenArea()
    {
        if (!isClear)
        {
            t += 2.0f * Time.deltaTime;

            if (t >= 1f)
                isClear = true;
        }
    }

    public void CloseArea()
    {
        t -= 2.0f * Time.deltaTime;
    }
}

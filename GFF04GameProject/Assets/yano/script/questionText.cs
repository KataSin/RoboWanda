using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questionText : MonoBehaviour
{
    private enum Q_Text
    {
        Question,
        Sally,
    }

    [SerializeField]
    private Q_Text textState_;

    [SerializeField]
    private GameObject text_obj_;
    private Text text_;

    // Use this for initialization
    void Start()
    {
        textState_ = Q_Text.Question;

        text_ = text_obj_.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (textState_)
        {
            case Q_Text.Question:
                text_.text = "作戦地点到着までに少し時間が掛かる\n訓練の予習をしておくか？";
                break;

            case Q_Text.Sally:
                text_obj_.GetComponent<RectTransform>().localPosition = new Vector3(-157.6f, -6.4f, 0f);
                text_.text = "よし、それでは作戦地点まで移動する";
                break;
        }
    }

    public void SetState(int state)
    {
        textState_ = (Q_Text)state;
    }
}

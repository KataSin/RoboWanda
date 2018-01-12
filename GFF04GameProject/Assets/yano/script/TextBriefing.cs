using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBriefing : MonoBehaviour
{
    private enum TextState
    {
        None,
        P0,
        P1,
        P2,
        P3,
        P4,
        P5,
        P6,
        P7,
        P8,
        P9,
    }

    private TextState m_textState;

    private Text text_;

    private RectTransform rect_;

    private char[] m_text_char;

    private float m_interval;

    private int m_charNum;

    [SerializeField]
    private bool isClear;

    // Use this for initialization
    void Start()
    {
        m_textState = TextState.None;
        text_ = GetComponent<Text>();
        rect_ = GetComponent<RectTransform>();

        rect_.localPosition = new Vector3(-127f, -228f, 0f);

        m_text_char = new char[43];
        p0Char_Init();

        m_interval = 1f;
        m_charNum = 0;

        isClear = false;
    }

    private void p0Char_Init()
    {
        m_text_char[0] = '任';
        m_text_char[1] = '務';
        m_text_char[2] = '内';
        m_text_char[3] = '容';
        m_text_char[4] = 'を';
        m_text_char[5] = '確';
        m_text_char[6] = '認';
        m_text_char[7] = 'す';
        m_text_char[8] = 'る';
        m_text_char[9] = '。';
    }

    private void p1Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '本';
        m_text_char[1] = '任';
        m_text_char[2] = '務';
        m_text_char[3] = 'の';
        m_text_char[4] = '目';
        m_text_char[5] = '的';
        m_text_char[6] = 'は';
        m_text_char[7] = '暴';
        m_text_char[8] = '走';
        m_text_char[9] = 'し';
        m_text_char[10] = 'た';
        m_text_char[11] = '機';
        m_text_char[12] = '械';
        m_text_char[13] = '兵';
        m_text_char[14] = '器';
        m_text_char[15] = 'の';
        m_text_char[16] = '撃';
        m_text_char[17] = '破';
        m_text_char[18] = 'だ';
        m_text_char[19] = '。';

        isClear = true;
    }

    private void p2Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '作';
        m_text_char[1] = '戦';
        m_text_char[2] = 'を';
        m_text_char[3] = '三';
        m_text_char[4] = '段';
        m_text_char[5] = '階';
        m_text_char[6] = 'に';
        m_text_char[7] = '分';
        m_text_char[8] = 'け';
        m_text_char[9] = 'て';
        m_text_char[10] = '実';
        m_text_char[11] = '行';
        m_text_char[12] = 'し';
        m_text_char[13] = 'て';
        m_text_char[14] = 'も';
        m_text_char[15] = 'ら';
        m_text_char[16] = 'う';
        m_text_char[17] = '。';

        isClear = true;
    }

    private void p3Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '第';
        m_text_char[1] = '一';
        m_text_char[2] = '段';
        m_text_char[3] = '階';
        m_text_char[4] = 'は';
        m_text_char[5] = '任';
        m_text_char[6] = '務';
        m_text_char[7] = '開';
        m_text_char[8] = '始';
        m_text_char[9] = 'か';
        m_text_char[10] = 'ら';
        m_text_char[11] = '9';
        m_text_char[12] = '0';
        m_text_char[13] = '秒';
        m_text_char[14] = '。';
        m_text_char[15] = '爆';
        m_text_char[16] = '撃';
        m_text_char[17] = '機';
        m_text_char[18] = 'を';
        m_text_char[19] = '用';
        m_text_char[20] = 'い';
        m_text_char[21] = 'た';
        m_text_char[22] = '空';
        m_text_char[23] = '爆';
        m_text_char[24] = 'だ';
        m_text_char[25] = '。';

        isClear = true;
    }

    private void p4Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'ヘ';
        m_text_char[1] = 'リ';
        m_text_char[2] = '隊';
        m_text_char[3] = '、';
        m_text_char[4] = '地';
        m_text_char[5] = '上';
        m_text_char[6] = '兵';
        m_text_char[7] = 'は';
        m_text_char[8] = '爆';
        m_text_char[9] = '撃';
        m_text_char[10] = 'に';
        m_text_char[11] = '備';
        m_text_char[12] = 'え';
        m_text_char[13] = 'て';
        m_text_char[14] = '対';
        m_text_char[15] = '象';
        m_text_char[16] = 'か';
        m_text_char[17] = 'ら';
        m_text_char[18] = '離';
        m_text_char[19] = 'れ';
        m_text_char[20] = 'て';
        m_text_char[21] = 'お';
        m_text_char[22] = 'け';
        m_text_char[23] = '。';

        isClear = true;
    }

    private void p5Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '第';
        m_text_char[1] = '二';
        m_text_char[2] = '段';
        m_text_char[3] = '階';
        m_text_char[4] = 'は';
        m_text_char[5] = '任';
        m_text_char[6] = '務';
        m_text_char[7] = '開';
        m_text_char[8] = '始';
        m_text_char[9] = 'か';
        m_text_char[10] = 'ら';
        m_text_char[11] = '1';
        m_text_char[12] = '8';
        m_text_char[13] = '0';
        m_text_char[14] = '秒';
        m_text_char[15] = '。';
        m_text_char[16] = '戦';
        m_text_char[17] = '車';
        m_text_char[18] = '部';
        m_text_char[19] = '隊';
        m_text_char[20] = '降';
        m_text_char[21] = '下';
        m_text_char[22] = 'の';
        m_text_char[23] = '支';
        m_text_char[24] = '援';
        m_text_char[25] = 'だ';
        m_text_char[26] = '。';

        isClear = true;
    }

    private void p6Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'マ';
        m_text_char[1] = 'ッ';
        m_text_char[2] = 'プ';
        m_text_char[3] = '上';
        m_text_char[4] = 'に';
        m_text_char[5] = '示';
        m_text_char[6] = 'し';
        m_text_char[7] = 'た';
        m_text_char[8] = '区';
        m_text_char[9] = '画';
        m_text_char[10] = 'の';
        m_text_char[11] = 'ビ';
        m_text_char[12] = 'ル';
        m_text_char[13] = 'の';
        m_text_char[14] = '破';
        m_text_char[15] = '壊';
        m_text_char[16] = '、';
        m_text_char[17] = '対';
        m_text_char[18] = '象';
        m_text_char[19] = 'の';
        m_text_char[20] = '視';
        m_text_char[21] = '界';
        m_text_char[22] = 'を';
        m_text_char[23] = '奪';
        m_text_char[24] = 'う';
        m_text_char[25] = 'ス';
        m_text_char[26] = 'モ';
        m_text_char[27] = 'ー';
        m_text_char[28] = 'ク';
        m_text_char[29] = '弾';
        m_text_char[30] = 'を';
        m_text_char[31] = '利';
        m_text_char[32] = '用';
        m_text_char[33] = 'し';
        m_text_char[34] = '安';
        m_text_char[35] = '全';
        m_text_char[36] = 'に';
        m_text_char[37] = '降';
        m_text_char[38] = '下';
        m_text_char[39] = 'さ';
        m_text_char[40] = 'せ';
        m_text_char[41] = 'ろ';
        m_text_char[42] = '。';

        isClear = true;
    }

    private void p7Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '第';
        m_text_char[1] = '三';
        m_text_char[2] = '段';
        m_text_char[3] = '階';
        m_text_char[4] = 'は';
        m_text_char[5] = '降';
        m_text_char[6] = '下';
        m_text_char[7] = 'し';
        m_text_char[8] = 'た';
        m_text_char[9] = '戦';
        m_text_char[10] = '車';
        m_text_char[11] = '部';
        m_text_char[12] = '隊';
        m_text_char[13] = 'の';
        m_text_char[14] = '指';
        m_text_char[15] = '揮';
        m_text_char[16] = 'だ';
        m_text_char[17] = '。';
       
        isClear = true;
    }

    private void p8Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '戦';
        m_text_char[1] = '車';
        m_text_char[2] = '部';
        m_text_char[3] = '隊';
        m_text_char[4] = 'は';
        m_text_char[5] = '照';
        m_text_char[6] = '明';
        m_text_char[7] = '弾';
        m_text_char[8] = 'が';
        m_text_char[9] = '照';
        m_text_char[10] = 'っ';
        m_text_char[11] = 'て';
        m_text_char[12] = 'い';
        m_text_char[13] = 'る';
        m_text_char[14] = '間';
        m_text_char[15] = '攻';
        m_text_char[16] = '撃';
        m_text_char[17] = 'を';
        m_text_char[18] = '行';
        m_text_char[19] = 'う';
        m_text_char[20] = '。';
        m_text_char[21] = '巻';
        m_text_char[22] = 'き';
        m_text_char[23] = '込';
        m_text_char[24] = 'ま';
        m_text_char[25] = 'れ';
        m_text_char[26] = 'な';
        m_text_char[27] = 'い';
        m_text_char[28] = 'よ';
        m_text_char[29] = 'う';
        m_text_char[30] = '注';
        m_text_char[31] = '意';
        m_text_char[32] = 'し';
        m_text_char[33] = 'ろ';
        m_text_char[34] = '。';

        isClear = true;
    }

    private void p9Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '任';
        m_text_char[1] = '務';
        m_text_char[2] = '内';
        m_text_char[3] = '容';
        m_text_char[4] = 'の';
        m_text_char[5] = '確';
        m_text_char[6] = '認';
        m_text_char[7] = 'は';
        m_text_char[8] = '以';
        m_text_char[9] = '上';
        m_text_char[10] = 'だ';
        m_text_char[11] = '。';
        m_text_char[12] = '健';
        m_text_char[13] = '闘';
        m_text_char[14] = 'を';
        m_text_char[15] = '祈';
        m_text_char[16] = 'る';
        m_text_char[17] = '。';

        isClear = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_textState)
        {
            case TextState.P0:
                P0TextUpdate();
                break;
            case TextState.P1:
                rect_.localPosition = new Vector3(-265f, -228f, 0f);
                p1Char_Init();
                P1TextUpdate();
                break;
            case TextState.P2:
                rect_.localPosition = new Vector3(-228f, -228f, 0f);
                p2Char_Init();
                P2TextUpdate();
                break;
            case TextState.P3:
                rect_.localPosition = new Vector3(-335f, -228f, 0f);
                p3Char_Init();
                P3TextUpdate();
                break;
            case TextState.P4:
                rect_.localPosition = new Vector3(-336f, -228f, 0f);
                p4Char_Init();
                P4TextUpdate();
                break;
            case TextState.P5:
                rect_.localPosition = new Vector3(-360f, -228f, 0f);
                p5Char_Init();
                P5TextUpdate();
                break;
            case TextState.P6:
                rect_.localPosition = new Vector3(-349f, -228f, 0f);
                p6Char_Init();
                P6TextUpdate();
                break;
            case TextState.P7:
                rect_.localPosition = new Vector3(-228f, -228f, 0f);
                p7Char_Init();
                P7TextUpdate();
                break;
            case TextState.P8:
                rect_.localPosition = new Vector3(-268f, -228f, 0f);
                p8Char_Init();
                P8TextUpdate();
                break;
            case TextState.P9:
                rect_.localPosition = new Vector3(-228f, -228f, 0f);
                p9Char_Init();
                P9TextUpdate();
                break;

            default:
                break;
        }
    }

    private void P0TextUpdate()
    {
        if (m_charNum <= 9)
        {
            if (m_interval <= 0f)
            {
                text_.text += m_text_char[m_charNum];
                m_charNum++;
                m_interval = 1f;
            }
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P1TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 19)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P2TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 17)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P3TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 25)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P4TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 23)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P5TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 26)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P6TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 42)
        {           
            text_.text += m_text_char[m_charNum];
            if (m_charNum == 16)
                text_.text += System.Environment.NewLine;
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P7TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 17)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P8TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 34)
        {
            text_.text += m_text_char[m_charNum];
            if (m_charNum == 20)
                text_.text += System.Environment.NewLine;
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P9TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 17)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    public void Set_State(int l_state)
    {
        m_textState = (TextState)l_state;
    }

    public void TextReset()
    {
        text_.text = "";
        m_charNum = 0;
        m_interval = 1f;
        isClear = false;
    }
}

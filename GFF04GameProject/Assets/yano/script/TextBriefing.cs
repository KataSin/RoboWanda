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
        P10,
        P11,
        P12,
        P13,
    }

    private TextState m_textState;

    private Text text_;

    private RectTransform rect_;

    private char[] m_text_char;

    private float m_interval;

    private int m_charNum;

    [SerializeField]
    private bool isClear;

    [SerializeField]
    private bool isText;

    // Use this for initialization
    void Start()
    {
        m_textState = TextState.None;
        text_ = GetComponent<Text>();
        rect_ = GetComponent<RectTransform>();

        rect_.localPosition = new Vector3(-127f, -245f, 0f);

        m_text_char = new char[43];
        p0Char_Init();

        m_interval = 1f;
        m_charNum = 0;

        isClear = false;
        isText = false;
    }

    private void p0Char_Init()
    {
        m_text_char[0] = '任';
        m_text_char[1] = '務';
        m_text_char[2] = '内';
        m_text_char[3] = '容';
        m_text_char[4] = 'の';
        m_text_char[5] = '通';
        m_text_char[6] = '達';
        m_text_char[7] = 'を';
        m_text_char[8] = '行';
        m_text_char[9] = 'う';
        m_text_char[10] = '。';
    }

    private void p1Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '新';
        m_text_char[1] = '型';
        m_text_char[2] = '巨';
        m_text_char[3] = '大';
        m_text_char[4] = '兵';
        m_text_char[5] = '器';
        m_text_char[6] = 'の';
        m_text_char[7] = '暴';
        m_text_char[8] = '走';
        m_text_char[9] = 'を';
        m_text_char[10] = '想';
        m_text_char[11] = '定';
        m_text_char[12] = 'し';
        m_text_char[13] = 'た';
        m_text_char[14] = '戦';
        m_text_char[15] = '闘';
        m_text_char[16] = 'を';
        m_text_char[17] = '行';
        m_text_char[18] = 'い';
        m_text_char[19] = '、';
        m_text_char[20] = '対';
        m_text_char[21] = '象';
        m_text_char[22] = 'を';
        m_text_char[23] = '撃';
        m_text_char[24] = '破';
        m_text_char[25] = 'す';
        m_text_char[26] = 'る';
        m_text_char[27] = 'こ';
        m_text_char[28] = 'と';
        m_text_char[29] = 'が';
        m_text_char[30] = '目';
        m_text_char[31] = '的';
        m_text_char[32] = 'と';
        m_text_char[33] = 'な';
        m_text_char[34] = 'る';
        m_text_char[35] = '。';

        isClear = true;
    }

    private void p2Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '断';
        m_text_char[1] = '続';
        m_text_char[2] = '照';
        m_text_char[3] = '射';
        m_text_char[4] = 'ビ';
        m_text_char[5] = 'ー';
        m_text_char[6] = 'ム';
        m_text_char[7] = 'に';
        m_text_char[8] = 'よ';
        m_text_char[9] = 'る';
        m_text_char[10] = '広';
        m_text_char[11] = '範';
        m_text_char[12] = '囲';
        m_text_char[13] = '攻';
        m_text_char[14] = '撃';
        m_text_char[15] = 'と';

        isClear = true;
    }

    private void p3Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '高';
        m_text_char[1] = '精';
        m_text_char[2] = '度';
        m_text_char[3] = '誘';
        m_text_char[4] = '導';
        m_text_char[5] = 'ミ';
        m_text_char[6] = 'サ';
        m_text_char[7] = 'イ';
        m_text_char[8] = 'ル';
        m_text_char[9] = 'を';
        m_text_char[10] = '使';
        m_text_char[11] = '用';
        m_text_char[12] = 'し';
        m_text_char[13] = 'た';
        m_text_char[14] = 'ピ';
        m_text_char[15] = 'ン';
        m_text_char[16] = 'ポ';
        m_text_char[17] = 'イ';
        m_text_char[18] = 'ン';
        m_text_char[19] = 'ト';
        m_text_char[20] = '攻';
        m_text_char[21] = '撃';
        m_text_char[22] = 'に';
        m_text_char[23] = '注';
        m_text_char[24] = '意';
        m_text_char[25] = 'が';
        m_text_char[26] = '必';
        m_text_char[27] = '要';
        m_text_char[28] = 'だ';
        m_text_char[29] = '。';

        isClear = true;
    }

    private void p4Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '本';
        m_text_char[1] = '任';
        m_text_char[2] = '務';
        m_text_char[3] = 'で';
        m_text_char[4] = 'は';
        m_text_char[5] = '作';
        m_text_char[6] = '戦';
        m_text_char[7] = 'を';
        m_text_char[8] = '三';
        m_text_char[9] = '段';
        m_text_char[10] = '階';
        m_text_char[11] = 'に';
        m_text_char[12] = '分';
        m_text_char[13] = 'け';
        m_text_char[14] = 'て';
        m_text_char[15] = '実';
        m_text_char[16] = '行';
        m_text_char[17] = 'し';
        m_text_char[18] = 'て';
        m_text_char[19] = 'も';
        m_text_char[20] = 'ら';
        m_text_char[21] = 'う';
        m_text_char[22] = '。';

        isClear = true;
    }

    private void p5Char_Init()
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

    private void p6Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '地';
        m_text_char[1] = '上';
        m_text_char[2] = 'へ';
        m_text_char[3] = 'の';
        m_text_char[4] = '注';
        m_text_char[5] = '意';
        m_text_char[6] = 'を';
        m_text_char[7] = '引';
        m_text_char[8] = 'き';
        m_text_char[9] = 'つ';
        m_text_char[10] = 'つ';
        m_text_char[11] = '、';
        m_text_char[12] = '巻';
        m_text_char[13] = 'き';
        m_text_char[14] = '込';
        m_text_char[15] = 'ま';
        m_text_char[16] = 'れ';
        m_text_char[17] = 'な';
        m_text_char[18] = 'い';
        m_text_char[19] = 'よ';
        m_text_char[20] = 'う';
        m_text_char[21] = '対';
        m_text_char[22] = '象';
        m_text_char[23] = 'か';
        m_text_char[24] = 'ら';
        m_text_char[25] = '離';
        m_text_char[26] = 'れ';
        m_text_char[27] = 'ろ';
        m_text_char[28] = '。';

        isClear = true;
    }

    private void p7Char_Init()
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
        m_text_char[20] = 'の';
        m_text_char[21] = '進';
        m_text_char[22] = '入';
        m_text_char[23] = '支';
        m_text_char[24] = '援';
        m_text_char[25] = 'だ';
        m_text_char[26] = '。';

        isClear = true;
    }

    private void p8Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '作';
        m_text_char[1] = '戦';
        m_text_char[2] = '開';
        m_text_char[3] = '始';
        m_text_char[4] = 'と';
        m_text_char[5] = '同';
        m_text_char[6] = '時';
        m_text_char[7] = 'に';
        m_text_char[8] = '発';
        m_text_char[9] = '破';
        m_text_char[10] = 'す';
        m_text_char[11] = 'る';
        m_text_char[12] = '遠';
        m_text_char[13] = '隔';
        m_text_char[14] = '操';
        m_text_char[15] = '作';
        m_text_char[16] = '爆';
        m_text_char[17] = '弾';
        m_text_char[18] = 'を';
        m_text_char[19] = '用';
        m_text_char[20] = 'い';
        m_text_char[21] = 'て';
        m_text_char[22] = '区';
        m_text_char[23] = '画';
        m_text_char[24] = '外';
        m_text_char[25] = '周';
        m_text_char[26] = 'の';
        m_text_char[27] = '障';
        m_text_char[28] = '害';
        m_text_char[29] = '物';
        m_text_char[30] = 'を';
        m_text_char[31] = '破';
        m_text_char[32] = '壊';
        m_text_char[33] = 'し';

        isClear = true;
    }

    private void p9Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '作';
        m_text_char[1] = '戦';
        m_text_char[2] = '区';
        m_text_char[3] = '画';
        m_text_char[4] = '進';
        m_text_char[5] = '入';
        m_text_char[6] = '後';
        m_text_char[7] = 'は';
        m_text_char[8] = '、';
        m_text_char[9] = 'ス';
        m_text_char[10] = 'モ';
        m_text_char[11] = 'ー';
        m_text_char[12] = 'ク';
        m_text_char[13] = '弾';
        m_text_char[14] = 'を';
        m_text_char[15] = '使';
        m_text_char[16] = '用';
        m_text_char[17] = 'し';
        m_text_char[18] = '定';
        m_text_char[19] = '位';
        m_text_char[20] = '置';
        m_text_char[21] = 'に';
        m_text_char[22] = '着';
        m_text_char[23] = 'く';
        m_text_char[24] = 'ま';
        m_text_char[25] = 'で';
        m_text_char[26] = '対';
        m_text_char[27] = '象';
        m_text_char[28] = 'の';
        m_text_char[29] = '目';
        m_text_char[30] = 'を';
        m_text_char[31] = '逸';
        m_text_char[32] = 'ら';
        m_text_char[33] = 'す';
        m_text_char[34] = '。';

        isClear = true;
    }

    private void p10Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '第';
        m_text_char[1] = '三';
        m_text_char[2] = '段';
        m_text_char[3] = '階';
        m_text_char[4] = 'は';
        m_text_char[5] = '進';
        m_text_char[6] = '入';
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

    private void p11Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '照';
        m_text_char[1] = '明';
        m_text_char[2] = '弾';
        m_text_char[3] = 'を';
        m_text_char[4] = '合';
        m_text_char[5] = '図';
        m_text_char[6] = 'に';
        m_text_char[7] = '戦';
        m_text_char[8] = '車';
        m_text_char[9] = '部';
        m_text_char[10] = '隊';
        m_text_char[11] = 'が';
        m_text_char[12] = '攻';
        m_text_char[13] = '撃';
        m_text_char[14] = 'を';
        m_text_char[15] = '行';
        m_text_char[16] = 'う';
        m_text_char[17] = '。';

        isClear = true;
    }
    private void p12Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '以';
        m_text_char[1] = '上';
        m_text_char[2] = '、';
        m_text_char[3] = '三';
        m_text_char[4] = '段';
        m_text_char[5] = '階';
        m_text_char[6] = 'を';
        m_text_char[7] = '以';
        m_text_char[8] = 'て';
        m_text_char[9] = '本';
        m_text_char[10] = '任';
        m_text_char[11] = '務';
        m_text_char[12] = 'の';
        m_text_char[13] = '作';
        m_text_char[14] = '戦';
        m_text_char[15] = 'と';
        m_text_char[16] = 'な';
        m_text_char[17] = 'る';
        m_text_char[18] = '。';

        isClear = true;
    }

    private void p13Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '任';
        m_text_char[1] = '務';
        m_text_char[2] = '内';
        m_text_char[3] = '容';
        m_text_char[4] = 'の';
        m_text_char[5] = '通';
        m_text_char[6] = '達';
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
                rect_.localPosition = new Vector3(-268f, -245f, 0f);
                p1Char_Init();
                P1TextUpdate();
                break;
            case TextState.P2:
                rect_.localPosition = new Vector3(-222f, -245f, 0f);
                p2Char_Init();
                P2TextUpdate();
                break;
            case TextState.P3:
                rect_.localPosition = new Vector3(-415f, -245f, 0f);
                p3Char_Init();
                P3TextUpdate();
                break;
            case TextState.P4:
                rect_.localPosition = new Vector3(-323f, -245f, 0f);
                p4Char_Init();
                P4TextUpdate();
                break;
            case TextState.P5:
                rect_.localPosition = new Vector3(-358f, -245f, 0f);
                p5Char_Init();
                P5TextUpdate();
                break;
            case TextState.P6:
                rect_.localPosition = new Vector3(-386f, -245f, 0f);
                p6Char_Init();
                P6TextUpdate();
                break;
            case TextState.P7:
                rect_.localPosition = new Vector3(-337f, -245f, 0f);
                p7Char_Init();
                P7TextUpdate();
                break;
            case TextState.P8:
                rect_.localPosition = new Vector3(-476f, -245f, 0f);
                p8Char_Init();
                P8TextUpdate();
                break;
            case TextState.P9:
                rect_.localPosition = new Vector3(-476f, -245f, 0f);
                p9Char_Init();
                P9TextUpdate();
                break;
            case TextState.P10:
                rect_.localPosition = new Vector3(-252f, -245f, 0f);
                p10Char_Init();
                P10TextUpdate();
                break;
            case TextState.P11:
                rect_.localPosition = new Vector3(-252f, -245f, 0f);
                p11Char_Init();
                P11TextUpdate();
                break;
            case TextState.P12:
                rect_.localPosition = new Vector3(-252f, -245f, 0f);
                p12Char_Init();
                P12TextUpdate();
                break;
            case TextState.P13:
                rect_.localPosition = new Vector3(-252f, -245f, 0f);
                p13Char_Init();
                P13TextUpdate();
                break;

            default:
                break;
        }
    }

    private void P0TextUpdate()
    {
        if (m_charNum <= 10)
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
        if (m_charNum <= 35)
        {
            if (m_interval <= 0f)
            {
                text_.text += m_text_char[m_charNum];
                if (m_charNum == 19)
                    text_.text += System.Environment.NewLine;
                m_charNum++;
                m_interval = 1f;
            }
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P2TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 15)
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
            m_charNum <= 29)
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
            m_charNum <= 22)
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
            m_charNum <= 25)
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
            m_charNum <= 28)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P7TextUpdate()
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

    private void P8TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 33)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P9TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 34)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P10TextUpdate()
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

    private void P11TextUpdate()
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

    private void P12TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 18)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= 15.0f * Time.deltaTime;
    }

    private void P13TextUpdate()
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

    public bool Get_TextFlag()
    {
        return isText;
    }
}

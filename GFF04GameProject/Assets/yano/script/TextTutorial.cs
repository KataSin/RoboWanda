using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTutorial : MonoBehaviour
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
        P14,
        P15,
        P16,
        P17,
        P18,
        Other1,
        Other2,
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

    private float m_text_speed;

    // Use this for initialization
    void Start()
    {
        m_textState = TextState.None;
        text_ = GetComponent<Text>();
        rect_ = GetComponent<RectTransform>();

        rect_.localPosition = new Vector3(-78f, -290f, 0f);

        m_text_char = new char[43];
        p0Char_Init();

        m_interval = 1f;
        m_charNum = 0;
        m_text_speed = 20f;

        isClear = false;
        isText = false;
    }

    private void p0Char_Init()
    {
        m_text_char[0] = 'こ';
        m_text_char[1] = 'れ';
        m_text_char[2] = 'よ';
        m_text_char[3] = 'り';
        m_text_char[4] = '訓';
        m_text_char[5] = '練';
        m_text_char[6] = 'を';
        m_text_char[7] = '始';
        m_text_char[8] = 'め';
        m_text_char[9] = 'る';
        m_text_char[10] = '。';
    }

    private void p1Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'ま';
        m_text_char[1] = 'ず';
        m_text_char[2] = '始';
        m_text_char[3] = 'め';
        m_text_char[4] = 'に';
        m_text_char[5] = '歩';
        m_text_char[6] = '行';
        m_text_char[7] = '訓';
        m_text_char[8] = '練';
        m_text_char[9] = 'だ';
        m_text_char[10] = '。';

        isClear = true;
    }

    private void p2Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '目';
        m_text_char[1] = '的';
        m_text_char[2] = '地';
        m_text_char[3] = 'ま';
        m_text_char[4] = 'で';
        m_text_char[5] = '歩';
        m_text_char[6] = 'い';
        m_text_char[7] = 'て';
        m_text_char[8] = '移';
        m_text_char[9] = '動';
        m_text_char[10] = 'し';
        m_text_char[11] = 'ろ';
        m_text_char[12] = '。';

        isClear = true;
    }

    private void p3Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '次';
        m_text_char[1] = 'は';
        m_text_char[2] = '走';
        m_text_char[3] = 'っ';
        m_text_char[4] = 'て';
        m_text_char[5] = '目';
        m_text_char[6] = '的';
        m_text_char[7] = '地';
        m_text_char[8] = 'ま';
        m_text_char[9] = 'で';
        m_text_char[10] = '移';
        m_text_char[11] = '動';
        m_text_char[12] = 'し';
        m_text_char[13] = 'ろ';
        m_text_char[14] = '。';

        isClear = true;
    }

    private void p4Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'よ';
        m_text_char[1] = 'し';
        m_text_char[2] = '歩';
        m_text_char[3] = '行';
        m_text_char[4] = '訓';
        m_text_char[5] = '練';
        m_text_char[6] = 'は';
        m_text_char[7] = 'こ';
        m_text_char[8] = 'れ';
        m_text_char[9] = 'で';
        m_text_char[10] = '終';
        m_text_char[11] = '了';
        m_text_char[12] = 'だ';
        m_text_char[13] = '。';

        isClear = true;
    }

    private void p5Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '続';
        m_text_char[1] = 'い';
        m_text_char[2] = 'て';
        m_text_char[3] = '武';
        m_text_char[4] = '装';
        m_text_char[5] = '訓';
        m_text_char[6] = '練';
        m_text_char[7] = 'を';
        m_text_char[8] = '行';
        m_text_char[9] = 'う';
        m_text_char[10] = '。';

        isClear = true;
    }

    private void p6Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'ま';
        m_text_char[1] = 'ず';
        m_text_char[2] = 'は';
        m_text_char[3] = '炸';
        m_text_char[4] = '烈';
        m_text_char[5] = '弾';
        m_text_char[6] = 'の';
        m_text_char[7] = '訓';
        m_text_char[8] = '練';
        m_text_char[9] = 'だ';
        m_text_char[10] = '。';

        isClear = true;
    }

    private void p7Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'R';
        m_text_char[1] = 'B';
        m_text_char[2] = 'を';
        m_text_char[3] = '押';
        m_text_char[4] = 'し';
        m_text_char[5] = 'て';
        m_text_char[6] = 'い';
        m_text_char[7] = 'る';
        m_text_char[8] = '間';
        m_text_char[9] = '照';
        m_text_char[10] = '準';
        m_text_char[11] = 'し';
        m_text_char[12] = '、';
        m_text_char[13] = '照';
        m_text_char[14] = '準';
        m_text_char[15] = '時';
        m_text_char[16] = 'に';
        m_text_char[17] = 'L';
        m_text_char[18] = 'B';
        m_text_char[19] = 'を';
        m_text_char[20] = '押';
        m_text_char[21] = 'す';
        m_text_char[22] = 'こ';
        m_text_char[23] = 'と';
        m_text_char[24] = 'で';
        m_text_char[25] = '弾';
        m_text_char[26] = 'を';
        m_text_char[27] = '発';
        m_text_char[28] = '射';
        m_text_char[29] = 'で';
        m_text_char[30] = 'き';
        m_text_char[31] = 'る';
        m_text_char[32] = '。';

        isClear = true;
    }

    private void p8Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '炸';
        m_text_char[1] = '裂';
        m_text_char[2] = '弾';
        m_text_char[3] = 'は';
        m_text_char[4] = '建';
        m_text_char[5] = '物';
        m_text_char[6] = 'を';
        m_text_char[7] = '破';
        m_text_char[8] = '壊';
        m_text_char[9] = 'し';
        m_text_char[10] = 'た';
        m_text_char[11] = 'り';
        m_text_char[12] = '敵';
        m_text_char[13] = 'に';
        m_text_char[14] = 'ダ';
        m_text_char[15] = 'メ';
        m_text_char[16] = 'ー';
        m_text_char[17] = 'ジ';
        m_text_char[18] = 'を';
        m_text_char[19] = '与';
        m_text_char[20] = 'え';
        m_text_char[21] = 'る';
        m_text_char[22] = 'こ';
        m_text_char[23] = 'と';
        m_text_char[24] = 'が';
        m_text_char[25] = '出';
        m_text_char[26] = '来';
        m_text_char[27] = 'る';
        m_text_char[28] = '。';

        isClear = true;
    }

    private void p9Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '炸';
        m_text_char[1] = '裂';
        m_text_char[2] = '弾';
        m_text_char[3] = 'を';
        m_text_char[4] = '使';
        m_text_char[5] = 'っ';
        m_text_char[6] = 'て';
        m_text_char[7] = '建';
        m_text_char[8] = '物';
        m_text_char[9] = 'を';
        m_text_char[10] = '破';
        m_text_char[11] = '壊';
        m_text_char[12] = 'し';
        m_text_char[13] = 'ろ';
        m_text_char[14] = '。';

        isClear = true;
    }

    private void p10Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '次';
        m_text_char[1] = 'は';
        m_text_char[2] = 'ス';
        m_text_char[3] = 'モ';
        m_text_char[4] = 'ー';
        m_text_char[5] = 'ク';
        m_text_char[6] = '弾';
        m_text_char[7] = 'の';
        m_text_char[8] = '訓';
        m_text_char[9] = '練';
        m_text_char[10] = 'だ';
        m_text_char[11] = '。';

        isClear = true;
    }

    private void p11Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'ス';
        m_text_char[1] = 'モ';
        m_text_char[2] = 'ー';
        m_text_char[3] = 'ク';
        m_text_char[4] = '弾';
        m_text_char[5] = 'は';
        m_text_char[6] = '建';
        m_text_char[7] = '物';
        m_text_char[8] = 'に';
        m_text_char[9] = '着';
        m_text_char[10] = '弾';
        m_text_char[11] = 'さ';
        m_text_char[12] = 'せ';
        m_text_char[13] = 'る';
        m_text_char[14] = 'こ';
        m_text_char[15] = 'と';
        m_text_char[16] = 'で';
        m_text_char[17] = '起';
        m_text_char[18] = '爆';
        m_text_char[19] = 'す';
        m_text_char[20] = 'る';
        m_text_char[21] = '。';

        isClear = true;
    }

    private void p12Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'ス';
        m_text_char[1] = 'モ';
        m_text_char[2] = 'ー';
        m_text_char[3] = 'ク';
        m_text_char[4] = 'の';
        m_text_char[5] = '中';
        m_text_char[6] = 'に';
        m_text_char[7] = '入';
        m_text_char[8] = 'っ';
        m_text_char[9] = 'て';
        m_text_char[10] = 'い';
        m_text_char[11] = 'る';
        m_text_char[12] = '間';
        m_text_char[13] = 'は';
        m_text_char[14] = '標';
        m_text_char[15] = '的';
        m_text_char[16] = 'か';
        m_text_char[17] = 'ら';
        m_text_char[18] = '見';
        m_text_char[19] = 'つ';
        m_text_char[20] = 'か';
        m_text_char[21] = 'ら';
        m_text_char[22] = 'な';
        m_text_char[23] = 'い';
        m_text_char[24] = '。';

        isClear = true;
    }

    private void p13Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'ス';
        m_text_char[1] = 'モ';
        m_text_char[2] = 'ー';
        m_text_char[3] = 'ク';
        m_text_char[4] = 'の';
        m_text_char[5] = '中';
        m_text_char[6] = 'に';
        m_text_char[7] = '入';
        m_text_char[8] = 'り';
        m_text_char[9] = '標';
        m_text_char[10] = '的';
        m_text_char[11] = 'か';
        m_text_char[12] = 'ら';
        m_text_char[13] = '隠';
        m_text_char[14] = 'れ';
        m_text_char[15] = 'ろ';
        m_text_char[16] = '。';

        isClear = true;
    }

    private void p14Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '最';
        m_text_char[1] = '後';
        m_text_char[2] = 'に';
        m_text_char[3] = '照';
        m_text_char[4] = '明';
        m_text_char[5] = '弾';
        m_text_char[6] = 'の';
        m_text_char[7] = '訓';
        m_text_char[8] = '練';
        m_text_char[9] = 'だ';
        m_text_char[10] = '。';

        isClear = true;
    }

    private void p15Char_Init()
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
        m_text_char[8] = 'を';
        m_text_char[9] = '合';
        m_text_char[10] = '図';
        m_text_char[11] = 'に';
        m_text_char[12] = '砲';
        m_text_char[13] = '撃';
        m_text_char[14] = 'を';
        m_text_char[15] = '開';
        m_text_char[16] = '始';
        m_text_char[17] = 'す';
        m_text_char[18] = 'る';
        m_text_char[19] = '。';


        isClear = true;
    }

    private void p16Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = '建';
        m_text_char[1] = '物';
        m_text_char[2] = 'を';
        m_text_char[3] = '照';
        m_text_char[4] = '明';
        m_text_char[5] = '弾';
        m_text_char[6] = 'で';
        m_text_char[7] = '照';
        m_text_char[8] = 'ら';
        m_text_char[9] = 'し';
        m_text_char[10] = '砲';
        m_text_char[11] = '撃';
        m_text_char[12] = 'を';
        m_text_char[13] = '行';
        m_text_char[14] = 'え';
        m_text_char[15] = '。';

        isClear = true;
    }

    private void p17Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'よ';
        m_text_char[1] = 'し';
        m_text_char[2] = '武';
        m_text_char[3] = '装';
        m_text_char[4] = '訓';
        m_text_char[5] = '練';
        m_text_char[6] = 'は';
        m_text_char[7] = 'こ';
        m_text_char[8] = 'れ';
        m_text_char[9] = 'で';
        m_text_char[10] = '終';
        m_text_char[11] = '了';
        m_text_char[12] = 'だ';
        m_text_char[13] = '。';

        isClear = true;
    }

    private void p18Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'こ';
        m_text_char[1] = 'れ';
        m_text_char[2] = 'に';
        m_text_char[3] = 'て';
        m_text_char[4] = '訓';
        m_text_char[5] = '練';
        m_text_char[6] = 'を';
        m_text_char[7] = '終';
        m_text_char[8] = '了';
        m_text_char[9] = 'す';
        m_text_char[10] = 'る';
        m_text_char[11] = '。';

        isClear = true;
    }

    private void other1Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'よ';
        m_text_char[1] = 'し';
        m_text_char[2] = 'そ';
        m_text_char[3] = 'の';
        m_text_char[4] = '調';
        m_text_char[5] = '子';
        m_text_char[6] = 'だ';
        m_text_char[7] = '。';

        isClear = true;
    }

    private void other2Char_Init()
    {
        if (isClear) return;

        m_text_char[0] = 'よ';
        m_text_char[1] = 'し';
        m_text_char[2] = '次';
        m_text_char[3] = 'の';
        m_text_char[4] = '部';
        m_text_char[5] = '屋';
        m_text_char[6] = 'に';
        m_text_char[7] = '移';
        m_text_char[8] = '動';
        m_text_char[9] = 'し';
        m_text_char[10] = 'ろ';
        m_text_char[11] = '。';

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
                rect_.localPosition = new Vector3(-78f, -290f, 0f);
                p1Char_Init();
                P1TextUpdate();
                break;
            case TextState.P2:
                rect_.localPosition = new Vector3(-96f, -290f, 0f);
                p2Char_Init();
                P2TextUpdate();
                break;
            case TextState.P3:
                rect_.localPosition = new Vector3(-128f, -290f, 0f);
                p3Char_Init();
                P3TextUpdate();
                break;
            case TextState.P4:
                rect_.localPosition = new Vector3(-118f, -290f, 0f);
                p4Char_Init();
                P4TextUpdate();
                break;
            case TextState.P5:
                rect_.localPosition = new Vector3(-65f, -290f, 0f);
                p5Char_Init();
                P5TextUpdate();
                break;
            case TextState.P6:
                //rect_.localPosition = new Vector3(-386f, -245f, 0f);
                p6Char_Init();
                P6TextUpdate();
                break;
            case TextState.P7:
                rect_.localPosition = new Vector3(-362f, -290f, 0f);
                p7Char_Init();
                P7TextUpdate();
                break;
            case TextState.P8:
                rect_.localPosition = new Vector3(-307f, -290f, 0f);
                p8Char_Init();
                P8TextUpdate();
                break;
            case TextState.P9:
                rect_.localPosition = new Vector3(-117f, -290f, 0f);
                p9Char_Init();
                P9TextUpdate();
                break;
            case TextState.P10:
                rect_.localPosition = new Vector3(-86f, -290f, 0f);
                p10Char_Init();
                P10TextUpdate();
                break;
            case TextState.P11:
                rect_.localPosition = new Vector3(-224f, -290f, 0f);
                p11Char_Init();
                P11TextUpdate();
                break;
            case TextState.P12:
                rect_.localPosition = new Vector3(-294f, -290f, 0f);
                p12Char_Init();
                P12TextUpdate();
                break;
            case TextState.P13:
                rect_.localPosition = new Vector3(-143f, -290f, 0f);
                p13Char_Init();
                P13TextUpdate();
                break;
            case TextState.P14:
                p14Char_Init();
                P14TextUpdate();
                break;
            case TextState.P15:
                rect_.localPosition = new Vector3(-192f, -290f, 0f);
                p15Char_Init();
                P15TextUpdate();
                break;
            case TextState.P16:
                rect_.localPosition = new Vector3(-128f, -290f, 0f);
                p16Char_Init();
                P16TextUpdate();
                break;
            case TextState.P17:
                rect_.localPosition = new Vector3(-86f, -290f, 0f);
                p17Char_Init();
                P17TextUpdate();
                break;
            case TextState.P18:
                rect_.localPosition = new Vector3(-83f, -290f, 0f);
                p18Char_Init();
                P18TextUpdate();
                break;
            case TextState.Other1:
                other1Char_Init();
                Other1TextUpdate();
                break;
            case TextState.Other2:
                rect_.localPosition = new Vector3(-86f, -290f, 0f);
                other2Char_Init();
                Other2TextUpdate();
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
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P1TextUpdate()
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
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P2TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 12)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P3TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 14)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P4TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 13)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P5TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 10)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P6TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 10)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P7TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 32)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P8TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 28)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P9TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 14)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P10TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 11)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P11TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 21)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P12TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 24)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P13TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 16)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P14TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 10)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P15TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 19)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P16TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 15)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P17TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 13)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void P18TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 11)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void Other1TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 7)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
    }

    private void Other2TextUpdate()
    {
        if (m_interval <= 0f
            &&
            m_charNum <= 11)
        {
            text_.text += m_text_char[m_charNum];
            m_charNum++;
            m_interval = 1f;
        }
        m_interval -= m_text_speed * Time.deltaTime;
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

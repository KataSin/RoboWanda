using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スクリプト：プレイヤーUI制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ExplosionImage;    // 爆弾残弾の画像
    [SerializeField]
    private GameObject m_FlashGrenadeImage; // 閃光弾残弾の画像
    [SerializeField]
    private GameObject m_SmokeGrenadeImage; // スモーク弾残弾の画像

    [SerializeField]
    private GameObject m_ExplosionCount;    // 爆弾残弾の文字
    [SerializeField]
    private GameObject m_FlashGrenadeCount; // 閃光弾残弾の文字
    [SerializeField]
    private GameObject m_SmokeGrenadeCount; // スモーク弾残弾の文字

    private BomSpawn.Bom m_Mode;            // UIモード（BomSpawnスクリプトから取得）

    bool m_GetFlash = false;                // 閃光弾を入手したか
    bool m_GetSmoke = false;                // スモーク弾を入手したか

    [SerializeField]
    private GameObject m_BomSpawn;          // 擲弾オブジェクト

    // 動作検証用のダミー変数
    int m_ModeSelect = 0;                   // モード選択
    bool m_IsTriggered = false;             // 十字キーの操作判定

    // Use this for initialization
    void Start()
    {
        // 擲弾オブジェクトが存在しない場合、強制終了
        if (m_BomSpawn == null)
        {
            Debug.Log("エラー発生したので終了します");
            Debug.Log("Error Log：擲弾オブジェクトが存在しない");
            Application.Quit();
        }

        // m_Mode = PlayerUIMode.Bomb;
        m_Mode = BomSpawn.Bom.BOM;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        switch (m_Mode)
        {
            // case PlayerUIMode.Bomb:
            case BomSpawn.Bom.BOM:
                // Debug.Log("爆弾選択中");
                Bomb();
                break;
            // case PlayerUIMode.FlashGrenade:
            case BomSpawn.Bom.LIGHT_BOM:
                // Debug.Log("閃光弾選択中");
                FlashGrenade();
                break;
            // case PlayerUIMode.SmokeGrenade:
            case BomSpawn.Bom.SMOKE_BOM:
                // Debug.Log("煙玉選択中");
                SmokeGrenade();
                break;
        }
        */

        switch (m_Mode)
        {
            case BomSpawn.Bom.BOM:
                Debug.Log("爆弾選択中");
                Bomb();
                break;
            case BomSpawn.Bom.LIGHT_BOM:
                Debug.Log("閃光弾選択中");
                FlashGrenade();
                break;
            case BomSpawn.Bom.SMOKE_BOM:
                Debug.Log("煙玉選択中");
                SmokeGrenade();
                break;
        }

        // 動作検証用のダミープログラム
        /*
        switch (m_ModeSelect)
        {
            case 0:
                Debug.Log("爆弾選択中");
                Bomb();
                break;
            case 1:
                Debug.Log("閃光弾選択中");
                FlashGrenade();
                break;
            case 2:
                Debug.Log("煙玉選択中");
                SmokeGrenade();
                break;
        }
        */

        // 現在選択中の弾種を反映
        CheckMode();

        // 現在の残弾数を反映
        /*
        Text flash_count, smoke_count;
        flash_count = m_FlashGrenadeCount.GetComponent<Text>();
        smoke_count = m_SmokeGrenadeCount.GetComponent<Text>();

        flash_count.text = " / ";
        smoke_count.text = " / ";
        */
    }

    // 選択弾種を更新
    private void CheckMode()
    {
        /*
        switch (m_BomSpawn.GetComponent<BomSpawn>().GetMode())
        {
            case 0:
                m_Mode = PlayerUIMode.Bomb;
                break;
            case 1:
                m_Mode = PlayerUIMode.FlashGrenade;
                break;
            case 2:
                m_Mode = PlayerUIMode.SmokeBall;
                break;
            default:
                m_Mode = PlayerUIMode.Bomb;
                break;
        }
        */

        m_Mode = m_BomSpawn.GetComponent<BomSpawn>().m_Bom;

        // 動作検証用のダミープログラム
        // 十字キーで弾種を選択
        // 十字キーの入力が無い場合、操作判定を解除
        /*
        if (Input.GetAxisRaw("Bullet_Select") == 0)
        {
            m_IsTriggered = false;
        }

        // 十字ボタンの左右で弾種を選択
        if (m_IsTriggered == false)
        {
            // 左ボタン
            if (Input.GetAxisRaw("Bullet_Select") < -0.9)
            {
                m_IsTriggered = true;
                m_ModeSelect = m_ModeSelect - 1;
            }
            // 右ボタン
            if (Input.GetAxisRaw("Bullet_Select") > 0.9)
            {
                m_IsTriggered = true;
                m_ModeSelect = m_ModeSelect + 1;
            }
        }
        */

        // モードの変数を０～２の範囲内に限定
        // m_ModeSelect = Mathf.Clamp(m_ModeSelect, 0, 2);
        /*
        if (m_ModeSelect > 2) m_ModeSelect = 0;
        if (m_ModeSelect < 0) m_ModeSelect = 2;
        */

        // 現在選択されたモードを反映
        /*
        switch (m_ModeSelect)
        {
            case 0:
                m_Mode = PlayerUIMode.Bomb;
                break;
            case 1:
                m_Mode = PlayerUIMode.FlashGrenade;
                break;
            case 2:
                m_Mode = PlayerUIMode.SmokeGrenade;
                break;
            default:
                m_Mode = PlayerUIMode.Bomb;
                break;
        }
        */
    }

    // 爆弾選択中の処理
    void Bomb()
    {
        m_ExplosionImage.SetActive(true);
        m_ExplosionCount.SetActive(true);

        m_FlashGrenadeImage.SetActive(false);
        m_FlashGrenadeCount.SetActive(false);

        m_SmokeGrenadeImage.SetActive(false);
        m_SmokeGrenadeCount.SetActive(false);
    }

    // 閃光弾選択中の処理
    void FlashGrenade()
    {
        m_ExplosionImage.SetActive(false);
        m_ExplosionCount.SetActive(false);

        m_FlashGrenadeImage.SetActive(true);
        m_FlashGrenadeCount.SetActive(true);

        m_SmokeGrenadeImage.SetActive(false);
        m_SmokeGrenadeCount.SetActive(false);
    }

    // 煙玉選択中の処理
    void SmokeGrenade()
    {
        m_ExplosionImage.SetActive(false);
        m_ExplosionCount.SetActive(false);

        m_FlashGrenadeImage.SetActive(false);
        m_FlashGrenadeCount.SetActive(false);

        m_SmokeGrenadeImage.SetActive(true);
        m_SmokeGrenadeCount.SetActive(true);
    }
}
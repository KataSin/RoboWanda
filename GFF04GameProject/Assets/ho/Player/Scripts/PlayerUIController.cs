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
    private GameObject m_ExplosionImage;    // 爆発弾残弾の画像
    [SerializeField]
    private GameObject m_FlashGrenadeImage; // 照明弾残弾の画像
    [SerializeField]
    private GameObject m_SmokeGrenadeImage; // スモーク弾残弾の画像

    [SerializeField]
    private GameObject m_ExplosionCount;    // 爆発弾残弾の文字
    [SerializeField]
    private GameObject m_FlashGrenadeCount; // 照明弾残弾の文字
    [SerializeField]
    private GameObject m_SmokeGrenadeCount; // スモーク弾残弾の文字

    private BomSpawn.Bom m_Mode;            // UIモード（BomSpawnスクリプトから取得）

    [SerializeField]
    bool m_GetFlash = false;                // 照明弾を入手したか
    [SerializeField]
    bool m_GetSmoke = false;                // スモーク弾を入手したか

    Vector3 m_ExplosionPosition;            // 現在 爆発弾表示の座標
    Vector3 m_FlashPosition;                // 現在 照明弾表示の座標
    Vector3 m_SmokePosition;                // 現在 スモーク弾表示の座標

    Vector3 m_ExplosionScale;               // 現在 爆発弾表示のスケール
    Vector3 m_FlashScale;                   // 現在 照明弾表示のスケール
    Vector3 m_SmokeScale;                   // 現在 スモーク弾表示のスケール

    [SerializeField]
    private GameObject m_BomSpawn;          // 擲弾オブジェクト

    // 動作検証用のダミー変数
    /*
    int m_ModeSelect = 0;                   // モード選択
    bool m_IsTriggered = false;             // 十字キーの操作判定
    */

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

        // 弾種表示の初期位置とサイズを設定（scaleを使用）
        // 爆発弾
        m_ExplosionImage.transform.localPosition = new Vector3(430.0f, -290.0f, 0.0f);
        m_ExplosionImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        m_ExplosionCount.transform.localPosition = new Vector3(430.0f, -290.0f, 0.0f);
        m_ExplosionCount.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // 照明弾
        m_FlashGrenadeImage.transform.localPosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_FlashGrenadeImage.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
        m_FlashGrenadeCount.transform.localPosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_FlashGrenadeCount.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);

        // スモーク弾
        m_SmokeGrenadeImage.transform.localPosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_SmokeGrenadeImage.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
        m_SmokeGrenadeCount.transform.localPosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_SmokeGrenadeCount.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);

        // 現在各表示の座標とスケールを記録
        // 爆発弾
        m_ExplosionPosition = m_ExplosionImage.transform.localPosition;
        m_ExplosionScale = m_ExplosionImage.transform.localScale;

        // 照明弾
        m_FlashPosition = m_FlashGrenadeImage.transform.localPosition;
        m_FlashScale = m_FlashGrenadeImage.transform.localScale;

        // スモーク弾
        m_SmokePosition = m_SmokeGrenadeImage.transform.localPosition;
        m_SmokeScale = m_SmokeGrenadeImage.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        switch (m_Mode)
        {
            // case PlayerUIMode.Bomb:
            case BomSpawn.Bom.BOM:
                // Debug.Log("爆発弾選択中");
                Bomb();
                break;
            // case PlayerUIMode.FlashGrenade:
            case BomSpawn.Bom.LIGHT_BOM:
                // Debug.Log("照明弾選択中");
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
                Debug.Log("爆発弾選択中");
                Bomb();
                break;
            case BomSpawn.Bom.LIGHT_BOM:
                Debug.Log("照明弾選択中");
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
                Debug.Log("爆発弾選択中");
                Bomb();
                break;
            case 1:
                Debug.Log("照明弾選択中");
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

    // 爆発弾選択中の処理
    void Bomb()
    {
        // 照明弾を入手すると、照明弾の残弾数を表示
        /*
        if (m_GetFlash)
        {
            m_FlashGrenadeImage.SetActive(true);
            m_FlashGrenadeCount.SetActive(true);
        }
        */
        // 照明弾を所持せず、スモーク弾を入手すると、スモーク弾の残弾数を表示
        /*
        else if (m_GetSmoke)
        {
            m_SmokeGrenadeImage.SetActive(true);
            m_SmokeGrenadeCount.SetActive(true);
        }
        */

        if (!m_GetFlash && !m_GetSmoke)
        {
            // 表示しない
        }
        else if (!m_GetFlash && m_GetSmoke)
        {
            m_FlashGrenadeImage.SetActive(false);
            m_FlashGrenadeCount.SetActive(false);

            m_SmokeGrenadeImage.SetActive(true);
            m_SmokeGrenadeCount.SetActive(true);
        }
        else
        {
            m_FlashGrenadeImage.SetActive(true);
            m_FlashGrenadeCount.SetActive(true);

            m_SmokeGrenadeImage.SetActive(false);
            m_SmokeGrenadeCount.SetActive(false);
        }

        m_ExplosionImage.SetActive(true);
        m_ExplosionCount.SetActive(true);

        // 爆発弾表示の座標とスケール
        m_ExplosionPosition = new Vector3(430.0f, -290.0f, 0.0f);
        m_ExplosionScale = new Vector3(1.0f, 1.0f, 1.0f);

        // 照明弾/スモーク弾表示の座標とスケール
        m_FlashPosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_FlashScale = new Vector3(0.75f, 0.75f, 1.0f);

        m_SmokePosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_SmokeScale = new Vector3(0.75f, 0.75f, 1.0f);

        // 指定した座標に移動し、表示のスケールを変更
        // 爆発弾
        m_ExplosionImage.transform.localPosition = m_ExplosionPosition;
        m_ExplosionImage.transform.localScale = m_ExplosionScale;
        m_ExplosionCount.transform.localPosition = m_ExplosionPosition;
        m_ExplosionCount.transform.localScale = m_ExplosionScale;

        // 照明弾
        m_FlashGrenadeImage.transform.localPosition = m_FlashPosition;
        m_FlashGrenadeImage.transform.localScale = m_FlashScale;
        m_FlashGrenadeCount.transform.localPosition = m_FlashPosition;
        m_FlashGrenadeCount.transform.localScale = m_FlashScale;

        // スモーク弾
        m_SmokeGrenadeImage.transform.localPosition = m_SmokePosition;
        m_SmokeGrenadeImage.transform.localScale = m_SmokeScale;
        m_SmokeGrenadeCount.transform.localPosition = m_SmokePosition;
        m_SmokeGrenadeCount.transform.localScale = m_SmokeScale;
    }

    // 照明弾選択中の処理
    void FlashGrenade()
    {
        // スモーク弾を所持していれば、スモーク弾の残弾数を表示し、爆発弾を表示を消す
        /*
        if (m_GetSmoke)
        {
            m_SmokeGrenadeImage.SetActive(true);
            m_SmokeGrenadeCount.SetActive(true);

            m_ExplosionImage.SetActive(false);
            m_ExplosionCount.SetActive(false);
        }
        */
        // そうでなければ、爆発弾の残弾数を表示
        /*
        else
        {
            m_SmokeGrenadeImage.SetActive(false);
            m_SmokeGrenadeCount.SetActive(false);

            m_ExplosionImage.SetActive(true);
            m_ExplosionCount.SetActive(true);
        }
        */

        if (!m_GetSmoke)
        {

        }
        else
        {
            m_SmokeGrenadeImage.SetActive(true);
            m_SmokeGrenadeCount.SetActive(true);

            m_ExplosionImage.SetActive(false);
            m_ExplosionCount.SetActive(false);
        }

        // 照明弾表示の座標とスケール
        m_FlashPosition = new Vector3(430.0f, -290.0f, 0.0f);
        m_FlashScale = new Vector3(1.0f, 1.0f, 1.0f);

        // スモーク弾/爆発弾表示の座標とスケール
        m_SmokePosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_SmokeScale = new Vector3(0.75f, 0.75f, 1.0f);

        m_ExplosionPosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_ExplosionScale = new Vector3(0.75f, 0.75f, 1.0f);

        // 指定した座標に移動し、表示のスケールを変更
        // 爆発弾
        m_ExplosionImage.transform.localPosition = m_ExplosionPosition;
        m_ExplosionImage.transform.localScale = m_ExplosionScale;
        m_ExplosionCount.transform.localPosition = m_ExplosionPosition;
        m_ExplosionCount.transform.localScale = m_ExplosionScale;

        // 照明弾
        m_FlashGrenadeImage.transform.localPosition = m_FlashPosition;
        m_FlashGrenadeImage.transform.localScale = m_FlashScale;
        m_FlashGrenadeCount.transform.localPosition = m_FlashPosition;
        m_FlashGrenadeCount.transform.localScale = m_FlashScale;

        // スモーク弾
        m_SmokeGrenadeImage.transform.localPosition = m_SmokePosition;
        m_SmokeGrenadeImage.transform.localScale = m_SmokeScale;
        m_SmokeGrenadeCount.transform.localPosition = m_SmokePosition;
        m_SmokeGrenadeCount.transform.localScale = m_SmokeScale;
    }

    // スモーク弾選択中の処理
    void SmokeGrenade()
    {
        // 照明弾の表示を消す
        m_FlashGrenadeImage.SetActive(false);
        m_FlashGrenadeCount.SetActive(false);

        // 爆発弾を表示
        m_ExplosionImage.SetActive(true);
        m_ExplosionCount.SetActive(true);

        // スモーク表示の座標とスケール
        m_SmokePosition = new Vector3(430.0f, -290.0f, 0.0f);
        m_SmokeScale = new Vector3(1.0f, 1.0f, 1.0f);

        // 爆発弾/照明弾表示の座標とスケール
        m_ExplosionPosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_ExplosionScale = new Vector3(0.75f, 0.75f, 1.0f);

        m_FlashPosition = new Vector3(471.0f, -195.0f, 0.0f);
        m_FlashScale = new Vector3(0.75f, 0.75f, 1.0f);

        // 指定した座標に移動し、表示のスケールを変更
        // 爆発弾
        m_ExplosionImage.transform.localPosition = m_ExplosionPosition;
        m_ExplosionImage.transform.localScale = m_ExplosionScale;
        m_ExplosionCount.transform.localPosition = m_ExplosionPosition;
        m_ExplosionCount.transform.localScale = m_ExplosionScale;

        // 照明弾
        m_FlashGrenadeImage.transform.localPosition = m_FlashPosition;
        m_FlashGrenadeImage.transform.localScale = m_FlashScale;
        m_FlashGrenadeCount.transform.localPosition = m_FlashPosition;
        m_FlashGrenadeCount.transform.localScale = m_FlashScale;

        // スモーク弾
        m_SmokeGrenadeImage.transform.localPosition = m_SmokePosition;
        m_SmokeGrenadeImage.transform.localScale = m_SmokeScale;
        m_SmokeGrenadeCount.transform.localPosition = m_SmokePosition;
        m_SmokeGrenadeCount.transform.localScale = m_SmokeScale;
    }
}